using Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
    Date:
    Name:
    Overview:控制可交互门的终端,与InteractionGate一起使用
*/

public class GateConsole : MonoBehaviour
{
    private bool useful = true;
    [SerializeField] InteractionGate gate;
    private void OnTriggerEnter(Collider other)
    {
        if (!useful) return;
        UIManager.Instance.OpenWorldTip("E 互动", transform);
    }
    private void OnTriggerExit(Collider other)
    {
        UIManager.Instance.CloseWorldTip();
    }
    private void OnTriggerStay(Collider other)
    {
        if (!gate.Islocked) return;
        if (Input.GetKey(KeyCode.E))
        {
            this.useful= false;
            gate.OpenGate();
            UIManager.Instance.CloseWorldTip();
        }
    }
}
