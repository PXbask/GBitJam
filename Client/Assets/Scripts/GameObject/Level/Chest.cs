using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview:箱子
*/

public class Chest : TrapLogic
{
    [Tooltip("1->普通 2->史诗 3->传奇")]
    [SerializeField] public int type;
    Animation animat;

    bool isOpened;
    protected override void OnInit()
    {
        base.OnInit();
        interactKey = KeyCode.F;
        tipStr = "F 打开";

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
