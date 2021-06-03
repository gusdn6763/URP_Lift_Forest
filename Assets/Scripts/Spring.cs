using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public List<SeedSocket> seedsGround;

    public GameObject hourCenter;
    private bool done = false;  // �ð� �̺�Ʈ�� �̹� �߻��ߴ����� �˻��Ѵ�.
    private float startAngle;

    [SerializeField]
    private int changeCount = 3; // �� �� ������ �̺�Ʈ�� �߻����� ���Ѵ�.
    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        startAngle = Mathf.Round(transform.rotation.x * 10);
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Mathf.Round(transform.rotation.x * 10);

        if (angle * -1 == startAngle)
        {
            count++;
            if (count >= changeCount && !done == true)
            {
                done = true;
                for(int i = 0; i < seedsGround.Count; i++)
                {
                    if(seedsGround[i].isOn)
                    {
                        StartCoroutine(seedsGround[i].currentSeed.Growing());
                    }
                }
            }
            startAngle *= -1;
            hourCenter.transform.Rotate(new Vector3(0, 0, 30));
        }
    }
}
