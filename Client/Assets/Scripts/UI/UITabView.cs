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

public class UITabView : MonoBehaviour
{

    public UITabButton[] uITabButtons;
    public GameObject[] tabPages;
    public UnityAction<int> OnTabSelect;
    public int index = -1;

    public ScrollRect scroll;

    private IEnumerator Start()
    {
        if(scroll) OnTabSelect += ScrollRectChanged;
        for (int i = 0; i < uITabButtons.Length; i++)
        {
            uITabButtons[i].tabView = this;
            uITabButtons[i].tabIndex = i;
        }
        SelectTab(0);
        yield return null;
    }

    internal void SelectTab(int tabIndex)
    {
        if (this.index != tabIndex)
        {
            for (int j = 0; j < uITabButtons.Length; j++)
            {
                uITabButtons[j].DoSelect(j == tabIndex);
                if (j < tabPages.Length)
                    tabPages[j].SetActive(j == tabIndex);
            }
            this.index = tabIndex;
            if (this.OnTabSelect != null)
                this.OnTabSelect(tabIndex);
        }
    }
    public void ScrollRectChanged(int index)
    {
        scroll.content = tabPages[index].transform as RectTransform;
    }
    private void OnDestroy()
    {
        if (scroll) 
            OnTabSelect -= ScrollRectChanged;
    }
}
