using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellingNPC : NPC
{
    [Header("아이템 판매 정보")]
    [SerializeField] private List<SellingItem> sellingItems;
    [SerializeField] private string sellingDialogue;
    [SerializeField] private string needGoldDialogue;
    [SerializeField] private string buyDialogue;
    [SerializeField] private Button okButton;
    [SerializeField] private Button noButton;
    
    private Item sellingItem;
    private int sellingItemPrice;

    private void Start()
    {
        for (int i = 0; i < sellingItems.Count; i++)
        {
            sellingItems[i].sellingNPC += GetDialogue;
        }
    }

    public void GetDialogue(Item choiceItem, int price)
    {
        sellingItem = choiceItem;
        sellingItemPrice = price;
        npcUI.gameObject.SetActive(true);
        npcUI.ShowDialogue(transform, sellingItem + "은(는)" + price + "골드야." + sellingDialogue.ToString());

        ButtonCheck(true);
    }    

    public void BuyItemCheck()
    {
        if(Player.instance.Money > sellingItemPrice)
        {
            Instantiate(sellingItem);
        }
        else
        {
            npcUI.ShowDialogue(transform, needGoldDialogue.ToString());
        }
        ButtonCheck(false);
    }

    public void CancleItem()
    {
        npcUI.ShowDialogue(transform, defaultDialogue);
        npcUI.gameObject.SetActive(false);
    }

    public void ButtonCheck(bool isOn)
    {
        okButton.gameObject.SetActive(isOn);
        noButton.gameObject.SetActive(isOn);
    }
}
