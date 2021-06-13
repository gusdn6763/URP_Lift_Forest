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
        particle.Play();
        for (int i = 0; i< seedsGround.Count; i++)
        {
            if (seedsGround[i].socket.isOn)
            {
                if (seedsGround[i].socket.currentSeed != null)
                {
                    StartCoroutine(seedsGround[i].socket.currentSeed.Growing());
                }
            }
        }
        base.OnSelectEntered(args);
    }
}
