using Descriptors;
using Managers;
using UnityEngine;
using UnityEngine.Video;

namespace Controllers
{
    public class InterruptibleVideoController : VideoController
    {
        public InterruptibleVideoDescriptor interruptibleVideoDescriptor;

        private int _startingInterruption = 0;
        protected override void Start()
        {
            base.Start();
            EventManager.OnInterruptibleVideoResume.AddListener(ResumeVideo);
            EventManager.OnInterruptibleVideoPause.AddListener(PauseVideo);
            EventManager.OnInterruptionVideoCompleted.AddListener(ResumeVideo);
        }

        public void StartVideo(int startingInterruption = 0)
        {
            if (interruptibleVideoDescriptor == null)
            {
                Debug.LogError("Descriptor is missing!! Unable to start video");
                return;
            }

            _startingInterruption = startingInterruption;
            StartVideo(interruptibleVideoDescriptor.video.url);
        }

        public void SetVideoDescriptorAndStart(InterruptibleVideoDescriptor descriptor,int startingInterruption = 0)
        {
            interruptibleVideoDescriptor = descriptor;
            StartVideo(startingInterruption);
        }

        protected override void VideoPlayerOnPrepareCompleted(VideoPlayer source)
        {
            base.VideoPlayerOnPrepareCompleted(source);
            var interruptionController = new InterruptionController(interruptibleVideoDescriptor.interruptions, source);
            StartCoroutine(interruptionController.ManageInterruptions(_startingInterruption));
        }

        protected override void EndVideo(VideoPlayer vp)
        {
            base.EndVideo(vp);
            EventManager.OnInterruptibleVideoCompleted.Invoke();
        }
    }
}