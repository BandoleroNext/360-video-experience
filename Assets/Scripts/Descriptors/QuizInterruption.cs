using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descriptors
{
    [CreateAssetMenu(menuName = "360°video/New Quiz Interruption Descriptor")]
    public class QuizInterruption : InterruptionDescriptor
    {
        public string question;
        public List<Answer> answers;
    }
    
    [Serializable]
    public struct Answer
    {
        public string answer;
        public bool isCorrect;
    }
}
