using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NonInteractable : MonoBehaviour, IClickable
{
    [Expandable] [SerializeField] Effect nonInteractable;

    public void Click()
    {
        Game.EffectHandler.Play(nonInteractable, gameObject);
        //StartCoroutine(Shake());

    }

    public bool IsClickable()
    {
        return true;
    }

}
