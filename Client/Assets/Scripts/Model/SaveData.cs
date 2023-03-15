using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Model
{
    public struct PXVector3
    {
        public float x;
        public float y;
        public float z;
        public PXVector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public static implicit operator PXVector3(Vector3 v) { return new PXVector3(v.x, v.y, v.z); }
        public static implicit operator Vector3(PXVector3 v) { return new Vector3(v.x, v.y, v.z); }
    }
    public class SaveData
    {
        public Model.Attribute playerAttri;//角色基本数据
        public List<int[]> gainedTitleData;//第一列 title ID，第二列 level
        public List<int> equipedTitle;//身上装备的称号
        public int sceneIndex;//场景ID
        public PXVector3 playerPos;//玩家位置
    }
}
