using System;
using System.IO;
using System.Net;
using Descriptors;
using Managers;
using UnityEngine;
using UnityEngine.Video;
using Utils;

public class InterruptibleVideoController : VideoController
{
    public InterruptibleVideoDescriptor interruptibleVideoDescriptor;

    private new void Start()
    {
        base.Start();
        EventManager.Instance.onInterruptibleVideoResume.AddListener(ResumeVideo);
        EventManager.Instance.onInterruptibleVideoPause.AddListener(PauseVideo);
        EventManager.Instance.onInterruptionVideoStart.AddListener(InterruptVideo);
        EventManager.Instance.onInterruptionVideoCompleted.AddListener(ResumeVideo);
        StartVideo(interruptibleVideoDescriptor.video.url);
    }

    private void InterruptVideo(string interruptionUrl)
    {
        EventManager.Instance.onInterruptibleVideoPause.Invoke();
    }

    protected override void VideoPlayerOnPrepareCompleted(VideoPlayer source)
    {
        base.VideoPlayerOnPrepareCompleted(source);
        var interruptionController = new InterruptionController(interruptibleVideoDescriptor.interruptions, source);
    }

    protected override void EndVideo(VideoPlayer vp)
    {
        base.EndVideo(vp);
        EventManager.Instance.onVideoCompleted.Invoke();
    }
}