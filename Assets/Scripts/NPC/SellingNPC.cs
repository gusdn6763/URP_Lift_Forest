using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellingNPC : NPC
{
    [Header("아이템 판매 정보")]
    [SerializeField] private List<SellingItem> sellingItems;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private string sellingDialogue;
    [SerializeField] private string needGoldDialogue;
    [SerializeField] private string buyDialogue;
    
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
        npcUI.ButtonOnOff(true);
        npcUI.ChangeButtonName("구입", "취소");

        npcUI.SetOnClickAction(() =>
        {
            if (Player.instance.Money > sellingItemPrice)
            {
                Instantiate(sellingItem, spawnPoint.position, spawnPoint.rotation);
                Player.instance.Money -= sellingItemPrice;
                npcUI.ShowDialogue(transform, buyDialogue);
            }
            else
            {
                
                npcUI.ShowDialogue(transform, needGoldDialogue.ToString());
            }

            npcUI.ButtonOnOff(false);
        });

        npcUI.SetNoClickAction(() =>
        {
            npcUI.ShowDialogue(transform, defaultDialogue);
            npcUI.gameObject.SetActive(false);
        });
    }    





}
