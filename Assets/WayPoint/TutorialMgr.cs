using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMgr : MonoBehaviour
{
    public Transform playerLoc;

    // ������ ��� �迭
    public GameObject[] ItemBocket;
    public Vector3[] ItemLoc;

    void Start()
    {
        // �±׷� ������ ��ġ ã��
        ItemBocket = GameObject.FindGameObjectsWithTag("Item");
        ItemLoc = new Vector3[ItemBocket.Length];
    }

    void Update()
    {
        for (int i = 0; i < ItemBocket.Length; i++)
        {
            ItemLoc[i] = Camera.main.WorldToScreenPoint(ItemBocket[i].transform.position);
        }
    }

    void OnGUI()
    {

        for (int i = 0; i < ItemBocket.Length; i++)
        {
            GUI.color = Color.magenta;
            GUI.Label(new Rect(ItemLoc[i].x + 6, Screen.height - ItemLoc[i].y, 100, 20), "Item");
            GUI.Label(new Rect(ItemLoc[i].x - 6, Screen.height - ItemLoc[i].y, 100, 20), "��");

        }
    }
}
