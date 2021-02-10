using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeHandler : Singleton<TimeHandler>
{
    [SerializeField] private int time, timeMin, timeMax;

    [SerializeField] AudioClip rearrangeClip;

    public event System.Action<int> OnTimeChanged;

    public void StartNewSequence(int startTime, int endTime, bool startFromBeginning = true)
    {
        timeMin = startTime;
        timeMax = endTime;

        time = startFromBeginning ? startTime : endTime;
        OnTimeChanged(time);
    }

    public void IncreaseTime(int amount)
    {
        if (time + amount <= timeMax)
        {
            time = Mathf.Clamp(time + amount, timeMin, timeMax);
            Game.SoundPlayer.Play(rearrangeClip, null, volume: 0.25f, randomPitchRange: 0.5f);
            OnTimeChanged(time);
        }

        if (time + amount >= timeMax)
            Game.SequenceHandler.TryPlayAfter();
    }

    public void ForceTimeSet(int newTime)
    {
        Debug.LogWarning("Forced Time to " + newTime);

        time = newTime;
        OnTimeChanged(newTime);
    }

    public void DecreaseTime(int amount)
    {
        if (time - amount >= timeMin)
        {
            time = Mathf.Clamp(time - amount, timeMin, timeMax);
            Game.SoundPlayer.Play(rearrangeClip, null, volume: 0.25f, randomPitchRange: 0.5f);
            OnTimeChanged(time);
        }

        if (time <= timeMin)
            Game.SequenceHandler.TryPlayBefore();
    }

    [Button]
    private void Set0()
    {
        OnTimeChanged.Invoke(0);
    }

    [Button]
    private void Set3()
    {
        OnTimeChanged.Invoke(3);
    }

    [Button]
    private void Set6()
    {
        OnTimeChanged.Invoke(6);
    }

    [Button]
    private void Set9()
    {
        OnTimeChanged.Invoke(9);
    }
}
