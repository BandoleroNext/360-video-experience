using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Descriptors;
using Managers;
using UnityEngine;
using UnityEngine.Video;

namespace Controllers
{
    public class InterruptionController
    {
        private readonly VideoPlayer _currentVideoPlayer;
        private readonly Dictionary<long, InterruptionDescriptor> _interruptions;

        public InterruptionController(IEnumerable<InterruptionDescriptor> interruptions, VideoPlayer player)
        {
            _currentVideoPlayer = player;
            var orderedInterruptions = interruptions.OrderBy(descriptor => descriptor.interruptAtPercentage).ToList();
            _interruptions = new Dictionary<long, InterruptionDescriptor>();
            var videoFrameLength = (long)(player.length * player.frameRate);
            foreach (var interruption in orderedInterruptions)
            {
                var timestamp = videoFrameLength * interruption.interruptAtPercentage / 100;
                _interruptions[timestamp] = interruption;
            }

            if (orderedInterruptions.Count != _interruptions.Count)
                Debug.LogWarning(
                    $"Some interruptions with the same percentage were found. Only the last one with the same percentage will be used. Update the percentage to be different");
        }

        public IEnumerator ManageInterruptions(int startingInterruption = 0)
        {
            if (startingInterruption < 0 || startingInterruption >= _interruptions.Keys.Count)
                yield break;
            for (var i = startingInterruption; i < _interruptions.Keys.Count; i++)
            {
                var key = _interruptions.Keys.ToArray()[i];
                yield return new WaitUntil(() => _currentVideoPlayer.frame >= key);
                var interruption = _interruptions[key];
                switch (interruption)
                {
                    case InterruptionVideoDescriptor videoInterruption:
                        Debug.Log(
                            $"Video interruption {videoInterruption.video.title}, pausing 360 video at {_currentVideoPlayer.frame}");
                        EventManager.OnInterruptionVideoStart.Invoke(videoInterruption.video.url);
                        break;
                    case QuizInterruptionDescriptor quizInterruption:
                        Debug.Log(
                            $"Quiz interruption {quizInterruption}, pausing 360 video at {_currentVideoPlayer.frame}");
                        EventManager.OnQuizStart.Invoke(quizInterruption);
                        break;
                    default:
                        Debug.Log($"Interruption not managed");
                        break;
                }
            }
        }
    }
}