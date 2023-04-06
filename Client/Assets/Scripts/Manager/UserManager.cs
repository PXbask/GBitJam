﻿using Model;
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
        public Action OnPlayerGoldChanged = null;
        public Action OnPlayerPartChanged = null;
        public Action OnPlayerLoadChanged = null;
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
                OnPlayerLoadChanged?.Invoke();
            }
        }

        private int gold;
        public int Gold
        {
            get => gold;
            set
            {
                gold = value;
                OnPlayerGoldChanged?.Invoke();
            }
        }
        private int parts;
        public int Parts
        {
            get => parts;
            set
            {
                parts = value;
                OnPlayerPartChanged?.Invoke();
            }
        }

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
            Gold = DataManager.Instance.SaveData.gold;
            Parts = DataManager.Instance.SaveData.parts;
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
            Level++;
            GetMaxParams();
            Exp = 0;
            Debug.LogFormat("角色升级:当前等级:{0}", Level.ToString());
        }
        ~UserManager()
        {
            OnPlayerLevelChanged -= GetMaxParams;
        }
    }
}
