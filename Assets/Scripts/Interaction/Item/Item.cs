using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
public class Item : XRGrabInteractable
{
    [Header("아이템 정보")]
    public string itemName;
    [SerializeField] private string price;
    [SerializeField] private string introudce;

    private ItemUI ui;
    private Collider col;
    private Rigidbody rigi;

    [Header("인벤토리에 들어가기위한 제한갯수")]
    protected bool defaultSell = false;
    public int maxSlotCount = 99;
    public bool makedItem = false;

    [Header("아이템 재스폰->사과 또는 NPC의 판매 아이템")]
    private Transform parentTransform;
    private Vector3 spawnPoint;
    public bool spawnItem = false;
    public float spawnTime = 5f;

    protected override void Awake()
    {
        if(spawnItem)
        {
            parentTransform = GetComponentInParent<Transform>();
            spawnPoint = GetComponent<Transform>().position;
        }
        rigi = GetComponent<Rigidbody>();
        ui = ItemManager.instance.itemUI;
        col = GetComponent<Collider>();
        base.Awake();
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
        rigi.isKinematic = false;
        if (spawnItem)
        {
            StartCoroutine(SpawnItem());
        }
        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        col.isTrigger = false;
        base.OnSelectExited(args);
    }

    protected IEnumerator SpawnItem()
    {
        yield return new WaitForSeconds(spawnTime);
        Instantiate(ItemManager.instance.FineItem(this), spawnPoint, Quaternion.identity, parentTransform);
    }
}
