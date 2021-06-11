using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
public class Item : Introduce
{
    private Collider col;
    protected Rigidbody rigi;

    private Transform parentTransform;
    private Vector3 spawnPoint;
    protected bool defaultSell = false;
    protected bool rayActive = false;

    [Header("아이템 정보")]
    public int maxSlotCount = 99;

    [Header("스폰되는 아이템일 경우")]
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
        col = GetComponent<Collider>();
        base.Awake();
        isOn = true;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        col.isTrigger = true;
        if (spawnItem)
        {
            StartCoroutine(SpawnItem());
        }

        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        col.isTrigger = false;
        rigi.isKinematic = false;
        base.OnSelectExited(args);
    }

    protected IEnumerator SpawnItem()
    {
        yield return new WaitForSeconds(spawnTime);                         
        Instantiate(ItemManager.instance.FineItem(this), spawnPoint, Quaternion.identity, parentTransform.parent);
    }
    public override bool IsSelectableBy(XRBaseInteractor interactor)
    {
        if (!rayActive)
        {
            return base.IsSelectableBy(interactor) && (interactor.CompareTag(Constant.hand) || interactor.CompareTag(Constant.inventory));
        }
        else
        {
            return base.IsSelectableBy(interactor);
        }
    }
}
