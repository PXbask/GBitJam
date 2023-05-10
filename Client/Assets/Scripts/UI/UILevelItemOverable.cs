using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
    Date:
    Name:
    Overview:
*/

public class UILevelItemOverable : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public int Id;
    public UITitle title;

    public Action<int> OnLevelItemTouched=null;
    public Action<int> OnLevelItemUnTouched=null;

    private Image rawImage;
    public Color color
    {
        get
        {
            return rawImage.color;
        }
        set
        {
            rawImage.color = value;
        }
    }
    private void Awake()
    {
        rawImage = GetComponent<Image>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        OnLevelItemTouched?.Invoke(Id);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnLevelItemUnTouched?.Invoke(Id);
    }
}
