using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Spring : XRGrabInteractable
{
    [SerializeField] private ParticleSystem particle;
    public List<Dirt> seedsGround;
    

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        for(int i = 0; i< seedsGround.Count; i++)
        {
            if (seedsGround[i].socket.isOn)
            {
                StartCoroutine(seedsGround[i].socket.currentSeed.Growing());
                particle.Play();
            }
        }
        base.OnSelectEntered(args);
    }
}
