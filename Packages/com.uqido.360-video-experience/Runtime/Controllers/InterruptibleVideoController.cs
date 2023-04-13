using Descriptors;
using Managers;
using UnityEngine;
using UnityEngine.Video;

namespace Controllers
{
    public class InterruptibleVideoController : VideoController
    {
        public InterruptibleVideoDescriptor interruptibleVideoDescriptor;

        protected override void Start()
        {
            base.Start();
            EventManager.OnInterruptibleVideoResume.AddListener(ResumeVideo);
            EventManager.OnInterruptibleVideoPause.AddListener(PauseVideo);
            EventManager.OnInterruptionVideoCompleted.AddListener(ResumeVideo);
        }

        public void StartVideo()
        {
            if (interruptibleVideoDescriptor == null)
            {
                Debug.LogError("Descriptor is missing!! Unable to start video");
                return;
            }

            StartVideo(interruptibleVideoDescriptor.video.url);
        }

        public void SetVideoDescriptorAndStart(InterruptibleVideoDescriptor descriptor)
        {
            interruptibleVideoDescriptor = descriptor;
            StartVideo(interruptibleVideoDescriptor.video.url);
        }

        protected override void VideoPlayerOnPrepareCompleted(VideoPlayer source)
        {
            base.VideoPlayerOnPrepareCompleted(source);
            var interruptionController = new InterruptionController(interruptibleVideoDescriptor.interruptions, source);
            StartCoroutine(interruptionController.ManageInterruptions());
        }

        protected override void EndVideo(VideoPlayer vp)
        {
            base.EndVideo(vp);
            EventManager.OnInterruptibleVideoCompleted.Invoke();
        }
    }
}