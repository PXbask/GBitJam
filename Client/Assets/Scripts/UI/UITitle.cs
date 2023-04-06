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

/*
    Date:
    Name:
    Overview:
*/

public class UITitle : UIWindow
{
    #region components
    [Header("左侧显示UI")]
    public Text hptext;
    public Text damageresistext;
    public Text movevelotext;
    public Text damgtext;
    public Text goldgaintext;
    public Text expgaintext;
    public Slider expslider;
    public Text exptext;
    public Text expbartext;
    public List<GameObject> slots = new List<GameObject>();
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
    public RawImage titleimg;
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
    public UITitleItem selectedItem;
    Dictionary<int, List<UITitleItem>> TitleItems = new Dictionary<int, List<UITitleItem>>();
    protected override void OnStart()
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
        TitleManager.Instance.OnGainNewTitle += SetTitleContent;

        UserManager.Instance.OnPlayerGoldChanged += this.SetResourcesData;
        UserManager.Instance.OnPlayerPartChanged += this.SetResourcesData;

        UserManager.Instance.OnPlayerLevelChanged += SetLevelBar;
        UserManager.Instance.OnPlayerExpChanged += SetLevelBar;
    }
    private void OnDestroy()
    {
        TitleManager.Instance.OnTitleEquiped -= this.OnEquipedTitleChanged;
        TitleManager.Instance.OnTitleUnEquiped -= this.OnEquipedTitleChanged;
        TitleManager.Instance.OnGainNewTitle -= SetTitleContent;

        UserManager.Instance.OnPlayerGoldChanged -= this.SetResourcesData;
        UserManager.Instance.OnPlayerPartChanged -= this.SetResourcesData;

        UserManager.Instance.OnPlayerLevelChanged -= SetLevelBar;
        UserManager.Instance.OnPlayerExpChanged -= SetLevelBar;
    }
    public void SetInfo()
    {
        SetPlayerAttriInfo();
        SetLevelBar();
        SetTitleSlot();
        SetResourcesData();
        SetLoadBar();
        SetTitleContent();
        SetTitleInfoPanel();
    }
    private void SetPlayerAttriInfo()
    {
        Model.Attribute attri = GameManager.Instance.player.charBase.attributes.curAttribute;

        hptext.text = attri.HP.ToString();
        damageresistext.text = (attri.DamageResistence * 100).ToString() + "%";
        movevelotext.text = (attri.MoveVelocityRatio * 100).ToString() + "%";
        damgtext.text = attri.DamageRatio.ToString();
        goldgaintext.text = (attri.GoldRatio * 100).ToString() + "%";
        expgaintext.text = (attri.ExpRatio * 100).ToString() + "%";
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
        if (selectedItem == null) return;
        (int gold, int part) res = selectedItem.info.GetLevelupResCost();
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
    private void SetTitleInfoPanel()
    {
        if (selectedItem == null)
        {
            rightcontent.SetActive(false);
        }
        else
        {
            rightcontent.SetActive(true);
            titletype.text = selectedItem.info.define.TitleType.ToString();
            titlename.text = selectedItem.info.define.Name.ToString();
            titledescri.text = selectedItem.info.define.Description.ToString();
            titleaffectinfo.text = selectedItem.info.GetDetailedInfo(selectedItem.info.level);
            for (int i = 0; i < levelitems.Count; i++)
            {
                levelitems[i].color = i <= selectedItem.info.level - 1 ? Color.yellow : Color.white;
            }
            equipbtn.gameObject.SetActive(!selectedItem.info.equiped);
            unequipbtn.gameObject.SetActive(selectedItem.info.equiped);
        }

    }
    private void SetDetailedInfoPanel(int level)
    {
        if(selectedItem!=null)
            titleaffectinfo.text = selectedItem.info.GetDetailedInfo(level);
    }   
    public void OnTitleItemClicked(UITitleItem item)
    {
        if(selectedItem==null || selectedItem != item)
        {
            selectedItem = item;
            SetTitleInfoPanel();
        }
    }
    public void OnClickEquipBtn()
    {
        if (UserManager.Instance.isOverLoad) return;
        if (!selectedItem) return;
        if (selectedItem.info.equiped) return;
        if (TitleManager.Instance.EquipedSize + selectedItem.info.define.Size <= 11)
        {
            UserManager.Instance.Load = Math.Clamp(UserManager.Instance.Load + Consts.Title.Equip_Load, 0, UserManager.Instance.loadMax);
            TitleManager.Instance.Equip(selectedItem.info.ID);
        }
        else
        {
            TitleManager.Instance.UnEquip(TitleManager.Instance.EquipedTitle.First().ID);
            OnClickEquipBtn();
        }
    }
    public void OnClickUnEquipBtn()
    {
        if (UserManager.Instance.isOverLoad) return;
        if (!selectedItem) return;
        if (!selectedItem.info.equiped) return;
        UserManager.Instance.Load = Math.Clamp(UserManager.Instance.Load + Consts.Title.UnEquip_Load, 0, UserManager.Instance.loadMax);
        TitleManager.Instance.UnEquip(selectedItem.info.ID);
    }
    public void OnEquipedTitleChanged(int id)
    {
        SetTitleSlot();
        SetTitleContent(id);
        SetPlayerAttriInfo();
        SetLoadBar();
        SetTitleInfoPanel();
    }
    private void OnLevelItemTouched(int obj)
    {
        SetDetailedInfoPanel(obj);
    }
    private void OnLevelItemUnTouched(int obj)
    {
        if(selectedItem) SetDetailedInfoPanel(selectedItem.info.level);
    }
    public void OnClickUpgradeBtn()
    {
        if (!selectedItem) return;
        selectedItem.info.Upgrade();
        SetResourcesData();
        SetTmpResourcesData();
        SetTitleContent(selectedItem.info.ID);
        SetTitleInfoPanel();
        Debug.LogFormat("升级芯片[Name:{0} Id:{1}]", selectedItem.info.define.Name, selectedItem.info.define.ID);
    }
    public void OnClickAltaBtn()
    {
        UIManager.Instance.Show<UIAtla>();
    }
}
