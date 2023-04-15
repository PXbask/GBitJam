using Define;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Model
{
    public class PXSceneInfo
    {
        public bool isFirstEntered;
        public MapDefine define;
        public Vector3 startPosition;
        public bool isFirstScene => define.ID == 1;
        public PXSceneInfo(MapDefine define)
        {
            this.define = define;
            
            isFirstEntered= true;
            startPosition = new Vector3(define.StartPosition[0], define.StartPosition[1], define.StartPosition[2]);
        }
    }
}
