using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview:
*/

public class UIAtla : UIWindow
{
    public GameObject itemPrefab;
    public ListView listMain;
    public Transform itemRoot;
    protected override void OnStart()
    {
        UpdateList();
    }
    private void UpdateList()
    {
        this.ClearList();
        this.InitItems();
    }
    private void InitItems()
    {
        foreach (var key in DataManager.Instance.Titles.Keys)
        {
            TitleInfo title = null;
            if(TitleManager.Instance.AllGainedTitle.TryGetValue(key, out title))
            {
                GameObject go = Instantiate(itemPrefab, itemRoot);
                UIAtlaItem item = go.GetComponent<UIAtlaItem>();
                item.SetInfo(title);
                this.listMain.AddItem(item);
                item.background.color = Color.white;
            }
            else if(TitleManager.Instance.AllUnGainedTitle.TryGetValue(key, out title))
            {
                GameObject go = Instantiate(itemPrefab, itemRoot);
                UIAtlaItem item = go.GetComponent<UIAtlaItem>();
                item.SetInfo(title);
                this.listMain.AddItem(item);
                item.background.color = Color.gray;
            }
        }
    }

    private void ClearList()
    {
        listMain.RemoveAll();
    }
}
