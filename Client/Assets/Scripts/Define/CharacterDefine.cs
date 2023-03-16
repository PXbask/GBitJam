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
        public float Accuracy { get; set; }//命中率
        public float Dodge { get; set; }//闪避
        public float MoveVelocityRatio { get; set; }//移动速度倍率
        public float AttackVelocityRatio { get; set; }//攻击速度倍率
        public float DamageRatio { get; set; } //伤害倍率
        public float Damage { get; set; }//伤害

    }
}
