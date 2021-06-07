using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestNPC : NPC
{
    [SerializeField] private string acceptAfterAnswer;
    [SerializeField] private string acceptAnswer;
    [SerializeField] private string noAnswer;
    [SerializeField] private string yesItem;
    [SerializeField] private string noItem;
    [SerializeField] private string completeItem;
    [SerializeField] private Item requestItem;
    [SerializeField] private int requestItemCount;

    private int currentCount = 0;
    private bool isOn = false;

    public void GetDialogue(Item choiceItem, int price)
    {
        if ((Vector3.Distance(transform.position, Player.instance.transform.position) < interactiveRange))
        {
            npcUI.gameObject.SetActive(true);

            if (!isOn)
            {
                npcUI.ButtonOnOff(true);
                npcUI.ChangeButtonName("����", "����");
                npcUI.ShowDialogue(this, defaultDialogue, defaultDialogueTime);
                npcUI.SetOnClickAction(() =>
                {
                    npcUI.ShowDialogue(this, acceptAnswer, defaultDialogueTime);
                    npcUI.ButtonOnOff(false);
                });

                npcUI.SetNoClickAction(() =>
                {
                    npcUI.ShowDialogue(this, defaultDialogue, defaultDialogueTime);
                    npcUI.gameObject.SetActive(false);
                });
            }
            else
            {
                npcUI.ShowDialogue(this, acceptAfterAnswer, defaultDialogueTime);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Constant.item))
        {
            Item tmp =  other.GetComponent<Item>();
            if(tmp.Name == requestItem.Name)
            {
                currentCount++;
                if(requestItemCount == currentCount)
                {
                    npcUI.ShowDialogue(this, completeItem, defaultDialogueTime);
                }
                else
                {
                    npcUI.ShowDialogue(this, currentCount + yesItem, defaultDialogueTime);
                }
            }
            else
            {
                npcUI.ShowDialogue(this, noItem, defaultDialogueTime);
            }
        }
    }
}
