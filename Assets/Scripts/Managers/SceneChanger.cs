using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public abstract class SceneChanger
    {
        public static void ChangeScene(int sceneIndex)
        {
            var sceneName = SceneManager.GetSceneAt(sceneIndex).name;
            ChangeScene(sceneName);
        }

        public static void ChangeScene(string sceneName)
        {
            EventManager.Instance.onSceneChange.Invoke();
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        }
    }
}