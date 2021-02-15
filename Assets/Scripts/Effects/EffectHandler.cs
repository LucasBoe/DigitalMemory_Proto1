using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHandler : Singleton<EffectHandler>
{
    internal void Play(Effect attachEffect, GameObject gameObject)
    {
        if (attachEffect != null)
            attachEffect.Play(gameObject);
    }
}
