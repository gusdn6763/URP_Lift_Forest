using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMove2 : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed = 3f;
    public float rotSpeed = 100f;

    bool isWandering = false;
    bool isRotatingLeft = false;
    bool isRotatingRight = false;
    bool isWalking = false;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
         
    }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       if(isWandering == false)
        {
            StartCoroutine(Wander());
            
        }
       if(isRotatingRight == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
        }
        if (isRotatingLeft == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
        }
        
        if(isWalking == true )
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }


    }

    IEnumerator Wander()
    {
        int rotTime = Random.Range(1, 2);
        int rotateWait = Random.Range(1, 4);
        int rotateLorR = Random.Range(1, 1);
        int walkWait = Random.Range(1, 4);
        int walkTime = Random.Range(1, 5);

        isWandering = true;
        animator.SetBool("IsEating", true);
        yield return new WaitForSeconds(walkWait);
        
        isWalking = true;
        animator.SetBool("IsWalking", true);
        yield return new WaitForSeconds(walkTime);

        isWalking = false;

        animator.SetBool("IsWalking", false);
        yield return new WaitForSeconds(rotateWait);

        if(rotateLorR ==1)
        {
            isRotatingRight = true;         
            yield return new WaitForSeconds(rotTime);
            isRotatingRight = false;
        }
        if(rotateLorR ==2)
        {
            isRotatingLeft = true;
           
            yield return new WaitForSeconds(rotTime);
            isRotatingLeft = false;
        }
        isWandering = false;
    }
}
