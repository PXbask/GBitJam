using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class CharacterManager:Singleton<CharacterManager>
    {
        public List<CharBase> Characters = new List<CharBase>();
        public void Init() { }
        public void AddCharacter(CharBase cha)
        {
            if (!Characters.Contains(cha))
            {
                this.Characters.Add(cha);
            }
        }
        public void RemoveCharacter(CharBase cha)
        {
            this.Characters.Remove(cha);
        }
        public void Update()
        {
            for(int i = 0; i < Characters.Count; i++)
            {
                Characters[i].Update();
            }
        }
    }
}
