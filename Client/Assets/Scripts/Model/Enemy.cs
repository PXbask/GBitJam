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
            (controller as EnemyController).SetHeadDir();
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
        public override void OnDamage(float damage, Creature attacker)
        {
            base.OnDamage(damage, attacker);
            EnemyController owncontroller = controller as EnemyController;
            attributes.curAttribute.HP -= damage;

            UserManager.Instance.TargetEnemy = this;

            if(owncontroller == null || owncontroller.attackTarget != attacker)
            {
                owncontroller.SetTarget(attacker);
            }
            if (attributes.curAttribute.HP <= 0)
            {
                controller.OnDeath();
                UserManager.Instance.Exp += define.ExpEarn;
                UserManager.Instance.Gold += define.GoldEarn;
                Debug.Log("Enemy Dead");
            }
        }
    }
}
