using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "360°video/New Skybox Video Descriptor")]
public class SkyboxVideoDescriptor : ScriptableObject
{
    public string Title = "";
    public string Description = "";
    public string Url = "";
    public float FadeResume = 1;
    public float FadePause = 0.4f;
}
