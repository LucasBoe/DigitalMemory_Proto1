using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour, IDragable
{
    bool isDragging = false;
    Vector3 startDragPosition;
    [SerializeField] float drawerSpeedMultiplier;
    [SerializeField] Vector3 dragAxis, position, current, target, minDrag, maxDrag;

    public void EndDrag(Vector3 position)
    {
        SetPulledOut(startDragPosition, position);
        target = Vector3.zero;
        isDragging = false;
    }

    private void SetPulledOut(Vector3 startDragPosition, Vector3 currentDragPosition)
    {
        Vector3 currentDrag = (currentDragPosition.FilterByAxisVector(dragAxis) - startDragPosition.FilterByAxisVector(dragAxis));
        current = (position + currentDrag).FilterByAxisVector(dragAxis);
        if (current.FilterByAxis(dragAxis) <= minDrag.FilterByAxis(dragAxis) && current.FilterByAxis(dragAxis) >= maxDrag.FilterByAxis(dragAxis))
            target = position + currentDrag;
    }

    public float GetEndDragYOffset()
    {
        return 0;
    }

    public bool IsDragable()
    {
        return !isDragging;
    }

    public void StartDrag()
    {
        isDragging = true;
        position = transform.localPosition;
        startDragPosition = Vector3.zero;
    }

    public void UpdateDragPosition(Vector3 hitpoint, Vector3 position)
    {
        if (startDragPosition == Vector3.zero)
            startDragPosition = hitpoint;
        else
        {
            SetPulledOut(startDragPosition, hitpoint);
        }
    }

    void FixedUpdate()
    {
        if (target != Vector3.zero)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, drawerSpeedMultiplier);
        }
    }
}
