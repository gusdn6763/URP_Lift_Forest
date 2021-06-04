using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Hook : MonoBehaviour
{
    public GameObject FishPrefab;
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag(Constant.fishingHole))
        {
            GameObject tmp = Instantiate(FishPrefab, transform.position, transform.rotation);
            tmp.transform.SetParent(this.transform);
        }
    }

    
}
