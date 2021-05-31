using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

/// <summary>
/// 첫번째 질문-> Socket에 오브젝트를 넣을시 OnSelectEntered함수를 실행하지 않음
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
    /// 소켓에 어떠한 물체가 있으면 이 함수를 실행하지않음
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
                //열거형식 or 이런식
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
            //Default오브젝트를 추가
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
