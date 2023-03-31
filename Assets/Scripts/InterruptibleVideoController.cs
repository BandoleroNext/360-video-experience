using System;
using System.IO;
using System.Net;
using Descriptors;
using DG.Tweening;
using Managers;
using UnityEngine;
using UnityEngine.Video;
using Utils;

public class InterruptibleVideoController : VideoController
{
    public InterruptibleVideoDescriptor interruptibleVideoDescriptor;

    public float fadeResume = 1;
    public float fadePause = 0.4f;
    public float fadeTime = 0.2f;
    
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

    private new void ResumeVideo()
    {
        VideoControllerHelper.DoFadeAndCallCallback(fadeResume, () => { base.ResumeVideo(); }, fadeTime, exposure,
            targetMaterial);
    }

    private new void PauseVideo()
    {
        VideoControllerHelper.DoFadeAndCallCallback(fadePause, () => { base.PauseVideo(); }, fadeTime, exposure,
            targetMaterial);
    }

    protected override void VideoPlayerOnPrepareCompleted(VideoPlayer source)
    {
        base.VideoPlayerOnPrepareCompleted(source);
        VideoControllerHelper.DoFade(fadeResume, 0, exposure, targetMaterial);
        var interruptionController = new InterruptionController(interruptibleVideoDescriptor.interruptions, source);
    }

    protected override void EndVideo(VideoPlayer vp)
    {
        base.EndVideo(vp);
        VideoControllerHelper.DoFadeAndCallCallback(0, () => { EventManager.Instance.onVideoCompleted.Invoke(); }, fadeTime,exposure,targetMaterial);
    }
}