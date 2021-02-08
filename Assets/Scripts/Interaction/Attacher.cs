using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttacher
{
    bool CanAttach(string attachBehaviour);
    Transform GetTransform();
    void OnAttach();
    void OnDetach();
    Vector3 GetPosition();
}

public class Attacher : MonoBehaviour, IAttacher
{
    public string attachmentName;
    [SerializeField] protected bool isAttached;
    public event System.Action<bool> OnChangeAttached;

    public bool CanAttach(string attachmentName)
    {
        return this.attachmentName == attachmentName;
    }

    public virtual Vector3 GetPosition()
    {
        return transform.position;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public virtual void OnAttach()
    {
        isAttached = true;
        OnChangeAttached?.Invoke(isAttached);
    }

    public virtual void OnDetach()
    {
        isAttached = false;
        OnChangeAttached?.Invoke(isAttached);
    }
}
