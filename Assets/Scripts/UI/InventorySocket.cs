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
                if (currentItem != null)                    //�κ��丮�� �������� �־����� �ְ�, �������� ������Ʈ��
                {
                    if (currentItem != checkItem)
                    {
                        if (currentItem.Name == checkItem.Name)
                        {
                            Destroy(args.interactable.gameObject);
                            CurrentCount++;
                        }
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

        if (currentItem == null)
        {
            currentItem = checkItem;
            CurrentCount++;
        }
        
        base.OnSelectEntered(args);
    }

    /// <summary>
    /// ���Կ� �������� ������
    /// </summary>
    /// <param name="args"></param>
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        CurrentCount--;
        if (CurrentCount > 0)
        {
            startingSelectedInteractable = Instantiate(ItemManager.instance.FineItem(currentItem), transform.position, transform.rotation);
            CurrentCount--;
            currentItem = null;
        }
        else
        {
            currentItem = null;
            base.OnSelectExited(args);
        }
    }

}
