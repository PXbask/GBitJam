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
    protected override void OnStart()
    {
        base.OnStart();
        UIManager.Instance.dynamicPanel = this;
    }
    public void AddGainMsg(string str)
    {
        gainTips.AddMessage(str);
    }
    private void OnDestroy()
    {
        UIManager.Instance.dynamicPanel = null;
    }
}
