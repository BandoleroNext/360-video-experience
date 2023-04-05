using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Descriptors;
using Managers;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class AnswerButton : MonoBehaviour
{
    [SerializeField] private AudioClip rightAnswerSound;
    [SerializeField] private AudioClip wrongAnswerSound;
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
            ? rightAnswerSound
            : wrongAnswerSound;
    }

    public void UpdateCallBack()
    {
        EventManager.onAnswerGiven.Invoke(_quizAnswer.isCorrect);
    }
}