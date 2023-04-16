using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/*
    Date:
    Name:
    Overview:
*/

public class UIMessageBox : MonoBehaviour
{
    public Text message;

    public Button buttonYes;
    public Button buttonNo;

    public Text buttonYesTitle;
    public Text buttonNoTitle;

    public Action OnYes;
    public Action OnNo;

    public void Init(string message, string btnOK, string btnCancel, Action onYes = null, Action onNo = null)
    {
        this.message.text = message;

        if (!string.IsNullOrEmpty(btnOK)) this.buttonYesTitle.text = btnOK;
        if (!string.IsNullOrEmpty(btnCancel)) this.buttonNoTitle.text = btnCancel;

        OnYes = onYes;
        OnNo = onNo;

        this.buttonYes.onClick.AddListener(OnClickYes);
        this.buttonNo.onClick.AddListener(OnClickNo);
    }

    void OnClickYes()
    {
        Destroy(this.gameObject);
        if (this.OnYes != null)
            this.OnYes();
    }

    void OnClickNo()
    {
        Destroy(this.gameObject);
        if (this.OnNo != null)
            this.OnNo();
    }
}
