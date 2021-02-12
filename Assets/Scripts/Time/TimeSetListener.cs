using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSetListener : TimeListener
{
    bool listen;
    int lastSet = 0;
    protected override void OnTimeChanged(int newTime)
    {
        if (lastSet != newTime)
            listen = true;

        base.OnTimeChanged(newTime);
    }

    private void Update()
    {
        if (listen)
        {
            AnimatorStateInfo current = animator.GetCurrentAnimatorStateInfo(0);

            if (current.IsName("Set Time"))
            {
                listen = false;
                int time = GetTimeFromTagHash(current.tagHash);
                Game.TimeHandler.ForceTimeSet(time);
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
