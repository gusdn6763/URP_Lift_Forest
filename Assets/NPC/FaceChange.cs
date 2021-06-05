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

//[ExecuteInEdit$$anonymous$$ode]
//[RequireComponent(typeof(MySkinnedMeshRenderer))]

public class FaceChange : MonoBehaviour
{
    //GameObject findCharacter = GameObject.Find("face1");
    //SkinnedMeshRenderer findSkinnedMesh = findCharacter.GetComponent<SkinnedMeshRenderer>();

    //GameObject findChangeObject = GameObject.Find("face27");
    //SkinnedMeshRenderer findMeshFilter = findChangeObject.GetComponent<SkinnedMeshRenderer>();

    //findSkinnedMesh.sharedMesh = findMeshFilter.sharedMesh;
    public Material[] myMesh;
    public SkinnedMeshRenderer MySkin;

    public void Start()
    {
        transform.Find("Face").GetComponent<SkinnedMeshRenderer>().material = myMesh[0];

        var materials = GetComponentInChildren<SkinnedMeshRenderer>().materials;
        materials[1] = myMesh[0];
        GetComponentInChildren<SkinnedMeshRenderer>().materials = materials;
        //MySkin = GetComponent<SkinnedMeshRenderer>();
        //MySkin.materials[0] = myMesh[0];
    }

   
    // public FaceInfo[] faceInfo;

    //public void ChangedFace()
    //{
    //    if (MySkinnedMeshRenderer != null)
    //    {
    //        MySkinnedMeshRenderer.materials[0] = faceInfo[0].face;
    //        transform.GetComponent<SkinnedMeshRenderer>().MySkinnedMeshRenderer;
    //    }

    //}
    //}


}

   
