using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Rod : Item
{
    private Hook hook;
    private bool isActive = true;
    public bool IsActive { get => isActive; set => isActive = value; }

    protected override void Awake()
    {
        hook = GetComponentInChildren<Hook>();
        base.Awake();
    }

    public override bool IsSelectableBy(XRBaseInteractor interactor)
    {
        return base.IsSelectableBy(interactor) && IsActive;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        if (hook.Grabbing && !hook.Hole)
        {
            IsActive = false;
        }
        base.OnSelectExited(args);
        rigi.isKinematic = true;
    }
}
