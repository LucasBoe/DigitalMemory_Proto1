using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeListener : MonoBehaviour
{
    [SerializeField] Animator animator;
    private void OnEnable()
    {
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

    private void OnTimeChanged(int newTime)
    {
        animator.SetFloat("time", newTime);
    }
}
