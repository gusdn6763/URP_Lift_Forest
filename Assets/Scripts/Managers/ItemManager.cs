using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;

    public ItemUI itemUI;

    private void Awake()
    {
        instance = this;
    }
    public List<Item> items;

    public Item FineItem(Item item)
    {
        for(int i =0; i <items.Count; i++)
        {
            if(items[i].GetType() == item.GetType())
            {
                return items[i];
            }
        }
        return null;
    }
}
