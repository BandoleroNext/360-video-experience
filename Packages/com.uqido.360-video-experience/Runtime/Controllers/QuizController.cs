using System.Collections.Generic;
using Descriptors;
using Managers;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controllers
{
    public class QuizController : MonoBehaviour
    {
        [SerializeField] private Quiz quizPrefab;
        [SerializeField] private bool isTimed;
        [SerializeField] private float timeRemaining = 10;
        [SerializeField] private Transform quizContainer;
        
        private float _timerValue;
        private bool _timerIsRunning;
        private Quiz _question;


        private void Start()
        {
            EventManager.OnQuizStart.AddListener(QuizStart);
            EventManager.OnAnswerGiven.AddListener(ContinueVideo);
        }

        private void QuizStart(QuizInterruptionDescriptor quizDescriptor)
        {
            EventManager.OnInterruptibleVideoPause.Invoke();
            _timerValue = timeRemaining;
            _question = Instantiate(quizPrefab,quizContainer.position,Quaternion.identity);
            _question.SetupQuestion(quizDescriptor);
            if (!isTimed) return;
            _timerIsRunning = true;
        }

        private void ContinueVideo(bool isCorrect)
        {
            _timerIsRunning = false;
            Destroy(_question);
            Debug.Log(isCorrect ? "Selected right answer" : "Selected wrong answer");
            EventManager.OnInterruptibleVideoResume.Invoke();
        }

        private void Update()
        {
            if (!_timerIsRunning) return;
            if (_timerValue > 0)
            {
                _timerValue -= Time.deltaTime;
                _question.UpdateTimerText(_timerValue);
                return;
            }

            Debug.Log("Time has run out!");
            _timerValue = 0;
            _timerIsRunning = false;
            EventManager.OnAnswerGiven.Invoke(false);
        }
    }
}