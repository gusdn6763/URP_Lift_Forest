using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SellingItem : Item
{
    public delegate void npcTell(Item itemInfo, int price);
    public npcTell sellingNPC;

    [Header("ÆÇ¸Å")]
    [SerializeField] private int sellingPrice;
    [SerializeField] private Item sellingPrefab;


    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        sellingNPC(sellingPrefab, sellingPrice);
        base.OnSelectEntered(args);
    }

    public override bool IsSelectableBy(XRBaseInteractor interactor)
    {
        return base.IsSelectableBy(interactor);
    }
}
