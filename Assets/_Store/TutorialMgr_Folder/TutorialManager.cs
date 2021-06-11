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
    public float arrowTargetSmooth;             // 화살표 방향 전환 부드럽기 조정.

    public GameObject arrow;
    public ItemComponents[] itemComponents;

    private Transform ItemArrow; // 아이템 화살표 방향 변경용.
    private Transform currentItemPoint; // 최근 아이템 포인트
    private Transform arrowTarget;
    private int TotalWaypoints;                  // 위치 갯수 조정.
    private int nextItem;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        GameObject newObject = new GameObject();
        newObject.name = "Arrow Target";                           // 애로우 화살표 지정 방향
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