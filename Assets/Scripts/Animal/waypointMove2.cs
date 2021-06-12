using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class waypointMove2 : MonoBehaviour
{
    [SerializeField] private Transform[] pointPos;
    [SerializeField] private float waitTime = 2f;
    [SerializeField] private float speed = 1f;

    private Animator animator;
    private int pointNum = 0;


    private void Awake()
    {
        animator = GetComponent<Animator>();
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

    public void Stop(bool ison)
    {
        if (ison)
        {
            transform.DOPlay();
        }
        else
        {
            transform.DOPause();
        }
    }
}
