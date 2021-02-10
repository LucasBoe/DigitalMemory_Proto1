using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : TimeListener
{
    public int startTime, endTime;
    public Sequence before, after;
    public float StartDelay = 1f, StartReversedDelay = 1f;
}
