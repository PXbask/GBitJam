﻿using AI;
using Define;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Enemy : Creature
    {
        public AIAgent agent;
        public BattleStatus battleStatus;
        public Enemy(CharacterDefine define): base(define)
        {
            agent = new AIAgent(this);
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
        public void SetTitles(List<TitleInfo> values)
        {
            this.titles = values;
        }
        public override void Update()
        {
            base.Update();
            agent.Update();
        }
    }
}
