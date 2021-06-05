using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class Introduce : XRGrabInteractable
{
    protected ItemUI ui;

    [Header("설명 정보")]
    [SerializeField] private string obame;
    [SerializeField] private string price;
    [SerializeField] private string introudce;

    protected override void Awake()
    {
        base.Awake();
        ui = GetComponentInChildren<ItemUI>();
    }


    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        if (!(ui.gameObject.activeSelf))
        {
            ui.TranceInfo(transform, obame, introudce, price);
            ui.gameObject.SetActive(true);
        }
        base.OnHoverEntered(args);
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        ui.gameObject.SetActive(false);
        base.OnHoverExited(args);
    }

}
