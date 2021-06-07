using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SellingItem : Item
{
    private SellingNPC sellingNPC;

    [Header("ÆÇ¸Å")]
    [SerializeField] private int sellingPrice;
    [SerializeField] private Item sellingPrefab;

    public SellingNPC SellingNPC { get => sellingNPC; set => sellingNPC = value; }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        SellingNPC.GetDialogue(sellingPrefab, sellingPrice);
        base.OnSelectEntered(args);
    }

    public override bool IsSelectableBy(XRBaseInteractor interactor)
    {
        return base.IsSelectableBy(interactor);
    }
}
