using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachableWatch : SimpleAttachable
{
    [SerializeField] Transform hourHand, minuteHand;
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
        Vector3 hourEuler = hourHand.localRotation.eulerAngles;
        Quaternion newRoration = Quaternion.Euler(hourEuler.x, ((float)(newTime % 12)/12f) * 360f, hourEuler.z);
        hourHand.localRotation = newRoration;
    }
}
