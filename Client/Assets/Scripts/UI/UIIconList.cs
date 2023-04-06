using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
    Date:
    Name:
    Overview:
*/

public class UIIconList : MonoBehaviour
{
    public GameObject itemPrefab;
    public List<UIIconItem> lists = new List<UIIconItem>();
    public void AddNewItem(TitleInfo info)
    {
        if(lists.FirstOrDefault(i => i.ID == info.ID) == null)
        {
            GameObject obj = Instantiate(itemPrefab, transform);
            var item = obj.GetComponent<UIIconItem>();
            item.SetInfo(info);

            lists.Add(item);
        }
    }
    public void RemoveItem(TitleInfo info)
    {
        var item = lists.FirstOrDefault(i => i.ID == info.ID);
        if (item != null)
        {
            lists.Remove(item);
            Destroy(item.gameObject);
        }
    }

    internal void Clear()
    {
        foreach (var item in lists)
        {
            Destroy(item.gameObject);
        }
        lists.Clear();
    }
}
