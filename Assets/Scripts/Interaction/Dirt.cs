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

    public bool DigAble { get => digAble; set => digAble = value; }

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
        if(currentCount < count && DigAble)
        {
            Field.transform.localPosition = overPosition[currentCount];
            currentCount++;
            DigAble = false;
            if (currentCount == count)
            {
                socket.socketActive = true;
            }
        }
    }

    public void RestDirt()
    {
        Field.transform.localPosition = new Vector3(0f, -0.2f, 0f);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.agricultural))
        {
            Dig();
        }
    }
}
