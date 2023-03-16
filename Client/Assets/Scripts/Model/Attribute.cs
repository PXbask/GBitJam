using Define;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview:��ҵĸ�������
*/
namespace Model
{
    public class Attribute
    {
        public float HP { get; set; }
        public float DamageResistence { get; set; }//�˺�����
        public float Accuracy { get; set; }//������
        public float Dodge { get; set; }//����
        public float MoveVelocityRatio { get; set; }//�ƶ��ٶȱ���
        public float AttackVelocityRatio { get; set; }//�����ٶȱ���
        public float DamageRatio { get; set; } //�˺�����
        public float Damage { get; set; }//�˺�
        public Attribute() { }
        public Attribute(CharacterDefine define)
        {
            this.HP= define.HP;
            this.DamageResistence= define.DamageResistence;
            this.Accuracy= define.Accuracy;
            this.Dodge= define.Dodge;
            this.MoveVelocityRatio= define.MoveVelocityRatio;
            this.AttackVelocityRatio= define.AttackVelocityRatio;
            this.DamageRatio= define.DamageRatio;
            this.Damage = define.Damage;
        }
        public void Copy(Attribute attri)
        {
            this.HP = attri.HP;
            this.DamageResistence = attri.DamageResistence;
            this.Accuracy = attri.Accuracy;
            this.Dodge = attri.Dodge;
            this.MoveVelocityRatio = attri.MoveVelocityRatio;
            this.AttackVelocityRatio = attri.AttackVelocityRatio;
            this.DamageRatio = attri.DamageRatio;
            this.Damage = attri.Damage;
        } 
    }
}

