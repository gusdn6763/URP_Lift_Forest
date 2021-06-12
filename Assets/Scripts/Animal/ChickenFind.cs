using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenFind : MonoBehaviour
{
    public Transform targetPos1;
    public float ChickSpeed = 10f;
    private waypointMove2 way;
    private Animator chick;

    private void Awake()
    {
        way = GetComponent<waypointMove2>();
        chick = GetComponent<Animator>();
    }


    public void MoveAction()
    {
        this.transform.position = targetPos1.position;
        chick.SetBool(Constant.move, false);
        way.Stop(false);
    }

}
