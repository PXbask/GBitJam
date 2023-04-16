using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview:
*/

public class MessageBox
{
    static UnityEngine.Object cacheObject = null;

    public static UIMessageBox Show(string message, string btnOK = "", string btnCancel = "", Action onYes = null, Action onNo = null)
    {
        if (cacheObject == null)
        {
            cacheObject = Resloader.Load<UnityEngine.Object>("Prefab/UI/UIMessageBox");
        }

        GameObject go = (GameObject)GameObject.Instantiate(cacheObject);
        UIMessageBox msgbox = go.GetComponent<UIMessageBox>();
        msgbox.Init(message, btnOK, btnCancel, onYes, onNo);
        return msgbox;
    }
}
