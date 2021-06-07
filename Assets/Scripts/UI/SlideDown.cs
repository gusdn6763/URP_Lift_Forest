using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SlideDown : MonoBehaviour
{
    public GameObject canvas;
    public Vector3 movepoint;
   
    public void Update()
    {
        canvas.transform.LookAt(Player.instance.transform);
    }

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
