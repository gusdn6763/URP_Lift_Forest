using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

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

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        GetDialogue();
    }

    public void GetDialogue( )
    {
        if ((Vector3.Distance(transform.position, Player.instance.transform.position) < interactiveRange))
        {
            npcUI.gameObject.SetActive(true);

            if (!isOn)
            {
                npcUI.ButtonOnOff(true);
                npcUI.ChangeButtonName("수락", "거절");
                npcUI.ShowDialogue(this, defaultDialogue, defaultDialogueTime);
                npcUI.SetOnClickAction(() =>
                {
                    isOn = true;
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
            print(other.gameObject.name);
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
