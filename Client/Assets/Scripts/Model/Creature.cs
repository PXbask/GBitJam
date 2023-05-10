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

public class Creature
{
    public PXCharacterController controller;
    public CharacterDefine define;
    public Attributes attributes;//属性总类

    public WeaponManager weaponManager;//武器管理类
    public bool IsPlayer => define.ID == Consts.Character.PlayerID;
    public List<TitleInfo> titles;

    public bool invincible = false;

    public Creature(CharacterDefine define, PXCharacterController controller)
    {
        this.define = define;
        this.controller = controller;
        attributes = new Attributes(define, this);
        weaponManager = new WeaponManager(this);
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
    public void AttackImmediately()
    {
        weaponManager.AttackImmediately();
    }
    protected virtual void OnAttack() { }
    public virtual void Update()
    {
        weaponManager.Update();
    }
    public void ReceiveDamage(Battle.BattleContext context)
    {
        //If Dodged
        float dodge = attributes.curAttribute.Dodge;
        float random_dodge = Random.Range(0, 1f);
        if(random_dodge< dodge)
        {
            //dodged
            return;
        }
        var damage = context.weapon.Damage * context.attacker.attributes.curAttribute.DamageRatio * (1 - attributes.curAttribute.DamageResistence);
        OnDamage(damage, context.attacker);
    }

    public virtual void OnDamage(float damage, Creature attacker)
    {
        Debug.LogFormat("Take Damage:[{0}]", damage.ToString());
    }
}
