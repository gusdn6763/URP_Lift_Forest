using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fork : Item
{
    private void Start()
    {
        defaultSell = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        
    }
}
