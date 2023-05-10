using Define;
using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Model
{
    public class Player: Creature
    {
        public int Level => UserManager.Instance.Level;
        public int Exp => UserManager.Instance.Exp;
        public int Load => UserManager.Instance.Load;
        public int Gold => UserManager.Instance.Gold;
        public int Parts => UserManager.Instance.Parts;

        public Player(CharacterDefine define, PlayerController attackable) : base(define, attackable)
        {
            titles = TitleManager.Instance.EquipedTitle;
            UserManager.Instance.playerlogic = this;

            TitleManager.Instance.OnTitleEquiped += this.attributes.Recalculate;
            TitleManager.Instance.OnTitleUnEquiped += this.attributes.Recalculate;
            attributes.Recalculate();
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
        public override void OnDamage(float damage, Creature attacker)
        {
            base.OnDamage(damage, attacker);
            if (invincible) return;
            UserManager.Instance.HP -= Mathf.CeilToInt(damage);
            UserManager.Instance.SetInvincibleInterval(1);
            if(UserManager.Instance.HP <= 0)
            {
                controller.OnDeath();
            }
        }
    }
}
