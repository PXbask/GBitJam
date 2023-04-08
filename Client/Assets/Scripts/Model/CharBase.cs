using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;
using Define;
using Manager;

/*
    Date:
    Name:
    Overview:玩家&敌人的基类
*/

public class CharBase
{
    public EntityController controller;
    public CharacterDefine define;
    public Attributes attributes;//属性总类

    public WeaponManager weaponManager;//武器管理类
    public bool IsPlayer => define.ID == Consts.Player.ID;


    public CharBase(CharacterDefine define)
    {
        this.define = define;
        attributes = new Attributes(define);
    }
    ~CharBase()
    {
        weaponManager = null;
        controller= null;
    }

    public void Update()
    {
        weaponManager.Update();
    }
}
