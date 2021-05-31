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
    [SerializeField] private SelectEnterEventArgs tmpArgs;
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private MeshRenderer meshRenderer;

    private Text myText;

    public Item currentItem;

    public int maxCount;
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
    /// ���Ͽ� ��� ��ü�� ������ �� �Լ��� ������������
    /// </summary>
    /// <param name="args"></param>
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        print("2");
        if (args.interactable.CompareTag(Constant.item))
        {
            tmpArgs = args;
            Item tmp = args.interactable.GetComponent<Item>();
            CurrentCount++;
            if (currentItem == null)
            {
                tmp.transform.SetParent(transform);
                currentItem = tmp;
            }
            else
            {
                //�������� or �̷���
                if (currentItem.GetType() == tmp.GetType())
                {
                    Destroy(tmp.gameObject);
                }
                else
                {
                    base.OnSelectEntered(args);
                }
                return ;
            }
        }
        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        print("1");
        CurrentCount -= 1;
        if (CurrentCount > 0)
        {
            Item item = Instantiate(currentItem, currentItem.transform.position, currentItem.transform.rotation);
            item.transform.SetParent(transform);
            item.RigiKinematic(true);
            //Default������Ʈ�� �߰�
        }
        else
        {
            currentItem = null;
            tmpArgs = null;
            base.OnSelectExited(args);
        }
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        if (tmpArgs != null)
        {
            OnSelectEntered(tmpArgs);
        }
        base.OnHoverEntered(args);
    }
}
