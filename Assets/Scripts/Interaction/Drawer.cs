using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour, IDragable
{
    bool isDragging = false;
    Vector3 startDragPosition, defaultPosition;
    [SerializeField] Vector3 dragAxis, drag, minDrag, maxDrag;

    private void Start ()
    {
        defaultPosition = transform.localPosition;
    }

    public void EndDrag(Vector3 position)
    {
        SetPulledOut(startDragPosition, position);
        isDragging = false;
    }

    private void SetPulledOut(Vector3 startDragPosition, Vector3 currentDragPosition)
    {
        Vector3 currentDrag = drag + (currentDragPosition.FilterByAxisVector(dragAxis) - startDragPosition.FilterByAxisVector(dragAxis));
        if (currentDrag.FilterByAxisVector(dragAxis).MinWithoutZero() >= minDrag.MinWithoutZero() && currentDrag.FilterByAxisVector(dragAxis).MaxWithoutZero() <= maxDrag.MaxWithoutZero())
            transform.localPosition = currentDrag;
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
        drag = transform.localPosition;
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
}
