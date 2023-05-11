using DG.Tweening;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    Date:
    Name:
    Overview:
*/

public class UIGainTipItem : MonoBehaviour
{
    public Image iconImage;
    public Text tipText;
    public RectTransform rect;
    public GameObject root;
    UIGainTips owner;

    const int MOVE_X = 500;
    const int MOVE_Y = 80;
    const float TIME = 0.3f;

    private void Start()
    {
        rect.DOMoveX(MOVE_X, TIME, true)
            .SetEase(Ease.Linear)
            .SetRelative()
            .SetLink(this.gameObject)
            .SetUpdate(true);
    }
    public void SetInfo(string str, UIGainTips owner)
    {
        this.owner= owner;
        tipText.text = str;

        StartCoroutine(DestroyObj());
    }
    public void UpToword()
    {
        rect.DOMoveY(MOVE_Y, TIME, true)
            .SetEase(Ease.Linear)
            .SetRelative()
            .SetLink(this.gameObject)
            .SetUpdate(true);
    }
    IEnumerator DestroyObj()
    {
        yield return new WaitForSecondsRealtime(owner.duation);
        var tween = root.transform.DOMoveY(MOVE_Y, TIME, true)
                        .SetEase(Ease.Linear)
                        .SetRelative()
                        .SetLink(this.gameObject)
                        .SetUpdate(true);
        tween.OnComplete(() => {
            owner.DequeueItem();
            Destroy(transform.parent.gameObject); 
        });
    }
}
