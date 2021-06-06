using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPointScript : MonoBehaviour
{
    [SerializeField]
    private Transform Target;

     void Update()
    {
        Vector3 targetPos = Target.transform.position;
        targetPos.y = transform.position.y;
        transform.LookAt(targetPos);
    }
}
