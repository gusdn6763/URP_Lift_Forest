using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [SerializeField] private Text introduceTxt;
    [SerializeField] private Text priceTxt;
    [SerializeField] private Vector3 addSize;

    private Canvas canvas;
    private Transform parentTransform;
    private Vector3 tmpVector;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    private void Start()
    {
        canvas.worldCamera = Camera.main;
        transform.position = Vector3.zero;
    }

    private void Update()
    {
        transform.position = parentTransform.position + addSize;
        tmpVector = new Vector3(Player.instance.transform.position.x, transform.position.y, Player.instance.transform.position.z);
        transform.LookAt(tmpVector);
    }

    public void TranceInfo(Transform parent, string introduce, string price)
    {
        parentTransform = parent;
        introduceTxt.text = introduce.ToString();
        priceTxt.text = price.ToString();
    }

}
