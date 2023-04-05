using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Manager
{
    public class UserManager : Singleton<UserManager>
    {
        public Action OnPlayerHpChanged = null; 
        public Action OnPlayerLevelChanged = null; 
        public Action OnPlayerExpChanged = null;
        public Action OnPlayerDead = null;

        public Player playerdata;
        private int Hp;
        public int HP
        {
            get { return Hp; }
            set
            {
                Hp = value;
                if (playerdata != null) playerdata.attributes.curAttribute.HP = value;
                OnPlayerHpChanged?.Invoke();
                if (value <= 0)
                    OnPlayerDead?.Invoke();
            }
        }

        private int level;
        public int Level
        {
            get => level;
            set
            {
                level = value;
                OnPlayerLevelChanged?.Invoke();
            }
        }
        private int exp;
        public int Exp {
            get => exp;
            set
            {
                exp = value;
                OnPlayerExpChanged?.Invoke();
            }
        }
        private int load;
        public int Load
        {
            get => load;
            set
            {
                if (value >= this.loadMax)
                {
                    Debug.Log("玩家已过载");
                    isOverLoad = true;
                }
                else if(value <= 0)
                {
                    Debug.Log("过载状态恢复");
                    isOverLoad = false;
                }
                load = value;
            }
        }

        public int gold;
        public int parts;

        public int exp2NextLevel;
        public int loadMax;
        public int slotMax;
        public int hpMax;

        public bool isOverLoad = false;
        public void Init()
        {
            OnPlayerLevelChanged += GetMaxParams;

            Level = DataManager.Instance.SaveData.playerLevel;
            HP = DataManager.Instance.SaveData.Hp;
            Exp = DataManager.Instance.SaveData.exp;
            Load = DataManager.Instance.SaveData.load;
            gold = DataManager.Instance.SaveData.gold;
            parts = DataManager.Instance.SaveData.parts;
        }
        private void GetMaxParams()
        {
            hpMax = 100;
            loadMax = DataManager.Instance.Levels[Consts.Player.ID][Level].LoadMax;
            slotMax = DataManager.Instance.Levels[Consts.Player.ID][Level].SlotNum;
            exp2NextLevel = DataManager.Instance.Levels[Consts.Player.ID][Level].ExpCost;
        }
        public void OnLevelUp()
        {
            level++;
            GetMaxParams();
            exp = 0;
        }
        ~UserManager()
        {
            OnPlayerLevelChanged -= GetMaxParams;
        }
    }
}
