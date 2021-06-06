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

    public NPC currentNpc;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    protected void Update()
    {
        transform.LookAt(new Vector3(Player.instance.transform.position.x, transform.position.y, Player.instance.transform.position.z));
    }

    public void ShowDialogue(NPC parent, string dialogue)
    {
        currentNpc = parent;
        dialogueTxt.text = dialogue.ToString();
        transform.position = parent.transform.position + addSize;
    }

    public void ButtonOnOff(bool isOn)
    {
        okButton.gameObject.SetActive(isOn);
        noButton.gameObject.SetActive(isOn);
    }

    public void ChangeButtonName(string ok, string no)
    {
        okButton.SetTitle(ok);
        noButton.SetTitle(no);
    }

    public void SetOnClickAction(UnityAction action)
    {
        okButton.SetOnClickAction(action);
    }

    public void SetNoClickAction(UnityAction action)
    {
        noButton.SetOnClickAction(action);
    }

}
