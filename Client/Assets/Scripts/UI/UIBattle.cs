using Manager;
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

public class UIBattle : UIWindow
{
    public Image avatar;
    public Text leveltext;
    public Slider hpslider;
    public Slider expslider;

    public GameObject enemyobj;
    public Text enemynametext;
    public Slider enemyhpslider;

    public UIIconList playerIcons;

    //public UIIconList enemyIcons;//TODO£∫µ–»À–æ∆¨Õº±Íœ‘ æ
    protected override void OnStart()
    {
        UserManager.Instance.OnPlayerHpChanged += SetHpSlider;
        UserManager.Instance.OnPlayerExpChanged += SetExpSlider;
        UserManager.Instance.OnPlayerLevelChanged += SetLevelText;
        TitleManager.Instance.OnTitleEquiped += AddIcon;
        TitleManager.Instance.OnTitleUnEquiped += RemoveIcon;

        Init();
    }
    private void Init()
    {
        SetLevelText();
        SetExpSlider();
        SetHpSlider();
        SetEnemyHpSlider();
        SetIconBar();
    }

    private void SetLevelText()
    {
        leveltext.text = UserManager.Instance.Level.ToString();
    }

    private void SetExpSlider()
    {
        expslider.maxValue = UserManager.Instance.exp2NextLevel;
        expslider.value = UserManager.Instance.Exp;
    }

    private void SetHpSlider()
    {
        hpslider.maxValue = UserManager.Instance.hpMax;
        hpslider.value = UserManager.Instance.HP;
    }
    private void SetEnemyHpSlider()
    {
        enemynametext.text = "BOSS";
        enemyhpslider.maxValue = 1000;
        enemyhpslider.value = 600;
        enemyobj.SetActive(true);
    }
    public void SetIconBar()
    {
        playerIcons.Clear();
        foreach (var item in TitleManager.Instance.EquipedTitle)
        {
            playerIcons.AddNewItem(item);
        }
    }
    private void RemoveIcon(int infoId)
    {
        var info = TitleManager.Instance.GetTitleInfoByID(infoId);
        playerIcons.RemoveItem(info);
    }
    private void AddIcon(int infoId)
    {
        var info = TitleManager.Instance.GetTitleInfoByID(infoId);
        playerIcons.AddNewItem(info);
    }

    private void OnDestroy()
    {
        UserManager.Instance.OnPlayerHpChanged -= SetHpSlider;
        UserManager.Instance.OnPlayerExpChanged -= SetExpSlider;
        UserManager.Instance.OnPlayerLevelChanged -= SetLevelText;

        TitleManager.Instance.OnTitleEquiped -= AddIcon;
        TitleManager.Instance.OnTitleUnEquiped -= RemoveIcon;
    }
}
