using Descriptors;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Managers
{
    public class EventManager : Singleton<EventManager>
    {
        public UnityEvent onSceneChange;
        [FormerlySerializedAs("onVideoPause")] public UnityEvent onInterruptibleVideoPause;
        [FormerlySerializedAs("onVideoResume")] public UnityEvent onInterruptibleVideoResume;
        public UnityEvent onVideoCompleted;
        [FormerlySerializedAs("onInterruptibleVideoStart")] public UnityEvent<string> onInterruptionVideoStart;
        [FormerlySerializedAs("onInterruptibleVideoCompleted")] public UnityEvent onInterruptionVideoCompleted;
        public UnityEvent<QuizInterruptionDescriptor> onQuizStart;
        public UnityEvent<bool> onAnswerGiven;

        protected override void Awake()
        {
            base.Awake();
            onSceneChange = new UnityEvent();
            onInterruptibleVideoPause = new UnityEvent();
            onInterruptibleVideoResume = new UnityEvent();
            onVideoCompleted = new UnityEvent();
            onInterruptionVideoStart = new UnityEvent<string>();
            onInterruptionVideoCompleted = new UnityEvent();
            onQuizStart = new UnityEvent<QuizInterruptionDescriptor>();
            onAnswerGiven = new UnityEvent<bool>();
        }
    }
}