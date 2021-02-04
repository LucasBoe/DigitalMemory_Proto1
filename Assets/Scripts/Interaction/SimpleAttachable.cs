using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAttachable : SimpleDragable, IAttachable
{
    public string attachment;
    Transform defaultParent;
    bool isAttached;

    [SerializeField] protected AudioClip attachClip;

    public void Attach(Transform toAttachTo)
    {
        isBeeingDragged = false;
        isAttached = true;

        defaultParent = transform.parent;
        transform.parent = toAttachTo;
        transform.localPosition = Vector3.zero;

        gameObject.layer = 0;

        Game.SoundPlayer.Play(attachClip, gameObject);
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
