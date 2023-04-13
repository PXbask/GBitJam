using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    Date:
    Name:
    Overview:
*/

public class UISkillBar : MonoBehaviour
{
    [SerializeField] List<UISkillBarItem> skillImages;
    [SerializeField] List<Image> passiveImages;
    private void Start()
    {
        ResetBar();

        SkillManager.Instance.OnSkillTitleChanged += ResetBar;
    }
    public void ResetBar()
    {
        var skills = SkillManager.Instance.skillTitles;
        for (int i = 0; i < skillImages.Count; i++)
        {
            if(skills.Count > i)
                skillImages[i].SetInfo(skills[i]);
            else
                skillImages[i].SetInfo(null);
        }

        var pass = SkillManager.Instance.passiveTitles;
        for (int i = 0; i < passiveImages.Count; i++)
        {
            if(pass.Count > i)
                passiveImages[i].sprite = null;
            else
                passiveImages[i].sprite = null;
        }
    }
    private void OnDestroy()
    {
        SkillManager.Instance.OnSkillTitleChanged -= ResetBar;
    }
}
