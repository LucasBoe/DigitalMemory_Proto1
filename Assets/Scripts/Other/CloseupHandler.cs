using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CloseupHandler : Singleton<CloseupHandler>
{
    Vector3 originalPosition;
    Quaternion originalRotation;

    Vector3 targetPosition;
    Quaternion targetRotation;

    [SerializeField] Transform closeupTransform;

    internal void StartCloseup(ICloseupable currentCloseupable)
    {
        Debug.Log("start closeup");

        currentCloseupable.OnStartCloseup();
        originalPosition = currentCloseupable.GetPosition();
        originalRotation = currentCloseupable.GetRotation();
    }

    internal void UpdateCloseup(ICloseupable currentCloseupable)
    {
        Debug.Log("update closeup");

        targetPosition = closeupTransform.position;
        targetRotation = Quaternion.Euler(Input.mousePosition.y,Input.mousePosition.x,0);
        UpdatePositionAndRotation(currentCloseupable, targetPosition, targetRotation, Vector3.Distance(targetPosition, currentCloseupable.GetPosition()) > 0.01f);
    }

    internal void EndCloseup(ICloseupable currentCloseupable)
    {
        Debug.Log("end closeup");

        StartCoroutine(PanBackRoutine(currentCloseupable));
    }

    IEnumerator PanBackRoutine(ICloseupable closeupable)
    {
        Vector3 tPos = originalPosition;
        Quaternion tRot = originalRotation;

        while (Vector3.Distance(tPos, closeupable.GetPosition()) > 0.01f)
        {
            yield return null;
            UpdatePositionAndRotation(closeupable, tPos, tRot, lerp: true);
        }

        closeupable.UpdatePositionAndRotation(tPos,tRot);
        closeupable.OnEndCloseup();
    }

    private void UpdatePositionAndRotation(ICloseupable closeupable, Vector3 tPos, Quaternion tRot, bool lerp = false)
    {
        if (lerp)
        {
            Vector3 pos = Vector3.MoveTowards(closeupable.GetPosition(), tPos, Time.deltaTime * 100);
            Quaternion rot = Quaternion.Euler(Vector3.MoveTowards(closeupable.GetRotation().eulerAngles, tRot.eulerAngles, Time.deltaTime * 100));
            closeupable.UpdatePositionAndRotation(pos, rot);
        }
        else
        {
            closeupable.UpdatePositionAndRotation(tPos, tRot);
        }
    }
}
