using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInteractor : Singleton<MouseInteractor>
{
    [SerializeField] LayerMask ignoreRaycast;
    [SerializeField] Effect onHoverDragableEnter, onHoverDragableExit;

    GameObject currenHoverTEMP;

    IHoverable currentHover;
    IDragable currentDrag;
    IAttachable currentAttachable;
    ICloseupable currentCloseupable;



    public bool IsInCloseup { get => (currentCloseupable != null); }
    public bool IsDragging { get => (currentDrag != null); }
    public bool IsDraggingAttachable { get => (currentAttachable != null); }

    private void Update()
    {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100, ~ignoreRaycast))
        {
            currenHoverTEMP = hit.collider.gameObject;

            if (IsInCloseup)
            {
                UpdateCloseup(hit);
            }
            else
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
        else
        {
            currenHoverTEMP = null;
        }
    }

    private void UpdateCloseup(RaycastHit hit)
    {
        if (Input.GetMouseButtonDown(1))
        {
            Game.CloseupHandler.EndCloseup(currentCloseupable);
            currentCloseupable = null;
        }
        else
        {
            Game.CloseupHandler.UpdateCloseup(currentCloseupable);

            if (Input.GetMouseButtonDown(0))
            {
                HiddenAttachable hiddenAttachable = hit.collider.GetComponent<HiddenAttachable>();
                if (hiddenAttachable != null)
                {
                    Game.CloseupHandler.EndCloseup(currentCloseupable);
                    currentCloseupable = hiddenAttachable;
                    Game.CloseupHandler.StartCloseup(hiddenAttachable);
                }
            }
        }
    }

    private void UpdateNonDrag(RaycastHit hit)
    {
        IDragable dragable = hit.collider.GetComponent<IDragable>();
        IClickable clickable = hit.collider.GetComponent<IClickable>();
        IAttachable attachable = hit.collider.GetComponent<IAttachable>();

        //Hover
        IHoverable newDragHover = null;
        if (dragable != null)
            newDragHover = hit.collider.GetComponent<IHoverable>();

        if (currentHover != newDragHover)
        {
            if (currentHover != null)
            {
                Game.EffectHandler.Play(onHoverDragableExit, currentHover.GetGameObject());
                currentHover.EndHover();
            }

            if (newDragHover != null)
            {
                Game.EffectHandler.Play(onHoverDragableEnter, newDragHover.GetGameObject());
                newDragHover.StartHover();
            }

            currentHover = newDragHover;
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

    public void ForceEndDrag()
    {
        EndDrag(currentDrag, Vector3.zero);
    }

    public void ForceEndHover()
    {
        if (currentHover != null)
        {
            Game.EffectHandler.Play(onHoverDragableExit, currentHover.GetGameObject());
            currentHover.EndHover();
            currentHover = null;
        }
    }

    private void Attach(IAttachable attachable, IAttacher attacher)
    {
        currentAttachable = null;
        currentDrag = null;

        attacher.OnAttach(attachable);
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
