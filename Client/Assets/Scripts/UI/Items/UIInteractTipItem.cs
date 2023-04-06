using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    Date:
    Name:
    Overview:
*/

public class UIInteractTipItem : MonoBehaviour
{
    public Image iconImage;
    public Text tipText;

    public Transform root;
    private void LateUpdate()
    {
        if (root == null) return;
        Vector3 pos = Camera.main.WorldToScreenPoint(root.position);
        transform.position = pos + new Vector3(1,1,0) * 50;
    }
    public void SetInfo(string str, Transform root)
    {
        tipText.text = str;
        this.root = root;
    }
}
