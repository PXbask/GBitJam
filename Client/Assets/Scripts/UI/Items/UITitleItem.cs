using Define;
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

public class UITitleItem : MonoBehaviour,IPointerClickHandler
{
    public UITitle title;
    public Image background;
    public Image lftImage;
    public Text nameText;
    public List<Image> levelItem = new List<Image>();
    public GameObject eqtag;

    public TitleInfo info;

    public void SetInfo(TitleInfo info)
    {
        this.info = info;

        nameText.text = info.define.Name;
        for (int i = 0; i < levelItem.Count; i++)
        {
            levelItem[i].color = i <= info.level - 1 ? Color.yellow : Color.white;
        }
        eqtag.SetActive(info.equiped);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        title.OnTitleItemClicked(this);
    }
}
