using DG.Tweening;
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
        public Action OnPlayerHurt = null;

        public Player playerlogic;
        public GameObject playerObject => playerlogic.controller.gameObject;
        public PlayerController playerController => playerlogic.controller as PlayerController;
        private float Hp;
        public float HP
        {
            get { return Hp; }
            set
            {
                if (Hp > value) OnPlayerHurt?.Invoke();
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
                if (value >= exp)
                {
                    int remain = value;
                    while(remain >= exp2NextLevel)
                    {
                        remain -= exp2NextLevel;
                        OnLevelUp();
                    }
                    exp = remain;
                }
                else
                {
                    exp = value;
                }
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

        public Vector3 checkPoint;

        public Camera GetPlayerBindingCamera()
        {
            return (playerlogic?.controller as PlayerController).mainCamera ?? Camera.main;
        } 

        public void SetInvincible(bool b)
        {
            playerlogic.invincible= b;
        }
        /** Turn the player to invincible, searval seconds later turn to vincible */
        public void SetInvincibleInterval(float dur)
        {
            Sequence sequence = DOTween.Sequence();

            sequence.AppendCallback(() => SetInvincible(true))
                    .AppendInterval(dur)
                    .AppendCallback(() => SetInvincible(false));

            sequence.SetLoops(1).Play();
        }

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

            //Level = DataManager.Instance.SaveData.playerLevel;
            //HP = DataManager.Instance.SaveData.Hp;
            //Exp = DataManager.Instance.SaveData.exp;
            //Load = DataManager.Instance.SaveData.load;
            //Gold = DataManager.Instance.SaveData.gold;
            //Parts = DataManager.Instance.SaveData.parts;

            Level = 1;
            HP = DataManager.Instance.PlayerDefine.HP;
            checkPoint = Utils.GetDefaultPointPosition();
            Exp = 0;
            Load = 0;
            Gold = 0;
            Parts = 0;
        }

        public void ReBorn()
        {
            HP = DataManager.Instance.PlayerDefine.HP;
            Load = 0;
            playerObject.transform.position = checkPoint;

            InputManager.Activate();
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
            SoundManager.Instance.PlayLevelUpSound();
        }
        public void OnWeaponConfigChanged()
        {
            OnPlayerWeaponConfigChanged?.Invoke();
        }

        public void SetCheckPoint(Vector3 pos)
        {
            this.checkPoint = pos;
            UIManager.Instance.AddGainMessage("检查点更新");
        }

        ~UserManager()
        {
            OnPlayerLevelChanged -= GetMaxParams;
            weaponManager.OnWeaponConfigChanged -= OnWeaponConfigChanged;
        }
    }
}
