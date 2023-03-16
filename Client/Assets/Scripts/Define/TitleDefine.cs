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
        public bool EnemyCanEquip { get; set; }//敌人是否可拥有
        public bool EnemyCanFall { get; set; }//敌人是否可掉落
        public int LinkedAchievement { get; set; }//关联成就
        public int LinkedQuest { get; set; }//关联任务
        public bool IsConsumable { get; set; }//是否消耗
        public int TitleAffect { get; set; }//对应的TitleAffect类的ID号
    }
}
