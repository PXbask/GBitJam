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
        public int ID => define.ID;
        public int level;
        public TitleInfo(TitleDefine define, int level)
        {
            this.define = define;
            this.level = level;
        }
    }
}
