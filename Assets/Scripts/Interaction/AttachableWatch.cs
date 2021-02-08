using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachableWatch : SimpleAttachable
{
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
        //
    }
}
