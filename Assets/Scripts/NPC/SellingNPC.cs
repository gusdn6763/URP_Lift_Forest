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
    [SerializeField] private string SellingItem;
    
    private Item sellingItem;
    private int sellingItemPrice;

    private void Start()
    {
        for (int i = 0; i < sellingItems.Count; i++)
        {
            sellingItems[i].SellingNPC = this;
        }
    }

    public void GetDialogue(Item choiceItem, int price)
    {
        print(Vector3.Distance(transform.position, Player.instance.transform.position));
        if (Vector3.Distance(transform.position, Player.instance.transform.position) < interactiveRange)
        {
            sellingItem = choiceItem;
            sellingItemPrice = price;

            npcUI.gameObject.SetActive(true);
            npcUI.ButtonOnOff(true);

            npcUI.ChangeButtonName("구입", "취소");
            npcUI.ShowDialogue(this, sellingItem.Name + "은(는)" + price + "골드야." + sellingDialogue.ToString(), defaultDialogueTime);
            npcUI.SetOnClickAction(() =>
            {
                if (Player.instance.Money >= sellingItemPrice)
                {
                    Instantiate(sellingItem, spawnPoint.position, spawnPoint.rotation);
                    Player.instance.Money -= sellingItemPrice;
                    npcUI.ShowDialogue(this, buyDialogue, defaultDialogueTime);
                }
                else
                {
                    npcUI.ShowDialogue(this, needGoldDialogue.ToString(), defaultDialogueTime);
                }

                npcUI.ButtonOnOff(false);
            });

            npcUI.SetNoClickAction(() =>
            {
                npcUI.ShowDialogue(this, defaultDialogue, defaultDialogueTime);
                npcUI.gameObject.SetActive(false);
            });
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Constant.item))
        {
            Item tmp = other.GetComponent<Item>();
            if(tmp.Price > 0)
            {
                Player.instance.Money += tmp.Price;
                npcUI.gameObject.SetActive(true);
                npcUI.ButtonOnOff(false);
                npcUI.ShowDialogue(this, SellingItem, defaultDialogueTime);
                Destroy(tmp.gameObject);
            }
             
        }
    }
}
