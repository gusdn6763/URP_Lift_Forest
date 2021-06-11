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
    /// 슬롯에 아이템을 부딪힐시
    /// </summary>
    /// <param name="args"></param>
    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {

        if (checkItem = args.interactable.GetComponent<Item>())             //만약의 에러 체크용
        {
            if (checkItem.maxSlotCount >= currentCount)                     //인벤토리에 아이템을 넣을 수 있는 최대 갯수
            {
                if (currentItem != null)                    //인벤토리에 아이템이 넣어진게 있고, 나누어진 오브젝트면
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
    /// 슬롯에 아이템을 넣을시
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
    /// 슬롯에 아이템을 빼낼시
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
