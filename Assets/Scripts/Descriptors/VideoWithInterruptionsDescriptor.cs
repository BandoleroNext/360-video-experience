using System.Collections.Generic;
using UnityEngine;

namespace Descriptors
{
    [CreateAssetMenu(menuName = "360°video/New Video Descriptor with interruptions")]
    public class VideoWithInterruptionsDescriptor : ScriptableObject
    {
        public VideoDescriptor video;
        public List<InterruptionDescriptor> interruptions;
    }
}
