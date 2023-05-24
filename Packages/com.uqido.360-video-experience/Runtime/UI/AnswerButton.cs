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
        private Quiz _referenceQuiz;

        public void Setup(Answer quizAnswer, Quiz referenceQuiz)
        {
            _quizAnswer = quizAnswer;
            _referenceQuiz = referenceQuiz;
            answerText.text = _quizAnswer.answer;
            audioSource.clip = _quizAnswer.isCorrect
                ? rightAnswerSound
                : wrongAnswerSound;
        }

        public void OnAnswerGiven()
        {
            if(_referenceQuiz.answerGiven) return;
            _referenceQuiz.answerGiven = true;
            EventManager.OnAnswerGiven.Invoke(_quizAnswer.isCorrect);
        }
    }
}