using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : UI
{
    [SerializeField] private Text nameTxt;
    [SerializeField] private Text introduceTxt;
    [SerializeField] private Text priceTxt;


    private Transform parentTransform;
    private Vector3 tmpVector;

    private void Update()
    {
        if (parentTransform)
        {
            transform.position = parentTransform.position + addSize;
            print(transform.position);
        }
        tmpVector = new Vector3(Player.instance.transform.position.x, transform.position.y, Player.instance.transform.position.z);
        transform.LookAt(tmpVector);
    }

    public void TranceInfo(Transform parent, Vector3 addsize ,string name, string introduce, string price)
    {
        this.addSize = addsize;
        parentTransform = parent;
        nameTxt.text = name.ToString();
        introduceTxt.text = introduce.ToString();
        priceTxt.text = price.ToString();
    }

}
