using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseOn : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Constant.player))
        {
            ItemManager.instance.houseUI.SetActive(true);
            GridBuildingSystem3D.instance.StartBuild();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constant.player))
        {
            ItemManager.instance.houseUI.SetActive(false);
            GridBuildingSystem3D.instance.StopBuilding();
        }
    }

}
