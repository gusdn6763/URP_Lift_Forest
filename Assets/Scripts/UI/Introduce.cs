using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class Introduce : XRGrabInteractable
{
    protected ItemUI ui;
    [SerializeField] private bool interaction = true;
    [Header("설명 정보")]
    [SerializeField] private Vector3 addsize;
    [SerializeField] private string obame;
    public string Name { get { return obame; } set { obame = value; } }
    [SerializeField] private int price;
    public int Price { get { return price; } set { price = value; } }
    [SerializeField] private string introudce;

    protected bool isOn = true;

    protected override void Awake()
    {
        base.Awake();
        ui = ItemManager.instance.itemUI;
    }


    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        if ((!ui.gameObject.activeSelf) && args.interactor.CompareTag(Constant.ray))
        {
            ui.TranceInfo(transform, addsize, obame, introudce, price.ToString(), isOn);
            ui.gameObject.SetActive(true);
        }
        // 부모 클래스 호출.
        base.OnHoverEntered(args);
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        ui.gameObject.SetActive(false);
        base.OnHoverExited(args);
    }

    public override bool IsSelectableBy(XRBaseInteractor interactor)
    {
        return base.IsSelectableBy(interactor) && interaction;
    }

}
