using UnityEngine;

namespace Descriptors
{
    public class InterruptionDescriptor : ScriptableObject
    {
        [Range(0, 100)] public int interruptAtPercentage;
    }
}