using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Define
{
    public class TitleDefine
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public int Size { get; set; }//占有空间
        public int Quality { get; set; }//稀有度
        public TitleType TitleType { get; set; }
        public SkillTitleType SkillType { get; set; }
        public bool EnemyCanEquip { get; set; }//敌人是否可拥有
        public bool EnemyCanFall { get; set; }//敌人是否可掉落
        public bool IsConsumable { get; set; }//是否消耗
        public float CoolingDown { get; set; }
        public int TitleAffect { get; set; }//对应的TitleAffect类的ID号
        public int LindedWeapon { get; set; }//对应的武器ID
        public float DropRate { get; set; }//掉落概率
        public int PartsBorn { get; set; }//碎片产出数量
        public string IconPath { get; set; }

        public static int[,] GoldCost = new int[3, 3]
        {
            {100,400,500 },
            {200,500,700 },
            {500,700,900 }
        };
        public static int[,] PartCost = new int[3, 3]
        {
            {10,15,25 },
            {15,20,35 },
            {25,30,45 }
        };
    }
}
