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
    [SerializeField] private SkinnedMeshRenderer mesh;
    [SerializeField] private List<FaceInfo> allFaces;

    public Material faceMaterial;

    private void Awake()
    {
        faceMaterial = mesh.material;
    }

    public void ChangeFace(Faces face, float time)
    {
        mesh.material = allFaces.Find(x => x.faces == face).face;
        StartCoroutine(ResetFace(time));
    }

    IEnumerator ResetFace(float time)
    {
        yield return new WaitForSeconds(time);
        mesh.material = faceMaterial;
    }

}

   
