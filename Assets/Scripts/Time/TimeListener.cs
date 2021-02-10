using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeListener : MonoBehaviour
{
    protected Animator animator;
    private void OnEnable()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogWarning("No Animator Referenced at " + gameObject + ". Please add one or remove the TimeListener script.");
        }

        animator.keepAnimatorControllerStateOnDisable = true;
        Game.TimeHandler.OnTimeChanged += OnTimeChanged;
    }

    private void OnDisable()
    {
        Game.TimeHandler.OnTimeChanged -= OnTimeChanged;
    }

    protected virtual void OnTimeChanged(int newTime)
    {
        animator.SetFloat("time", newTime);
    }
}
