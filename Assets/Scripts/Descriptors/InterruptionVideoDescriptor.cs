using UnityEngine;

namespace Descriptors
{
    [CreateAssetMenu(menuName = "360°video/New Plain Video Interruption Descriptor")]
    public class InterruptionVideoDescriptor : InterruptionDescriptor
    {
        public VideoDescriptor video;
    }
}
