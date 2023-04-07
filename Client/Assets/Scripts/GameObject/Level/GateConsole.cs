using Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
    Date:
    Name:
    Overview:���ƿɽ����ŵ��ն�,��InteractionGateһ��ʹ��
*/

public class GateConsole : TrapLogic
{
    private bool useful = true;
    [SerializeField] InteractionGate gate;
    protected override void OnInit()
    {
        base.OnInit();
        interactKey = KeyCode.F;
        tipStr = "E ����";
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (!useful) return;
        base.OnTriggerEnter(other);
    }
    protected override void OnInteract(KeyCode code)
    {
        base.OnInteract(code);
        if (!gate.Islocked) return;
        this.useful = false;
        gate.OpenGate();
        UIManager.Instance.RemoveInteractMessage(transform);
    }
}
