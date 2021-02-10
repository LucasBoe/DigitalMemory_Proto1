using NaughtyAttributes.Test;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaylightTimeAdapter : MonoBehaviour
{
    [SerializeField] float lerpMultiplier = 1;

    [SerializeField] float dayLightAngleOffset = 80f;

    [SerializeField] Light mainLight;

    private Vector3 myEuler;
    private Quaternion targetRotation;
    private Quaternion currentRotation;

   


    private void OnEnable()
    {
        Game.TimeHandler.OnTimeChanged += OnTimeChanged;
    }

    private void OnDisable()
    {
        Game.TimeHandler.OnTimeChanged -= OnTimeChanged;
    }


    private void Start()
    {
        myEuler = gameObject.transform.localRotation.eulerAngles;
        OnTimeChanged(0);
    }

    private void Update()
    {
        currentRotation = gameObject.transform.localRotation;
        currentRotation = Quaternion.Lerp(currentRotation, targetRotation, Time.deltaTime * lerpMultiplier);
        gameObject.transform.localRotation = currentRotation;

        if (currentRotation.eulerAngles.x > 180 || currentRotation.eulerAngles.x < 0)
        {
            mainLight.enabled = false;
        }
        else
        {
            mainLight.enabled = true;
        }
    }


    private void OnTimeChanged(int currentTime)
    {
       targetRotation = Quaternion.Euler(((currentTime % 24f) / 24) * 360f + dayLightAngleOffset, myEuler.y, myEuler.z);
    }


}
 
