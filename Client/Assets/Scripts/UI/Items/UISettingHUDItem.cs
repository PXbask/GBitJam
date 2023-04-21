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

public class UISettingHUDItem : MonoBehaviour
{
    public UISetting uisetting;

    public ToggleGroup togglegroup;
    [SerializeField] Toggle close;
    [SerializeField] Toggle open;
    public Text text;
    public HUDItemEvent hUDItemEvent;
    public HUDItemEvent InitData;
    [Serializable]
    public class HUDItemEvent : UnityEvent<bool> { }
    private void Start()
    {
        togglegroup.allowSwitchOff = false;
    }
    public void Init(bool b)
    {
        open.isOn= b;
        close.isOn= !b;
    }
    void FixedUpdate()
    {
        Toggle toggle = togglegroup.GetFirstActiveToggle();
        bool b = toggle.Equals(close);
        text.text = b ? "¹Ø" : "¿ª";
        hUDItemEvent.Invoke(!b);
    }
}
