using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsingEffector : MonoBehaviour
{
    Vector3 defaultScale;
    private void Start()
    {
        defaultScale = transform.localScale;
    }

    public float duration;
    //
    internal void StartPulsing(float duration)
    {
        this.duration = duration;
    }

    public void StartPulsing()
    {
        duration = float.MaxValue;
    }
    private void Update()
    {
        duration -= Time.deltaTime;
        transform.localScale = defaultScale + Vector3.one * Mathf.Sin(Time.time);

        if (duration <= 0)
            Stop();
    }

    public void Stop()
    {
        transform.localScale = defaultScale;
        Destroy(this);
    }
}
