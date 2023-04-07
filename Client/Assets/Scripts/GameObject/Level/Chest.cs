using Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview:����
*/

public class Chest : TrapLogic
{
    [Tooltip("1->��ͨ 2->ʷʫ 3->����")]
    [SerializeField] public int type;
    Animation animat;
    Outline outline;

    bool isOpened;
    protected override void OnInit()
    {
        base.OnInit();
        interactKey = KeyCode.F;
        tipStr = "F ��";

        isOpened= false;
        animat= GetComponentInChildren<Animation>();
        outline = GetComponentInChildren<Outline>();
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (isOpened) return;
        base.OnTriggerEnter(other);
    }
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }
    protected override void OnInteract(KeyCode code)
    {
        base.OnInteract(code);
        OpenChest();
        UIManager.Instance.RemoveInteractMessage(transform);
    }
    private void OpenChest()
    {
        if(isOpened) { return; }
        animat.Play();
        outline.enabled = false;
        isOpened= true;
        var info = TitleManager.Instance.RandomTitleWithTitleType(type);
        TitleManager.Instance.GainTitle(info);
    }
}
