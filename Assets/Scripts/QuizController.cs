using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Descriptors;
using Managers;
using Oculus.Interaction;
using TMPro;
using UnityEngine;

public class QuizController : MonoBehaviour
{
    public GameObject answerButtonPrefab;
    public GameObject questionPrefab;
    private List<GameObject> _listOfAnswerButtons;
    public bool timer;
    public float timeRemaining = 10;
    private bool _timerIsRunning = false;

    private void Start()
    {
         EventManager.Instance.onQuizStart.AddListener(QuizStart);
         EventManager.Instance.onAnswerGiven.AddListener(ContinueVideo);
    }

    private void QuizStart(QuizInterruptionDescriptor quizDescriptor)
    {
        EventManager.Instance.onVideoPause.Invoke();
        var question = quizDescriptor.question;
        var answers = quizDescriptor.answers;
        _listOfAnswerButtons = new List<GameObject>();
        CreateAndSetQuestion(question);
        foreach (var singleAnswer in answers)
        {
            CreateAndSetEachButton(singleAnswer);
        }
        PlaceAnswersIntoScene(); 
    }

    private void CreateAndSetQuestion(string question)
    {
        var questionView = Instantiate(questionPrefab, new Vector3(0, 1.5f, 0.5f), Quaternion.identity);
        questionView.name = "Question";
        questionView.GetComponentInChildren<TextMeshPro>().text = question;
    }

    private void CreateAndSetEachButton(Answer singleAnswer)
    {
        var answerButton = Instantiate(answerButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        answerButton.name = "Answer";
        _listOfAnswerButtons.Add(answerButton);
        answerButton.GetComponent<AnswerButton>().Setup(singleAnswer);
    }
    
    private void PlaceAnswersIntoScene()
    {
        var twoOptionsLocalPositions = new[]
            {new Vector3(-0.25f,1,0.5f), new Vector3(0.25f,1,0.5f)};
        
        var threeOptionsLocalPositions = new[]
            {new Vector3(-0.25f, 1.13f, 0.5f), new Vector3(0.25f, 1.13f, 0.5f), new Vector3(0, 0.7f, 0.5f)};

        var fourOptionsLocalPositions = new[]
        {
            new Vector3(-0.25f, 1.13f, 0.5f), new Vector3(0.25f, 1.13f, 0.5f),
            new Vector3(-0.25f, 0.7f, 0.5f), new Vector3(0.25f, 0.7f, 0.5f)
        };

        var positions = _listOfAnswerButtons.Count switch
        {
            2 => twoOptionsLocalPositions,
            3 => threeOptionsLocalPositions,
            _ => fourOptionsLocalPositions
        };

        for (var i = 0; i < _listOfAnswerButtons.Count; i++)
        {
            var answer = _listOfAnswerButtons[i];
            answer.transform.localPosition = positions[i];
        }

        if (timer)
        {
            _timerIsRunning = true;
        }
    }

    private void ContinueVideo(bool isCorrect)
    {
        Destroy(GameObject.Find("Question"));
        foreach (var t in _listOfAnswerButtons)
        {
            Destroy(t);
        }
        Debug.Log(isCorrect ? "Selected right answer" : "Selected wrong answer");
        EventManager.Instance.onVideoResume.Invoke();
    }

    private void Update()
    {
        if (!_timerIsRunning) return;
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            Debug.Log("Time has run out!");
            timeRemaining = 0;
            _timerIsRunning = false;
            EventManager.Instance.onAnswerGiven.Invoke(false);
        }
    }
}
