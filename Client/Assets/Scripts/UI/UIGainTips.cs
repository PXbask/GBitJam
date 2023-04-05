using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

/*
    Date:
    Name:
    Overview:
*/

public class UIGainTips : MonoBehaviour
{
    const int START_X = -500;
    const int START_Y = -150;
    public GameObject itemPrefab;
    public float duation;
    [SerializeField] LinkedList<UIGainTipItem> root = new LinkedList<UIGainTipItem>();
    LinkedListNode<UIGainTipItem> _ptr;
    public void AddMessage(string str)
    {
        GameObject obj = Instantiate(itemPrefab, transform);
        var rect = obj.transform as RectTransform;
        rect.localPosition = new(START_X, START_Y);
        UIGainTipItem item = obj.GetComponentInChildren<UIGainTipItem>();
        item.rect = rect;
        item.SetInfo(str,this);
        EnqueueItem(item);
    }
    public void EnqueueItem(UIGainTipItem item)
    {
        _ptr = root.AddFirst(item);
        while (_ptr.Next != null)
        {
            _ptr = _ptr.Next;
            _ptr.Value.UpToword();
        }
    }
    public void DequeueItem()
    {
        if (root.Count <= 0) return;
        root.RemoveLast();
    }
}
