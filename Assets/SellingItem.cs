using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SellingItem : Item
{
    [SerializeField] private Item sellingItem;
    private ItemUI ui;
    [SerializeField] private SellingNPC sellingNPC;
    [SerializeField] private int sellingPrice;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        sellingNPC.GetDialogue(sellingItem, sellingPrice);
        base.OnSelectEntered(args);
    }

    public override bool IsSelectableBy(XRBaseInteractor interactor)
    {
        return base.IsSelectableBy(interactor);
    }
}
