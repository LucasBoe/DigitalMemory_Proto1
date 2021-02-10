using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NonInteractable : MonoBehaviour,IClickable
{
    [SerializeField] AudioClip nonInteractable;

    [SerializeField] float shakeSpeed = 30f, shakeAmount = 0.05f, shakeDuration = 0.5f;

    public void Click()
    {
        Game.SoundPlayer.Play(nonInteractable, gameObject);
        //StartCoroutine(Shake());

    }

    public bool IsClickable()
    {
        return true;
    }

    private IEnumerator Shake() {

        float elapsed = 0f;
        Vector3 myZ = gameObject.transform.position;

        while (elapsed < shakeDuration);
        {
            elapsed += Time.deltaTime;

            Vector3 myPosition = gameObject.transform.position;
            myPosition.z = myZ.z + Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;
            gameObject.transform.position = myPosition;

            yield return null;
        }

        gameObject.transform.position = myZ;

    }
}
