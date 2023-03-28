using UnityEngine.Events;

namespace Managers
{
    public class EventManager : Singleton<EventManager>
    {
        public UnityEvent onVideoPause;
        public UnityEvent onVideoResume;
        public UnityEvent onVideoCompleted;
        public UnityEvent<string> onInterruptibleVideoStart;
        public UnityEvent onInterruptibleVideoCompleted;

        protected override void Awake()
        {
            base.Awake();
            onVideoPause = new UnityEvent();
            onVideoResume = new UnityEvent();
            onVideoCompleted = new UnityEvent();
            onInterruptibleVideoStart = new UnityEvent<string>();
            onInterruptibleVideoCompleted = new UnityEvent();
        }
    }
}