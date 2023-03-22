using UnityEngine.Events;

public class EventManager : Singleton<EventManager>
{
    public UnityEvent onSkyboxVideoPause;
    public UnityEvent onSkyboxVideoResume;
    public UnityEvent onSkyboxVideoCompleted;
    public UnityEvent<string> onPLainVideoBegin;
    public UnityEvent onPlainVideoCompleted;
    
    protected override void Awake()
    {
        base.Awake();
        onSkyboxVideoPause = new UnityEvent();
        onSkyboxVideoResume = new UnityEvent();
        onSkyboxVideoCompleted = new UnityEvent();
        onPLainVideoBegin = new UnityEvent<string>();
        onPlainVideoCompleted = new UnityEvent();
    }
}