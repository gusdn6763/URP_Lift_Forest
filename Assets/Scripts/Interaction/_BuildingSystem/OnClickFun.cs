using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OnClickFun : XRBaseInteractable
{
    [SerializeField] private PlacedObjectTypeSO buildingObject;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        GridBuildingSystem3D.instance.GetObject(buildingObject);
        base.OnSelectEntered(args);
    }
}
