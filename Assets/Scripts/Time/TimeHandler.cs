using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeHandler : Singleton<TimeHandler>
{
    [SerializeField] private int time, timeMin, timeMax;

    [SerializeField] AudioClip rearrangeClip;

    public event System.Action<int> OnTimeChanged;

    public void IncreaseTime(int amount)
    {
        time= Mathf.Clamp(time+amount,timeMin,timeMax);
        Game.SoundPlayer.Play(rearrangeClip, null, volume: 0.25f, randomPitchRange: 0.5f);
        OnTimeChanged(time);
    }

    public void DecreaseTime(int amount)
    {
        time = Mathf.Clamp(time - amount, timeMin, timeMax);
        Game.SoundPlayer.Play(rearrangeClip, null, volume: 0.25f, randomPitchRange: 0.5f);
        OnTimeChanged(time);
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
