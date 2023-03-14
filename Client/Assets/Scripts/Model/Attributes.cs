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
        public void Recalculate()
        {
            baseAttribute = new Attribute()
            {
                HP = 100,
                SPD = 4f,
                ATK = 100,
                DEF = 50
            };
            curAttribute = new Attribute();
            curAttribute.SPD = baseAttribute.SPD;
            curAttribute.HP = baseAttribute.HP;
            curAttribute.ATK = baseAttribute.ATK;
            curAttribute.DEF = baseAttribute.DEF;
            foreach (var title in TitleManager.Instance.EquipedTitle)
            {
                curAttribute.ATK += baseAttribute.ATK * title.define.ATKratio;
                curAttribute.DEF += baseAttribute.DEF * title.define.DEFratio;
            }

        }
    }
}
