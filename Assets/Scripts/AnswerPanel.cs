using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class AnswerPanel : MonoBehaviour
{
    [SerializeField] private Transform[] answersTransforms;

    public void PlaceAnswersIntoScene(IReadOnlyList<AnswerButton> answerButtons)
    {
        if (answersTransforms.Length < answerButtons.Count)
        {
            Debug.LogError("Insufficient number of slots in QuizPositionReference");
            Destroy(gameObject);
            return;
        }
        
        for (var i = 0; i < answerButtons.Count; i++)
        {
            var answer = answerButtons[i];
            var answerTransform = answer.transform;
            answerTransform.position = answersTransforms[i].position;
            answerTransform.rotation = answersTransforms[i].rotation;
        }
    }
}