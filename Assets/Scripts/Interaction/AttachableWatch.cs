using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachableWatch : SimpleAttachable
{
    [SerializeField] Transform hourHand, minuteHand;
    [SerializeField] AnimationCurve timeBlendingCurve;

    float currentTime, targetTime;
    bool isBlending = false;
    float blendStartTime, blendEndTime, blendedTime;

    private void OnEnable()
    {
        Game.TimeHandler.OnTimeChanged += OnTimeChanged;
    }

    private void OnDisable()
    {
        Game.TimeHandler.OnTimeChanged -= OnTimeChanged;
    }

    private void OnTimeChanged(int newTime)
    {
        if (isBlending)
            currentTime = blendedTime;

        blendStartTime = Time.time;
        blendEndTime = blendStartTime + timeBlendingCurve.keys[timeBlendingCurve.length - 1].time;
        targetTime = newTime;
        isBlending = true;
    }

    private void Update()
    {
        if (isBlending)
        {
            float alpha = timeBlendingCurve.Evaluate(Time.time - blendStartTime);
            blendedTime = Mathf.Lerp(currentTime, targetTime, alpha);
            SetVisualsForTime(blendedTime);

            if (Time.time >= blendEndTime)
            {
                isBlending = false;
                currentTime = targetTime;
                SetVisualsForTime(currentTime);
            }
        }
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
}
