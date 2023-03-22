using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterruptionDescriptor : ScriptableObject
{
    [Range(0,100)]
    public int interruptAtPercentage = 0;
}
