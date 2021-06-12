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
    [SerializeField] private string completeItem;                   //����Ʈ �Ϸ�� ���
    [SerializeField] private string questCompleted;                 //����Ʈ �Ϸ��� ��ȭ�� ���
    [SerializeField] private Item requestItem;                      //�䱸�ϴ� ����Ʈ ������
    [SerializeField] private int requestItemCount;                  //�䱸�ϴ� ����Ʈ ������ ����

    private FaceChange face;
    private int currentCount = 0;
    private bool questAccept = false;                               //����Ʈ ��������
    private bool questComplete = false;                             //����Ʈ �Ϸ����

    protected override void Awake()
    {
        face = GetComponent<FaceChange>();
        base.Awake();
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        GetDialogue();
        base.OnSelectEntered(args);
    }

    /// <summary>
    /// Ư�� ���ǿ� ���� NPC�� ��ȭâ�� ���� ���ϴ� �Լ�
    /// </summary>
    public void GetDialogue( )
    {
        //���� �Ÿ��� ������
        if ((Vector3.Distance(transform.position, Player.instance.transform.position) < interactiveRange))
        {
            //NPC ��ȭâ Ȱ��ȭ
            npcUI.gameObject.SetActive(true);

            //����Ʈ ������ ���� ���� ���
            if (!questAccept)
            {
                //���� ��Ȱ��ȭ Ȱ��ȭ �ϰ� ��ư �ؽ�Ʈ ����
                npcUI.ButtonOnOff(true);
                npcUI.ChangeButtonName("����", "����");
                //��ȭâ�� �ؽ�Ʈ�� defaultDialogue�� ����
                npcUI.ShowDialogue(this, defaultDialogue, defaultDialogueTime);

                //���� ��ư(����)�� Ŭ���� ����
                npcUI.SetOnClickAction(() =>
                {
                    questAccept = true;
                    npcUI.ShowDialogue(this, acceptAnswer, defaultDialogueTime);
                    npcUI.ButtonOnOff(false);
                });

                //������ ��ư(����)�� Ŭ���� ����
                npcUI.SetNoClickAction(() =>
                {
                    npcUI.ShowDialogue(this, noAnswer, defaultDialogueTime);
                    npcUI.ButtonOnOff(false);
                });
            }
            else            
            {
                //����Ʈ�� �Ϸ��߾��� ���
                if (questComplete)
                {
                    npcUI.ShowDialogue(this, questCompleted, defaultDialogueTime);
                }
                //����Ʈ�� �������� ���
                else
                {
                    npcUI.ShowDialogue(this, acceptAfterAnswer, defaultDialogueTime);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (questAccept && !questComplete)
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
                        questComplete = true;
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
