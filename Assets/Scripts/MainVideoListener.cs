using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using Utils;

[RequireComponent(typeof(VideoController))]
public class MainVideoListener : MonoBehaviour
{
    private VideoController _mainVideoController;
    private void Start()
    {
        _mainVideoController = GetComponent<VideoController>();
        EventManager.Instance.onSceneChange.AddListener(FadeBeforeSceneChange);
    }

    private void FadeBeforeSceneChange()
    {
        VideoControllerHelper.DoFade(0,GameManager.Instance.changeSceneFadeTime,_mainVideoController.exposure,_mainVideoController.targetMaterial);
    }
}
