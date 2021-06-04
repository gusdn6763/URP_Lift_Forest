using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Dirt : MonoBehaviour
{
    [SerializeField] private GameObject Field;
    [SerializeField] private List<Vector3> overPosition;
    public SeedSocket socket;

    private bool digAble = true;
    private int count;
    private int currentCount;

    private void Awake()
    {
        socket = GetComponentInChildren<SeedSocket>();
    }

    private void Start()
    {
        currentCount = 0;
        count = overPosition.Count;
        socket.socketActive = false;
    }

    public void Dig()
    {
        if(currentCount < count && digAble)
        {
            Field.transform.localPosition = overPosition[currentCount];
            currentCount++;
            digAble = false;
            if (currentCount == count)
            {
                socket.socketActive = true;
            }
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.agricultural))
        {
            Dig();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constant.agricultural))
        {
            digAble = true;
        }
    }
}
