using UnityEngine;

namespace Descriptors
{
    [CreateAssetMenu(menuName = "360°video/New Video Descriptor")]
    public class VideoDescriptor : ScriptableObject
    {
        public string title;
        public string description;
        public string url;
    }
}
