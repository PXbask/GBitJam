using Manager;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Consts;

/*
    Date:
    Name:
    Overview:
*/

public class UITitle : UIWindow
{
    #region components
    [Header("左侧显示UI")]
    public Text weapontext;
    public Text hptext;
    public Text damageresistext;
    public Text movevelotext;
    public Text damgtext;
    public Text goldgaintext;
    public Text expgaintext;
    public Slider expslider;
    public Text exptext;
    public Text expbartext;
    public List<UITitleSlot> slots = new List<UITitleSlot>();
    public List<UITitleSlotMask> masks = new List<UITitleSlotMask>();
    public Slider loadslider;
    public Text loadbartext;
    public Text goldtext;
    public Text tmpgoldtext;
    public Text parttext;
    public Text tmpparttext;
    [Header("中间显示UI")]
    public GameObject contentprefab;
    public UITabView tabview;
    [Header("右侧显示UI")]
    public GameObject rightcontent;
    public Image titleimg;
    public Text titletype;
    public Text titlename;
    public Text titledescri;
    public Text titleaffectinfo;
    public List<UILevelItemOverable> levelitems = new List<UILevelItemOverable>();
    public Button equipbtn;
    public Button unequipbtn;
    public Button uplevelbtn;
    [Header("其他")]
    public Button exit;
    #endregion
    #region consts
    public const int SLOTS_STEP = 20;
    #endregion
    private UITitleItem selectedItem;
    public UITitleItem SelectedItem
    {
        get => selectedItem;
        set
        {
            selectedItem = value;
            if (GameManager.Instance.Status == GameStatus.Novice)
            {
                if(NoviceManager.Instance.Step == NoviceManager.NoviceStep.OpenUITitle)
                {
                    OnSelectedItemChanged?.Invoke();
                }
                else if(NoviceManager.Instance.Step == NoviceManager.NoviceStep.AfterOpenUIAtla)
                {
                    OnSelectedItemChanged_2?.Invoke();
                }
            }
        }
    }
    Dictionary<int, List<UITitleItem>> TitleItems = new Dictionary<int, List<UITitleItem>>();

    public Action OnClickExit = null;
    public Action OnSelectedItemChanged = null;
    public Action OnSelectedItemChanged_2 = null;
    public Action OnClickEquip = null;
    public Action OnClickAtla = null;
    public Action OnClickMidBtn= null;
    public override void OnStart()
    {
        for(int i=0;i<levelitems.Count; i++)
        {
            levelitems[i].Id = i + 1;
            levelitems[i].OnLevelItemTouched += this.OnLevelItemTouched;
            levelitems[i].OnLevelItemUnTouched += this.OnLevelItemUnTouched;
        }
        SetInfo();
        TitleManager.Instance.OnTitleEquiped += this.OnEquipedTitleChanged;
        TitleManager.Instance.OnTitleUnEquiped += this.OnEquipedTitleChanged;
        TitleManager.Instance.OnGainNewTitle += OnGainNewTitle;
        //金币&碎片
        UserManager.Instance.OnPlayerGoldChanged += this.SetResourcesData;
        UserManager.Instance.OnPlayerPartChanged += this.SetResourcesData;
        //Level
        UserManager.Instance.OnPlayerLevelChanged += SetLevelBar;
        UserManager.Instance.OnPlayerLevelChanged += SetTitleSlotMaxNum;
        UserManager.Instance.OnPlayerExpChanged += SetLevelBar;
        //HP
        UserManager.Instance.OnPlayerHpChanged += SetHpText;
        //Load
        UserManager.Instance.OnPlayerLoadChanged += SetLoadBar;
    }

    private void OnDestroy()
    {
        TitleManager.Instance.OnTitleEquiped -= this.OnEquipedTitleChanged;
        TitleManager.Instance.OnTitleUnEquiped -= this.OnEquipedTitleChanged;
        TitleManager.Instance.OnGainNewTitle -= OnGainNewTitle;

        UserManager.Instance.OnPlayerGoldChanged -= this.SetResourcesData;
        UserManager.Instance.OnPlayerPartChanged -= this.SetResourcesData;

        UserManager.Instance.OnPlayerLevelChanged -= SetLevelBar;
        UserManager.Instance.OnPlayerLevelChanged -= SetTitleSlotMaxNum;
        UserManager.Instance.OnPlayerExpChanged -= SetLevelBar;

        UserManager.Instance.OnPlayerHpChanged -= SetHpText;

        UserManager.Instance.OnPlayerLoadChanged -= SetLoadBar;
    }
    public void SetInfo()
    {
        SetPlayerAttriInfo();
        SetLevelBar();
        SetTitleSlotMaxNum();
        SetTitleSlot();
        SetResourcesData();
        SetLoadBar();
        SetTitleContent();
        SetTitleInfoPanel();
    }
    private void SetPlayerAttriInfo()
    {
        Model.Attribute attri = GameManager.Instance.player.charBase.attributes.curAttribute;

        hptext.text = attri.HP.ToString("f0");
        damageresistext.text = (attri.DamageResistence * 100).ToString() + "%";
        movevelotext.text = (attri.MoveVelocityRatio * 100).ToString() + "%";
        damgtext.text = attri.DamageRatio.ToString();
        goldgaintext.text = (attri.GoldRatio * 100).ToString() + "%";
        expgaintext.text = (attri.ExpRatio * 100).ToString() + "%";

        if (UserManager.Instance.CurrentWeapon != null) weapontext.text = UserManager.Instance.CurrentWeapon.define.Name;
        else weapontext.text = "<未装备武器>";

    }
    private void SetHpText()
    {
        hptext.text = UserManager.Instance.HP.ToString();
    }
    private void SetLevelBar()
    {
        var playerInfo = GameManager.Instance.player.charBase;
        exptext.text = "Lv. " + UserManager.Instance.Level.ToString();
        expslider.maxValue = UserManager.Instance.exp2NextLevel;
        expslider.value = UserManager.Instance.Exp;
        expbartext.text = string.Format("{0} / {1}", expslider.value, expslider.maxValue);
    }
    private void SetTitleSlot()
    {
        foreach (var mask in masks)
        {
            mask.MaskReset();
        }
        int totalSize = 0;
        foreach (var title in TitleManager.Instance.EquipedTitle)
        {
            int size = title.define.Size;
            masks[totalSize].MaskApply(size, title);
            totalSize += size;
        }
    }
    private void SetTitleSlotMaxNum()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].ResetStatus();
        }
    }
    private void SetLoadBar()
    {
        var playerInfo = GameManager.Instance.player.charBase;
        loadslider.maxValue = DataManager.Instance.Levels[1][UserManager.Instance.Level].LoadMax;
        loadslider.value = UserManager.Instance.Load;
        loadbartext.text = string.Format("{0} / {1}", loadslider.value, loadslider.maxValue);
    }
    private void SetResourcesData()
    {
        goldtext.text = UserManager.Instance.Gold.ToString();
        parttext.text = UserManager.Instance.Parts.ToString();
    }
    public void SetTmpResourcesData()
    {
        if (SelectedItem == null) return;
        (int gold, int part) res = SelectedItem.info.GetLevelupResCost();
        if (res.gold != 0) tmpgoldtext.text = (-res.gold).ToString();
        if (res.part != 0) tmpparttext.text = (-res.part).ToString();
    }
    public void ResetTmpResourcesData()
    {
        tmpgoldtext.text = string.Empty;
        tmpparttext.text = string.Empty;
    }
    private void SetTitleContent()
    {
        foreach (var p in tabview.tabPages)
        {
            for (int i = 0; i < p.transform.childCount; i++)
            {
                Destroy(p.transform.GetChild(i).gameObject);
            }
        }
        TitleItems.Clear();

        TitleItems.Add(0, new List<UITitleItem>());
        TitleItems.Add(1, new List<UITitleItem>());
        TitleItems.Add(2, new List<UITitleItem>());
        foreach (var title in TitleManager.Instance.AllTitles)
        {
            if (!title.Value.gained) continue;
            TitleType type = title.Value.define.TitleType;
            if (type == TitleType.None) continue;
            GameObject gobj = tabview.tabPages[(int)type - 1];
            var obj = Instantiate(contentprefab, gobj.transform);
            UITitleItem item = obj.GetComponent<UITitleItem>();
            item.SetInfo(title.Value);
            item.title = this;

            TitleItems[(int)type - 1].Add(item);
        }
    }
    private void SetTitleContent(int id)
    {
        foreach (var lists in TitleItems.Values)
        {
            
            for(int i = 0; i < lists.Count; i++)
            {
                if (lists[i].info.ID==id)
                {
                    lists[i].SetInfo(lists[i].info);
                }
            }
        }
    }
    private void OnGainNewTitle(int id)
    {
        var info = TitleManager.Instance.GetTitleInfoByID(id);
        var type = info.define.TitleType;
        GameObject gobj = tabview.tabPages[(int)type - 1];
        var obj = Instantiate(contentprefab, gobj.transform);
        UITitleItem item = obj.GetComponent<UITitleItem>();
        item.SetInfo(info);
        item.title = this;

        TitleItems[(int)type - 1].Add(item);
    }
    private void SetTitleInfoPanel()
    {
        if (SelectedItem == null)
        {
            rightcontent.SetActive(false);
        }
        else
        {
            rightcontent.SetActive(true);
            titleimg.sprite = SelectedItem.info.sprite;
            titletype.text = SelectedItem.info.define.TitleType.ToString();
            titlename.text = SelectedItem.info.define.Name.ToString();
            titledescri.text = SelectedItem.info.define.Description.ToString();
            titleaffectinfo.text = SelectedItem.info.GetDetailedInfo(SelectedItem.info.level);
            for (int i = 0; i < levelitems.Count; i++)
            {
                levelitems[i].color = i <= SelectedItem.info.level - 1 ? Color.yellow : Color.white;
            }
            equipbtn.gameObject.SetActive(!SelectedItem.info.equiped);
            unequipbtn.gameObject.SetActive(SelectedItem.info.equiped);
            uplevelbtn.gameObject.SetActive(SelectedItem.info.CanUpgraded);
        }

    }
    private void SetDetailedInfoPanel(int level)
    {
        if(SelectedItem!=null)
            titleaffectinfo.text = SelectedItem.info.GetDetailedInfo(level);
    }   
    public void OnTitleItemClicked(UITitleItem item)
    {
        if(SelectedItem==null || SelectedItem != item)
        {
            SelectedItem = item;
            SetTitleInfoPanel();
        }
    }
    public void OnClickEquipBtn()
    {
        SoundManager.Instance.PlayBtnClickSound();

        if (!SelectedItem) return;
        if (SelectedItem.info.equiped) return;
        if (UserManager.Instance.isOverLoad)
        {
            UIManager.Instance.AddWarning("您已过载，无法装备芯片");
            return;
        }
        if (SelectedItem.info.define.TitleType == TitleType.Attack && UserManager.Instance.CurrentWeapon != null)
        {
            UIManager.Instance.AddWarning("请卸载当前武器");
            return;
        }
        if (TitleManager.Instance.EquipedSize + SelectedItem.info.define.Size <= UserManager.Instance.slotMax)
        {
            TitleManager.Instance.Equip(SelectedItem.info.ID);
        }
        else
        {
            UIManager.Instance.AddWarning("当前槽数量不足，无法继续装备");
            //TitleManager.Instance.UnEquip(TitleManager.Instance.EquipedTitle.First().ID);
            //OnClickEquipBtn();
        }
    }
    public void OnClickUnEquipBtn()
    {
        SoundManager.Instance.PlayBtnClickSound();

        if (UserManager.Instance.isOverLoad)
        {
            UIManager.Instance.AddWarning("您已过载，无法卸下芯片");
            return;
        }
        if (!SelectedItem) return;
        if (!SelectedItem.info.equiped) return;
        TitleManager.Instance.UnEquip(SelectedItem.info.ID);
    }
    public void OnEquipedTitleChanged(int id)
    {
        SoundManager.Instance.PlayBtnClickSound();

        SetTitleSlot();
        SetTitleContent(id);
        SetPlayerAttriInfo();
        SetTitleInfoPanel();
    }
    private void OnLevelItemTouched(int obj)
    {
        SetDetailedInfoPanel(obj);
    }
    private void OnLevelItemUnTouched(int obj)
    {
        if(SelectedItem) SetDetailedInfoPanel(SelectedItem.info.level);
    }
    public void OnClickUpgradeBtn()
    {
        SoundManager.Instance.PlayBtnClickSound();

        if (!SelectedItem) return;
        SelectedItem.info.Upgrade();
        SetResourcesData();
        SetTmpResourcesData();
        SetTitleContent(SelectedItem.info.ID);
        SetTitleInfoPanel();
        Debug.LogFormat("升级芯片[Name:{0} Id:{1}]", SelectedItem.info.define.Name, SelectedItem.info.define.ID);
    }
    public void OnClickAltaBtn()
    {
        SoundManager.Instance.PlayBtnClickSound();

        UIManager.Instance.Show<UIAtla>();
    }
    #region Apply
    public void ApplyOnClickQuit()
    {
        OnClickExit?.Invoke();
    }
    public void ApplyOnClickEquip()
    {
        OnClickEquip?.Invoke();
    }
    public void ApplyOnClickAtla()
    {
        OnClickAtla?.Invoke();
    }
    public void ApplyOnClickMidBtn()
    {
        OnClickMidBtn?.Invoke();
    }
    #endregion
}
