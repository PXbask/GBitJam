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
        public Attributes(CharacterDefine define)
        {
            baseAttribute = new Attribute(define);
            curAttribute = new Attribute(define);
            Recalculate();
        }

        public void Recalculate()
        {
            float curHp = curAttribute.HP;
            curAttribute.Copy(baseAttribute);
            curAttribute.HP = curHp;
            //TODO:计算curAttribute属性
            foreach (var title in TitleManager.Instance.EquipedTitle)
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
