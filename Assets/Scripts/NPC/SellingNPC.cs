using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellingNPC : NPC
{
    [SerializeField] private string sellingDialogue;
    [SerializeField] private string needGoldDialogue;
    [SerializeField] private string buyDialogue;
    [SerializeField] private Button okButton;
    [SerializeField] private Button noButton;
    
    private Item sellingItem;
    private int sellingItemPrice;

    public void GetDialogue(Item choiceItem, int price)
    {
        sellingItem = choiceItem;
        sellingItemPrice = price;
        npcUI.gameObject.SetActive(true);
        npcUI.ShowDialogue(defaultDialogue);
        npcUI.ShowDialogue(sellingItem + "Àº(´Â)" + price + "°ñµå¾ß." + sellingDialogue.ToString());

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
            //npcText.text = needGoldDialogue.ToString();
        }
        ButtonCheck(false);
    }

    public void ButtonCheck(bool isOn)
    {
        okButton.gameObject.SetActive(isOn);
        noButton.gameObject.SetActive(isOn);
    }
}
