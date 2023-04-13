using Managers;
using UnityEngine;
using UnityEngine.Video;
using Utils;

namespace Controllers
{
    [RequireComponent(typeof(VideoPlayer))]
    public class VideoController : MonoBehaviour
    {
        public Material targetMaterial;
        [SerializeField] protected int renderTextureWidth = 4096;
        [SerializeField] protected int renderTextureHeight = 2048;
        [SerializeField] private int secondsToSkip;
        
        private RenderTexture _renderTexture;
        protected VideoPlayer videoPlayer;

        protected virtual void Start()
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        public virtual void StartVideo(string url)
        {
            var correctUrl = VideoControllerHelper.GeneratePathToVideo(url);
            if (correctUrl == "")
            {
                Debug.LogError("VIDEO NOT FOUND ");
                return;
            }

            CreateTextureAndSetupMaterial();
            SetupVideoPlayer(correctUrl);
        }

        private void SetupVideoPlayer(string videoUrl)
        {
            videoPlayer.url = videoUrl;
            Debug.Log("TO PREPARE!");
            videoPlayer.prepareCompleted += VideoPlayerOnPrepareCompleted;
            videoPlayer.errorReceived += VideoPlayerOnerrorReceived;
            videoPlayer.Prepare();
        }

        private static void VideoPlayerOnerrorReceived(VideoPlayer source, string message)
        {
            Debug.LogError("There were some issue with the URL of the video");
            Debug.LogError("Sending the OnVideoCompleted Event");
            EventManager.OnInterruptibleVideoCompleted.Invoke();
        }

        protected virtual void VideoPlayerOnPrepareCompleted(VideoPlayer source)
        {
            Debug.Log("READY");
            Debug.Log($"Video Playback: {source.width}:{source.height}@{source.frameRate}");
            videoPlayer.Play();
            videoPlayer.loopPointReached += EndVideo;
        }

        protected virtual void EndVideo(VideoPlayer player)
        {
            Debug.Log($"Video completed");
        }

        private void CreateTextureAndSetupMaterial()
        {
            _renderTexture = new RenderTexture(renderTextureWidth, renderTextureHeight, 32, RenderTextureFormat.ARGB32);
            targetMaterial.mainTexture = _renderTexture;
            videoPlayer.targetTexture = _renderTexture;
        }

        public void PauseVideo()
        {
            videoPlayer.Pause();
        }

        public void ResumeVideo()
        {
            videoPlayer.Play();
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
}