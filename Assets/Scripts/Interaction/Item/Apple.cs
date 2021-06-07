using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Apple : Item
{
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        rigi.isKinematic = false;
        base.OnSelectEntered(args);
    }
}
