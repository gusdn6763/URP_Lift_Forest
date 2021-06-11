using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Hook : MonoBehaviour
{
    private Rod rod;
    public Fish FishPrefab;

    private bool hole = false;
    private bool grabbing = false;

    public bool Grabbing { get => grabbing; set => grabbing = value; }
    public bool Hole { get => hole; set => hole = value; }

    private void Awake()
    {
        rod = GetComponentInParent<Rod>();
    }

    public void ChangeStatus()
    {
        Grabbing = false;
        rod.IsActive = true;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(Constant.fishingHole) && !Grabbing)
        {
            Fish fish = Instantiate(ItemManager.instance.FineItem(FishPrefab), 
                transform.position, transform.rotation).GetComponent<Fish>();

            Hole = true;
            Grabbing = true;
            fish.OnAction = ChangeStatus;
            fish.transform.SetParent(this.transform);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag(Constant.fishingHole))
        {
            Hole = false;
        }
    }
}
