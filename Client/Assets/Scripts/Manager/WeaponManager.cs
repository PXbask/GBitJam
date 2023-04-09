using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Manager
{
    public class WeaponManager
    {
        public Creature owner;
        public Action OnWeaponConfigChanged = null;
        public Dictionary<int, WeaponInfo> WeaponMap;
        public WeaponInfo WeaponConfig;
        public WeaponManager(Creature owner)
        {
            this.owner = owner;
            WeaponMap = new Dictionary<int, WeaponInfo>();

            if (owner.IsPlayer)
            {
                TitleManager.Instance.OnTitleEquiped += OnTitleEquiped;
                TitleManager.Instance.OnTitleUnEquiped += OnTitleUnEquiped;
                OnWeaponConfigChanged += UserManager.Instance.OnWeaponConfigChanged;
            }
        }
        ~WeaponManager()
        {
            owner = null;
            if (owner.IsPlayer)
            {
                TitleManager.Instance.OnTitleEquiped -= OnTitleEquiped;
                TitleManager.Instance.OnTitleUnEquiped -= OnTitleUnEquiped;
            }
        }
        public void OnTitleEquiped(int id)
        {
            var info = TitleManager.Instance.GetTitleInfoByID(id);
            if(info.define.TitleType==TitleType.Attack)
            {
                int wid = info.define.LindedWeapon;
                if (wid!=0 && !WeaponMap.ContainsKey(wid))
                {
                    WeaponMap.Add(wid, new WeaponInfo(DataManager.Instance.Weapons[wid], owner));
                }
                WeaponConfig = WeaponMap[wid];
                OnWeaponConfigChanged?.Invoke();
            }
        }
        private void OnTitleUnEquiped(int id)
        {
            var info = TitleManager.Instance.GetTitleInfoByID(id);
            if (info.define.TitleType == TitleType.Attack)
            {
                WeaponConfig = null;
                OnWeaponConfigChanged?.Invoke();
            }
        }
        /// <summary>
        /// 仅对敌人有用
        /// </summary>
        /// <param name="id">武器对应ID</param>
        public void SetWeapon(int id)
        {
            if (owner.IsPlayer) return;
            WeaponConfig = new WeaponInfo(DataManager.Instance.Weapons[id], owner);
        }
        public void Update()
        {
            WeaponConfig?.Update();
        }
    }
}
