using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class NPCUI : UI
{
    [SerializeField] private Text dialogueTxt;
    [SerializeField] private SCButton okButton;
    [SerializeField] private SCButton noButton;

    protected Coroutine stopCoroutine;
    public NPC currentNpc;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    protected void Update()
    {
        transform.LookAt(new Vector3(Player.instance.transform.position.x, transform.position.y, Player.instance.transform.position.z));
    }

    /// <summary>
    /// 대화창 보여주기
    /// </summary>
    /// <param name="parent">NPC 위치</param>
    /// <param name="dialogue"></param>
    public void ShowDialogue(NPC parent, string dialogue, float defaultTime)
    {
        currentNpc = parent;
        dialogueTxt.text = dialogue.ToString();
        print(dialogueTxt.text);
        transform.position = parent.transform.position + addSize;

        if (stopCoroutine != null)
            StopCoroutine(stopCoroutine);
        stopCoroutine = StartCoroutine(DisableUI(defaultTime));
    }

    /// <summary>
    /// 버튼 활성화 비활화
    /// </summary>
    /// <param name="isOn"></param>
    public void ButtonOnOff(bool isOn)
    {
        okButton.gameObject.SetActive(isOn);
        noButton.gameObject.SetActive(isOn);
    }

    /// <summary>
    /// 버튼 이름 바꾸기
    /// </summary>
    /// <param name="ok"></param>
    /// <param name="no"></param>
    public void ChangeButtonName(string ok, string no)
    {
        okButton.SetTitle(ok);
        noButton.SetTitle(no);
    }

    /// <summary>
    /// 버튼 클릭시 행동
    /// </summary>
    /// <param name="action"></param>
    public void SetOnClickAction(UnityAction action)
    {
        okButton.SetOnClickAction(action);
    }

    /// <summary>
    /// 버튼 클릭시 행동
    /// </summary>
    /// <param name="action"></param>
    public void SetNoClickAction(UnityAction action)
    {
        noButton.SetOnClickAction(action);
    }

    public IEnumerator DisableUI(float defaultTime)
    {
        yield return new WaitForSeconds(defaultTime);
        transform.position = new Vector3(0, 999f, 0f);
    }
}
