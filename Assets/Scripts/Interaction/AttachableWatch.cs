using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachableWatch : SimpleAttachable
{
    [SerializeField] Transform hourHand, minuteHand;
    [SerializeField] AudioClip rearrangeClip;
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
        Game.SoundPlayer.Play(rearrangeClip,null,volume: 0.25f,randomPitchRange: 0.5f);
        Vector3 hourEuler = hourHand.localRotation.eulerAngles;
        Quaternion newRoration = Quaternion.Euler(hourEuler.x, ((float)(newTime % 12)/12f) * 360f, hourEuler.z);
        hourHand.localRotation = newRoration;
    }
}
