using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMove : MonoBehaviour
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


    public void RandomMove()
    {
        speed = Random.Range(1.0f, 10.0f);
    }



    IEnumerator coMove()
    {
        RandomMove();
        
        while (true)
        {
            yield return null;


            transform.position = Vector3.MoveTowards

            (transform.position, pointPos[pointNum].transform.position, speed * Time.deltaTime);

            this.transform.LookAt(pointPos[pointNum].position);

            animator.SetBool("IsWalking", true);

            if (Vector3.Distance(transform.position, pointPos[pointNum].position) < 0.05f)
            {

                animator.SetBool("IsWalking", false);


                yield return waitSecond;

                Debug.Log(pointNum);



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
