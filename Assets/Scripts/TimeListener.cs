using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeListener : MonoBehaviour
{
    [SerializeField] Animator animator;
    private void OnEnable()
    {
        Game.TimeHandler.OnTimeChanged += OnTimeChanged;
    }

    private void OnDisable()
    {
        Game.TimeHandler.OnTimeChanged -= OnTimeChanged;
    }

    private void OnTimeChanged (int newTime)
    {
        animator.SetFloat("time", newTime);
    }
}
