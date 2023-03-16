using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Video;


public class SkyboxVideoController : MonoBehaviour
{
    private void Awake()
    {
        RenderSettings.skybox.SetFloat(Exposure, 0f);
        RenderSettings.skybox.mainTexture = SkyboxRenderTexture;
        _skyboxVideoPlayer = GetComponent<VideoPlayer>();
        _skyboxVideoPlayer.targetTexture = SkyboxRenderTexture;
    }
    
    public SkyboxVideoDescriptor skyboxDescriptor;
    private VideoPlayer _skyboxVideoPlayer;
    
    
    [SerializeField] private RenderTexture SkyboxRenderTexture;
    private static readonly int Exposure = Shader.PropertyToID("_Exposure");




    private void Start()
    {

        EventManager.Instance.OnSkyboxVideoResume.AddListener(ResumeVideo);
        EventManager.Instance.OnSkyboxVideoPause.AddListener(PauseVideo);


        var title = skyboxDescriptor.Title;
        var description = skyboxDescriptor.Description;
        var url = skyboxDescriptor.Url;
        if (CheckVideoUrl(url))
        {
            
        }
        else
        {
            Debug.Log($"Url non existent or not found");
        }
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
                Debug.Log($"Video not found!");
                return false;
            }
        }
    }
    
    private static bool RemoteFileExists(string url)
    {
        try
        {
            //Creating the HttpWebRequest
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "HEAD";
            //Getting the Web Response.
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            response.Close();
            return (response.StatusCode == HttpStatusCode.OK);
        }
        catch
        {
            return false;
        }
    }
    
    private void ResumeVideo()
    {
        DoFadeAndCallCallback(1, () => { _skyboxVideoPlayer.Play(); });
    }
    
    private void PauseVideo()
    {
        DoFadeAndCallCallback(1, () => { _skyboxVideoPlayer.Pause(); });
    }
    
    public void DoFadeAndCallCallback(float targetExposure, Action callback)
    {
        DOTween.To(() => RenderSettings.skybox.GetFloat(Exposure),
                (value) => RenderSettings.skybox.SetFloat(Exposure, value), targetExposure, 0.2f)
            .OnComplete(() => callback());
    }
}
