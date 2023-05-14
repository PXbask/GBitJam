using Define;
using Manager;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Model
{
    public class Boss : Creature
    {
        public EnemyAttackStyle attackStyle;
        public Boss(CharacterDefine define, BossController attackable) : base(define, attackable)
        {

        }
        protected override void OnAttack()
        {
            var weapon = weaponManager.WeaponConfig;
            var bcontroller = controller as BossController;
            switch (weapon.ID)
            {
                case 1://刀
                    bcontroller.Melee_AttackPerformance();
                    break;
                case 2://步枪
                    bcontroller.ShotGun_AttackPerformance();
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
            BossController owncontroller = controller as BossController;

            if (!owncontroller.activited) return;

            attributes.curAttribute.HP -= damage * 2.5f;
            owncontroller.OnHurt();

            UserManager.Instance.TargetEnemy = this;

            if (owncontroller == null || owncontroller.attackTarget != attacker)
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

            if (owncontroller.isinFoucs)
            {
                owncontroller.Stop(3f);
            }
        }
    }
}
