using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Fish : Item
{
    public Rod rod;
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        rod.IsGrab = false;
        base.OnSelectEntered(args);
    }
}
