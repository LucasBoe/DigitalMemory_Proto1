using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : TimeSetListener
{
    public int startTime, endTime, maxTime, minTime;
    public Sequence before, after;
    public float StartDelay = 1f, StartReversedDelay = 1f;
}
