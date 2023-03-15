using Define;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ItemInfo
    {
        public int ID;
        public int Count;
        public ItemDefine Define;
        public ItemInfo(int itemID, int count = 1)
        {
            ID = itemID;
            Count = count;
        }
        public override string ToString()
        {
            return string.Format("ID:{0}, Count:{1}", this.ID, this.Count);
        }
    }

}
