using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    public void ChangeScene(int sceneIndex)
    {
        SkyboxVideoController.DoFadeAndCallCallback(0, () =>
        {
            SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
        });
    }

    public void ChangeScene(string sceneName)
    {
        SkyboxVideoController.DoFadeAndCallCallback(0, () =>
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        }); 
    }
}
