﻿using Define;
using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Player: Creature
    {
        public int Level => UserManager.Instance.Level;
        public int Exp => UserManager.Instance.Exp;
        public int Load => UserManager.Instance.Load;
        public int Gold => UserManager.Instance.Gold;
        public int Parts => UserManager.Instance.Parts;
        public Player(CharacterDefine define) : base(define)
        {
            titles = TitleManager.Instance.EquipedTitle;
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
    }
}
