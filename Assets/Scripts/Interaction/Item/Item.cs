using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
public class Item : Introduce
{
    private Collider col;
    private Rigidbody rigi;

    private Transform parentTransform;
    private Vector3 spawnPoint;
    private bool makedItem = false;
    protected bool defaultSell = false;
    public bool MakedItem { get { return makedItem; } set { makedItem = value; } }

    [Header("������ ����")]
    public int maxSlotCount = 99;

    [Header("�����Ǵ� �������� ���")]
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
        print("aa");
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
