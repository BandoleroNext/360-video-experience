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
        UpdateSound();
    }

    private void UpdateText()
    {
        var answer = _quizAnswer.answer;
        GetComponentInChildren<TextMeshPro>().text = answer;
    }

    private void UpdateSound()
    {
        var source = GetComponentInChildren<AudioSource>();
        if (!source)
        {
            Debug.LogError("Audio Source not found in prefab");
            Destroy(gameObject);
            return;
        }
        source.clip = _quizAnswer.isCorrect
            ? GameManager.Instance.rightAnswerSound
            : GameManager.Instance.wrongAnswerSound;
    }

    public void UpdateCallBack()
    {
        EventManager.Instance.onAnswerGiven.Invoke(_quizAnswer.isCorrect);
    }
}