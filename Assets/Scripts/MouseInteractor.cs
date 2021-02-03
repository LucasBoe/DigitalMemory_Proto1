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
                UpdateDrag(currentDrag, ray.GetPoint(dragDistance));
                if (Input.GetMouseButtonUp(0))
                {
                    IAttacher attacher = hit.collider.GetComponent<IAttacher>();

                    if (IsDraggingAttachable && attacher != null && attacher.CanAttach(currentAttachable.GetAttachment()))
                    {
                        EndDrag(currentAttachable, attacher);
                    }
                    else
                    {
                        EndDrag(currentDrag, hit.point);
                    }
                }
            }
            else
            {
                IDragable dragable = hit.collider.GetComponent<IDragable>();
                IClickable clickable = hit.collider.GetComponent<IClickable>();
                SimpleAttachable attachable = hit.collider.GetComponent<SimpleAttachable>();

                if (Input.GetMouseButtonDown(0))
                {
                    if (dragable != null && dragable.IsDragable())
                    {
                        StartDrag(dragable, attachable);
                    }
                    else if (clickable != null && clickable.IsClickable())
                        ClickOn(clickable);
                }
            }
        }
    }

    private void ClickOn(IClickable clickable)
    {
        clickable.Click();
    }



    private void UpdateDrag(IDragable dragable, Vector3 position)
    {
        dragable.UpdateDragPosition(position);
    }

    private void StartDrag(IDragable dragable, SimpleAttachable attachable)
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

    private void EndDrag(IAttachable attachable, IAttacher attacher)
    {
        currentAttachable = null;
        currentDrag = null;
        attachable.EndDrag(attacher.GetTransform());
    }
}
