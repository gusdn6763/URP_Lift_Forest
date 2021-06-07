using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;

    public ItemUI itemUI;
    public NPCUI npcUI;

    private void Awake()
    {
        instance = this;
    }
    public List<Item> items;

    public Item FineItem(Item item)
    {
        for(int i =0; i <items.Count; i++)
        {
            if(items[i].Name == item.Name)
            {
                return items[i];
            }
        }
        return null;
    }
}
