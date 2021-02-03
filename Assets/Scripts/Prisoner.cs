using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDragable
{
    void UpdateDragPosition(Vector3 vector3);
     
    void StartDrag();
    void EndDrag(Vector3 position);
    bool IsDragable();
}

public class Prisoner : MonoBehaviour, IDragable
{
    bool isBeeingDragged = false;

    public bool IsDragable()
    {
        return !isBeeingDragged;
    }

    public void EndDrag(Vector3 position)
    {
        isBeeingDragged = false;
        transform.position = position;
        gameObject.layer = 0;
    }   

    public void StartDrag()
    {
        isBeeingDragged = true;
        gameObject.layer = Physics.IgnoreRaycastLayer;
    }

    public void UpdateDragPosition(Vector3 position)
    {
        transform.position = position;
    }
}
