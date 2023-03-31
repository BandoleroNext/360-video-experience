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
            EventManager.Instance.onInterruptibleVideoPause.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            EventManager.Instance.onInterruptibleVideoResume.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Going to Video Scene");
            GameManager.Instance.ChangeScene(0);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Going to Menu Scene");
            GameManager.Instance.ChangeScene("TestMenuScene");
        }
    }
#endif
}