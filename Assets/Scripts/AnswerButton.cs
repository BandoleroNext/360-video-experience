using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Descriptors;
using TMPro;
using UnityEngine;

public class AnswerButton : MonoBehaviour
{
    [SerializeField] private QuizInterruptionDescriptor _quizInterruptionDescriptor;
    void Start()
    {
        var answer = _quizInterruptionDescriptor.answers.First().answer;
        GetComponentInChildren<TextMeshPro>().text = answer;
    }
    
    void Update()
    {
        
    }
}
