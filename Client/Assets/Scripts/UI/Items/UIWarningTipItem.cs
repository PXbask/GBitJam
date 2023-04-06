using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    Date:
    Name:
    Overview:
*/

public class UIWarningTipItem : MonoBehaviour
{
    public Image image;
    public Text tipText;
    public RectTransform rect;
    UIWarningTips owner;

    const int MOVE_Y = 100;
    const float TIME = 0.5f;
    const int IMAGE_START_ALPHY = 150;
    const float LAST_TIME = 1;
    public void SetInfo(string str, UIWarningTips owner)
    {
        this.owner = owner;
        tipText.text = str;

        StartCoroutine(DestroyObj());
    }
    public void UpToword()
    {
        rect.DOMoveY(MOVE_Y, TIME, true)
            .SetEase(Ease.Linear)
            .SetRelative()
            .SetLink(this.gameObject);
    }
    IEnumerator DestroyObj()
    {
        yield return new WaitForSeconds(owner.duation);
        float time = 0;
        while (time <= owner.duation)
        {
            time += Time.deltaTime;

            Color _color = image.color;
            _color.a -= LAST_TIME * Time.deltaTime * IMAGE_START_ALPHY / 225f;
            image.color = _color;

            _color = tipText.color;
            _color.a -= LAST_TIME * Time.deltaTime;
            tipText.color = _color;

            yield return null;
        }
        owner.DequeueItem();
        Destroy(gameObject);
    }
}
