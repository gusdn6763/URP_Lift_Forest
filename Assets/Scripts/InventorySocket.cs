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


    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        if (currentItem != null && !dividObject)
        {
            if (currentItem.GetType() == args.interactable.GetType())
            {
                Destroy(args.interactable.gameObject);
                CurrentCount++;
            }
        }
        else if(dividObject && currentItem != null && args.interactable.GetComponent<Item>().makedItem == false)
        {
            if (currentItem.GetType() == args.interactable.GetType())
            {
                Destroy(args.interactable.gameObject);
                CurrentCount++;
            }
        }
        base.OnHoverEntered(args);
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactable.CompareTag(Constant.item))
        {
            Item tmp = args.interactable.GetComponent<Item>();
            if (tmp.makedItem == false)
            {
                CurrentCount++;
                if (currentItem == null)
                {
                    tmp.transform.SetParent(transform);
                    currentItem = tmp;
                }
            }
        }
        base.OnSelectEntered(args);
    }

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
        yield return new WaitForSeconds(0.5f);
        Item test = Instantiate(TestManager.instance.FineItem(currentItem));
        currentItem = test;
        dividObject = true;
        test.transform.SetParent(transform);
        test.transform.localPosition = Vector3.zero;
    }
}
