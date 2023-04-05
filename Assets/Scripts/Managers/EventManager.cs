using Descriptors;
using UnityEngine.Events;

namespace Managers
{
    public static class EventManager
    {
        public static readonly UnityEvent OnSceneChange = new UnityEvent();
        public static readonly UnityEvent OnInterruptibleVideoPause = new UnityEvent();
        public static readonly UnityEvent OnInterruptibleVideoResume = new UnityEvent();
        public static readonly UnityEvent OnVideoCompleted = new UnityEvent();
        public static readonly UnityEvent<string> OnInterruptionVideoStart = new UnityEvent<string>();
        public static readonly UnityEvent OnInterruptionVideoCompleted = new UnityEvent();
        public static readonly UnityEvent<QuizInterruptionDescriptor> OnQuizStart = new UnityEvent<QuizInterruptionDescriptor>();
        public static readonly UnityEvent<bool> OnAnswerGiven = new UnityEvent<bool>();
    }
}