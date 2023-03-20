using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugInteraction : MonoBehaviour
{

    // Update is called once per frame
#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            EventManager.Instance.OnSkyboxVideoPause.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            EventManager.Instance.OnSkyboxVideoResume.Invoke();
        }
    }
#endif
}
