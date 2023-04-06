using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview:
*/

public class UIDynamic : UIWindow
{
    public UIGainTips gainTips;
    public UIInteractTips interactTips;
    protected override void OnStart()
    {
        base.OnStart();
        UIManager.Instance.dynamicPanel = this;
    }
    public void AddGainMsg(string str)
    {
        gainTips.AddMessage(str);
    }
    public void AddInteractMsg(string str, Transform root)
    {
        interactTips.AddMessage(str, root);
    }
    public void RemoveInteractMsg(Transform root)
    {
        interactTips.RemoveMessage(root);
    }
    private void OnDestroy()
    {
        UIManager.Instance.dynamicPanel = null;
    }
}
