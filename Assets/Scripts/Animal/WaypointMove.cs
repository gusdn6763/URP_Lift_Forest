using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WaypointMove : MonoBehaviour
{
    [SerializeField] private Transform wayPoints;
    [SerializeField] private float waitTime = 2f;
    [SerializeField] private float speed = 4f;

    private Transform[] pointPos;
    private Animator animator;

    private int pointNum = 1;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        pointPos = wayPoints.GetComponentsInChildren<Transform>();
    }

    void Start()
    {
        transform.DOLookAt(pointPos[pointNum].transform.position, 1f);
        Move();
    }

    public void Move()
    {
        animator.SetBool(Constant.move, true);
        transform.DOMove(pointPos[pointNum].transform.position, speed).
        SetEase(Ease.Linear).SetSpeedBased(true).OnComplete(() =>
        {
            animator.SetBool(Constant.move, false);
            if (pointNum < pointPos.Length - 1)
            {
                pointNum++;
            }
            else
            {
                pointNum = 1;
            }
            transform.DOLookAt(pointPos[pointNum].transform.position, 1f);
            StartCoroutine(MoveCoroutine());
        });
    }

    IEnumerator MoveCoroutine()
    {
        animator.SetBool(Constant.move, false);
        yield return new WaitForSeconds(waitTime);
        Move();
    }
}
