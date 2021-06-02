using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFace : MonoBehaviour
{
    public Material sadFace;

    public SkinnedMeshRenderer MyskinnedMeshRenderer;

    private void Awake()
    {
        if(true)
        {
            ChangeFaceFunction();
        }
    }
    public void ChangeFaceFunction()
    {
        MyskinnedMeshRenderer.material = sadFace;
    }
}
