using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Descriptors;
using Managers;
using UnityEngine;

public class QuizController : MonoBehaviour
{
    public GameObject answerButtonPrefab;
    private List<GameObject> _listOfAnswerButtons;

    private void Start()
    {
         EventManager.Instance.onQuizStart.AddListener(QuizStart);
         EventManager.Instance.onAnswerGiven.AddListener(ContinueVideo);
    }

    private void QuizStart(QuizInterruptionDescriptor quizDescriptor)
    {
        EventManager.Instance.onVideoPause.Invoke();
        var answers = quizDescriptor.answers;
        _listOfAnswerButtons = new List<GameObject>();
        foreach (var singleAnswer in answers)
        {
            CreateAndSetEachButton(singleAnswer);
        }
        PlaceAnswersIntoScene(); 
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
            {new Vector3(-0.25f, 1.2f, 0.5f), new Vector3(0.25f, 1.2f, 0.5f), new Vector3(0, 0.7f, 0.5f)};

        var fourOptionsLocalPositions = new[]
        {
            new Vector3(-0.25f, 1.2f, 0.5f), new Vector3(0.25f, 1.2f, 0.5f),
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
    }

    private void ContinueVideo(bool isCorrect)
    {
        foreach (var t in _listOfAnswerButtons)
        {
            Destroy(t);
        }
        Debug.Log(isCorrect ? "Selected right answer" : "Selected wrong answer");
        EventManager.Instance.onVideoResume.Invoke();
    }
}
