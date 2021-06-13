using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : UI
{
    [SerializeField] private Text nameTxt;
    [SerializeField] private Text introduceTxt;
    [SerializeField] private Text priceTxt;
    [SerializeField] private GameObject gold;

    private Transform parentTransform;
    private Vector3 tmpVector;

    private void Update()
    {
        if (parentTransform)
        {
            transform.position = parentTransform.position + addSize;
        }
        tmpVector = new Vector3(Player.instance.transform.position.x, transform.position.y, Player.instance.transform.position.z);
        if (Vector3.Distance(Player.instance.transform.position, transform.position) > 1f)
        {
            transform.LookAt(tmpVector);
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(transform.position - tmpVector);
        }
    }

    public void TranceInfo(Transform parent, Vector3 addsize ,string name, string introduce, float price, bool isOn = false)
    {
        gold.SetActive(isOn);
        this.addSize = addsize;
        parentTransform = parent;
        nameTxt.text = name;
        introduceTxt.text = introduce;
        if (price != 0)
        {
            priceTxt.text = price.ToString();
        }
        else
        {
            priceTxt.text = "";
        }

        tmpVector = new Vector3(Player.instance.transform.position.x, transform.position.y, Player.instance.transform.position.z);
        if (Vector3.Distance(Player.instance.transform.position, transform.position) > 1f)
        {
            transform.LookAt(tmpVector);
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(transform.position - tmpVector);
        }
        
    }

}
