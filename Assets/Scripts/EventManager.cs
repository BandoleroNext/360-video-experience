using UnityEngine.Events;
using UnityEngine.Serialization;

public class EventManager : Singleton<EventManager>
{
    public UnityEvent onVideoPause;
    public UnityEvent onVideoResume;
    public UnityEvent onVideoCompleted;
    public UnityEvent<string> onInterruptionVideoStart;
    public UnityEvent onInterruptionVideoCompleted;
    
    protected override void Awake()
    {
        base.Awake();
        onVideoPause = new UnityEvent();
        onVideoResume = new UnityEvent();
        onVideoCompleted = new UnityEvent();
        onInterruptionVideoStart = new UnityEvent<string>();
        onInterruptionVideoCompleted = new UnityEvent();
    }
}