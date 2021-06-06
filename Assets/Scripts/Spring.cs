using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Spring : XRGrabInteractable
{
    public List<Dirt> seedsGround;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        for(int i = 0; i< seedsGround.Count; i++)
        {
            if (seedsGround[i].socket.isOn)
            {
                StartCoroutine(seedsGround[i].socket.currentSeed.Growing());
            }
        }
        base.OnSelectEntered(args);
    }
}
