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

        if (type == 1) RandomTitleWithTitleType(0.33f, 0.33f, 0.33f);
        if (type == 2)
        {
            RandomTitleWithTitleType(0f, 1f, 0f);
            RandomTitleWithTitleType(0.33f, 0.33f, 0.33f);
        }
        if(type == 3)
        {
            RandomTitleWithTitleType(0f, 0f, 1f);
            RandomTitleWithTitleType(0.6f, 0.4f, 1f);
            RandomTitleWithTitleType(0.6f, 0.4f, 1f);
        }

        MiniMapManager.Instance.Remove(this);
    }
    public string GetName() { return "Chest"; }

    public Transform GetTransform() { return transform; }

    public MapIconType GetIconType() { return MapIconType.Object; }
    public override void OnSomeTriggerEnter(Collider other)
    {
        if (isOpened) return;
        base.OnSomeTriggerEnter(other);
    }
    public override void OnSomeTriggerExit(Collider other)
    {
        base.OnSomeTriggerExit(other);
    }
    private void RandomTitleWithTitleType(float normalRate, float legendRate, float epicRate)
    {
        var info = TitleManager.Instance.RandomTitleWithTitleType(normalRate, legendRate, epicRate);
        TitleManager.Instance.GainTitle(info);
    }
}
