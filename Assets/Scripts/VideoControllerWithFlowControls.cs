using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Oculus.Interaction;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class VideoControllerWithFlowControls : MonoBehaviour
{
    [SerializeField] private Material targetMaterial;
    [SerializeField] private GameObject screenParent;
    [SerializeField] private int secondsToSkip;
    private VideoPlayer _videoPlayer;
    
    private void Start()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        screenParent.SetActive(false);
        EventManager.Instance.onInterruptionVideoStart.AddListener(SetupAndStartVideo);
    }

    private void SetupAndStartVideo(string url)
    {
        CreateTextureAndSetupMaterial();
        var correctUrl = GeneratePathToVideo(url);
        if (correctUrl == "")
        {
            Debug.LogError("VIDEO NOT FOUND ");
            return;
        }
        _videoPlayer.url = correctUrl;
        screenParent.SetActive(true);
        _videoPlayer.Play();
        _videoPlayer.loopPointReached += EndVideo;
    }
    
    private string GeneratePathToVideo(string url)
    {
        var persistentPath = Path.Combine(Application.persistentDataPath, url);
        if (File.Exists(persistentPath))
            return persistentPath;
        var streamingPath = Path.Combine(Application.streamingAssetsPath, url);
        if (File.Exists(streamingPath))
            return streamingPath;
        //if http ok, altrimenti cerco in streaming
        if (File.Exists(url)||RemoteFileExists(url))
            return url;
        return "";
    }
    private static bool RemoteFileExists(string url)
    {
        try
        {
            //Searching through an HttpWebRequest
            if (WebRequest.Create(url) is not HttpWebRequest request) return false;
            request.Method = "HEAD";
            if (request.GetResponse() is not HttpWebResponse response) return false;
            response.Close();
            return (response.StatusCode == HttpStatusCode.OK);
        }
        catch
        {
            Debug.LogError($"Video not found!");
            return false;
        }
    }
    private void CreateTextureAndSetupMaterial()
    {
        var renderTexture = new RenderTexture(1920,1080,32, RenderTextureFormat.ARGB32);
        targetMaterial.mainTexture = renderTexture;
        _videoPlayer.targetTexture = renderTexture;
    }

    private void EndVideo(VideoPlayer source)
    {
        screenParent.SetActive(false);
        EventManager.Instance.onInterruptionVideoCompleted.Invoke();
        EventManager.Instance.onVideoResume.Invoke();
    }

    public void PauseVideo()
    {
        _videoPlayer.Pause();
    }

    public void ResumeVideo()
    {
        _videoPlayer.Play();
    }

    public void SkipFrames(bool skipAhead)
    {
        var framesToSkip = (skipAhead ? 1 : -1) * (long)_videoPlayer.frameRate * secondsToSkip;
        var currentFrame = _videoPlayer.frame;
        var newFrame = (long)Mathf.Clamp(currentFrame + framesToSkip, 0, _videoPlayer.frameCount);
        _videoPlayer.frame = newFrame;
    }

    public void CloseVideo()
    {
        _videoPlayer.frame = (long)_videoPlayer.frameCount;
    }
}
