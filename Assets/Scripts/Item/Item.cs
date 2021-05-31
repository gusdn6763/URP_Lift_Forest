using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
public class Item : XRGrabInteractable
{
    private Rigidbody rigi;

    protected override void Awake()
    {
        rigi = GetComponent<Rigidbody>();
        base.Awake();
    }

    public void RigiKinematic(bool check)
    {
        rigi.isKinematic = check;
    }
}
