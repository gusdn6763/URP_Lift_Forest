using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenFind : MonoBehaviour
{
    public Transform targetPos1;
    public float ChickSpeed = 10f;
    private Animator chick;

    private void Awake()
    {
        chick = GetComponent<Animator>();
    }


    public void MoveAction()
    {
        this.transform.position = targetPos1.position;
        chick.SetBool(Constant.move, false);
    }

}
