using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AnswerPanel : MonoBehaviour
{
    public void PlaceAnswersIntoScene(IReadOnlyList<AnswerButton> answerButtons, Transform quizPosition)
    {
        var position = transform.localPosition;
        var twoOptionsLocalPositions = new[]
            { new Vector3(position.x-0.27f, position.y-0.4f, position.z), new Vector3(position.x+0.27f, position.y-0.4f, position.z) };

        var threeOptionsLocalPositions = new[]
            { new Vector3(position.x-0.27f, position.y-0.4f, position.z), new Vector3(position.x+0.27f, position.y-0.4f, position.z), new Vector3(position.x, position.y-0.8f, position.z) };

        var fourOptionsLocalPositions = new[]
        {
            new Vector3(position.x-0.27f, position.y-0.4f, position.z), new Vector3(position.x+0.27f, position.y-0.4f, position.z),
            new Vector3(position.x-0.27f, position.y-0.8f, position.z), new Vector3(position.x+0.27f, position.y-0.8f, position.z)
        };

        var positions = answerButtons.Count switch
        {
            2 => twoOptionsLocalPositions,
            3 => threeOptionsLocalPositions,
            _ => fourOptionsLocalPositions
        };

        for (var i = 0; i < answerButtons.Count; i++)
        {
            var answer = answerButtons[i];
            answer.transform.localPosition = positions[i];
            answer.transform.LookAt(Vector3.zero);
            answer.transform.Rotate(0,180,0);
        }
    }
}
