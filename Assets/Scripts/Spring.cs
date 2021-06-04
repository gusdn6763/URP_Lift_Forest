using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Spring : XRGrabInteractable
{
    public List<Dirt> seedsGround;

    public GameObject hourCenter;
    private bool done = false;  // 시계 이벤트가 이미 발생했는지를 검사한다.
    private float startAngle;

    [SerializeField]
    private int changeCount = 3; // 몇 번 돌리면 이벤트가 발생할지 정한다.
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
