using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypointmove3 : MonoBehaviour
{

    [SerializeField]
    Transform[] pointPos;


    [SerializeField]
    private float speed;
    private bool isFinish = false;

    private int pointNum = 0;

    bool isMoveEnd = false;

    YieldInstruction waitSecond = new WaitForSeconds(2.0f);

    Animator animator;



    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        MovePath();
    }

    void Update()
    {
        // transform.position = pointPos[pointNum].transform.position;
        // MovePath();


    }


    public void MovePath()
    {
        StartCoroutine(coMove());
    }

    IEnumerator coMove()
    {

        while (true)
        {
            yield return null;


            transform.position = Vector3.MoveTowards
            (transform.position, pointPos[pointNum].transform.position, speed * Time.deltaTime);

            this.transform.LookAt(pointPos[pointNum].position);

            animator.SetBool("IsRunning", true);



            if (Vector3.Distance(transform.position, pointPos[pointNum].position) < 0.05f)
            {

                animator.SetBool("IsRunning", false);

                yield return waitSecond;
                Debug.Log(pointNum);

                //this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(transform.position), 3f * Time.deltaTime);

                if (pointNum < pointPos.Length - 1)
                {
                    pointNum++;
                }
                else
                {
                    pointNum = 0;
                }
            }
            //this.transform.rotation = Quaternion.LookRotation(transform.position);
        }
    }
}
