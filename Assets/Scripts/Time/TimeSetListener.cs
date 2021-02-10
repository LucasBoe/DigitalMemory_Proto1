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
                lastSet = (int)current.speed;
                Game.TimeHandler.ForceTimeSet((int)current.speed);
            }

        }
    }
}
