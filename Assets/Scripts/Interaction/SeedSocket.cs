using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class SeedSocket : XRSocketInteractor
{
    private Collider co;
    public bool isOn = false;
    public Seed currentSeed;

    private bool realShowMesh = false;

    protected override void Awake()
    {
        base.Awake();
        co = GetComponent<Collider>();
    }

    protected override void DrawHoveredInteractables()
    {
        if (!currentSeed && realShowMesh)
        {
            base.DrawHoveredInteractables();
        }
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        if (args.interactable.CompareTag(Constant.seed))
        {
            realShowMesh = true;
        }
        else
        {
            realShowMesh = false;
        }
        base.OnHoverEntered(args);
    }

    public override bool CanSelect(XRBaseInteractable interactable)
    {
        //1첫 시작 & 여러번 반복
        return base.CanSelect(interactable) && interactable.CompareTag(Constant.seed);
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        co.isTrigger = true;
        if (args.interactable.CompareTag(Constant.seed))
        {
            isOn = true;
            currentSeed = args.interactable.GetComponent<Seed>();
        }
        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        co.isTrigger = false;
        base.OnSelectExited(args);
    }

}
