using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenFind : MonoBehaviour
{
    public Transform targetPos1;
    public float ChickSpeed = 10f;

    public void Start()
    {
        targetPos1.transform.position = new Vector3(10, 0, 0);
    }

    public void MoveAction()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        float translateMove = ChickSpeed * Time.deltaTime;

        targetPos1.transform.Translate
            (x * translateMove, 0, z * translateMove);

    }

}
