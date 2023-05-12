using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    Date:
    Name:
    Overview:
*/

public class ExitTransporter : TrapLogic
{

    public bool canUsed = false;

    protected override void OnInit()
    {
        base.OnInit();
        interactKey = KeyCode.F;
        tipStr = "F ´«ËÍ";

        EventManager.OnBossDefend += OnBossDefend;
    }

    protected override void OnInteract(KeyCode code)
    {
        base.OnInteract(code);

        if (canUsed)
            GameManager.Instance.CompleteGame();
        else
            DialogueManager.Instance.ShowDialogue(Consts.Dialogues.Door_Locked);
    }

    public override void OnSomeTriggerEnter(Collider other)
    {
        base.OnSomeTriggerEnter(other);
    }
    public override void OnSomeTriggerExit(Collider other)
    {
        base.OnSomeTriggerExit(other);
    }

    private void OnBossDefend() => canUsed = true; 
}
