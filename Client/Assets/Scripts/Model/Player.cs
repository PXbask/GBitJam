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
        public int level => UserManager.Instance.Level;
        public int exp => UserManager.Instance.Exp;
        public int load => UserManager.Instance.Load;
        public int gold => UserManager.Instance.Gold;
        public int parts => UserManager.Instance.Parts;
        public Player(CharacterDefine define) : base(define) { }
    }
}
