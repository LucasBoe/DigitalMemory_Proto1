using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class HiddenAttachable : SimpleAttachable
{
    ICloseupable hiddenIn;
    [SerializeField] bool isHidden;

    private void OnEnable()
    {
        if (isHidden)
        {
            hiddenIn = transform.parent.GetComponentInParent<ICloseupable>();
            hiddenIn.OnStartCloseupEvent += OnParentStartCloseup;
            hiddenIn.OnEndCloseupEvent += OnParentEndCloseup;
        }
    }

    private void OnDisable()
    {
        if (isHidden)
        {
            hiddenIn.OnStartCloseupEvent -= OnParentStartCloseup;
            hiddenIn.OnEndCloseupEvent -= OnParentEndCloseup;
        }
    }

    private void Start()
    {
        if (isHidden)
        {
            SetPhysicsActive(false);
            SetMouseRaycastable(false);
        }
    }

    public override void OnStartCloseup()
    {
        OnDisable();
        isHidden = false;
        base.OnStartCloseup();
    }

    public override void OnEndCloseup()
    {
        base.OnEndCloseup();
        Destroy(gameObject);
    }

    void OnParentStartCloseup()
    {
        SetMouseRaycastable(true);
    }

    void OnParentEndCloseup()
    {
        if (isHidden)
        {
            SetMouseRaycastable(false);
        }
    }
}
