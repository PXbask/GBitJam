using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview:
*/

public class UIWarningTips : MonoBehaviour
{
    public GameObject itemPrefab;
    public float duation;
    [SerializeField] LinkedList<UIWarningTipItem> root = new LinkedList<UIWarningTipItem>();
    LinkedListNode<UIWarningTipItem> _ptr;
    public void AddWarning(string str)
    {
        GameObject obj = Instantiate(itemPrefab, transform);
        var rect = obj.transform as RectTransform;
        rect.localPosition = Vector3.zero;
        UIWarningTipItem item = obj.GetComponentInChildren<UIWarningTipItem>();
        item.rect = rect;
        item.SetInfo(str, this);
        EnqueueItem(item);
    }
    public void EnqueueItem(UIWarningTipItem item)
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
