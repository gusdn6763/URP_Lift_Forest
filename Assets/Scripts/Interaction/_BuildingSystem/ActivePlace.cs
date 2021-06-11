using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ActivePlace : XRBaseInteractable
{
    [SerializeField] private PlacedObjectTypeSO placeObject;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        GridBuildingSystem3D.instance.SetPlacedObjectTypeSO(placeObject);
        base.OnSelectEntered(args);
    }
}
