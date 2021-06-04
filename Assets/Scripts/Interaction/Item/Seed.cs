using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]
public class SeedGrowingStage
{
    public float growingCount;
    public MeshRenderer renderer;
    public MeshFilter filter;
}

public class Seed : Item
{
    [SerializeField] private List<SeedGrowingStage> seeds;
    [SerializeField] private Item completeItem;
    [SerializeField] private int maxInstanteCount;

    private MeshFilter filter;
    private MeshRenderer meshRenderer;

    private int growingCount;
    private int currentGrowingCount;
    private bool buy = false;

    protected override void Awake()
    {
        base.Awake();
        filter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
    }


    private void Start()
    {
        currentGrowingCount = 0;
        growingCount = seeds.Count;
    }

    public void Buy()
    {
        buy = true;
        StartCoroutine(SpawnItem());
    }

    public IEnumerator Growing()
    {
        while (growingCount != currentGrowingCount)
        {
            yield return new WaitForSeconds(seeds[currentGrowingCount].growingCount);
            meshRenderer = seeds[currentGrowingCount].renderer;
            filter = seeds[currentGrowingCount].filter;
            currentGrowingCount++;
            if (currentGrowingCount == growingCount)
            {
                int count = Random.Range(0, maxInstanteCount);
                for (int i = 0; i < count; i++)
                {
                    Instantiate(completeItem, transform.position, transform.rotation);
                }
            }
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {

        base.OnSelectEntered(args);
    }

    public override bool IsSelectableBy(XRBaseInteractor interactor)
    {
        return base.IsSelectableBy(interactor) && buy;
    }
}
