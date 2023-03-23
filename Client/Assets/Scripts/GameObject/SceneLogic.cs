using DG.Tweening;
using Manager;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview:
*/

public class SceneLogic : MonoBehaviour
{
    public int id;
    PXScene sceneDefine;
    private void Awake()
    {
        sceneDefine = PXSceneManager.Instance.GetScene(id);
    }
    private void Start()
    {
        if (sceneDefine.isFirstEntered)
        {
            sceneDefine.isFirstEntered = false;
            PlayStartAnimation();
        }
    }

    private void PlayStartAnimation()
    {
        GameManager.Instance.maskText.text = string.Empty;
        Tween tween = GameManager.Instance.maskText.DOText("发件人：阿库玛义体之芯医疗公司：\r\n收件人：木秋\r\n执行人：木秋\r\n2039-03-22 浅水埗 盛邦中心施工尾楼楼顶\r\n根据非法交易处理条例，\r\n对倒卖义体之芯的犯罪分子实施逮捕，\r\n收缴所有芯片上交研究所。\r\n收到回复。\r", 20f);
        tween.onComplete = () => { UIManager.Instance.TurntoWhite(); };      
    }
}
