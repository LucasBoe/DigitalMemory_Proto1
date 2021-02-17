using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachableWatch : SimpleAttachable
{
    [SerializeField] Transform hourHand, minuteHand, face;
    [SerializeField] AttachableWatchHand hand;
    [SerializeField] Effect timeResetEffect;
    [SerializeField] GameObject[] dots;
    float time, targetTime;
    bool moves = false;

    float cursorSpeed = 90f;
    [SerializeField] float defaultCursorSpeed = 90f, resetCursorSpeed = 180f;

    private void OnEnable()
    {
        Game.TimeHandler.OnForceTimeReset += OnForceTimeReset;
        Game.TimeHandler.OnTimeChange += OnTimeChange;
        Game.SequenceHandler.OnStartNewSeqeunce += OnStartNewSeqeunce;
    }

    private void OnDisable()
    {
        Game.TimeHandler.OnForceTimeReset -= OnForceTimeReset;
        Game.TimeHandler.OnTimeChange -= OnTimeChange;
        Game.SequenceHandler.OnStartNewSeqeunce += OnStartNewSeqeunce;
    }

    private void OnStartNewSeqeunce(Sequence newSeqence)
    {
        CreateNewTimeDots(newSeqence.TimeDots);
    }

    private void CreateNewTimeDots(List<int> timeDots)
    {
        if (dots != null && dots.Length > 0)
        {
            for (int i = 0; i < dots.Length; i++)
                dots[i].SetActive(timeDots.Contains(i));
        }
    }

    private void OnForceTimeReset(float newTime)
    {
        Debug.LogWarning("TIMEREDETdasd");
        targetTime = newTime;
        moves = true;
        cursorSpeed = resetCursorSpeed;
        Game.EffectHandler.Play(timeResetEffect, gameObject);
        if (hand.IsDragging)
            Game.MouseInteractor.ForceEndDrag();
    }

    private void OnTimeChange(float newTime)
    {
        targetTime = newTime;
        moves = true;
    }

    private void SetVisualsForTime(float blendedTime)
    {
        Vector3 hourEuler = hourHand.localRotation.eulerAngles;
        Quaternion hourRoration = Quaternion.Euler(hourEuler.x, ((blendedTime % 12f) / 12f) * 360f, hourEuler.z);
        hourHand.localRotation = hourRoration;

        Vector3 minuteEuler = minuteHand.localRotation.eulerAngles;
        Quaternion minuteRoration = Quaternion.Euler(minuteEuler.x, (blendedTime % 1f) * 360f, minuteEuler.z);
        minuteHand.localRotation = minuteRoration;
    }

    protected override void SetMouseRaycastable(bool raycastable)
    {
        base.SetMouseRaycastable(raycastable);
        hourHand.gameObject.layer = raycastable ? 0 : Physics.IgnoreRaycastLayer;
        face.gameObject.layer = raycastable ? 0 : Physics.IgnoreRaycastLayer;

    }

    internal void UpdateWatchTimeByHand(float targetTime)
    {
        this.targetTime = targetTime;
        moves = true;
    }

    private void Update()
    {
        if (moves)
        {
            time = Mathf.MoveTowardsAngle(time * 30f, targetTime * 30f, Time.deltaTime * cursorSpeed) / 30f;
            Game.TimeHandler.MoveHand(time);
            SetVisualsForTime(time);

            if (Mathf.Abs(time % 12 - targetTime) < 0.01f)
            {
                cursorSpeed = defaultCursorSpeed;
                moves = false;
            }
        }
    }
}
