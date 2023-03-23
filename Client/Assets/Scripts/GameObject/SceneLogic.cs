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
        Tween tween = GameManager.Instance.maskText.DOText("�����ˣ�����������֮оҽ�ƹ�˾��\r\n�ռ��ˣ�ľ��\r\nִ���ˣ�ľ��\r\n2039-03-22 ǳˮ�� ʢ������ʩ��β¥¥��\r\n���ݷǷ����״���������\r\n�Ե�������֮о�ķ������ʵʩ������\r\n�ս�����оƬ�Ͻ��о�����\r\n�յ��ظ���\r", 20f);
        tween.onComplete = () => { UIManager.Instance.TurntoWhite(); };      
    }
}
