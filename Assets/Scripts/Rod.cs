using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Rod : Item
{
    private bool isGrab;

    public bool IsGrab { get => isGrab; set => isGrab = value; }

    public override bool IsSelectableBy(XRBaseInteractor interactor)
    {
        return base.IsSelectableBy(interactor) && !IsGrab;
    }
}
