using Define;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    Date:
    Name:
    Overview:
*/

public class UIDialogue : UIWindow
{
    public Image charimage;
    public Text nametext;
    public Text dialoguetext;

    private void Awake()
    {
        DialogueManager.Instance.uiDialogue = this;
    }
    protected override void OnStart()
    {
        base.OnStart();
    }
    public void SetInfo(DialogueDefine define)
    {
        nametext.text = define.Name;
        dialoguetext.text = define.Dialogue;
    }
}
