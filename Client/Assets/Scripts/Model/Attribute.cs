using Define;
using System.Collections;
using System.Collections.Generic;
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
        public float Accuracy { get; set; }//命中率
        public float Dodge { get; set; }//闪避
        public float MoveVelocityRatio { get; set; }//移动速度倍率
        public float AttackVelocityRatio { get; set; }//攻击速度倍率
        public float DamageRatio { get; set; } //伤害倍率
        public float Damage { get; set; }//伤害
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
    }
}

