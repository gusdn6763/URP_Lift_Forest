using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visual : MonoBehaviour
{
    public MeshRenderer mesh;
    public MeshRenderer anchor;


    public void SetMaterial(Material material)
    {
        mesh.material = material;
        anchor.material = material;
    }
}
