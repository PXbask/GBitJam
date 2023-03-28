using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoor: MonoBehaviour
{
    private Animator anim;
    public bool IsOpen = false;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (IsOpen) return;
        if (other.gameObject.CompareTag("Player"))
        {
            anim.Play("AutoMaticDoor_Open");
            IsOpen= true;
        }
    }
}
