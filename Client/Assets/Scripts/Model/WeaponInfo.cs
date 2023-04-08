using Define;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Model
{
    public class WeaponInfo
    {
        public int ID => define.ID;
        public WeaponDefine define;
        private float m_timer;
        public bool equiped;
        public bool IsUnderCooling
        {
            get => m_timer >= 0;
        }
        public WeaponInfo(WeaponDefine define)
        {
            this.define = define;
            m_timer = define.Interval;
        } 
        public void Update()
        {
            if(equiped) 
                m_timer -= Time.deltaTime;
        }
        public void Fire()
        {
            if (IsUnderCooling) return;
            switch(ID)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default: break;
            }
        }
    }
}
