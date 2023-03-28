using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Descriptors;
using Managers;
using UnityEngine;
using UnityEngine.Video;

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
        ManageInterruptions();
    }

    private async void ManageInterruptions()
    {
        foreach (var key in _interruptions.Keys)
        {
            await UniTask.WaitUntil(() => _currentVideoPlayer.frame >= key);
            var interruption = _interruptions[key];
            if (interruption is InterruptionVideoDescriptor videoInterruption)
            {
                Debug.Log(
                    $"Video interruption {videoInterruption.video.title}, pausing 360 video at {_currentVideoPlayer.frame}");
                EventManager.Instance.onInterruptibleVideoStart.Invoke(videoInterruption.video.url);
            }

            if (interruption is QuizInterruptionDescriptor quizInterruption)
            {
                Debug.Log(
                    $"Quiz interruption {quizInterruption}, pausing 360 video at {_currentVideoPlayer.frame}");
                EventManager.Instance.onQuizStart.Invoke();
            }
        }
    }
}