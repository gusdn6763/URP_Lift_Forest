using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class SeedSocket : XRSocketInteractor
{
    public bool isOn = false;
    public Seed currentSeed;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactable.CompareTag(Constant.seed))
        {
            isOn = true;
            currentSeed = args.interactable.GetComponent<Seed>();
        }
        base.OnSelectEntered(args);
    }
}
