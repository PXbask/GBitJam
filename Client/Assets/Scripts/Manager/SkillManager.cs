using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Manager
{
    public class SkillManager : Singleton<SkillManager>
    {
        public List<SkillInfo> skillTitles= new List<SkillInfo>(1);//只能存放1个技能，两个被动
        public List<TitleInfo> passiveTitles = new List<TitleInfo>(1);

        public Action OnSkillTitleChanged = null;
        public SkillManager() 
        {

        }
        internal void Reset()
        {
            OnSkillTitleChanged = null;

            TitleManager.Instance.OnTitleEquiped -= OnTitleEquiped;
            TitleManager.Instance.OnTitleUnEquiped -= OnTitleUnEquiped;

            skillTitles.Clear();
            passiveTitles.Clear();
        }
        public void Init()
        {
            TitleManager.Instance.OnTitleEquiped += OnTitleEquiped;
            TitleManager.Instance.OnTitleUnEquiped += OnTitleUnEquiped;

            ReloadSkill();
        }
        public void ReloadSkill()
        {
            skillTitles.Clear();
            passiveTitles.Clear();
            foreach (var item in TitleManager.Instance.EquipedTitle)
            {
                if (item.define.SkillType == SkillTitleType.Positive)
                    skillTitles.Add(new SkillInfo(item.define));
            }
        }
        public void CastSkill(int index)
        {
            if (skillTitles.Count < index || skillTitles[index-1] == null) return;
            var skill = skillTitles[index-1];
            if (skill.IsUnderCooling)
            {
                UIManager.Instance.AddWarning("技能还在冷却中");
                return;
            }
            switch (skill.ID)
            {
                case 80://纳米修复器
                    var maxHP = UserManager.Instance.playerlogic.attributes.baseAttribute.HP;
                    var cure = maxHP * 0.2f;
                    UserManager.Instance.HP = Math.Clamp(UserManager.Instance.HP + cure, 0, maxHP);
                    break;
                case 81://钻头
                    if (UserManager.Instance.playerlogic.controller is PlayerController playerController) playerController.TryDestroyObstacle();
                    break;
                default: break;
            }
            skill.OnCast();
        }
        public void Update()
        {
            for (int i = 0; i < skillTitles.Count; i++)
            {
                skillTitles[i].Update();
            }
        }
        private void OnTitleUnEquiped(int obj)
        {
            var info = TitleManager.Instance.GetTitleInfoByID(obj);
            if(info.define.SkillType == SkillTitleType.Positive)
            {
                SkillInfo skill = skillTitles.Where(x => x.ID == info.ID).FirstOrDefault();
                if (skill!=null)
                {
                    skillTitles.Remove(skill);
                    OnSkillTitleChanged?.Invoke();
                }
            }
            else if (info.define.SkillType == SkillTitleType.Passive)
            {
                if (passiveTitles.Contains(info))
                {
                    passiveTitles.Remove(info);
                    OnSkillTitleChanged?.Invoke();
                }
            }
        }

        private void OnTitleEquiped(int obj)
        {
            var info = TitleManager.Instance.GetTitleInfoByID(obj);
            if (info.define.SkillType == SkillTitleType.Positive)
            {
                if (!skillTitles.Where(x => x.ID == info.ID).Any())
                {
                    skillTitles.Add(new SkillInfo(info.define));
                    OnSkillTitleChanged?.Invoke();
                }
            }
            else if (info.define.SkillType == SkillTitleType.Passive)
            {
                if (!passiveTitles.Contains(info))
                {
                    passiveTitles.Add(info);
                    OnSkillTitleChanged?.Invoke();
                }
            }
        }
    }
}
