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

public class Creature
{
    public IAttackable controller;
    public CharacterDefine define;
    public Attributes attributes;//��������

    public WeaponManager weaponManager;//����������
    public bool IsPlayer => define.ID == Consts.Character.PlayerID;
    public List<TitleInfo> titles;

    public Creature(CharacterDefine define)
    {
        this.define = define;
        attributes = new Attributes(define, this);
    }
    ~Creature()
    {
        weaponManager = null;
        controller= null;
    }
    public void Attack()
    {
        var weapon = weaponManager.WeaponConfig;
        if (weapon == null) return;
        if (weapon.IsUnderCooling) return;
        OnAttack();
        weapon.OnFire();
    }
    protected virtual void OnAttack() { }
    public virtual void Update()
    {
        weaponManager.Update();
    }
}
