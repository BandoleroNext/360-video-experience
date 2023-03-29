using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Descriptors;
using UnityEngine;

public class QuizController : MonoBehaviour
{
    [SerializeField] private QuizInterruptionDescriptor quizInterruptionDescriptor;
    private Answer _testValue;
    public GameObject answerButtonPrefab;

    void Start()
    {
        var answers = quizInterruptionDescriptor.answers;
        foreach (var singleAnswer in answers )
        {
            CreateAndSetEachButton(singleAnswer);
        }
        
    }


    void CreateAndSetEachButton(Answer singleAnswer)
    {
        var answer1 = Instantiate(answerButtonPrefab, new Vector3(0, 1, 0.335f), Quaternion.identity);
        answer1.name = "Answer1";
        _testValue = quizInterruptionDescriptor.answers[0];
        GameObject.Find("Answer1").GetComponent<AnswerButton>().Setup(_testValue); 
    }
    
}
