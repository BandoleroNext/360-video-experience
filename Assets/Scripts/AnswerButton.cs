using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Descriptors;
using Managers;
using Oculus.Interaction;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class AnswerButton : MonoBehaviour
{
    [SerializeField] private QuizInterruptionDescriptor _quizInterruptionDescriptor;
    private Answer _testValue;
    
    void Start()
    {
        _testValue = _quizInterruptionDescriptor.answers.First();
        Setup();
    }
    
    private void Setup()
    {
        UpdateText();
        UpdateCallBack();
    }

    private void UpdateText()
    {
        var answer = _testValue.answer;
        GetComponentInChildren<TextMeshPro>().text = answer;
    }

    private void UpdateCallBack()
    {
        if (_testValue.isCorrect )
        {
            transform.Find("Audio/ButtonRelease").GetComponent<AudioSource>().clip = GameManager.Instance.rightAnswerSound;
            EventManager.Instance.onAnswerGiven.Invoke(true);
            Debug.Log("Selected right answer");
        }
        else
        {
            transform.Find("Audio/ButtonRelease").GetComponent<AudioSource>().clip = GameManager.Instance.wrongAnswerSound;
            EventManager.Instance.onAnswerGiven.Invoke(false);
            Debug.Log("Selected wrong answer");
        }
    }
}
