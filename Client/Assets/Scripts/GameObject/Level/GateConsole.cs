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

public class GateConsole : MonoBehaviour
{
    private bool useful = true;
    [SerializeField] InteractionGate gate;
    private void OnTriggerEnter(Collider other)
    {
        if (!useful) return;
        UIManager.Instance.OpenWorldTip("E ����", transform);
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
