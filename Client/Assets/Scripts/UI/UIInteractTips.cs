using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

/*
    Date:
    Name:
    Overview:
*/

public class UIInteractTips : MonoBehaviour
{
    public GameObject itemPrefabs;
    public Transform root;

    public Dictionary<Transform, UIInteractTipItem> itemMap = new Dictionary<Transform, UIInteractTipItem>();

    public void AddMessage(string msg, Transform owner)
    {
        GameObject obj = Instantiate(itemPrefabs, root);
        UIInteractTipItem item = obj.GetComponent<UIInteractTipItem>();
        item.SetInfo(msg, owner);

        itemMap.Add(owner, item);
    }
    public void RemoveMessage(Transform owner)
    {
        if(itemMap.TryGetValue(owner,out UIInteractTipItem item))
        {
            itemMap.Remove(owner);
            Destroy(item.gameObject);
        }
    }
}
