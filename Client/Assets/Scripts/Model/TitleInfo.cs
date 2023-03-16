using Define;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class TitleInfo
    {
        public TitleDefine define;
        public Dictionary<int,TitleAffectDefine> affects;
        public int ID;
        public int level;

        public TitleAffectDefine curAffect => affects[level];
        public TitleInfo(int ID, int level)
        {
            this.ID = ID;
            this.level = level;
            this.define = DataManager.Instance.Titles[ID];
            if (define.TitleAffect != 0)
                this.affects = DataManager.Instance.TitleAffects[define.TitleAffect];
        }
    }
}
