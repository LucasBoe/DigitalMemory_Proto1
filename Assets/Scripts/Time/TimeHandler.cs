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

    public void StartNewSequence(Sequence newSequence, bool startFromBeginning = true)
    {
        timeMin = newSequence.minTime;
        timeMax = newSequence.maxTime;
        timeStart = newSequence.startTime;
        timeEnd = newSequence.endTime;

        time = startFromBeginning ? newSequence.startTime : newSequence.startReversedTime;
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
        Debug.Log("Set Time to: " + newTime);

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
}
