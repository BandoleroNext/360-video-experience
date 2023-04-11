using Descriptors;
using Managers;
using UnityEngine.Video;

namespace Controllers
{
    public class InterruptibleVideoController : VideoController
    {
        public InterruptibleVideoDescriptor interruptibleVideoDescriptor;

        private new void Start()
        {
            base.Start();
            EventManager.OnInterruptibleVideoResume.AddListener(ResumeVideo);
            EventManager.OnInterruptibleVideoPause.AddListener(PauseVideo);
            EventManager.OnInterruptionVideoStart.AddListener(InterruptVideo);
            EventManager.OnInterruptionVideoCompleted.AddListener(ResumeVideo);
            if (interruptibleVideoDescriptor != null)
                StartVideo(interruptibleVideoDescriptor.video.url);
        }

        public void SetVideoDescriptorAndStart(InterruptibleVideoDescriptor descriptor)
        {
            interruptibleVideoDescriptor = descriptor;
            StartVideo(interruptibleVideoDescriptor.video.url);
        }

        private void InterruptVideo(string interruptionUrl)
        {
            EventManager.OnInterruptibleVideoPause.Invoke();
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
            EventManager.OnVideoCompleted.Invoke();
        }
    }
}