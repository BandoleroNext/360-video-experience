using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private float changeSceneFadeTime = 0.2f;
    public AudioClip rightAnswerSound;
    public AudioClip wrongAnswerSound;
    public void ChangeScene(int sceneIndex)
    {
        InterruptibleVideoController.DoFadeAndCallCallback(0, () =>
        {
            SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
        }, changeSceneFadeTime);
    }

    public void ChangeScene(string sceneName)
    {
        InterruptibleVideoController.DoFadeAndCallCallback(0, () =>
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        }, changeSceneFadeTime); 
    }
}
