using Define;
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
        public string GetDetailedInfo(int touchedLevel)
        {
            if(define.TitleType!=TitleType.Assist) return string.Empty;
            StringBuilder sb = new StringBuilder();
            TitleAffectDefine touchedAffect = affects[touchedLevel];
            if (curAffect.MeleeGainV != 0) sb.AppendLine(GetStandardtext("近战", curAffect.MeleeGainV, touchedAffect.MeleeGainV,touchedLevel ));
            if (curAffect.DodgeGainV != 0) sb.AppendLine(GetStandardtext("闪避", curAffect.DodgeGainV, touchedAffect.DodgeGainV, touchedLevel));
            if (curAffect.MoveGainV != 0) sb.AppendLine(GetStandardtext("移速", curAffect.MoveGainV, touchedAffect.MoveGainV, touchedLevel));
            if (curAffect.HPGainV != 0) sb.AppendLine(GetStandardtext("生命", curAffect.HPGainV, touchedAffect.HPGainV, touchedLevel));
            if (curAffect.AccuracyGainV != 0) sb.AppendLine(GetStandardtext("命中", curAffect.AccuracyGainV, touchedAffect.AccuracyGainV, touchedLevel));
            if (curAffect.DamgeResistenceGainV != 0) sb.AppendLine(GetStandardtext("抗性", curAffect.DamgeResistenceGainV, touchedAffect.DamgeResistenceGainV, touchedLevel));
            if (curAffect.GoldGainV != 0) sb.AppendLine(GetStandardtext("金币", curAffect.GoldGainV, touchedAffect.GoldGainV, touchedLevel));
            if (curAffect.ExpGainV != 0) sb.AppendLine(GetStandardtext("经验", curAffect.ExpGainV, touchedAffect.ExpGainV, touchedLevel));
            return sb.ToString();
        }
        private string GetStandardtext(string tag,float pre,float pro, int targetLevel)
        {
            char c1 = pre > 0 ? '+' : '-';
            bool isAdd = pro > pre;
            if (targetLevel <= level)
            {
                return string.Format("{0}{1}{2}%",tag, c1.ToString(), (Math.Abs(pre * 100)).ToString());
            }
            else
            {
                return string.Format("{0}{1}{2}%<color={3}>{4}{5}%</color>",
                tag, c1.ToString(), (Math.Abs(pre * 100)).ToString(), isAdd ? "#00FF00" : "#FF0000",
                isAdd ? "+" : "-", (Math.Abs(pro - pre) * 100).ToString());
            }
        }
    }
}
