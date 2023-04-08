using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;
using Define;
using Manager;

/*
    Date:
    Name:
    Overview:���&���˵Ļ���
*/

public class CharBase
{
    public EntityController controller;
    public CharacterDefine define;
    public Attributes attributes;//��������

    public WeaponManager weaponManager;//����������
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
