using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class NPC : XRBaseInteractable
{
    [SerializeField] private float disableDiaolgueTime;
    [SerializeField] protected string defaultDialogue;

    protected NPCUI npcUI;
    protected bool interactive = false;

    protected override void Awake()
    {
        base.Awake();
        npcUI = GetComponentInChildren<NPCUI>();
    }

    private void Start()
    {
        npcUI.gameObject.SetActive(false);
    }


    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if(interactive)
        {
            npcUI.gameObject.SetActive(interactive);
            npcUI.ShowDialogue(defaultDialogue);
        }
        base.OnSelectEntered(args);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Constant.player))
            interactive = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constant.player))
        {
            interactive = false;
            npcUI.gameObject.SetActive(interactive);
        }
    }
}
