using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [Header("Prefab Box")]
    [SerializeField]
    private Transform PlayerPrefab;
    [SerializeField]
    private GameObject ArrowPrefab;
    [Header("Target Box")]
    [SerializeField]
    private Transform Target_1;                       // Ÿ�� ����.
    private Transform Target_2;                       // Ÿ�� ����.
    private Transform Target_3;                       // Ÿ�� ����.
    private Transform Target_4;                       // Ÿ�� ����.
    private Transform Target_5;                       // Ÿ�� ����.

    public GameObject[] ObjectBox;


    void Start()

    {
        
    }

    void Update()
    {

    }

    void NextItemArrow()
    {

    }
}
