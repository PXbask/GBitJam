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

public class UITitle : UIWindow
{
    public Text hptext;
    public Text damageresistext;
    public Text accutext;
    public Text movevelotext;
    public Text atkvelotext;
    public Text damgtext;
    public Text goldgaintext;
    public Text expgaintext;
    protected override void OnStart()
    {
        SetLeftInfo();
    }

    private void SetLeftInfo()
    {
        Model.Attribute attri = GameManager.Instance.charc.charBase.attributes.curAttribute;

        hptext.text = attri.HP.ToString();
        damageresistext.text = (attri.DamageResistence * 100).ToString() + "%";
        accutext.text = (attri.Accuracy * 100).ToString() + "%";
        movevelotext.text = (attri.MoveVelocityRatio * 100).ToString() + "%";
        atkvelotext.text = (attri.AttackVelocityRatio * 100).ToString() + "%";
        damgtext.text = attri.Damage.ToString();
        goldgaintext.text = (attri.GoldRatio * 100).ToString() + "%";
        expgaintext.text = (attri.ExpRatio * 100).ToString() + "%";
    }
}
