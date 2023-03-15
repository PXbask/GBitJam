using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview:���߹�����
*/

public class ItemManager : Singleton<ItemManager>
{
    public Action OnItemGet = null;
    public Action OnItemRemove = null;
    public Action OnItemUse = null;

    public Dictionary<int, ItemInfo> gainedItems = new Dictionary<int, ItemInfo>();
    public void Init()
    {
        foreach (var item in DataManager.Instance.SaveData.gainedItemData)
        {
            gainedItems.Add(item[0], new ItemInfo(item[0], item[1]));
        }
    }
    public void GetItem(int id, int count=1)
    {
        if(!gainedItems.TryGetValue(id, out var item))
        {
            gainedItems.Add(id, new ItemInfo(id, count));
        }
        else
        {
            item.Count += count;
        }
        OnItemGet?.Invoke();
        Debug.LogFormat("�����(id:{0})*{1}", id.ToString(), count.ToString());
    }
    public void RemoveItem(int id, int count = 1)
    {
        if (gainedItems.TryGetValue(id, out var item))
        {
            item.Count -= count;
            if(item.Count<=0)
            {
                gainedItems.Remove(id);
            }
            Debug.LogFormat("������(id:{0})*{1}", id.ToString(), count.ToString());
        }
        else
        {
            Debug.LogErrorFormat("�����˲����ڵĵ��� id:{0}", id);
        }
        OnItemRemove?.Invoke();
    }
    public void UseItem(int id,int count = 1)
    {
        //TODO:ʹ�õ����߼�
        OnItemUse?.Invoke();
    }
}
