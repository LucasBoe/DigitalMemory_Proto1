using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAttachable : SimpleDragable, IAttachable
{
    public string attachment;
    Transform defaultParent;
    bool isAttached;

    public void EndDrag(Transform toAttachTo)
    {
        isBeeingDragged = false;

        defaultParent = transform.parent;
        transform.parent = toAttachTo;
        transform.localPosition = Vector3.zero;

        gameObject.layer = 0;
    }

    public override void StartDrag()
    {
        base.StartDrag();

        if (isAttached)
        {
            isAttached = false;
            transform.parent = defaultParent;
        }
    }
    public string GetAttachment()
    {
        return attachment;
    }
}
