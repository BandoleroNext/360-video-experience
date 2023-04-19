using System;
using System.Collections.Generic;
using System.Linq;
using Descriptors;
using Managers;
using TMPro;
using UnityEngine;

namespace UI
{
    public class Quiz : MonoBehaviour
    {
        [HideInInspector]
        public bool answerGiven;
        [SerializeField] private TMP_Text questionText;
        [SerializeField] private TMP_Text timerText;
        [SerializeField] private AnswerButton answerButtonPrefab;
        [SerializeField] private Transform answerContainer;
        [SerializeField] private Transform[] answersTransforms;

        private List<AnswerButton> _answerButtons;

        public void SetupQuestion(QuizInterruptionDescriptor questionDescriptor)
        {
            var answers = questionDescriptor.answers;
            if (answers.Count is < 2 or > 4)
            {
                Debug.LogError("Number of answers wrong: answers should be between 2 and 4");
                EventManager.OnInterruptibleVideoResume.Invoke();
                return;
            }

            if (answersTransforms.Length < answers.Count)
            {
                Debug.LogError("Number of answer slots is wrong: slots should match answers number");
                EventManager.OnInterruptibleVideoResume.Invoke();
                return;
            }

            questionText.text = questionDescriptor.question;
            transform.LookAt(new Vector3(0, transform.position.y, 0));
            transform.Rotate(0, 180, 0);

            _answerButtons = new List<AnswerButton>();
            var currentSlot = 0;
            foreach (var singleAnswer in answers)
            {
                var answerButton = Instantiate(answerButtonPrefab, answersTransforms[currentSlot].position,
                    answersTransforms[currentSlot].rotation, answerContainer);
                currentSlot++;
                _answerButtons.Add(answerButton);
                answerButton.Setup(singleAnswer,this);
            }
        }

        private static string FloatTimerToString(float timeToDisplay)
        {
            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);
            return $"{minutes:00}:{seconds:00}";
        }

        public void UpdateTimerText(float timerValue)
        {
            timerText.text = timerValue < 0 ? "" : FloatTimerToString(timerValue);
        }

        private void OnDestroy()
        {
            foreach (var button in _answerButtons.Where(button => button != null))
            {
                Destroy(button.gameObject);
            }
        }
    }
}