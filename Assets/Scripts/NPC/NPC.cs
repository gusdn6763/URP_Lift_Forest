using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class NPC : XRBaseInteractable
{
    [SerializeField] private float disableDiaolgueTime;
    [SerializeField] protected string defaultDialogue;
    [SerializeField] private float interactiveRange = 5f;

    protected NPCUI npcUI;

    protected override void Awake()
    {
        base.Awake();
        npcUI = ItemManager.instance.npcUI;
    }

    private void Start()
    {
        npcUI.gameObject.SetActive(false);
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if(Vector3.Distance(transform.position, Player.instance.transform.position) < interactiveRange)
        {
            npcUI.gameObject.SetActive(true);
            npcUI.ShowDialogue(transform, defaultDialogue);
        }
        base.OnSelectEntered(args);
    }
}
