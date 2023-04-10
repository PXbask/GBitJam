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
            Owner.Attack();
            return true;
        }
        private void Move()
        {
            //UnityEngine.Debug.Log("Move");
        }
    }
}
