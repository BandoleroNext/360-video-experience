using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public enum IndexOrName
    {
        Index,
        Name
    }

    private void Start()
    {
        
    }

    private static void ChangeScene(int index)
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
            GameManager.ChangeScene(1);
        }
    }
#endif
}
