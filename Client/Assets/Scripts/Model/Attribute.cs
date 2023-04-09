using Define;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/*
    Date:
    Name:
    Overview:玩家的各项属性
*/
namespace Model
{
    public class Attribute
    {
        public float HP { get; set; }
        public float DamageResistence { get; set; }//伤害抗性
        public float Dodge { get; set; }//闪避
        public float MoveVelocityRatio { get; set; }//移动速度倍率
        public float DamageRatio { get; set; } //伤害倍率
        public float GoldRatio { get; set; }//金币加成
        public float ExpRatio { get; set; }//经验加成
        public Attribute() { }
        public Attribute(CharacterDefine define)
        {
            this.HP= define.HP;
            this.DamageResistence= define.DamageResistence;
            this.Dodge= define.Dodge;
            this.MoveVelocityRatio= define.MoveVelocityRatio;
            this.DamageRatio= define.DamageRatio;
            this.GoldRatio = define.GoldRatio;
            this.ExpRatio = define.ExpRatio;
        }
        public void Copy(Attribute attri)
        {
            this.HP = attri.HP;
            this.DamageResistence = attri.DamageResistence;
            this.Dodge = attri.Dodge;
            this.MoveVelocityRatio = attri.MoveVelocityRatio;
            this.DamageRatio = attri.DamageRatio;
            this.GoldRatio = attri.GoldRatio;
            this.ExpRatio = attri.ExpRatio;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("Hp : [{0}]", HP));
            sb.AppendLine(string.Format("DamageRatio : [{0}]", DamageRatio));
            sb.AppendLine(string.Format("Dodge : [{0}]", Dodge));
            sb.AppendLine(string.Format("MoveVelocityRatio : [{0}]", MoveVelocityRatio));
            sb.AppendLine(string.Format("DamageResistence : [{0}]", DamageResistence));
            sb.AppendLine(string.Format("GoldRatio : [{0}]", GoldRatio));
            sb.AppendLine(string.Format("ExpRatio : [{0}]", ExpRatio));
            return sb.ToString();
        }
    }
}

