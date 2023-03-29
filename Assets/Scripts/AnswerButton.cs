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
    private Answer _quizAnswer;

    public void Setup(Answer quizAnswer)
    {
        _quizAnswer = quizAnswer;
        UpdateText();
    }

    private void UpdateText()
    {
        var answer = _quizAnswer.answer;
        GetComponentInChildren<TextMeshPro>().text = answer;
    }

    public void UpdateCallBack()
    {
        if (_quizAnswer.isCorrect)
        {
            transform.Find("Audio/ButtonRelease").GetComponent<AudioSource>().clip = GameManager.Instance.rightAnswerSound;
            Debug.Log("Selected right answer");
            EventManager.Instance.onAnswerGiven.Invoke(true);
        }
        else
        {
            transform.Find("Audio/ButtonRelease").GetComponent<AudioSource>().clip = GameManager.Instance.wrongAnswerSound;
            Debug.Log("Selected wrong answer");
            EventManager.Instance.onAnswerGiven.Invoke(false);
        }
    }
}
