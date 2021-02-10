using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeHandler : Singleton<TimeHandler>
{
    [SerializeField] private int time, timeMin, timeMax, timeStart, timeEnd;

    [SerializeField] AudioClip rearrangeClip;

    public event System.Action<int> OnTimeChanged;

    public void StartNewSequence(int minTime, int startTime, int endTime, int maxTime, bool startFromBeginning = true)
    {
        timeMin = minTime;
        timeMax = maxTime;
        timeStart = startTime;
        timeEnd = endTime;

        time = startFromBeginning ? startTime : endTime;
        OnTimeChanged(time);
    }

    public void IncreaseTime(int amount)
    {
        if (time + amount <= timeMax)
        {
            time += amount;
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
            time -= amount;
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
