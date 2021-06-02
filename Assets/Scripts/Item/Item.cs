using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
public class Item : XRGrabInteractable
{
    [SerializeField] private string price;
    [SerializeField] private string introudce;

    private ItemUI ui;

    protected bool defaultSell = false;
    public int maxSlotCount = 99;
    public bool makedItem = false;

    protected override void Awake()
    {
        base.Awake();
        ui = GetComponentInChildren<ItemUI>();
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        ui.UIOnOffCheck(introudce, price, true);
        base.OnHoverEntered(args);
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        ui.UIOnOffCheck("", "", false);
        base.OnHoverExited(args);
    }
}
