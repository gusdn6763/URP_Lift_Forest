using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]
public class SeedGrowingStage
{
    public float growingCountTime;
    public MeshRenderer renderer;
    public MeshFilter filter;
}

public class Seed : Item
{
    [Header("¾¾¾Ñ")]
    [SerializeField] private List<SeedGrowingStage> seeds;
    [SerializeField] private Item completeItem;
    [SerializeField] private int maxInstanteCount;

    private MeshFilter filter;
    private MeshRenderer meshRenderer;

    private int growingCount;
    private int currentGrowingCount;

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

    // Spring Script¿¡ µé¾î°¨.
    public IEnumerator Growing()
    {
        while (growingCount != currentGrowingCount)
        {
            yield return new WaitForSeconds(seeds[currentGrowingCount].growingCountTime);
            currentGrowingCount++;
            if (currentGrowingCount == growingCount)
            {
                int count = Random.Range(0, maxInstanteCount);
                for (int i = 0; i < count; i++)
                {
                    Instantiate(completeItem, transform.position + (Vector3.up * 2), transform.rotation);
                    Destroy(this.gameObject);
                }
            }
            else
            {
                meshRenderer = seeds[currentGrowingCount].renderer;
                filter.sharedMesh = seeds[currentGrowingCount].filter.sharedMesh;
            }
        }
    }
}
