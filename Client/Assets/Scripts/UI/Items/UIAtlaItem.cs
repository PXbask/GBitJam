using Model;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/*
    Date:
    Name:
    Overview:
*/

public class UIAtlaItem : ListView.ListViewItem
{
    public Image background;
    public Image iconImage;
    public Text nameText;
    public Text typeText;
    public Text affectText;
    public Text descriptionText;
    public List<Image> levelItem = new List<Image>();

    public TitleInfo info;
    public void SetInfo(TitleInfo info)
    {
        this.info = info;
        nameText.text = info.define.Name;
        typeText.text = info.define.TitleType.ToString();
        descriptionText.text = info.define.Description;
        affectText.text = info.GetDetailedInfo(info.level);

        for (int i = 0; i < levelItem.Count; i++)
        {
            levelItem[i].color = i <= info.level - 1 ? Color.yellow : Color.white;
        }
    }
}
