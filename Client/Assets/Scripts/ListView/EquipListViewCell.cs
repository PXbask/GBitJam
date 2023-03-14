using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
    Date:
    Name:
    Overview:
*/

public class EquipListViewCell : ListView.ListViewItem
{
    public Text titleNameText;
    public Image background;
    public TitleInfo titleInfo;

    Color _color;
    static Color selectedColor = new Color(175f / 255, 175f / 255, 175f / 255);
    private void Awake()
    {
        background = GetComponent<Image>();
        _color = titleNameText.color;
    }

    public override void onSelected(bool selected)
    {     
        if(selected)
        {
            this.titleNameText.color = Color.green;
            background.color = selectedColor;
            TitleManager.Instance.Equip(titleInfo.ID);
        }
        else
        {
            this.titleNameText.color = _color;
            background.color = Color.white;
            TitleManager.Instance.UnEquip(titleInfo.ID);
        }
    }
    public void SetInfo(TitleInfo info)
    {
        titleInfo= info;

        titleNameText.text = string.Format("{0} Lv.{1}", titleInfo.define.Name, titleInfo.level);
    }
}
