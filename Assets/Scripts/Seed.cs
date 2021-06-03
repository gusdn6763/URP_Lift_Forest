using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public bool isActive;
    private int count;
    private int currentCount;

    private void Start()
    {
        currentCount = 0;
        count = seeds.Count;
    }

    public IEnumerator Growing()
    {
        while (count != currentCount)
        {
            yield return new WaitForSeconds(seeds[currentCount].growingCount);
            meshRenderer = seeds[currentCount].renderer;
            filter = seeds[currentCount].filter;
            currentCount++;
            if (currentCount == count)
            {
                int count = Random.Range(0, maxInstanteCount);
                for (int i = 0; i < count; i++)
                {
                    Instantiate(completeItem, transform.position, transform.rotation);
                }
            }
        }
    }
}
