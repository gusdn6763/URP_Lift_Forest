using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public static TestManager instance;

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
