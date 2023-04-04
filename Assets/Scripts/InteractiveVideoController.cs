using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Managers;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class InteractiveVideoController : VideoController
{
    [SerializeField] private GameObject screenParent;
    [SerializeField] private int secondsToSkip;

    private new void Start()
    {
        base.Start();
        screenParent.SetActive(false);
        EventManager.Instance.onInterruptionVideoStart.AddListener(StartVideo);
    }

    private new void StartVideo(string url)
    {
        base.StartVideo(url);
    }

    protected override void VideoPlayerOnPrepareCompleted(VideoPlayer source)
    {
        screenParent.SetActive(true);
        base.VideoPlayerOnPrepareCompleted(source);
    }
    
    protected override void EndVideo(VideoPlayer source)
    {
        screenParent.SetActive(false);
        EventManager.Instance.onInterruptionVideoCompleted.Invoke();
    }

    public void SkipFrames(bool skipAhead)
    {
        var framesToSkip = (skipAhead ? 1 : -1) * (long)videoPlayer.frameRate * secondsToSkip;
        var currentFrame = videoPlayer.frame;
        var newFrame = (long)Mathf.Clamp(currentFrame + framesToSkip, 0, videoPlayer.frameCount);
        videoPlayer.frame = newFrame;
    }

    public void CloseVideo()
    {
        videoPlayer.Stop();
        EndVideo(videoPlayer);
    }
}