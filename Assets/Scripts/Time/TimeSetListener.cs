using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSetListener : TimeListener
{
    bool listen;
    int lastSet = 0;
    AnimatorStateInfo last;
    protected override void OnTimeUpdate(float newTime)
    {
        if (lastSet != newTime)
            listen = true;

        base.OnTimeUpdate(newTime);
    }

    private void Update()
    {
        if (listen)
        {
            AnimatorStateInfo current = animator.GetCurrentAnimatorStateInfo(0);
            if (last.shortNameHash != current.shortNameHash)
            {
                listen = false;
                int time = GetTimeFromTagHash(current.tagHash);
                if (time != int.MinValue)
                {
                    if ((int)Game.TimeHandler.Time != time)
                        Game.TimeHandler.ForceTimeSet(time);

                    last = current;
                }
            }

        }
    }
    private int GetTimeFromTagHash(int tagHash)
    {
        for (int i = 0; i <= 24; i++)
        {
            if (tagHash == Animator.StringToHash(i.ToString()))
                return i;
        }

        return int.MinValue;
    }
}
