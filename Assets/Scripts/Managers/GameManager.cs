using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private float changeSceneFadeTime = 0.2f; 
    public void ChangeScene(int sceneIndex)
    {
        VideoController.DoFadeAndCallCallback(0, () =>
        {
            SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
        }, changeSceneFadeTime);
    }

    public void ChangeScene(string sceneName)
    {
        VideoController.DoFadeAndCallCallback(0, () =>
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        }, changeSceneFadeTime); 
    }
}
