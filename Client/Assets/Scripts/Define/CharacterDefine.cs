using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Define
{
    public class CharacterDefine
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float HP { get; set; }
        public float DamageResistence { get; set; }//伤害抗性
        public float Dodge { get; set; }//闪避
        public float MoveVelocityRatio { get; set; }//移动速度倍率
        public float MaxMoveVelocityRatio { get; set; }//最大移动速度倍率
        public float DamageRatio { get; set; } //伤害倍率
        public float GoldRatio { get; set; } //金币获得倍率
        public float ExpRatio { get; set; }//经验获得倍率
        public int ExpEarn { get; set; }//击杀获得经验
        public int GoldEarn { get; set; }//击杀获得金币
    }
}
