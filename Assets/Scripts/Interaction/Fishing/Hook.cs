using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Hook : MonoBehaviour
{
    [SerializeField] private Rod rod;
    public Fish FishPrefab;
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag(Constant.fishingHole))
        {
            Fish tmp = Instantiate(FishPrefab, transform.position, transform.rotation);
            tmp.rod = rod;
            tmp.transform.SetParent(this.transform);
            rod.IsGrab = true;
        }
    }


}
