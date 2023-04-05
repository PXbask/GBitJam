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

    bool isOpened;
    protected override void OnInit()
    {
        base.OnInit();
        interactKey = KeyCode.F;
        tipStr = "F ��";

        isOpened= false;
        animat= GetComponentInChildren<Animation>();
    }
    protected override void OnTriggerEnter(Collider other)
    {
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
    }
    private void OpenChest()
    {
        if(isOpened) { return; }
        animat.Play();
        isOpened= true;
        var info = TitleManager.Instance.RandomTitleWithTitleType(type);
        TitleManager.Instance.GainTitle(info);
    }
}
