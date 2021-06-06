using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMove : MonoBehaviour
{
    [SerializeField] private Transform wayPoints;
    [SerializeField] private float waitTime = 2f;
    [SerializeField] private float speed = 1f;

    private Transform[] pointPos;
    private Animator animator;

    private int pointNum = 0;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        pointPos = wayPoints.GetComponentsInChildren<Transform>();
    }

    void Start()
    {
        StartCoroutine(coMove());
    }


    IEnumerator coMove()
    {
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointPos[pointNum].transform.position, speed * Time.deltaTime);
            this.transform.LookAt(pointPos[pointNum].position);
            animator.SetBool(Constant.move, true);

            if (Vector3.Distance(transform.position, pointPos[pointNum].position) < 0.05f)
            {
                animator.SetBool(Constant.move, false);    
                            
                if (pointNum < pointPos.Length - 1)
                {
                    pointNum++;
                }
                else
                {
                    pointNum = 0;
                }
                yield return new WaitForSeconds(waitTime);
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

}
