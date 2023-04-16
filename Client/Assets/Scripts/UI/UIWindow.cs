using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using System;

public abstract class UIWindow : MonoBehaviour
{
    public delegate void CloseHandler(UIWindow sender, WindowResult result);
    public event CloseHandler OnClose;
    public virtual System.Type Type { get { return this.GetType(); } }
    public GameObject Root;
    public enum WindowResult
    {
        None = 0,
        Yes,
        No,
    }
    public void Start()
    {
        this.OnStart();
    }
    public virtual void OnStart()
    {

    }
    public virtual void OnReopen()
    {
        
    }
    public void Close(WindowResult result = WindowResult.None)
    {
        if (UIManager.Instance.WindowStack.Peek() != this) return;
        UIManager.Instance.Close(this.Type);
        if (this.OnClose != null)
        {
            this.OnClose(this, result);
        }
    }
    public virtual void OnCloseClick()
    {
        this.Close();
    }
    public virtual void OnNoClick()
    {
        this.Close(WindowResult.No);
    }
    public virtual void OnYesClick()
    {
        this.Close(WindowResult.Yes);
    }
    private void OnDestroy()
    {
        if(UIManager.Instance.UIInstance.ContainsKey(Type))
        {
            UIManager.Instance.UIInstance.Remove(Type);
        }
    }
}
