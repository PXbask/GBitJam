using Define;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Model
{
    public class SkillInfo
    {
        public TitleDefine define;
        public float timer;
        public bool IsUnderCooling => timer > 0;
        public float CoolingDown => define.CoolingDown;
        public int ID => define.ID;
        public SkillInfo(TitleDefine define)
        { 
            this.define = define;
            timer= 0;
        }
        public void Update()
        {
            timer -= Time.deltaTime;
        }
        public void OnCast()
        {
            timer = CoolingDown;
        }
    }
}
