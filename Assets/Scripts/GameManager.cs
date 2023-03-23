using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    public void ChangeScene(int index)
    {
        StartCoroutine(Changing_CoroutineInd(index));
    }

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(Changing_CoroutineName(sceneName)); 
    }

    private static IEnumerator Changing_CoroutineInd(int index)
    {
        yield return null;
        SkyboxVideoController.DoFadeAndCallCallback(0, () =>
        {
            SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
        });
    }
    
    private static IEnumerator Changing_CoroutineName(string sceneName)
    {
        yield return null;
        SkyboxVideoController.DoFadeAndCallCallback(0, () =>
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        });
    }
}
