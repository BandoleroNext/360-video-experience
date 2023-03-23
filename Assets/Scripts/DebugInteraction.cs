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
            EventManager.Instance.OnSkyboxVideoPause.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            EventManager.Instance.OnSkyboxVideoResume.Invoke();
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
