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
    public class WeaponManager : Singleton<WeaponManager>
    {
        public Action OnWeaponConfigChanged = null;
        public Dictionary<int, WeaponInfo> WeaponMap= new Dictionary<int, WeaponInfo>();
        public WeaponInfo WeaponConfig;
        public void Init()
        {
            TitleManager.Instance.OnTitleEquiped += OnTitleEquiped;
            TitleManager.Instance.OnTitleUnEquiped += OnTitleUnEquiped;
        }

        ~WeaponManager()
        {
            TitleManager.Instance.OnTitleEquiped -= OnTitleEquiped;
            TitleManager.Instance.OnTitleUnEquiped -= OnTitleUnEquiped;
        }
        public void OnTitleEquiped(int id)
        {
            var info = TitleManager.Instance.GetTitleInfoByID(id);
            if(info.define.TitleType==TitleType.Attack)
            {
                int wid = info.define.LindedWeapon;
                if (wid!=0 && !WeaponMap.ContainsKey(wid))
                {
                    WeaponMap.Add(wid, new WeaponInfo(DataManager.Instance.Weapons[wid]));
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
        public void Update()
        {
            foreach (var info in WeaponMap.Values)
            {
                info.Update();
            }
        }
    }
}
