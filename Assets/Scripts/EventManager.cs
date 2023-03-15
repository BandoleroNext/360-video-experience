using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : Singleton<EventManager>
{
    public UnityEvent OnSkyboxVideoPause;
    public UnityEvent OnSkyboxVideoResume;
    public UnityEvent OnSkyboxVideoCompleted;
    
    protected override void Awake()
    {
        base.Awake();
        OnSkyboxVideoPause = new UnityEvent();
        OnSkyboxVideoResume = new UnityEvent();
        OnSkyboxVideoCompleted = new UnityEvent();
    }
}