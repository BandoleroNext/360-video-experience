using Descriptors;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Managers
{
    public class EventManager
    {
        public static UnityEvent onSceneChange = new UnityEvent();
        public static UnityEvent onInterruptibleVideoPause = new UnityEvent();
        public static UnityEvent onInterruptibleVideoResume = new UnityEvent();
        public static UnityEvent onVideoCompleted = new UnityEvent();
        public static UnityEvent<string> onInterruptionVideoStart = new UnityEvent<string>();
        public static UnityEvent onInterruptionVideoCompleted = new UnityEvent();
        public static UnityEvent<QuizInterruptionDescriptor> onQuizStart = new UnityEvent<QuizInterruptionDescriptor>();
        public static UnityEvent<bool> onAnswerGiven = new UnityEvent<bool>();
    }
}