using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Descriptors;
using UnityEngine;

public class QuizController : MonoBehaviour
{
    [SerializeField] private QuizInterruptionDescriptor quizInterruptionDescriptor;
    private Answer _testValue;
    
    void Start()
    {
        _testValue = quizInterruptionDescriptor.answers.First();
        GameObject.Find("ExampleAnswerButton").GetComponent<AnswerButton>().Setup(_testValue);
    }

    
    void Update()
    {
         
    }
}
