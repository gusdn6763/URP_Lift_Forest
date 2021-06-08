using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClimbInteractable : XRBaseInteractable
{
     AudioSource audioSource;
     void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if (args.interactor is XRDirectInteractor)
            Player.instance.ClimbingHand = args.interactor.GetComponent<XRController>();
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        if (args.interactor is XRDirectInteractor)
        {
            if (Player.instance.ClimbingHand && Player.instance.ClimbingHand.name == args.interactor.name)
            {
                audioSource.Play();
                Player.instance.ClimbingHand = null;
            }
        }
    }


}
