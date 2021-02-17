using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AttachableWatchHand : MonoBehaviour, IDragable
{
    [SerializeField] AttachableWatch watch;
    float targetTime;

    bool moves = false;
    bool isDragging = false;

    public bool IsDragging => isDragging;

    public void EndDrag(Vector3 position)
    {
        isDragging = false;
    }

    public float GetEndDragYOffset()
    {
        return 0f;
    }

    public bool IsDragable()
    {
        return true;
    }

    public void StartDrag()
    {
        isDragging = true;
    }

    public void UpdateDragPosition(Vector3 point, Vector3 vector3)
    {
        Vector2 head = transform.position.To2D();
        Vector2 target = point.To2D();

        float angle = Mathf.Atan2((target.x - head.x) / 2, target.y - head.y) * Mathf.Rad2Deg; //-180 => 180
        targetTime = ((angle + 90f) / 30f + 12f) % 12f; //0 => 1;

        watch.UpdateWatchTimeByHand(targetTime);
    }
}