using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "360°video/New Skybox Video Descriptor")]
public class SkyboxVideoDescriptor : ScriptableObject
{
    public string title = "";
    public string description = "";
    public string url = "";
    public List<InterruptionDescriptor> interruptions = new();
}
