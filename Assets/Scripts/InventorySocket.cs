using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

/// <summary>
/// ù��° ����-> Socket�� ������Ʈ�� ������ OnSelectEntered�Լ��� �������� ����
/// </summary>

public class InventorySocket : XRSocketInteractor
{
    private Text myText;
    private Item checkItem;

    public Item currentItem;

    public int currentCount;
    public bool dividObject = false;

    public int CurrentCount
    {
        get
        {
            return currentCount;
        }
        set
        {
            currentCount = value;
            myText.text = currentCount.ToString();
        }
    }

    protected override void Awake()
    {
        myText = GetComponentInChildren<Text>();
        base.Awake();
    }

    protected override void Start()
    {
        CurrentCount = 0;
        base.Start();
    }

    /// <summary>
    /// ���Կ� �������� �ε�����
    /// </summary>
    /// <param name="args"></param>
    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        if (checkItem = args.interactable.GetComponent<Item>())             //������ ���� üũ��
        {
            if (checkItem.maxSlotCount >= currentCount)                     //�κ��丮�� �������� ���� �� �ִ� �ִ� ����
            {
                if (currentItem != null && !dividObject)                    //�κ��丮�� �������� �־����� �ְ�, �������� ������Ʈ��
                {
                    if (currentItem.GetType() == args.interactable.GetType())
                    {
                        Destroy(args.interactable.gameObject);
                        CurrentCount++;
                    }
                }
                else if (dividObject && currentItem != null && checkItem.makedItem == false)
                {
                    if (currentItem.GetType() == args.interactable.GetType())
                    {
                        Destroy(args.interactable.gameObject);
                        CurrentCount++;
                    }
                }
            }
        }
        base.OnHoverEntered(args);
    }

    /// <summary>
    /// ���Կ� �������� ������
    /// </summary>
    /// <param name="args"></param>
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        checkItem = args.interactable.GetComponent<Item>();
        if (checkItem.makedItem == false)
        {
            CurrentCount++;
            if (currentItem == null)
            {
                checkItem.transform.SetParent(transform);
                currentItem = checkItem;
            }
        }
        
        base.OnSelectEntered(args);
    }

    /// <summary>
    /// ���Կ� �������� ������
    /// </summary>
    /// <param name="args"></param>
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        args.interactable.GetComponent<Item>().makedItem = false;
        CurrentCount -= 1;
        if (CurrentCount > 0)
        {
            StartCoroutine(MakeObject());
        }
        else
        {
            currentItem = null;
            dividObject = false;
            base.OnSelectExited(args);
        }
    }

    IEnumerator MakeObject()
    {
        yield return new WaitForSeconds(0.2f);
        Item test = Instantiate(TestManager.instance.FineItem(currentItem));
        currentItem = test;
        dividObject = true;
        test.transform.SetParent(transform);
        test.transform.localPosition = Vector3.zero;
    }
}
