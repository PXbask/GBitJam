using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AI
{
    public class AIBase
    {
        private Enemy Owner;
        public Creature Target;
        public AIBase(Enemy enemy)
        {
            this.Owner = enemy;
        }

        internal void Update()
        {
            if (this.Owner.battleStatus == BattleStatus.InBattle)
            {
                this.UpdateBattle();
            }
        }

        private void UpdateBattle()
        {
            if (this.Target == null)
            {
                this.Owner.battleStatus = BattleStatus.Idle;
                return;
            }
            if (!this.TryAttack())
            {
                this.Move();
            }
        }
        //进行攻击条件检测，若是则攻击
        private bool TryAttack()
        {
            //如果大概在一条水平线时，则攻击
            var rb = Owner.controller.GetRigidbody();
            var trb = Target.controller.GetRigidbody();
            if(Math.Abs(rb.position.x - trb.position.x) <= 0.5f)
            {
                Owner.Attack();
                return true;
            }
            else
                return false;
        }
        private void Move()
        {
            Owner.controller.Move();
        }
    }
}
