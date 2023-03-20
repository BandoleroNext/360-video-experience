using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Video;


public class SkyboxVideoController : MonoBehaviour
{
    public SkyboxVideoDescriptor skyboxDescriptor;
    private VideoPlayer _skyboxVideoPlayer;


    [SerializeField] private RenderTexture skyboxRenderTexture;
    private static readonly int Exposure = Shader.PropertyToID("_Exposure");


    private void Start()
    {
        EventManager.Instance.OnSkyboxVideoResume.AddListener(VideoResume);
        EventManager.Instance.OnSkyboxVideoPause.AddListener(VideoPause);
        EventManager.Instance.OnSkyboxVideoCompleted.AddListener(VideoEnd);
        VideoStart();
    }

    private void VideoStart()
    {
        var title = skyboxDescriptor.Title;
        var url = skyboxDescriptor.Url;
        url = CheckForDemoVideo(url);

        CreateSkybox(title);
        
        if (CheckVideoUrl(url))
        {
            PrepareAndStartVideoPlayback(url);
            EventManager.Instance.OnSkyboxVideoResume.Invoke();
        }
        else
        {
            Debug.Log($"Url non existent or not found");
        }
    }

    private void VideoEnd()
    {
    }

    private void VideoResume()
    {
        DoFadeAndCallCallback(1, () => { _skyboxVideoPlayer.Play(); });
    }

    private void VideoPause()
    {
        DoFadeAndCallCallback(0.4f, () => { _skyboxVideoPlayer.Pause(); });
    }

    private string CheckForDemoVideo(string url)
    {
        if (url == "DemoVideo.mp4")
        {
            Debug.Log($"Loading up demo video!");
            url = Application.dataPath + "/Videos/DemoVideo.mp4";
            return url;
        }
        else
        {
            return url;
        }
    }

    private void CreateSkybox(string title)
    {
        var skyboxMaterial = new Material(Shader.Find("Skybox/Panoramic"))
        {
            name = title
        };
        //SkyboxRenderTexture = new RenderTexture((int)_skyboxVideoPlayer.width,(int)_skyboxVideoPlayer.height,16, RenderTextureFormat.ARGB32);
        RenderSettings.skybox = skyboxMaterial;
        RenderSettings.skybox.SetFloat(Exposure, 0f);
        RenderSettings.skybox.mainTexture = skyboxRenderTexture;
        _skyboxVideoPlayer = GetComponent<VideoPlayer>();
        _skyboxVideoPlayer.targetTexture = skyboxRenderTexture;
    }

    private bool CheckVideoUrl(string url)
    {
        Debug.Log($"Searching for the video!");

        if (System.IO.File.Exists(url))
        {
            return true;
        }
        else
        {
            Debug.Log($"Not found in memory, searching on the web");
            if (RemoteFileExists(url))
            {
                Debug.Log($"Video found!");
                return true;
            }
            else
            {
                Debug.LogError($"Video not found!");
                return false;
            }
        }
    }

    private static bool RemoteFileExists(string url)
    {
        try
        {
            //Searching through an HttpWebRequest
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "HEAD";
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            response.Close();
            return (response.StatusCode == HttpStatusCode.OK);
        }
        catch
        {
            Debug.LogError($"Video not found!");
            return false;
        }
    }

    private void DoFadeAndCallCallback(float targetExposure, Action callback)
    {
        DOTween.To(() => RenderSettings.skybox.GetFloat(Exposure),
                (value) => RenderSettings.skybox.SetFloat(Exposure, value), targetExposure, 0.2f)
            .OnComplete(() => callback());
    }

    private void PrepareAndStartVideoPlayback(string videoUrl)
    {
        _skyboxVideoPlayer.url = videoUrl;

        if (!_skyboxVideoPlayer.isPrepared)
        {
            Debug.Log("TO PREPARE!");
            _skyboxVideoPlayer.prepareCompleted += VideoPlayerOnPrepareCompleted;
            _skyboxVideoPlayer.Prepare();
        }
        else
        {
            VideoPlayerOnPrepareCompleted(_skyboxVideoPlayer);
        }
    }

    private void VideoPlayerOnPrepareCompleted(VideoPlayer source)
    {
        Debug.Log("READY");
        Debug.Log($"Video Playback: {source.width}:{source.height}@{source.frameRate}");
    }

    private void Update()
    {
        //Da rimuovere in favore di un debugger esterno
        if (Input.GetKeyDown(KeyCode.H))
        {
            EventManager.Instance.OnSkyboxVideoPause.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            EventManager.Instance.OnSkyboxVideoResume.Invoke();
        }
    }


    private void OnDestroy()
    {
        RenderSettings.skybox.mainTexture = skyboxRenderTexture;
    }
}