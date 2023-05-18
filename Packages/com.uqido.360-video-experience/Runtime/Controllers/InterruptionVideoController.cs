using Managers;
using UnityEngine;
using UnityEngine.Video;

namespace Controllers
{
    public class InterruptionVideoController : VideoController
    {
        [SerializeField] private GameObject interruptionContainer;

        protected void Start()
        {
            interruptionContainer.SetActive(false);
            EventManager.OnInterruptionVideoStart.AddListener(StartVideo);
        }

        public override void StartVideo(string url)
        {
            EventManager.OnInterruptibleVideoPause.Invoke();
            base.StartVideo(url);
        }

        protected override void VideoPlayerOnPrepareCompleted(VideoPlayer source)
        {
            interruptionContainer.SetActive(true);
            base.VideoPlayerOnPrepareCompleted(source);
        }

        protected override void EndVideo(VideoPlayer source)
        {
            base.EndVideo(source);
            interruptionContainer.SetActive(false);
            EventManager.OnInterruptionVideoCompleted.Invoke();
        }
    }
}