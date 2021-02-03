using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInteractor : MonoBehaviour
{
    [SerializeField] float dragDistance;
    [SerializeField] LayerMask ignoreRaycast;

    IDragable currentDrag;

    public bool IsDragging { get => (currentDrag != null); }

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
                    EndDrag(currentDrag, hit.point);
            }
            else
            {
                IDragable dragable = hit.collider.GetComponent<IDragable>();
                IClickable clickable = hit.collider.GetComponent<IClickable>();

                if (Input.GetMouseButtonDown(0))
                {
                    if (dragable != null && dragable.IsDragable())
                        StartDrag(dragable);
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

    private void StartDrag(IDragable dragable)
    {
        currentDrag = dragable;
        dragable.StartDrag();
    }
    private void EndDrag(IDragable dragable, Vector3 point)
    {
        currentDrag = null;
        dragable.EndDrag(point);
    }
 }
