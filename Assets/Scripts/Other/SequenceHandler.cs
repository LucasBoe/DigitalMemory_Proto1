using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceHandler : Singleton<SequenceHandler>
{
    [SerializeField] Sequence current;
    internal void TryPlayBefore()
    {
        if (current.before != null)
        {
            Sequence before = current.before;
            before.gameObject.SetActive(true);
            current.gameObject.SetActive(false);
            current = before;

            Game.TimeHandler.StartNewSequence(current.startTime, current.endTime, startFromBeginning: false);
        }
    }

    internal void TryPlayAfter()
    {
        if (current.after != null)
        {
            Sequence after = current.after;
            after.gameObject.SetActive(true);
            current.gameObject.SetActive(false);
            current = after;

            Game.TimeHandler.StartNewSequence(current.startTime, current.endTime);
        }
    }
}
