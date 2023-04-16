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
        public Action OnPlayerGoldChanged = null;
        public Action OnPlayerPartChanged = null;
        public Action OnPlayerLoadChanged = null;
        public Action OnPlayerDead = null;
        public Action OnPlayerWeaponConfigChanged = null;
        public Action OnPlayerTargetChanged = null;

        public Player playerlogic;
        private float Hp;
        public float HP
        {
            get { return Hp; }
            set
            {
                Hp = value;
                if (playerlogic != null) playerlogic.attributes.curAttribute.HP = value;
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
        private Creature targetEnemy;
        public Creature TargetEnemy
        {
            get => targetEnemy;
            set
            {
                targetEnemy = value;
                OnPlayerTargetChanged?.Invoke();
            }
        }
        public int exp2NextLevel;
        public int loadMax;
        public int slotMax;
        public int hpMax;

        public bool isOverLoad = false;
        public WeaponManager weaponManager => playerlogic.weaponManager;
        public WeaponInfo CurrentWeapon => weaponManager.WeaponConfig;

        internal void Reset()
        {
            OnPlayerHpChanged = null;
            OnPlayerLevelChanged = null;
            OnPlayerExpChanged = null;
            OnPlayerGoldChanged = null;
            OnPlayerPartChanged = null;
            OnPlayerLoadChanged = null;
            OnPlayerDead = null;
            OnPlayerWeaponConfigChanged = null;
            OnPlayerTargetChanged = null;

            playerlogic = null;
            targetEnemy= null;
        }
        public void Init()
        {
            OnPlayerLevelChanged += GetMaxParams;
            TitleManager.Instance.OnTitleEquiped += OnTitleEquiped;
            TitleManager.Instance.OnTitleUnEquiped += OnTitleUnEquiped;

            Level = DataManager.Instance.SaveData.playerLevel;
            HP = DataManager.Instance.SaveData.Hp;
            Exp = DataManager.Instance.SaveData.exp;
            Load = DataManager.Instance.SaveData.load;
            Gold = DataManager.Instance.SaveData.gold;
            Parts = DataManager.Instance.SaveData.parts;
        }

        private void OnTitleEquiped(int obj)
        {
            Load = Math.Clamp(Load + Consts.Title.Equip_Load, 0, loadMax);
            OnPlayerLoadChanged?.Invoke();
        }

        private void OnTitleUnEquiped(int obj)
        {
            Load = Math.Clamp(Load + Consts.Title.UnEquip_Load, 0, loadMax);
            OnPlayerLoadChanged?.Invoke();
        }

        private void GetMaxParams()
        {
            hpMax = 100;
            loadMax = DataManager.Instance.Levels[Consts.Character.PlayerID][Level].LoadMax;
            slotMax = DataManager.Instance.Levels[Consts.Character.PlayerID][Level].SlotNum;
            exp2NextLevel = DataManager.Instance.Levels[Consts.Character.PlayerID][Level].ExpCost;
        }
        public void OnLevelUp()
        {
            Level++;
            GetMaxParams();
            Exp = 0;
            Debug.LogFormat("角色升级:当前等级:{0}", Level.ToString());
        }
        public void OnWeaponConfigChanged()
        {
            OnPlayerWeaponConfigChanged?.Invoke();
        }

        ~UserManager()
        {
            OnPlayerLevelChanged -= GetMaxParams;
            weaponManager.OnWeaponConfigChanged -= OnWeaponConfigChanged;
        }
    }
}
