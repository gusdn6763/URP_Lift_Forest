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
    private bool makedItem = false;
    protected bool defaultSell = false;
    public bool MakedItem { get { return makedItem; } set { makedItem = value; } }

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
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        col.isTrigger = true;
        rigi.isKinematic = false;
        if (spawnItem)
        {
            StartCoroutine(SpawnItem());
        }
        if (args.interactor.CompareTag(Constant.handRight) || args.interactor.CompareTag(Constant.handLeft))
        {
            Player.instance.GrabItem = this;
        }
        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        col.isTrigger = false;
        rigi.isKinematic = false;
        if ((args.interactor.CompareTag(Constant.handRight) || args.interactor.CompareTag(Constant.handLeft)))
        {
            Player.instance.GrabItem = null;
        }
        base.OnSelectExited(args);
    }

    protected IEnumerator SpawnItem()
    {
        // spawnTime = 5f
        yield return new WaitForSeconds(spawnTime);                         
        Instantiate(ItemManager.instance.FineItem(this), spawnPoint, Quaternion.identity, parentTransform);
    }
}
