using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICloseupable
{
    Vector3 GetPosition();
    Quaternion GetRotation();
    void UpdatePositionAndRotation(Vector3 pos, Quaternion rot);
    void OnStartCloseup();
    void OnEndCloseup();
}


[SelectionBase]
public class SimpleAttachable : SimpleDragable, IAttachable, ICloseupable
{
    public string attachment;
    private bool isInCloseup;
    [SerializeField] private bool isAttached;
    [SerializeField] private Transform defaultParent;

    [Expandable]
    [SerializeField] protected Effect attachEffect, detachEffect;

    public void Attach(IAttacher toAttachTo)
    {
        isBeeingDragged = false;
        isAttached = true;

        defaultParent = transform.parent;
        transform.parent = toAttachTo.GetTransform();

        if (toAttachTo.ResetPositionOnAttach())
            transform.localPosition = toAttachTo.GetAttachOffset();
        else
            transform.localPosition += toAttachTo.GetAttachOffset();

        if (toAttachTo.ResetOrientationOnAttach())
            transform.localRotation = Quaternion.identity;

        gameObject.layer = 0;

        Game.EffectHandler.Play(attachEffect, gameObject);
    }

    public override void StartDrag()
    {
        base.StartDrag();

        if (isAttached)
        {
            Game.EffectHandler.Play(detachEffect, gameObject);

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

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public Quaternion GetRotation()
    {
        return transform.rotation;
    }

    public void UpdatePositionAndRotation(Vector3 pos, Quaternion rot)
    {
        transform.position = pos;
        transform.rotation = rot;
    }

    public void OnStartCloseup()
    {
        isInCloseup = true;
        SetPhysicsActive(false);
        SetMouseRaycastable(false);
    }

    public void OnEndCloseup()
    {
        isInCloseup = false;
        SetMouseRaycastable(true);
        if (!isAttached)
            SetPhysicsActive(true);
    }

    public override bool IsDragable()
    {
        return base.IsDragable() && !isInCloseup;
    }
}
