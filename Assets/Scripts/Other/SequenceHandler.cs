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
            StartCoroutine(ChangeSequenceDelayed(current, current.before, startFromBeginning: false));
    }

    internal void TryPlayAfter()
    {
        if (current.after != null)
            StartCoroutine(ChangeSequenceDelayed(current, current.after));
    }

    IEnumerator ChangeSequenceDelayed(Sequence from, Sequence to, bool startFromBeginning = true)
    {
        yield return new WaitForSeconds(startFromBeginning ? to.StartDelay : to.StartReversedDelay);

        to.gameObject.SetActive(true);
        from.gameObject.SetActive(false);

        current = to;
        Game.TimeHandler.StartNewSequence(current.startTime, current.endTime, startFromBeginning);
    }
}
