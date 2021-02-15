using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInteractor : MonoBehaviour
{
    [SerializeField] LayerMask ignoreRaycast;
    [SerializeField] Effect onHoverDragableEnter, onHoverDragableExit;

    GameObject currenHoverTEMP, currentDragHover;

    IDragable currentDrag;
    IAttachable currentAttachable;
    ICloseupable currentCloseupable;

    

    public bool IsInCloseup { get => (currentCloseupable != null); }
    public bool IsDragging { get => (currentDrag != null); }
    public bool IsDraggingAttachable { get => (currentAttachable != null); }

    private void Update()
    {
        if (IsInCloseup)
        {
            UpdateCloseup();
        }
        else
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100, ~ignoreRaycast))
            {
                currenHoverTEMP = hit.collider.gameObject;

                if (IsDragging)
                {
                    UpdateDrag(hit, ray);
                }
                else
                {
                    UpdateNonDrag(hit);
                }
            }
            else
            {
                currenHoverTEMP = null;
            }
        }
    }

    private void UpdateCloseup()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Game.CloseupHandler.EndCloseup(currentCloseupable);
            currentCloseupable = null;
        }
        else
        {
            Game.CloseupHandler.UpdateCloseup(currentCloseupable);
        }
    }

    private void UpdateNonDrag(RaycastHit hit)
    {
        IDragable dragable = hit.collider.GetComponent<IDragable>();
        IClickable clickable = hit.collider.GetComponent<IClickable>();
        IAttachable attachable = hit.collider.GetComponent<IAttachable>();

        //Hover
        GameObject newDragHover = null;
        if (dragable != null)
            newDragHover = hit.collider.gameObject;

        if (currentDragHover != newDragHover)
        {
            if (currentDragHover != null)
                Game.EffectHandler.Play(onHoverDragableExit, currentDragHover);

            if (newDragHover != null)
                Game.EffectHandler.Play(onHoverDragableEnter, newDragHover);

            currentDragHover = newDragHover;
        }

        //Mouse
        if (Input.GetMouseButtonDown(0))
        {

            if (dragable != null && dragable.IsDragable())
                StartDrag(dragable, attachable);
            else if (clickable != null && clickable.IsClickable())
                ClickOn(clickable);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            ICloseupable closeupable = hit.collider.GetComponent<ICloseupable>();
            if (closeupable != null)
            {
                currentCloseupable = closeupable;
                Game.CloseupHandler.StartCloseup(currentCloseupable);
            }
        }
    }

    private void UpdateDrag(RaycastHit hit, Ray ray)
    {
        IAttacher attacher = hit.collider.GetComponent<IAttacher>();

        //preview
        if (IsDraggingAttachable && attacher != null && attacher.CanAttach(currentAttachable.GetAttachment()))
        {
            currentDrag.UpdateDragPosition(hit.point, attacher.GetPreviewPosition(hit.point) + Game.Settings.AttachPreviewOffset * (3.5f + Mathf.Sin(Time.time * 2f) * 0.5f));
        }
        else
        {
            float dragDistance = Vector3.Distance(ray.origin, hit.point) - Game.Settings.DragDistanceToFloor;
            currentDrag.UpdateDragPosition(hit.point, ray.GetPoint(dragDistance));
        }

        //click
        if (Input.GetMouseButtonUp(0))
        {
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

        IAttacher attacher = (attachable == null ? null : attachable.GetCurrentAttached());
        if (attacher != null)
            attacher.OnDetach();

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

        attacher.OnAttach();
        attachable.Attach(attacher);
    }

    private void OnGUI()
    {
        if (currenHoverTEMP != null)
            GUILayout.Box("hover: " + currenHoverTEMP + "\n closeup: " + IsInCloseup);
        else
            GUILayout.Box("no hover.");
    }
}
