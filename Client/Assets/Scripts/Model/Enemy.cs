using AI;
using Define;
using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityEngine.EventSystems.EventTrigger;

namespace Model
{
    public class Enemy : Creature
    {
        public AIAgent agent;
        public EnemyAttackStyle attackStyle;
        public BattleStatus battleStatus;
        public Enemy(CharacterDefine define, IAttackable attackable): base(define, attackable)
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
        public void SetEquips(List<TitleInfo> values, EnemyAttackStyle style)
        {
            this.titles = values;
            this.attackStyle = style;
            weaponManager.SetWeapon((int)style);
        }
        public override void Update()
        {
            base.Update();
            agent.Update();
        }
    }
}
