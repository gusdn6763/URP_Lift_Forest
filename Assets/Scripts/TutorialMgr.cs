using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class TutorialMgr : MonoBehaviour
{
    [SerializeField]
    private Vector3 targetPos;                  // Ÿ�� ������
    [SerializeField]
    private RectTransform pointerRect;          // ���� ǥ��
    private void Awake()
    {
        targetPos = new Vector3(200, 45);
        pointerRect = transform.Find("Pointer").GetComponent<RectTransform>();
    }
    void Update()
    {
        Vector3 toPos = targetPos;
        Vector3 formPos = Camera.main.transform.position;

        formPos.z = 0f;
        Vector3 dir = (toPos - formPos).normalized;

        float angle= UtilsClass.GetAngleFromVectorFloat(dir);
        pointerRect.localEulerAngles = new Vector3(0, 0, angle);
    }
}
