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
    [SerializeField] private bool spawnItem = false;
    [SerializeField] private float spawnTime = 5f;

    protected override void Awake()
    {
        rigi = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        GetInfo();
        base.Awake();
        isOn = true;
    }

    public void GetInfo()
    {
        if (spawnItem)
        {
            parentTransform = transform.parent.GetComponent<Transform>();
            spawnPoint = transform.position;
            GetComponent<MeshRenderer>().enabled = false;
            col.enabled = false;
            StartCoroutine(Active());
        }
    }

    IEnumerator Active()
    {
        yield return new WaitForSeconds(spawnTime);
        GetComponent<MeshRenderer>().enabled = true;
        col.enabled = true;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        col.isTrigger = true;

        if (spawnItem && parentTransform.CompareTag(Constant.tree))
        {
            Item tmp =  Instantiate(ItemManager.instance.FineItem(this), spawnPoint, Quaternion.identity, parentTransform);
            tmp.spawnItem = true;
            tmp.GetInfo();
            transform.SetParent(null);
            tmp.transform.localScale += transform.localScale;
            spawnItem = false;
        }

        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        col.isTrigger = false;
        rigi.isKinematic = false;
        base.OnSelectExited(args);
    }

    public override bool IsSelectableBy(XRBaseInteractor interactor)
    {
        if (!rayActive)
        {
            return base.IsSelectableBy(interactor) && (interactor.CompareTag(Constant.hand) || interactor.CompareTag(Constant.inventory) || interactor.CompareTag(Constant.dirt));
        }
        else
        {
            return base.IsSelectableBy(interactor);
        }
    }
}
