using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class DebugInteraction : MonoBehaviour
{
#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            EventManager.onInterruptibleVideoPause.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            EventManager.onInterruptibleVideoResume.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Going to Video Scene");
            SceneChanger.ChangeScene(0);
        }

        if (!Input.GetKeyDown(KeyCode.T)) return;
        Debug.Log("Going to Menu Scene");
        SceneChanger.ChangeScene("TestMenuScene");
    }
#endif
}