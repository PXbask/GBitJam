using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    Date:
    Name:
    Overview:
*/

public class UISkillBarItem : MonoBehaviour
{
    public Image bgImage;
    public Image mask;
    public Text timer;
    public SkillInfo skillInfo;
    public void SetInfo(SkillInfo info)
    {
        if(info== null)
        {
            mask.gameObject.SetActive(false);
            timer.gameObject.SetActive(false);
        }
        else
        {
            mask.gameObject.SetActive(true);
            timer.gameObject.SetActive(true);
        }
        skillInfo= info;
        if (info != null)
        {
            var titleinfo = TitleManager.Instance.GetTitleInfoByID(info.ID);
            if (titleinfo != null)
                bgImage.sprite = titleinfo.sprite;
        }
    }
    private void Update()
    {
        if (skillInfo == null) return;
        mask.gameObject.SetActive(skillInfo.IsUnderCooling);

        timer.text = skillInfo.timer.ToString("f1");
        mask.fillAmount = skillInfo.timer / skillInfo.CoolingDown;
    }
}
