using System;
using Descriptors;
using Managers;
using UnityEngine;
using UnityEngine.Video;
using Utils;

[RequireComponent(typeof(VideoPlayer))]
public class VideoController : MonoBehaviour
{
    public readonly int exposure = Shader.PropertyToID("_Exposure");
    public Material targetMaterial;
    [SerializeField] protected int renderTextureWidth = 4096;
    [SerializeField] protected int renderTextureHeight = 2048;


    private RenderTexture _renderTexture;
    protected VideoPlayer videoPlayer;

    protected void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    protected void StartVideo(string url)
    {
        var correctUrl = VideoControllerHelper.GeneratePathToVideo(url);
        if (correctUrl == "")
        {
            Debug.LogError("VIDEO NOT FOUND ");
            return;
        }

        CreateTextureAndSetupMaterial();
        PrepareAndStartVideoPlayback(correctUrl);
    }

    private void PrepareAndStartVideoPlayback(string videoUrl)
    {
        videoPlayer.url = videoUrl;

        if (!videoPlayer.isPrepared)
        {
            Debug.Log("TO PREPARE!");
            videoPlayer.prepareCompleted += VideoPlayerOnPrepareCompleted;
            videoPlayer.errorReceived += VideoPlayerOnerrorReceived;
            videoPlayer.Prepare();
        }
        else
        {
            VideoPlayerOnPrepareCompleted(videoPlayer);
        }
    }

    private static void VideoPlayerOnerrorReceived(VideoPlayer source, string message)
    {
        Debug.LogError("There were some issue with the URL of the video");
        Debug.LogError("Sending the OnVideoCompleted Event");
        EventManager.onVideoCompleted.Invoke();
    }

    protected virtual void VideoPlayerOnPrepareCompleted(VideoPlayer source)
    {
        Debug.Log("READY");
        Debug.Log($"Video Playback: {source.width}:{source.height}@{source.frameRate}");
        videoPlayer.Play();
        videoPlayer.loopPointReached += EndVideo;
    }

    protected virtual void EndVideo(VideoPlayer player)
    {
        Debug.Log($"Video completed");
    }

    private void CreateTextureAndSetupMaterial()
    {
        _renderTexture = new RenderTexture(renderTextureWidth, renderTextureHeight, 32, RenderTextureFormat.ARGB32);
        targetMaterial.mainTexture = _renderTexture;
        videoPlayer.targetTexture = _renderTexture;
    }

    public void PauseVideo()
    {
        videoPlayer.Pause();
    }

    public void ResumeVideo()
    {
        videoPlayer.Play();
    }
}