using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeHandler : Singleton<TimeHandler>
{
    [SerializeField] private float time, timeMin, timeMax, timeStart, timeEnd;
    public float Time => time;

    [SerializeField] AudioClip rearrangeClip;

    public event System.Action<float> OnTimeUpdate;
    public event System.Action<float> OnTimeChange;
    public event System.Action<float> OnForceTimeReset;

    public void StartNewSequence(Sequence newSequence, bool startFromBeginning = true)
    {
        timeMin = newSequence.minTime;
        timeMax = newSequence.maxTime;
        timeStart = newSequence.startTime;
        timeEnd = newSequence.endTime;

        time = startFromBeginning ? newSequence.startTime : newSequence.startReversedTime;
        OnTimeUpdate(time);
        OnTimeChange(time);
    }

    public void IncreaseTime(int amount)
    {
        if (time + amount <= timeMax)
        {
            time += amount;
            Game.SoundPlayer.Play(rearrangeClip, null, volume: 0.25f, randomPitchRange: 0.5f);
            OnTimeUpdate(time);
            OnTimeChange(time);
        }

        if (time + amount >= timeMax)
            Game.SequenceHandler.TryPlayAfter();
    }

    public void ForceTimeSet(float newTime)
    {
        Debug.Log("Set Time to: " + newTime);

        time = newTime;
        OnTimeUpdate(newTime);
        OnForceTimeReset(newTime);
    }
    internal void MoveHand(float newTime)
    {
        time = newTime;
        OnTimeUpdate(newTime);
    }

    public void DecreaseTime(int amount)
    {
        if (time - amount >= timeMin)
        {
            time -= amount;
            Game.SoundPlayer.Play(rearrangeClip, null, volume: 0.25f, randomPitchRange: 0.5f);
            OnTimeUpdate(time);
            OnTimeChange(time);
        }

        if (time <= timeMin)
            Game.SequenceHandler.TryPlayBefore();
    }

    
}
