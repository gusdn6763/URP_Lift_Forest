using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Spring : XRGrabInteractable
{
    public List<Dirt> seedsGround;

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

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        for(int i = 0; i> seedsGround.Count; i++)
        {
            if (seedsGround[i].socket.isOn)
            {
                StartCoroutine(seedsGround[i].socket.currentSeed.Growing());
            }
        }
        base.OnSelectEntered(args);
    }
}
