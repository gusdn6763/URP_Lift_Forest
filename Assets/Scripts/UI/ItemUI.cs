using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    private Canvas canvas;
    [SerializeField] private Text introduceTxt;
    [SerializeField] private Text priceTxt;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    private void Start()
    {
        canvas.worldCamera = Camera.main;
    }

    private void Update()
    {
        transform.LookAt(Player.instance.transform);
    }

    public void UIOnOffCheck(string introduce, string price, bool check)
    {
        introduceTxt.text = introduce.ToString();
        priceTxt.text = price.ToString();
        gameObject.SetActive(check);
    }


}
