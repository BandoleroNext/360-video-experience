using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugInteraction : MonoBehaviour
{
    
#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            EventManager.Instance.onVideoPause.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            EventManager.Instance.onVideoResume.Invoke();
        }
    }
#endif
}
