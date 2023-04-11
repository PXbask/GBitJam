using Define;
using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Model
{
    public class Enemy : Creature
    {
        public EnemyAttackStyle attackStyle;
        public BattleStatus battleStatus;
        public Enemy(CharacterDefine define, EnemyController attackable): base(define, attackable)
        {
            
        }
        protected override void OnAttack()
        {
            var weapon = weaponManager.WeaponConfig;
            switch (weapon.ID)
            {
                case 1://刀
                    controller.Melee_Attack();
                    break;
                case 2://步枪
                    controller.Rifle_Attack();
                    break;
                case 3://霰弹枪
                    controller.ShotGun_Attack();
                    break;
                default: break;
            }
        }
        public void SetEquips(List<TitleInfo> values, EnemyAttackStyle style)
        {
            this.titles = values;
            this.attackStyle = style;
            weaponManager.SetWeapon((int)style);
        }
        public override void Update()
        {
            base.Update();
        }
        public override void OnDamage(float damage)
        {
            base.OnDamage(damage);
            attributes.curAttribute.HP -= damage;
            UserManager.Instance.TargetEnemy = this;
            if (attributes.curAttribute.HP <= 0)
            {
                controller.OnDeath();
                UserManager.Instance.Exp += define.ExpEarn;
                UserManager.Instance.Exp += define.GoldEarn;
                Debug.Log("Enemy Dead");
            }
        }
    }
}
