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
        public Player playerdata;
        public int level;
        public int exp;
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

        public bool isOverLoad = false;
        public void Init()
        {
            level = DataManager.Instance.SaveData.playerLevel;
            OnLevelUp();

            exp = DataManager.Instance.SaveData.exp;
            Load = DataManager.Instance.SaveData.load;
            gold = DataManager.Instance.SaveData.gold;
            parts = DataManager.Instance.SaveData.parts;
        }
        public void OnLevelUp()
        {
            loadMax = DataManager.Instance.Levels[Consts.Player.ID][level].LoadMax;
            slotMax = DataManager.Instance.Levels[Consts.Player.ID][level].SlotNum;
            exp2NextLevel = DataManager.Instance.Levels[Consts.Player.ID][level].ExpCost;
        }
    }
}
