using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
public class Item : XRGrabInteractable
{
    [Header("아이템 정보")]
    [SerializeField] private string price;
    [SerializeField] private string introudce;

    private ItemUI ui;
    public Collider col;

    protected bool defaultSell = false;
    public int maxSlotCount = 99;
    public bool makedItem = false;

    protected override void Awake()
    {
        col = GetComponent<Collider>();
        base.Awake();

    }

    private void Start()
    {
        ui = ItemManager.instance.setItemUI();
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        if(!(ui.gameObject.activeSelf))
        {
            ui.TranceInfo(transform, introudce, price);
            ui.gameObject.SetActive(true);
        }
        base.OnHoverEntered(args);
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        ui.gameObject.SetActive(false);
        base.OnHoverExited(args);
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        col.isTrigger = true;
        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        col.isTrigger = false;
        base.OnSelectExited(args);
    }
}
