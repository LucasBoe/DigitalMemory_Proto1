using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeHandler : Singleton<TimeHandler>
{
    public event System.Action<int> OnTimeChanged;

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
