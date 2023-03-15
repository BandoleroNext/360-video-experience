using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
            
            #region Singleton
    
            public static EventManager Instance;
    
    
            private void Awake()
            {
                if (!Instance)
                {
                    Instance = this;
                    OnSkyboxVideoPause = new UnityEvent();
                    OnSkyboxVideoResume = new UnityEvent();
                    OnSkyboxVideoCompleted = new UnityEvent();

                }
                else if (Instance != this)
                    Destroy(gameObject);
    
                DontDestroyOnLoad(this);
            }
    
            #endregion
            
            public UnityEvent OnSkyboxVideoPause;
            public UnityEvent OnSkyboxVideoResume;
            public UnityEvent OnSkyboxVideoCompleted;
}

