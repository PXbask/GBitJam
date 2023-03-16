using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Define
{
    public class WeaponDefine
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Knockback { get; set; }
        public int ClipCount { get; set; }
        public float Interval { get; set; }
        public float Range { get; set; }
        public float Damage { get; set; }
    }
}
