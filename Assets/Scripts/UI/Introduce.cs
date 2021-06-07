using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class Introduce : XRGrabInteractable
{
    protected ItemUI ui;

    [Header("���� ����")]
    [SerializeField] private string obame;
    public string Name { get { return obame; } set { obame = value; } }
    [SerializeField] private int price;
    public int Price { get { return price; } set { price = value; } }
    [SerializeField] private string introudce;

    protected override void Awake()
    {
        base.Awake();
        ui = ItemManager.instance.itemUI;
    }


    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        if ((!ui.gameObject.activeSelf) && args.interactor.CompareTag(Constant.ray))
        {
            ui.TranceInfo(transform, obame, introudce, price.ToString());
            ui.gameObject.SetActive(true);
        }
        // �θ� Ŭ���� ȣ��.
        base.OnHoverEntered(args);
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        ui.gameObject.SetActive(false);
        base.OnHoverExited(args);
    }

}
