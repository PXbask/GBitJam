using Define;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Attributes
    {
        public Attribute baseAttribute;
        public Attribute curAttribute;
        public Creature owner;
        public Attributes(CharacterDefine define, Creature owner)
        {
            this.owner= owner;
            baseAttribute = new Attribute(define);
            curAttribute = new Attribute(define);
            Recalculate();
        }
        public void Recalculate(int _) { Recalculate(); }
        public void Recalculate()
        {
            if (owner.titles == null) return;
            float curHp = curAttribute.HP;
            curAttribute.Copy(baseAttribute);
            curAttribute.HP = curHp;
            //TODO:计算curAttribute属性
            foreach (var title in owner.titles)
            {
                if(title.define.TitleType==TitleType.Assist)
                {
                    curAttribute.DamageRatio += title.curAffect.DamageGainV;
                    curAttribute.Dodge += title.curAffect.DodgeGainV;
                    curAttribute.MoveVelocityRatio += title.curAffect.MoveGainV;
                    curAttribute.GoldRatio += title.curAffect.GoldGainV;
                    curAttribute.ExpRatio += title.curAffect.ExpGainV;
                    curAttribute.DamageResistence += title.curAffect.DamgeResistenceGainV;
                }
            }
        }
    }
}
