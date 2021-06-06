using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClimbInteractable : XRBaseInteractable
{

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
                Player.instance.ClimbingHand = null;
            }
        }
    }


}
