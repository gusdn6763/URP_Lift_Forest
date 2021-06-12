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
    [SerializeField] private string completeItem;                   //퀘스트 완료시 대답
    [SerializeField] private string questCompleted;                 //퀘스트 완료후 대화시 대답
    [SerializeField] private Item requestItem;                      //요구하는 퀘스트 아이템
    [SerializeField] private int requestItemCount;                  //요구하는 퀘스트 아이템 갯수

    private FaceChange face;
    private int currentCount = 0;
    private bool questAccept = false;                               //퀘스트 수락상태
    private bool questComplete = false;                             //퀘스트 완료상태

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
    /// 특정 조건에 따라 NPC가 대화창을 띄우고 말하는 함수
    /// </summary>
    public void GetDialogue( )
    {
        //일정 거리에 있을시
        if ((Vector3.Distance(transform.position, Player.instance.transform.position) < interactiveRange))
        {
            //NPC 대화창 활성화
            npcUI.gameObject.SetActive(true);

            //퀘스트 수락한 적이 없을 경우
            if (!questAccept)
            {
                //수락 비활성화 활성화 하고 버튼 텍스트 변경
                npcUI.ButtonOnOff(true);
                npcUI.ChangeButtonName("수락", "거절");
                //대화창의 텍스트를 defaultDialogue로 변경
                npcUI.ShowDialogue(this, defaultDialogue, defaultDialogueTime);

                //왼쪽 버튼(수락)을 클릭시 실행
                npcUI.SetOnClickAction(() =>
                {
                    questAccept = true;
                    npcUI.ShowDialogue(this, acceptAnswer, defaultDialogueTime);
                    npcUI.ButtonOnOff(false);
                });

                //오른쪽 버튼(수락)을 클릭시 실행
                npcUI.SetNoClickAction(() =>
                {
                    npcUI.ShowDialogue(this, noAnswer, defaultDialogueTime);
                    npcUI.ButtonOnOff(false);
                });
            }
            else            
            {
                //퀘스트를 완료했었을 경우
                if (questComplete)
                {
                    npcUI.ShowDialogue(this, questCompleted, defaultDialogueTime);
                }
                //퀘스트를 수행중인 경우
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
