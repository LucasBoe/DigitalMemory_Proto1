using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeChangeButton : MonoBehaviour, IClickable
{
    [SerializeField] bool increase;

    public void Click()
    {
        if (increase)
            Game.TimeHandler.IncreaseTime();
        else
            Game.TimeHandler.DecreaseTime();
    }

    public bool IsClickable()
    {
        return true;
    }
}
