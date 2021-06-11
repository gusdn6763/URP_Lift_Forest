using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class QuestNPC : NPC
{
    [SerializeField] private string acceptAfterAnswer;              //퀘스트 수락한 후의 대답
    [SerializeField] private string acceptAnswer;                   //퀘스트 수락시 대답
    [SerializeField] private string noAnswer;                       //퀘스트 거절 대답
    [SerializeField] private string yesItem;                        //퀘스트 아이템이 맞을시 대답
    [SerializeField] private string noItem;                         //퀘스트 아이템이 아닐시 대답
    [SerializeField] private string completeItem;                   //퀘스트 완료상태의 대답
    [SerializeField] private Item requestItem;                      //요구하는 퀘스트 아이템
    [SerializeField] private int requestItemCount;                  //요구하는 퀘스트 아이템 갯수

    private int currentCount = 0;
    private bool questAccept = false;               //퀘스트 수락상태

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

            if (!questAccept)
            {
                npcUI.ButtonOnOff(true);
                npcUI.ChangeButtonName("수락", "거절");
                npcUI.ShowDialogue(this, defaultDialogue, defaultDialogueTime);
                npcUI.SetOnClickAction(() =>
                {
                    questAccept = true;
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
        if (questAccept)
        {
            if (other.CompareTag(Constant.item))
            {
                Item tmp = other.GetComponent<Item>();
                if (tmp.Name == requestItem.Name)
                {
                    currentCount++;
                    if (requestItemCount == currentCount)
                    {
                        npcUI.ShowDialogue(this, completeItem, defaultDialogueTime);
                    }
                    else
                    {
                        npcUI.ShowDialogue(this, requestItemCount - currentCount + yesItem, defaultDialogueTime);
                    }
                    Destroy(other.gameObject);
                }
                else
                {
                    npcUI.ShowDialogue(this, noItem, defaultDialogueTime);
                }
            }
        }
    }
}
