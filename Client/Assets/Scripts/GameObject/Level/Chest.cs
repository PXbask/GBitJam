using Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview:箱子
*/

public class Chest : TrapLogic, IVisibleinMap
{
    [Tooltip("1->普通 2->史诗 3->传奇")]
    [SerializeField] public int type;
    protected Animation animat;
    protected Outline outline;

    protected bool isOpened;
    protected override void OnInit()
    {
        base.OnInit();
        interactKey = KeyCode.F;
        tipStr = "F 打开";

        isOpened= false;
        animat= GetComponentInChildren<Animation>();
        outline = GetComponentInChildren<Outline>();

        MiniMapManager.Instance.Register(this);
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
    protected virtual void OpenChest()
    {
        if(isOpened) { return; }
        animat.Play();
        outline.enabled = false;
        isOpened= true;
        var info = TitleManager.Instance.RandomTitleWithTitleType(type);
        TitleManager.Instance.GainTitle(info);
        MiniMapManager.Instance.Remove(this);
    }
    public string GetName() { return "Chest"; }

    public Transform GetTransform() { return transform; }

    public MapIconType GetIconType() { return MapIconType.Object; }
}
