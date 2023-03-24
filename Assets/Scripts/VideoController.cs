using System;
using System.IO;
using System.Net;
using Descriptors;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class VideoController : MonoBehaviour
{
    public VideoWithInterruptionsDescriptor videoWithInterruptionsDescriptor;
    [SerializeField] private int renderTextureWidth = 4096;
    [SerializeField] private int renderTextureHeight = 2048;
    [SerializeField] private Material targetMaterial;

    public float fadeResume = 1;
    public float fadePause = 0.4f;
    public float fadeTime = 0.2f;
    private VideoPlayer _videoPlayer;


    private RenderTexture _renderTexture;
    private static readonly int _exposure = Shader.PropertyToID("_Exposure");


    private void Start()
    {
        EventManager.Instance.onVideoResume.AddListener(VideoResume);
        EventManager.Instance.onVideoPause.AddListener(VideoPause);
        _videoPlayer = GetComponent<VideoPlayer>();
        VideoStart();
    }

    private void VideoStart()
    {
        var title = videoWithInterruptionsDescriptor.video.title;
        var path = GeneratePathToVideo(videoWithInterruptionsDescriptor.video.url);

        if (path != "")
        {
            CreateTextureAndSetupMaterial();
            PrepareAndStartVideoPlayback(path);
            EventManager.Instance.onVideoResume.Invoke();
            _videoPlayer.loopPointReached += VideoCompleted;
        }
        else
        {
            Debug.Log($"Url non existent or not found");
        }
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
    private void VideoResume()
    {
        DoFadeAndCallCallback(fadeResume, () => { _videoPlayer.Play(); },fadeTime);
    }

    private void VideoPause()
    {
        DoFadeAndCallCallback(fadePause, () => { _videoPlayer.Pause(); },fadeTime);
    }

    private void CreateTextureAndSetupMaterial()
    {
        _renderTexture = new RenderTexture(renderTextureWidth,renderTextureHeight,32, RenderTextureFormat.ARGB32);
        targetMaterial.SetFloat(_exposure, 0f);
        targetMaterial.mainTexture = _renderTexture;
        _videoPlayer.targetTexture = _renderTexture;
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

    public static void DoFadeAndCallCallback(float targetExposure, Action callback, float fadeTime)
    {
        DOTween.To(() => RenderSettings.skybox.GetFloat(_exposure),
                (value) => RenderSettings.skybox.SetFloat(_exposure, value), targetExposure, fadeTime)
            .OnComplete(() => callback());
    }

    private void PrepareAndStartVideoPlayback(string videoUrl)
    {
        _videoPlayer.url = videoUrl;

        if (!_videoPlayer.isPrepared)
        {
            Debug.Log("TO PREPARE!");
            _videoPlayer.prepareCompleted += VideoPlayerOnPrepareCompleted;
            _videoPlayer.Prepare();
        }
        else
        {
            VideoPlayerOnPrepareCompleted(_videoPlayer);
        }
    }

    private void VideoPlayerOnPrepareCompleted(VideoPlayer source)
    {
        Debug.Log("READY");
        Debug.Log($"Video Playback: {source.width}:{source.height}@{source.frameRate}");
    }

    private void VideoCompleted(VideoPlayer vp)
    {
        Debug.Log("Video has ended");
        DoFadeAndCallCallback(0, () =>
        {
            EventManager.Instance.onVideoCompleted.Invoke();
        }, fadeTime);
    }
}