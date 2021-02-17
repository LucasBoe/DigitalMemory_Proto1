using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachableWatch : SimpleAttachable
{
    [SerializeField] Transform hourHand, minuteHand;
    [SerializeField] AttachableWatchHand hand;
    float time, targetTime;
    bool moves = false;

    float cursorSpeed = 90f;
    [SerializeField] float defaultCursorSpeed = 90f, resetCursorSpeed = 180f;

    private void OnEnable()
    {
        Game.TimeHandler.OnForceTimeReset += OnForceTimeReset;
        Game.TimeHandler.OnTimeChange += OnTimeChange;
    }

    private void OnDisable()
    {
        Game.TimeHandler.OnForceTimeReset -= OnForceTimeReset;
        Game.TimeHandler.OnTimeChange -= OnTimeChange;
    }

    private void OnForceTimeReset(float newTime)
    {
        targetTime = newTime;
        moves = true;
        cursorSpeed = resetCursorSpeed;
        if (hand.IsDragging)
            Game.MouseInteractor.ForceEndDrag();
    }

    private void OnTimeChange(float newTime)
    {
        targetTime = newTime;
        moves = true;
    }

    private void SetVisualsForTime(float blendedTime)
    {
        Vector3 hourEuler = hourHand.localRotation.eulerAngles;
        Quaternion hourRoration = Quaternion.Euler(hourEuler.x, ((blendedTime % 12f) / 12f) * 360f, hourEuler.z);
        hourHand.localRotation = hourRoration;

        Vector3 minuteEuler = minuteHand.localRotation.eulerAngles;
        Quaternion minuteRoration = Quaternion.Euler(minuteEuler.x, (blendedTime % 1f) * 360f, minuteEuler.z);
        minuteHand.localRotation = minuteRoration;
    }

    internal void UpdateWatchTimeByHand(float targetTime)
    {
        this.targetTime = targetTime;
        moves = true;
    }

    private void Update()
    {
        if (moves)
        {
            time = Mathf.MoveTowardsAngle(time * 30f, targetTime * 30f, Time.deltaTime * cursorSpeed) / 30f;
            Game.TimeHandler.MoveHand(time);
            SetVisualsForTime(time);

            if (Mathf.Abs(time % 12 - targetTime) < 0.01f)
            {
                cursorSpeed = defaultCursorSpeed;
                moves = false;
            }
        }
    }
}
