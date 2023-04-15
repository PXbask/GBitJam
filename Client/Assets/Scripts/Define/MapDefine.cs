using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Define
{
    public class MapDefine
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<float> StartPosition { get; set; }
        public bool HasTortise { get; set; }
        public string InterludeText { get; set; }
    }
}
