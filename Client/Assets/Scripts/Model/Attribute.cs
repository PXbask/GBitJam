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
        public float GoldRatio { get; set; }//��Ҽӳ�
        public float ExpRatio { get; set; }//����ӳ�
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

