using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointMove : MonoBehaviour
{
    [SerializeField] Transform[] chickenPos;
    //[SerializeField] Transform[] chickenRot;
    [SerializeField] Vector3 lookDirection;


    [SerializeField] float speed = 0.5f;
    //[SerializeField] float turnSpeed = 30f;

    int chickenNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = chickenPos[chickenNum].transform.position;
        //transform.rotation = chickenRot[chickenNum].transform.rotation;

    }

    // Update is called once per frame
    void Update()
    {
        MovePath();
    }

    public void MovePath()
    {
        transform.position = Vector3.MoveTowards
            (transform.position, chickenPos[chickenNum].transform.position, speed * Time.deltaTime);

        if (transform.position == chickenPos[chickenNum].transform.position)
            chickenNum++;

        if (chickenNum == chickenPos.Length)
            chickenNum = 0;

        Vector3 relativePos = chickenPos[chickenNum].transform.position - transform.position;

        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);

        //float xx = Input.GetAxisRaw("Vertical");
        //float zz = Input.GetAxisRaw("Horizontal");'

        //lookDirection = chickenPos[chickenNum].transform.rotation * Vector3.right;

        //this.transform.rotation = Quaternion.LookRotation(lookDirection);
        //this.transform.Translate(Vector3.forward * speed * Time.deltaTime);



        //if (transform.position == chickenPos[chickenNum].transform.position)
        //    transform.Translate(Vector3.left * speed * Time.deltaTime);

        //transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);

        //transform.LookAt(chickenPos[chickenNum].transform.position*Time.deltaTime);
        //
        //float MyAngleZ = Input.GetAxis("Horizontal") * MyAngle;
        //Quaternion target = Quaternion.Euler(0, 0, MyAngleZ);
        //transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * speed);

        //if (transform.rotation == chickenRot[chickenNum].transform.rotation)
        //    chickenNum++;

        //Debug.Log(Vector3.Angle(transform.forward, chickenPos[chickenNum].transform.position - transform.position));


        //if (Vector3.Angle(transform.forward, chickenPos[chickenNum].transform.position-transform.position) > 10f)
        //    transform.Rotate(Vector3.up, -10f*Time.deltaTime);


    }
    
}
