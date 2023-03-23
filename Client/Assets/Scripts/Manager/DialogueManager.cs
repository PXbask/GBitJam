using Define;
using Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview:
*/

public class DialogueManager : MonoSingleton<DialogueManager>
{
    public Action OnConversationStart = null;
    public Action OnConversationEnd = null;
    public Dictionary<int, List<DialogueDefine>> Dialogues = new Dictionary<int, List<DialogueDefine>>();
    public UIDialogue uiDialogue;

    private List<DialogueDefine> _defines = new List<DialogueDefine>();
    private int index = 0;
    private DialogueDefine _define = null;
    public void Init()
    {
        foreach (var diadics in DataManager.Instance.Dialogues)
        {
            _defines.Clear();
            index= 1;
            _define = null;
            while(diadics.Value.TryGetValue(index++,out _define))
            {
                _defines.Add(_define);
            }
            Dialogues.Add(diadics.Key, _defines);
        }
        //DataManager.Instance.Dialogues.Clear();
    }
    protected override void OnAwake()
    {
        base.OnAwake();
    }
    public void ShowDialogue(int diaId)
    {
        OnConversationStart?.Invoke();

        _defines = Dialogues[diaId];
        StartCoroutine(ShowDialogue(_defines));

        OnConversationEnd?.Invoke();
    }
    IEnumerator ShowDialogue(List<DialogueDefine> def)
    {
        UIManager.Instance.Show<UIDialogue>();
        foreach (var define in def)
        {
            uiDialogue.SetInfo(define);
            yield return new WaitUntil(()=>Input.GetKeyDown(KeyCode.Space));
            yield return new WaitUntil(()=>Input.GetKeyUp(KeyCode.Space));
        }
        UIManager.Instance.Close<UIDialogue>();
    }
}
