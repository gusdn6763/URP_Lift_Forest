using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SlideDown : MonoBehaviour
{
    public GameObject canvas;
   


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag =="Player")
        {
            StartCoroutine(slideView());
        }
        
    }

   
  IEnumerator slideView ()
    {
        canvas.SetActive(true);
        
        yield return new WaitForSeconds(1.5f);
        
        canvas.SetActive(false);
    }
}
