using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    Date:
    Name:
    Overview: ¿ΩÁUI
*/

public class UIWorldTips : UIWindow
{
    public Transform owner;
    public float height = 1.25f;

    public Text text;
    Canvas canvas;
    private void Awake()
    {
        canvas= GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
    }
    public void SetText(string text)
    {
        this.text.text = text;
    }
    void LateUpdate()
    {
        if (owner != null)
        {
            this.transform.position = owner.position + Vector3.up * height;
        }
        if (Camera.main != null)
        {
            this.transform.forward = Camera.main.transform.forward;
        }
    }
    public void SetOwner(Transform root)
    {
        this.owner = root;
    }
}
