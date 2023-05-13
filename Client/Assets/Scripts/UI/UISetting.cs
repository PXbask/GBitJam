using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview:
*/

public class UISetting : UIWindow
{
    #region GameObject
    public List<UISettingVideoItem> videoItems = new List<UISettingVideoItem>();
    public List<UISettingAudioItem> audioItems= new List<UISettingAudioItem>();
    public List<UISettingHUDItem> hUDItems= new List<UISettingHUDItem>();

    public bool inGame;
    #endregion
    private void OnEnable()
    {
        InitChild();
    }
    public void InitChild()
    {
        videoItems[0].Init(windowsModeIndex);
        videoItems[1].Init(resolutionIndex);
        videoItems[2].Init(videoQulityIndex);

        audioItems[0].Init(mainVolume);
        audioItems[1].Init(musicVolume);
        audioItems[2].Init(sfxVolume);

        hUDItems[0].Init(minimapEnabled);
        hUDItems[1].Init(healthBarEnabled);
        hUDItems[2].Init(expBarEnabled);
        hUDItems[3].Init(skillBarEnabled);
        hUDItems[4].Init(enemyDetailEnabled);
        hUDItems[5].Init(tipsEnabled);
    }
    #region parmenentData
    private int windowsModeIndex;
    private int resolutionIndex;
    private int videoQulityIndex;
    private float mainVolume;
    private float musicVolume;
    private float sfxVolume;
    private bool minimapEnabled;
    private bool healthBarEnabled;
    private bool expBarEnabled;
    private bool skillBarEnabled;
    private bool enemyDetailEnabled;
    private bool tipsEnabled;
    #endregion
    #region m_var
    public int M_windowsModeIndex;
    public int M_resolutionIndex;
    public int M_videoQulityIndex;
    public float M_mainVolume;
    public float M_musicVolume;
    public float M_sfxVolume;
    public bool M_minimapEnabled;
    public bool M_healthBarEnabled;
    public bool M_expBarEnabled;
    public bool M_skillBarEnabled;
    public bool M_enemyDetailEnabled;
    public bool M_tipsEnabled;
    #endregion
    #region Apply
    private void ApplyWindowsMode()
    {
        if (M_windowsModeIndex != windowsModeIndex)
        {
            windowsModeIndex= M_windowsModeIndex;
            switch (windowsModeIndex)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                default:
                    break;
            }
        }
    }
    private void ApplyResolution()
    {
        if (M_resolutionIndex != resolutionIndex)
        {
            resolutionIndex = M_resolutionIndex;
            switch (resolutionIndex)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    break;
            }
        }
    }
    private void ApplyVideoQulity()
    {
        if (M_videoQulityIndex != videoQulityIndex)
        {
            videoQulityIndex = M_videoQulityIndex;
            switch (videoQulityIndex)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                default:
                    break;
            }
        }
    }
    private void ApplyMainVolume()
    {
        if (M_mainVolume != mainVolume)
        {
            mainVolume = M_mainVolume;
            SoundManager.Instance.MainVolume = mainVolume;
        }
    }
    private void ApplyMusicVolume()
    {
        if (M_musicVolume != musicVolume)
        {
            musicVolume = M_musicVolume;
            SoundManager.Instance.MusicVolume = musicVolume;
        }
    }
    private void ApplySFXVolume()
    {
        if (M_sfxVolume != sfxVolume)
        {
            sfxVolume = M_sfxVolume;
            SoundManager.Instance.SFXVolume = sfxVolume;
        }
    }
    private void ApplyMinimap()
    {
        if (M_minimapEnabled != minimapEnabled)
        {
            minimapEnabled = M_minimapEnabled;
            GameManager.Instance.MiniMapEnabled = minimapEnabled;
        }
    }
    private void ApplyHealthBar()
    {
        if (M_healthBarEnabled != healthBarEnabled)
        {
            healthBarEnabled = M_healthBarEnabled;
            GameManager.Instance.HealthBarEnabled = healthBarEnabled;
        }
    }
    private void ApplyExpBar()
    {
        if (M_expBarEnabled != expBarEnabled)
        {
            expBarEnabled = M_expBarEnabled;
            GameManager.Instance.ExpBarEnabled = expBarEnabled;
        }
    }
    private void ApplySkillBar()
    {
        if (M_expBarEnabled != skillBarEnabled)
        {
            skillBarEnabled = M_expBarEnabled;
            GameManager.Instance.SkillBarEnabled = skillBarEnabled;
        }
    }
    private void ApplyEnemyDetail()
    {
        if (M_expBarEnabled != enemyDetailEnabled)
        {
            enemyDetailEnabled = M_expBarEnabled;
            GameManager.Instance.EnemyDetailEnabled = enemyDetailEnabled;
        }
    }
    private void ApplyTipsEnabled()
    {
        if (M_tipsEnabled != tipsEnabled)
        {
            tipsEnabled = M_tipsEnabled;
            GameManager.Instance.TipsEnabled = tipsEnabled;
        }
    }
    #endregion
    #region RegisterEvent
    public void OnWindowsModeIndexChanged(int index)
    {
        M_windowsModeIndex= index;
    }
    public void OnResolutionIndexChanged(int index)
    {
        M_resolutionIndex = index;
    }
    public void OnVideoQulityIndexChanged(int index)
    {
        M_videoQulityIndex = index;
    }
    public void OnMainVolumeChanged(float v)
    {
        M_mainVolume= v;
    }
    public void OnMusicVolumeChanged(float v)
    {
        M_musicVolume = v;
    }
    public void OnSFXVolumeChanged(float v)
    {
        M_sfxVolume = v;
    }
    public void OnMinimapEnabledChanged(bool b)
    {
        M_minimapEnabled= b;
    }
    public void OnHealthBarEnabledChanged(bool b)
    {
        M_healthBarEnabled = b;
    }
    public void OnExpBarEnabledChanged(bool b)
    {
        M_expBarEnabled = b;
    }
    public void OnSkillBarEnabledChanged(bool b)
    {
        M_skillBarEnabled = b;
    }
    public void OnEnemyDetailEnabledChanged(bool b)
    {
        M_enemyDetailEnabled = b;
    }
    public void OnTipsEnabledChanged(bool b)
    {
        M_tipsEnabled = b;
    }
    #endregion
    #region Event
    public void OnClickApply()
    {
        SoundManager.Instance.PlayBtnClickSound();

        ApplyWindowsMode();
        ApplyResolution();
        ApplyVideoQulity();
        ApplyMusicVolume();
        ApplySFXVolume();
        ApplyMainVolume();
        ApplyMinimap();
        ApplyHealthBar();
        ApplyExpBar();
        ApplySkillBar();
        ApplyEnemyDetail();
        ApplyTipsEnabled();
    }
    public void OnClickDefult()
    {
        SoundManager.Instance.PlayBtnClickSound();
    }
    public void OnClickQuit()
    {
        SoundManager.Instance.PlayBtnClickSound();

        M_windowsModeIndex = windowsModeIndex;
        M_resolutionIndex= resolutionIndex;
        M_videoQulityIndex= videoQulityIndex;
        M_mainVolume = mainVolume;
        M_musicVolume= musicVolume;
        M_sfxVolume = sfxVolume;
        M_minimapEnabled= minimapEnabled;
        M_healthBarEnabled= healthBarEnabled;
        M_expBarEnabled= expBarEnabled;
        M_skillBarEnabled= skillBarEnabled;
        M_enemyDetailEnabled=enemyDetailEnabled;
        M_tipsEnabled= tipsEnabled;

        if(!inGame) 
            gameObject.SetActive(false);
        else
            Close();
    }
    #endregion
}
