using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Video;

public class InterruptionController
{
    private readonly VideoPlayer _currentVideoPlayer;
    
    public InterruptionController(IEnumerable<InterruptionDescriptor> interruptions, VideoPlayer player)
    {
        _currentVideoPlayer = player;
        var orderedInterruptions = interruptions.OrderBy(descriptor => descriptor.interruptAtPercentage).ToList();
        var timestampToInterruption = new Dictionary<long, InterruptionDescriptor>();
        var videoFrameLength = (long)(player.length * player.frameRate);
        foreach (var interruption in orderedInterruptions)
        {
            var timestamp = videoFrameLength * interruption.interruptAtPercentage / 100;
            timestampToInterruption[timestamp] = interruption;
        }
        if(orderedInterruptions.Count != timestampToInterruption.Count)
            Debug.LogWarning($"Some interruptions with the same percentage were found. Only the last one with the same percentage will be used. Update the percentage to be different");
        ManageInterruptions(timestampToInterruption);
    }

    private async void ManageInterruptions(Dictionary<long,InterruptionDescriptor> interruptions)
    {
        var keys = interruptions.Keys.ToList();
        for (var currentInterruption = 0; currentInterruption < keys.Count; currentInterruption++)
        {
            var interruptionIndex = currentInterruption;
            await UniTask.WaitUntil(()=>_currentVideoPlayer.frame >= keys[interruptionIndex]);
            var interruption = interruptions[keys[interruptionIndex]];
            if (interruption is not PlainVideoInterruptionDescriptor videoInterruption) continue;
            Debug.Log($"Video interruption {videoInterruption.title}, pausing 360 video at {_currentVideoPlayer.frame}");
            EventManager.Instance.onSkyboxVideoPause.Invoke();
            EventManager.Instance.onPLainVideoBegin.Invoke(videoInterruption.url);
        }
    }
}
