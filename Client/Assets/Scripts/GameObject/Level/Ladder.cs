using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview:ÌÝ×Ó
*/

public class Ladder : TrapLogic
{
    protected override void OnInit()
    {
        base.OnInit();
        interactKey = KeyCode.Space;
        tipStr = "Space ÉÏÉý";
    }
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        controller.rb.useGravity = true;
    }
    protected override void OnInteract(KeyCode code)
    {
        base.OnInteract(code);
        controller.rb.useGravity = false;
        controller?.rb.MovePosition(controller.rb.position + Vector3.up * Time.deltaTime);
    }
    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.CompareTag(targetTag))
    //    {
    //        if (Input.GetKey(KeyCode.Space))
    //        {
    //            OnInteract(KeyCode.Space);
    //        }
    //    }
    //}
}
