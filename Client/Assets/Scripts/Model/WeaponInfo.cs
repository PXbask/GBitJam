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
        public Creature owner;
        public int ID => define.ID;
        public WeaponDefine define;
        private float m_timer;
        public bool equiped;
        public bool IsUnderCooling
        {
            get => m_timer >= 0;
        }
        public WeaponInfo(WeaponDefine define, Creature owner)
        {
            this.define = define;
            this.owner = owner;
            m_timer = define.Interval;
        } 
        public void Update()
        {
            m_timer -= Time.deltaTime;
        }
        public void OnFire()
        {
            m_timer = define.Interval;
        }
    }
}
