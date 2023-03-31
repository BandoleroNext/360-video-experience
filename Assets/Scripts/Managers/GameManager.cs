using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public float changeSceneFadeTime = 0.2f;
        public AudioClip rightAnswerSound;
        public AudioClip wrongAnswerSound;

        public void ChangeScene(int sceneIndex)
        {
            var sceneName = SceneManager.GetSceneAt(sceneIndex).name;
            ChangeScene(sceneName);
        }

        public void ChangeScene(string sceneName)
        {
            EventManager.Instance.onSceneChange.Invoke();
            StartCoroutine(WaitForFadeTimeAndChangeScene(sceneName));
        }

        private IEnumerator WaitForFadeTimeAndChangeScene(string sceneName)
        {
            yield return new WaitForSeconds(changeSceneFadeTime);
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        }
    }
}