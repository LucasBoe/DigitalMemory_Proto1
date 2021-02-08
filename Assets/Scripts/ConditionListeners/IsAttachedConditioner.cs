using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Attacher))]
public class IsAttachedConditioner : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] string variableName;
    Attacher attacher;
    private void OnEnable()
    {
        attacher = GetComponent<Attacher>();

        if (attacher != null)
        {
            attacher.OnChangeAttached += ChangedAttached;
        }
        else
        {
            Debug.LogWarning("No Attacher found.");
            Destroy(this);
        }
    }

    private void OnDisable()
    {
        if (attacher != null)
        {
            attacher.OnChangeAttached -= ChangedAttached;
        }
        else
        {
            Debug.LogWarning("No Attacher found.");
            Destroy(this);
        }
    }

    void ChangedAttached(bool isAttached)
    {
        if (animator != null)
            animator.SetBool(variableName, isAttached);
    }
}
