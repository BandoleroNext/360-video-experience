using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    private void Start()
    {
        
    }

    public void ChangeScene(int index)
    {
        
    }

    private static void ChangeScene(string videoName)
    {
        
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Going to Video Scene");
            GameManager.ChangeScene("Video");
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Going to Menu Scene");
            ChangeScene(1);
        }
    }
#endif
}
