using Descriptors;
using Managers;
using TMPro;
using UnityEngine;

namespace UI
{
    public class AnswerButton : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private TMP_Text answerText;
        [SerializeField] private AudioClip rightAnswerSound;
        [SerializeField] private AudioClip wrongAnswerSound;
        private Answer _quizAnswer;

        public void Setup(Answer quizAnswer)
        {
            _quizAnswer = quizAnswer;
            answerText.text = _quizAnswer.answer;
            audioSource.clip = _quizAnswer.isCorrect
                ? rightAnswerSound
                : wrongAnswerSound;
        }

        public void OnAnswerGiven()
        {
            EventManager.OnAnswerGiven.Invoke(_quizAnswer.isCorrect);
        }
    }
}