using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class CharacterManager:Singleton<CharacterManager>
    {
        public List<Creature> Characters = new List<Creature>();
        public void Init() { }
        public void AddCharacter(Creature cha)
        {
            if (!Characters.Contains(cha))
            {
                this.Characters.Add(cha);
            }
        }
        public void RemoveCharacter(Creature cha)
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
