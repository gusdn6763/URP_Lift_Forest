using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SellingItem : Item
{
    [Header("ÆÇ¸Å")]
    [SerializeField] private int sellingPrice;
    public delegate void npcTell(Item itemInfo, int price);

    public npcTell sellingNPC;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        sellingNPC(this, sellingPrice);
        base.OnSelectEntered(args);
    }

    public override bool IsSelectableBy(XRBaseInteractor interactor)
    {
        return base.IsSelectableBy(interactor);
    }
}
