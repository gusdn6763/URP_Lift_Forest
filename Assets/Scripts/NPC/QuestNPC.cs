using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class QuestNPC : NPC
{
    [SerializeField] private string acceptAfterAnswer;              //����Ʈ ������ ���� ���
    [SerializeField] private string acceptAnswer;                   //����Ʈ ������ ���
    [SerializeField] private string noAnswer;                       //����Ʈ ���� ���
    [SerializeField] private string yesItem;                        //����Ʈ �������� ������ ���
    [SerializeField] private string noItem;                         //����Ʈ �������� �ƴҽ� ���
    [SerializeField] private string completeItem;                   //����Ʈ �Ϸ������ ���
    [SerializeField] private Item requestItem;                      //�䱸�ϴ� ����Ʈ ������
    [SerializeField] private int requestItemCount;                  //�䱸�ϴ� ����Ʈ ������ ����

    private int currentCount = 0;
    private bool questAccept = false;               //����Ʈ ��������

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
                npcUI.ChangeButtonName("����", "����");
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
