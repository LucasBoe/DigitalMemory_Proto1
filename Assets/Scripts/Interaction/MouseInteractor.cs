using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInteractor : MonoBehaviour
{
    [SerializeField] float dragDistance;
    [SerializeField] LayerMask ignoreRaycast;

    IDragable currentDrag;
    IAttachable currentAttachable;

    public bool IsDragging { get => (currentDrag != null); }
    public bool IsDraggingAttachable { get => (currentAttachable != null); }

    private void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100, ~ignoreRaycast))
        {
            if (IsDragging)
            {
                UpdateDrag(hit, ray);
            }
            else
            {
                UpdateNonDrag(hit);
            }
        }
    }

    private void UpdateNonDrag(RaycastHit hit)
    {
        if (Input.GetMouseButtonDown(0))
        {
            IDragable dragable = hit.collider.GetComponent<IDragable>();
            IClickable clickable = hit.collider.GetComponent<IClickable>();
            IAttachable attachable = hit.collider.GetComponent<IAttachable>();

            if (dragable != null && dragable.IsDragable())
                StartDrag(dragable, attachable);
            else if (clickable != null && clickable.IsClickable())
                ClickOn(clickable);
        }
    }

    private void UpdateDrag(RaycastHit hit, Ray ray)
    {
        currentDrag.UpdateDragPosition(ray.GetPoint(dragDistance));

        if (Input.GetMouseButtonUp(0))
        {
            IAttacher attacher = hit.collider.GetComponent<IAttacher>();

            if (IsDraggingAttachable && attacher != null && attacher.CanAttach(currentAttachable.GetAttachment()))
                Attach(currentAttachable, attacher);
            else
                EndDrag(currentDrag, hit.point);
        }
    }

    private void ClickOn(IClickable clickable)
    {
        clickable.Click();
    }

    private void StartDrag(IDragable dragable, IAttachable attachable)
    {
        currentDrag = dragable;
        currentAttachable = attachable;
        dragable.StartDrag();
    }

    private void EndDrag(IDragable dragable, Vector3 point)
    {
        currentAttachable = null;
        currentDrag = null;
        dragable.EndDrag(point + dragable.GetEndDragYOffset() * Vector3.up);
    }

    private void Attach(IAttachable attachable, IAttacher attacher)
    {
        currentAttachable = null;
        currentDrag = null;
        attachable.Attach(attacher.GetTransform());
    }
}
