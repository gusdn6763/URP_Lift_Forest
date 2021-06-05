using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eatingmotion : MonoBehaviour
{
    bool IsWalking=false;
    bool IsEating = false;
  

    Animator animator;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();

 
    }
    private void start()
    {
        EatingAct();
    }
    
    public void EatingAct()
    {
        
        animator.SetBool("IsEating", true);
    }
}
