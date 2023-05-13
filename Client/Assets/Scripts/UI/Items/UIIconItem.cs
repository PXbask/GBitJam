using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    Date:
    Name:
    Overview:
*/

public class UIIconItem : MonoBehaviour
{
    public Image icon;
    public TitleInfo info;

    public int ID => info.ID;
    public void SetInfo(TitleInfo info)
    {
        this.info = info;
        icon.sprite = info.sprite;
    }
}
