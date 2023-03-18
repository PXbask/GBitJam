using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    Date:
    Name:
    Overview:
*/

public class UITabButton : MonoBehaviour
{
    public Sprite activeImage;
    public Sprite normalImage;
    public UITabView tabView;
    public int tabIndex;
    public bool isSelected = false;
    private Image tabImage;

    void Start()
    {
        tabImage = GetComponent<Image>();
        normalImage = tabImage.sprite;
        this.GetComponent<Button>().onClick.AddListener(OnClick);
    }
    public void DoSelect(bool select)
    {
        tabImage.sprite = select ? activeImage : normalImage;
    }
    private void OnClick()
    {
        this.tabView.SelectTab(this.tabIndex);
    }
}
