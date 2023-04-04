using Define;
using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Player: CharBase
    {
        public int level => UserManager.Instance.level;
        public int exp => UserManager.Instance.exp;
        public int load => UserManager.Instance.Load;
        public int gold => UserManager.Instance.gold;
        public int parts => UserManager.Instance.parts;
        public Player(CharacterDefine define) : base(define) { }
    }
}
