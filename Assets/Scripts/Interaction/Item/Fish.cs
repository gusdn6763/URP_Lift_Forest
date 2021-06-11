using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Fish : Item
{
    public Action OnAction;

    private bool oneClick = true;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (oneClick)
        {
            oneClick = false;
            if(OnAction != null)
            {
                OnAction();
                OnAction = null;
            }
            transform.SetParent(null);
        }
        base.OnSelectEntered(args);
    }
}
