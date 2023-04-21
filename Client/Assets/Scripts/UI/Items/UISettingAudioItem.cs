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

public class UISettingAudioItem : MonoBehaviour
{
    public UISetting uisetting;

    public Button leftbtn;
    public Button rightbtn;
    public Slider slider;
    public Text text;

    public AudioItemEvent audioItemEvent;
    public AudioItemEvent InitData;

    [Serializable]
    public class AudioItemEvent:UnityEvent<float> { }
    public class AudioInitEvent:UnityEvent<float> { }
    private void Start()
    {
        slider.value= 0;
        text.text = 0.ToString("f0");
        slider.maxValue = 100;
        slider.onValueChanged.AddListener(OnValueChanged);
        leftbtn.onClick.AddListener(OnClickLeft);
        rightbtn.onClick.AddListener(OnClickRight);
    }
    public void Init(float v)
    {
        slider.value = v;
    }
    public void OnValueChanged(float v)
    {
        text.text = v.ToString("f0");
        audioItemEvent.Invoke(v);
    }
    private void OnClickLeft()
    {
        slider.value--;
    }
    private void OnClickRight()
    {
        slider.value++;
    }
}
