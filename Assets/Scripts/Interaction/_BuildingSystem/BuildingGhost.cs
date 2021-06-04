using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    private Visual visual;
    private PlacedObjectTypeSO placedObjectTypeSO;

    [SerializeField] private Material green;
    [SerializeField]  private Material red;

    private void Start()
    {
        RefreshVisual();
    }


    private void LateUpdate()
    {
        Vector3 targetPosition = GridBuildingSystem3D.instance.GetMouseWorldSnappedPosition();
        targetPosition.y = 1f;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 15f);

        transform.rotation = Quaternion.Lerp(transform.rotation, GridBuildingSystem3D.instance.GetPlacedObjectRotation(), Time.deltaTime * 15f);

    }

    public void ChangeMaterial(bool canBuild)
    {
        if (visual != null)
        {
            if (canBuild)
            {
                visual.SetMaterial(green);
            }
            else
            {
                visual.SetMaterial(red);
            }
        }
    }

    public void RefreshVisual()
    {
        if (visual != null)
        {
            Destroy(visual.gameObject);
            visual = null;
        }

        PlacedObjectTypeSO placedObjectTypeSO = GridBuildingSystem3D.instance.GetPlacedObjectTypeSO();

        if (placedObjectTypeSO != null)
        {
            visual = Instantiate(placedObjectTypeSO.visual, Vector3.zero, Quaternion.identity);
            visual.transform.parent = transform;
            visual.transform.localPosition = Vector3.zero;
            visual.transform.localEulerAngles = Vector3.zero;
            SetLayerRecursive(visual.gameObject, 11);
        }
    }

    private void SetLayerRecursive(GameObject targetGameObject, int layer)
    {
        targetGameObject.layer = layer;
        foreach (Transform child in targetGameObject.transform)
        {
            SetLayerRecursive(child.gameObject, layer);
        }
    }

}

