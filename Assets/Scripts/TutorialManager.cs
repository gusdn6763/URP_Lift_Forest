using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;

    [Range(1, 20)] public float arrowTargetSmooth;             // ȭ��ǥ ���� ��ȯ �ε巴�� ����.

    [SerializeField] private GameObject arrow;

    private ItemLocation[] itemComponents;
    private Transform ItemArrow;                                // ������ ȭ��ǥ ���� �����.
    private Transform currentItemPoint;                         // �ֱ� ������ ����Ʈ
    private Transform arrowTarget;                              // ȭ�� ��ǥ ����
    private int TotalWaypoints;                                 // ��ġ ���� ����.
    private int nextItem;

    private void Awake()
    {
        instance = this;
        itemComponents = GetComponentsInChildren<ItemLocation>();
    }

    void Start()
    {
        GameObject newObject = new GameObject();
        newObject.name = "Arrow Target";                           // �ַο� ȭ��ǥ ���� ����
        newObject.transform.parent = gameObject.transform;
        arrowTarget = newObject.transform;
        newObject = null;

        nextItem = 0;
        TotalWaypoints = itemComponents.Length;
        ItemArrow = arrow.transform;
        changeTarget();

    }

    void Update()
    {
        if (arrowTarget != null)
        {
            arrowTarget.localPosition = Vector3.Lerp(arrowTarget.localPosition, currentItemPoint.localPosition, arrowTargetSmooth * Time.deltaTime);
            arrowTarget.localRotation = Quaternion.Lerp(arrowTarget.localRotation, currentItemPoint.localRotation, arrowTargetSmooth * Time.deltaTime);
        }
        else
        {
            arrowTarget = currentItemPoint;
        }
        ItemArrow.LookAt(arrowTarget);
    }


    public void changeTarget()
    {
        int check = nextItem;
        if (check < TotalWaypoints)
        {
            if (currentItemPoint == null)
            {
                currentItemPoint = itemComponents[0].transform;
            }
            currentItemPoint.gameObject.SetActive(false);
            currentItemPoint = itemComponents[nextItem].transform;
            currentItemPoint.gameObject.SetActive(true);
            nextItem += 1;
        }
        if (check == TotalWaypoints)
        {
            Destroy(ItemArrow.gameObject);
            Destroy(gameObject);
        }
    }
}