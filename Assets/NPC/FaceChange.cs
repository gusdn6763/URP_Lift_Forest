using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Faces
{
    Sad,
    Glad,
    Normal,
    Confused
}

[System.Serializable]
public class FaceInfo
{
    public Material face;
    public Faces faces;
}

public class FaceChange : MonoBehaviour
{
    // Start is called before the first frame update
    public SkinnedMeshRenderer MySkinnedMeshRenderer;

    public FaceInfo[] faceInfo;

    public void ChangedFace()
    {
        if (true)
        {
            MySkinnedMeshRenderer.materials[0] = faceInfo[0].face;
        }
        
    }
}
