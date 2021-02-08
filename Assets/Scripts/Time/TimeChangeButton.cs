using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class TimeChangeButton : MonoBehaviour, IClickable
{
    [SerializeField] bool increase;
    [SerializeField] Animator buttonAnimator;

    public void Click()
    {
        if (increase)
            Game.TimeHandler.IncreaseTime();
        else
            Game.TimeHandler.DecreaseTime();

        buttonAnimator.SetTrigger("OnClick");
    }

    public bool IsClickable()
    {
        return buttonAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle");
    }
}
