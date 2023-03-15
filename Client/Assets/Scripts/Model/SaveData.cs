using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Model
{
    public class SaveData
    {
        public Model.Attribute playerAttri;//角色基本数据
        public List<int[]> gainedTitleData;//第一列 title ID，第二列 level
        public List<int> equipedTitle;//身上装备的称号
        public List<int[]> gainedItemData;//第一列 item ID，第二列 count
        public int sceneIndex;//场景ID
        public PXVector3 playerPos;//玩家位置
    }
}
