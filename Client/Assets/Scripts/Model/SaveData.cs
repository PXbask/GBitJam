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
        public List<int[]> alltitlesData;//第一列 title ID，第二列 level, 第三列 (1:已获得,0:未获得)
        public List<int> equipedTitle;//身上装备的称号
        public int sceneIndex;//场景ID
        public PXVector3 playerPos;//玩家位置
        public int playerLevel;//玩家等级
        public int exp;//当前经验
        public int load;//负载值
        public int gold;//金币
        public int parts;//魂芯原件
    }
}
