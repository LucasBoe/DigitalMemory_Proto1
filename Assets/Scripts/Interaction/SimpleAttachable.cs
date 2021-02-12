using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAttachable : SimpleDragable, IAttachable
{
    public string attachment;
    [SerializeField] private bool isAttached;
    [SerializeField] private Transform defaultParent;

    [SerializeField] protected AudioClip attachClip;

    public void Attach(IAttacher toAttachTo)
    {
        isBeeingDragged = false;
        isAttached = true;

        defaultParent = transform.parent;
        transform.parent = toAttachTo.GetTransform();

        if (toAttachTo.ResetPositionOnAttach())
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
            if (defaultParent != null)
                transform.parent = defaultParent;
            else
            {
                transform.parent = null;
                Debug.LogWarning("No default parent defined unparenting impossible, please define one or ignore this warning.");
            }
        }
    }
    public string GetAttachment()
    {
        return attachment;
    }

    public IAttacher GetCurrentAttached()
    {
        if (isAttached)
            return GetComponentInParent<IAttacher>();

        return null;
    }
}
