using System.Collections;
using UnityEngine;

public class SpikeHazard : BasicHazard
{
    private Collider2D hazardCollider;
    private Animator animator;
    public bool Inverse = false;
    public override void Awake()
    {
        base.Awake();
        animator = GetComponentInChildren<Animator>();
    }

    // Turn on the fire
    public override void TurnOn()
    {
        if(!Inverse){
            Open();
            Debug.Log("soike");
            HandleDuration();
        }else{
            Close();
        }
    }

    // Turn off the fire
    public override void TurnOff()
    {
        if(!Inverse){
            Close();
        }else{
            Open();
        }
    }

    public void Open(){
        base.TurnOn();
        animator.enabled = true;
        animator.SetBool("OpenSpike", true);
    }
    public void Close(){
        base.TurnOff();
        animator.SetBool("OpenSpike", false);
    }
}
