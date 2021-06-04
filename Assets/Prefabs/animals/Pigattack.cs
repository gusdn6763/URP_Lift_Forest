using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pigattack : MonoBehaviour
{

    [SerializeField]
    float speed = 3f;


    Animator animator;

    bool isAttack = false;
    bool isAttackCompleted = false;
    bool isRunning = false;

    public Transform Waypoint;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    private void Start()
    {

    }



    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("¾È´¨");

        if (other.gameObject.tag == "Player")
        {

            StartCoroutine(moveAttack(other.transform));
            //animator.SetTrigger("IsAttack");
        }
    }

    IEnumerator moveAttack(Transform target)
    {
        while (true)
        {
            yield return null;

            if (isAttack == false)
            {
                transform.position = Vector3.MoveTowards
                (transform.position, target.position, speed * Time.deltaTime);

                this.transform.LookAt(target.position);

                animator.SetBool("IsWalking", true);
            }


            if (Vector3.Distance(transform.position, target.position) < 2f)
            {

                animator.SetBool("IsWalking", false);

                if (!isAttackCompleted)
                {
                    Debug.Log($"isAttackCompleted");

                    animator.SetBool("IsAttack", true);
                    isAttack = true;
                    isAttackCompleted = true;
                    yield return new WaitForSeconds(1f);
                    isRunning = true;
                }
            }

            if (isRunning)
            {
                transform.position = Vector3.MoveTowards
                (transform.position, Waypoint.position, speed * 2 * Time.deltaTime);

                this.transform.LookAt(Waypoint.position);
                animator.SetBool("IsRunning", true);
            
            }


            ////this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(transform.position), 3f * Time.deltaTime);

            //if (pointNum < pointPos.Length - 1)
            //{
            //    pointNum++;
            //}
            //else
            //{
            //    pointNum = 0;
            //}
        }
        //this.transform.rotation = Quaternion.LookRotation(transform.position);
    }
}

