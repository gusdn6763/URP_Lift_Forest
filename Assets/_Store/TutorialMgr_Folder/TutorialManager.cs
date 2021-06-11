using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ItemComponents
{
    public string ItemName = "Item Name";
    public ItemLocation itemLocation;
    public UnityEvent ItemLocationEvent;
}

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;



    [Range(1, 20)]
    public float arrowTargetSmooth;             // ȭ��ǥ ���� ��ȯ �ε巴�� ����.

    public GameObject arrow;
    public ItemComponents[] itemComponents;

    private Transform ItemArrow; // ������ ȭ��ǥ ���� �����.
    private Transform currentItemPoint; // �ֱ� ������ ����Ʈ
    private Transform arrowTarget;
    private int TotalWaypoints;                  // ��ġ ���� ����.
    private int nextItem;

    private void Awake()
    {
        instance = this;
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
                currentItemPoint = itemComponents[0].itemLocation.transform;
            }
            currentItemPoint.gameObject.SetActive(false);
            currentItemPoint = itemComponents[nextItem].itemLocation.transform;
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