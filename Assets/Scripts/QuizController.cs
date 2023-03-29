using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Descriptors;
using UnityEngine;

public class QuizController : MonoBehaviour
{
    [SerializeField] private QuizInterruptionDescriptor quizInterruptionDescriptor;
    public GameObject answerButtonPrefab;
    private List<GameObject> _listOfAnswerButtons;

    void Start()
    {
        var answers = quizInterruptionDescriptor.answers;
        _listOfAnswerButtons = new List<GameObject>();
        foreach (var singleAnswer in answers)
        {
            CreateAndSetEachButton(singleAnswer);
        }
        PlaceAnswersIntoScene();
    }


    void CreateAndSetEachButton(Answer singleAnswer)
    {
        var answerButton = Instantiate(answerButtonPrefab, new Vector3(0, 1, 0.335f), Quaternion.identity);
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
    
}
