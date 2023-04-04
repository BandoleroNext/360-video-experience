using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Descriptors;
using Managers;
using Oculus.Interaction;
using Oculus.Interaction.Samples;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class QuizController : MonoBehaviour
{
    public AnswerButton answerButtonPrefab;
    public GameObject questionPrefab;
    public bool timer;
    public float timeRemaining = 10;
    public Transform quizPosition;

    private TextMesh _timeText;

    private List<AnswerButton> _answerButtons;
    private bool _timerIsRunning;
    private GameObject _questionView;


    private void Start()
    {
        EventManager.Instance.onQuizStart.AddListener(QuizStart);
        EventManager.Instance.onAnswerGiven.AddListener(ContinueVideo);
    }

    private void QuizStart(QuizInterruptionDescriptor quizDescriptor)
    {
        EventManager.Instance.onInterruptibleVideoPause.Invoke();
        var question = quizDescriptor.question;
        var answers = quizDescriptor.answers;
        if (answers.Count is < 2 or > 4)
        {
            Debug.LogError("Number of answers wrong: answers should be between 2 and 4");
            EventManager.Instance.onInterruptibleVideoResume.Invoke();
            return;
        }

        _answerButtons = new List<AnswerButton>();
        CreateAndSetQuestion(question);
        foreach (var singleAnswer in answers)
        {
            CreateAndSetButton(singleAnswer);
        }

        GetComponentInChildren<AnswerPanel>().PlaceAnswersIntoScene(_answerButtons);

        if (!timer) return;
        _timerIsRunning = true;

        _timeText = _questionView.GetComponentInChildren<TextMesh>();

        if (_timeText) return;
        Debug.LogError("TextMesh for Timer missing in prefab");
        Destroy(gameObject);
        EventManager.Instance.onInterruptibleVideoResume.Invoke();
    }

    private void CreateAndSetQuestion(string question)
    {
        quizPosition.LookAt(new Vector3(0, quizPosition.position.y, 0));
        quizPosition.Rotate(0, 180, 0);
        _questionView = Instantiate(questionPrefab, quizPosition);
        _questionView.name = "Question";
        var questionText = _questionView.GetComponentInChildren<TextMeshPro>();

        if (!questionText)
        {
            Debug.LogError("TextMeshPro missing in prefab");
            Destroy(gameObject);
            EventManager.Instance.onInterruptibleVideoResume.Invoke();
            return;
        }

        questionText.text = question;
    }

    private void CreateAndSetButton(Answer singleAnswer)
    {
        var answerButton = Instantiate(answerButtonPrefab, Vector3.zero, Quaternion.identity, transform);
        answerButton.name = "Answer";
        _answerButtons.Add(answerButton);
        answerButton.Setup(singleAnswer);
    }

    private void ContinueVideo(bool isCorrect)
    {
        Destroy(_questionView);
        foreach (var t in _answerButtons)
        {
            Destroy(t.GameObject());
        }

        Debug.Log(isCorrect ? "Selected right answer" : "Selected wrong answer");
        EventManager.Instance.onInterruptibleVideoResume.Invoke();
    }

    private void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        _timeText.text = $"{minutes:00}:{seconds:00}";
    }

    private void Update()
    {
        if (!_timerIsRunning) return;
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
            return;
        }

        Debug.Log("Time has run out!");
        timeRemaining = 0;
        _timerIsRunning = false;
        EventManager.Instance.onAnswerGiven.Invoke(false);
    }
}