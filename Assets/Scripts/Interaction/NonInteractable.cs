using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonInteractable : MonoBehaviour,IClickable
{
    [SerializeField] AudioClip nonInteractable;


    public void Click()
    {
        Game.SoundPlayer.Play(nonInteractable, gameObject);
    }

    public bool IsClickable()
    {
        return true;
    }
}
