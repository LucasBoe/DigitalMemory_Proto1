using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDragable
{
    void UpdateDragPosition(Vector3 vector3);
     
    void StartDrag();
    void EndDrag(Vector3 position);
    void EndDrag(Transform toAttachTo);
    float GetEndDragYOffset();
    bool IsDragable();
}

public class Prisoner : MonoBehaviour, IDragable
{
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] float YOffsetOnDrop;

    Transform defaultParent;
    bool isBeeingDragged = false;
    bool isAttached;

    public bool IsDragable()
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
    }
    public void EndDrag(Transform toAttachTo)
    {
        isBeeingDragged = false;

        defaultParent = transform.parent;
        transform.parent = toAttachTo;
        transform.localPosition = Vector3.zero;

        gameObject.layer = 0;
    }

    public void StartDrag()
    {
        isBeeingDragged = true;
        gameObject.layer = Physics.IgnoreRaycastLayer;
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        transform.rotation = Quaternion.identity;

        if (isAttached)
        {
            isAttached = false;
            transform.parent = defaultParent;
        }
    }

    public void UpdateDragPosition(Vector3 position)
    {
        transform.position = position;
    }

    public float GetEndDragYOffset()
    {
        return YOffsetOnDrop;
    }
}
