using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OnClickClimbing : ClimbInteractable
{
    [SerializeField] private Transform spawnPoint;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        FadeManager.instance.OnAction = Action;
        FadeManager.instance.FadeOut();
        base.OnSelectEntered(args);
    }

    public void Action()
    {
        Player.instance.transform.position = spawnPoint.position;
    }
}
