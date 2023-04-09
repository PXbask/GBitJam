using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace AI
{
    public class AIAgent
    {
        private Enemy enemy;
        private AIBase ai;
        public Creature Target
        {
            get => ai.Target;
            set
            {
                ai.Target = value;
                Debug.LogFormat("当前目标:[{0}]", value.define.Name);
            }
        }
        public AIAgent(Enemy enemy)
        {
            this.enemy = enemy;
            string aiName = enemy.define.Name;

            switch (aiName)
            {
                case "普通敌人":
                    this.ai = new AIBase(this.enemy);
                    break;
                case "精英敌人":
                    this.ai = new AIBase(this.enemy);
                    break;
                case "BOSS":
                    this.ai = new AIBoss(this.enemy);
                    break;
                default:
                    break;
            }
        }

        internal void Update()
        {
            if (this.ai != null)
            {
                this.ai.Update();
            }
        }
    }
}
