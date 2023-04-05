using Descriptors;
using UnityEngine.Events;

namespace Managers
{
    public static class EventManager
    {
        public static readonly UnityEvent OnSceneChange = new();
        public static readonly UnityEvent OnInterruptibleVideoPause = new();
        public static readonly UnityEvent OnInterruptibleVideoResume = new();
        public static readonly UnityEvent OnVideoCompleted = new();
        public static readonly UnityEvent<string> OnInterruptionVideoStart = new();
        public static readonly UnityEvent OnInterruptionVideoCompleted = new();
        public static readonly UnityEvent<QuizInterruptionDescriptor> OnQuizStart = new();
        public static readonly UnityEvent<bool> OnAnswerGiven = new();
    }
}