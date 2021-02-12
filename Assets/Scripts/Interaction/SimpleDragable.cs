using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDragable
{
    void StartDrag();
    void EndDrag(Vector3 position);
    float GetEndDragYOffset();
    bool IsDragable();
    void UpdateDragPosition(Vector3 point, Vector3 vector3);
}

public interface IAttachable : IDragable
{
    void Attach(IAttacher toAttachTo);
    string GetAttachment();
    IAttacher GetCurrentAttached();
}

public class SimpleDragable : MonoBehaviour, IDragable
{
    [SerializeField] protected Rigidbody rigidbody;
    [SerializeField] private float YOffsetOnDrop;
    [SerializeField] protected AudioClip startDragClip, endDragClip;


    protected bool isBeeingDragged = false;

    public virtual bool IsDragable()
    {
        return !isBeeingDragged;
    }

    public void EndDrag(Vector3 position)
    {
        isBeeingDragged = false;
        transform.position = position;
        transform.rotation = Quaternion.Euler(Quaternion.identity.eulerAngles + new Vector3(9,9));
        rigidbody.constraints = RigidbodyConstraints.FreezeRotationY;
        gameObject.layer = 0;

        Game.SoundPlayer.Play(endDragClip, gameObject);
    }

    public virtual void StartDrag()
    {
        isBeeingDragged = true;
        gameObject.layer = Physics.IgnoreRaycastLayer;
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        transform.rotation = Quaternion.identity;

        Game.SoundPlayer.Play(startDragClip, gameObject);
    }

    public void UpdateDragPosition(Vector3 hitpoint, Vector3 position)
    {
        transform.position = position;
    }

    public float GetEndDragYOffset()
    {
        return YOffsetOnDrop;
    }
}
