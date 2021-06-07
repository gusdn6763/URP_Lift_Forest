using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtAfter : MonoBehaviour
{
    private Dirt dirt;

    private void Awake()
    {
        dirt = GetComponentInParent<Dirt>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constant.agricultural))
        {
            dirt.DigAble = true;
        }
    }
}
