using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TutorialNPC : NPC
{
    private FaceChange faceChange;
    [SerializeField] private string yesAnswer;
    [SerializeField] private string noAnswer;

    protected override void Awake()
    {
        base.Awake();
        faceChange = GetComponent<FaceChange>();
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if (Vector3.Distance(transform.position, Player.instance.transform.position) < interactiveRange)
        {
            npcUI.gameObject.SetActive(true);
            npcUI.ButtonOnOff(true);

            npcUI.ChangeButtonName("알았어?", "싫은데?");
            npcUI.ShowDialogue(this, defaultDialogue);

            npcUI.SetOnClickAction(() =>
            {
                npcUI.ButtonOnOff(false);
                faceChange.ChangeFace(Faces.Glad, 2f);
                npcUI.ShowDialogue(this, yesAnswer);
                StopCoroutine(stopCoroutine);
                StartCoroutine(DisableUI(defaultDialogueTime));
            });

            npcUI.SetNoClickAction(() =>
            {
                npcUI.ButtonOnOff(false);
                faceChange.ChangeFace(Faces.Sad, 5f);
                npcUI.ShowDialogue(this, noAnswer);
                StopCoroutine(stopCoroutine);
                StartCoroutine(DisableUI(defaultDialogueTime));
            });
        }
    }
}
