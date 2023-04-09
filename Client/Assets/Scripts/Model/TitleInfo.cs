using Define;
using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class TitleInfo
    {
        public TitleDefine define;
        public Dictionary<int,TitleAffectDefine> affects;
        public int ID;
        public int level;
        public bool gained;
        public bool equiped;
        public TitleAffectDefine curAffect => affects[level];
        public bool CanUpgraded
        {
            get
            {
                if (define.TitleType != TitleType.Assist) return false;
                return level < 3;
            }
        }
        public TitleInfo(int ID, int level, bool gained)
        {
            this.ID = ID;
            this.level = level;
            this.gained = gained;
            this.equiped = false;
            this.define = DataManager.Instance.Titles[ID];
            if (define.TitleAffect != 0)
                this.affects = DataManager.Instance.TitleAffects[define.TitleAffect];
        }
        /// <summary>
        /// 仅供敌人使用
        /// </summary>
        /// <param name="ID">TitleDefine ID</param>
        public TitleInfo(int ID): this(ID, 0, false) { }
        public string GetDetailedInfo(int touchedLevel)
        {
            if(define.TitleType!=TitleType.Assist) return string.Empty;
            StringBuilder sb = new StringBuilder();
            TitleAffectDefine touchedAffect = affects[touchedLevel];
            if (curAffect.DamageGainV != 0) sb.AppendLine(GetStandardtext("攻击", curAffect.DamageGainV, touchedAffect.DamageGainV,touchedLevel ));
            if (curAffect.DodgeGainV != 0) sb.AppendLine(GetStandardtext("闪避", curAffect.DodgeGainV, touchedAffect.DodgeGainV, touchedLevel));
            if (curAffect.MoveGainV != 0) sb.AppendLine(GetStandardtext("移速", curAffect.MoveGainV, touchedAffect.MoveGainV, touchedLevel));
            if (curAffect.HPGainV != 0) sb.AppendLine(GetStandardtext("生命", curAffect.HPGainV, touchedAffect.HPGainV, touchedLevel));
            if (curAffect.DamgeResistenceGainV != 0) sb.AppendLine(GetStandardtext("抗性", curAffect.DamgeResistenceGainV, touchedAffect.DamgeResistenceGainV, touchedLevel));
            if (curAffect.GoldGainV != 0) sb.AppendLine(GetStandardtext("金币", curAffect.GoldGainV, touchedAffect.GoldGainV, touchedLevel));
            if (curAffect.ExpGainV != 0) sb.AppendLine(GetStandardtext("经验", curAffect.ExpGainV, touchedAffect.ExpGainV, touchedLevel));
            if (curAffect.PartGainV != 0) sb.AppendLine(GetStandardtext("碎片", curAffect.PartGainV, touchedAffect.PartGainV, touchedLevel));
            return sb.ToString();
        }
        private string GetStandardtext(string tag,float pre,float pro, int targetLevel)
        {
            char c1 = pre > 0 ? '+' : '-';
            bool isAdd = pro > pre;
            if (targetLevel <= level)
            {
                return string.Format("{0}{1}{2}%",tag, c1.ToString(), (Math.Abs(pre * 100)).ToString("f1"));
            }
            else
            {
                return string.Format("{0}{1}{2}%<color={3}>{4}{5}%</color>",
                tag, c1.ToString(), (Math.Abs(pre * 100)).ToString("f1"), isAdd ? "#00FF00" : "#FF0000",
                isAdd ? "+" : "-", (Math.Abs(pro - pre) * 100).ToString("f1"));
            }
        }
        public (int, int) GetLevelupResCost()
        {
            int level = this.level;
            int quality = define.Quality;
            if (level < 0 || level > 2) return (0, 0);
            int gold = TitleDefine.GoldCost[quality - 1, level];
            int parts = TitleDefine.PartCost[quality - 1, level];
            return (gold, parts);
        }

        public void Upgrade()
        {
            (int gold, int part) = GetLevelupResCost();
            if (UserManager.Instance.Gold >= gold) UserManager.Instance.Gold -= gold;
            else
            {
                UIManager.Instance.ShowWarning("金币数量不足，无法升级");
                return;
            }
            if (UserManager.Instance.Parts >= part) UserManager.Instance.Parts -= part;
            else
            {
                UIManager.Instance.ShowWarning("碎片数量不足，无法升级");
                return;
            }
            level++;
        }
    }
}
