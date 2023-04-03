using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoor: TrapLogic
{
    private Animator anim;
    public bool IsOpen = false;
    protected override void OnInit()
    {
        base.OnInit();
        anim = GetComponent<Animator>();
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (IsOpen) return;
        if (other.gameObject.CompareTag("Player"))
        {
            OnInteract();
        }
    }
    protected override void OnInteract(KeyCode code = KeyCode.None)
    {
        anim.Play("AutoMaticDoor_Open");
        IsOpen = true;
    }
}
