using System;
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

public class UISettingVideoItem : MonoBehaviour
{
    public UISetting uisetting;

    public Button leftbtn;
    public Button rightbtn;
    public Text text;

    public List<Image> images = new List<Image>();
    public List<string> strs = new List<string>();
    private int index = 0;

    public VideoItemEvent videoItemEvent;
    public VideoItemEvent InitData;
    [Serializable]
    public class VideoItemEvent : UnityEvent<int> { }
    private void Start()
    {
        leftbtn.onClick.AddListener(OnClickLeft);
        rightbtn.onClick.AddListener(OnClickRight);
    }
    public void Init(int index = 0)
    {
        this.index = index;
        SetImages();
    }
    private void SetImages()
    {
        for (int i = 0; i < images.Count; i++)
        {
            images[i].color = i == index ? Color.red : Color.black;
        }
        //text.text = strs[index];
    }
    private void OnClickLeft()
    {
        if (--index < 0)
        {
            index += images.Count;
        }
        SetImages();
        videoItemEvent.Invoke(index);
    }
    private void OnClickRight()
    {
        if (++index >= images.Count)
        {
            index -= images.Count;
        }
        SetImages();
        videoItemEvent.Invoke(index);
    }
}
