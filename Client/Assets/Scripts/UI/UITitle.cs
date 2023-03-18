using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public Text accutext;
    public Text movevelotext;
    public Text atkvelotext;
    public Text damgtext;
    public Text goldgaintext;
    public Text expgaintext;
    public Slider expslider;
    public Text exptext;
    public List<GameObject> slots = new List<GameObject>();
    public List<UITitleSlotMask> masks = new List<UITitleSlotMask>();
    public Text goldtext;
    public Text tmpgoldtext;
    public Text parttext;
    public Text tmpparttext;
    [Header("中间显示UI")]
    public GameObject contentprefab;
    public UITabView tabview;
    [Header("右侧显示UI")]
    public RawImage titleimg;
    public Text titletype;
    public Text titlename;
    public Text titledescri;
    public Text titleaffectinfo;
    public List<UILevelItemOverable> levelitems = new List<UILevelItemOverable>();
    public Button equipbtn;
    public Button uplevelbtn;
    [Header("其他")]
    public Button exit;
    #endregion
    #region consts
    public const int SLOT_COLUME_COUNT = 9;
    public const int SLOTS_STEP = 20;
    #endregion
    public UITitleItem selectedItem;
    protected override void OnStart()
    {
        for(int i=0;i<levelitems.Count; i++)
        {
            levelitems[i].Id = i + 1;
            levelitems[i].OnLevelItemTouched += this.OnLevelItemTouched;
            levelitems[i].OnLevelItemUnTouched += this.OnLevelItemUnTouched;
        }
        SetPlayerAttriInfo();
        TitleManager.Instance.OnTitleEquiped += this.OnEquipedTitleChanged;
    }
    private void SetPlayerAttriInfo()
    {
        Model.Attribute attri = GameManager.Instance.charc.charBase.attributes.curAttribute;

        hptext.text = attri.HP.ToString();
        damageresistext.text = (attri.DamageResistence * 100).ToString() + "%";
        accutext.text = (attri.Accuracy * 100).ToString() + "%";
        movevelotext.text = (attri.MoveVelocityRatio * 100).ToString() + "%";
        atkvelotext.text = (attri.AttackVelocityRatio * 100).ToString() + "%";
        damgtext.text = attri.Damage.ToString();
        goldgaintext.text = (attri.GoldRatio * 100).ToString() + "%";
        expgaintext.text = (attri.ExpRatio * 100).ToString() + "%";

        expslider.maxValue = GameManager.Instance.charc.charBase.attributes.baseAttribute.HP;
        expslider.value = attri.HP;
        exptext.text = "";//暂定

        SetTitleSlot();
        SetResourcesData();

        SetTitleContent();
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
            if (totalSize < SLOT_COLUME_COUNT && size + totalSize > SLOT_COLUME_COUNT)
            {
                //分为两部分
                masks[totalSize].MaskApply(SLOT_COLUME_COUNT - totalSize);
                masks[SLOT_COLUME_COUNT].MaskApply(size + totalSize - SLOT_COLUME_COUNT);
            }
            else
            {
                masks[totalSize].MaskApply(size);
            }
            totalSize += size;
        }
    }
    private void SetResourcesData()
    {
        goldtext.text = GameManager.Instance.charc.charBase.gold.ToString();
        parttext.text = GameManager.Instance.charc.charBase.parts.ToString();
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
        foreach (var title in TitleManager.Instance.AllGainedTitle)
        {
            TitleType type = title.Value.define.TitleType;
            if (type == TitleType.None) continue;
            GameObject gobj = tabview.tabPages[(int)type - 1];
            var obj = Instantiate(contentprefab, gobj.transform);
            UITitleItem item = obj.GetComponent<UITitleItem>();
            item.SetInfo(title.Value);
            item.title = this;
        }
        foreach (var title in TitleManager.Instance.AllUnGainedTitle)
        {
            TitleType type = title.Value.define.TitleType;
            if (type == TitleType.None) continue;
            GameObject gobj = tabview.tabPages[(int)type - 1];
            var obj = Instantiate(contentprefab, gobj.transform);
            UITitleItem item = obj.GetComponent<UITitleItem>();
            item.SetInfo(title.Value);
            item.title = this;
        }
    }
    private void SetTitleInfoPanel()
    {
        if (selectedItem == null) return;
        titletype.text = selectedItem.info.define.TitleType.ToString();
        titlename.text = selectedItem.info.define.Name.ToString();
        titledescri.text = selectedItem.info.define.Description.ToString();
        titleaffectinfo.text = selectedItem.info.GetDetailedInfo(selectedItem.info.level);
        for (int i = 0; i < levelitems.Count; i++)
        {
            levelitems[i].color = i <= selectedItem.info.level - 1 ? Color.yellow : Color.white;
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
        if (!selectedItem.info.gained) return;
        if (selectedItem.info.equiped) return;
        if (TitleManager.Instance.EquipedSize + selectedItem.info.define.Size <= 12)//暂定为12
        {
            TitleManager.Instance.Equip(selectedItem.info.ID);
        }
        else
        {
            TitleManager.Instance.UnEquip(TitleManager.Instance.EquipedTitle.First().ID);
            OnClickEquipBtn();
        }
    }
    public void OnEquipedTitleChanged()
    {
        SetTitleSlot();
        SetTitleContent();
    }
    private void OnLevelItemTouched(int obj)
    {
        SetDetailedInfoPanel(obj);
    }

    private void OnLevelItemUnTouched(int obj)
    {
        SetDetailedInfoPanel(selectedItem.info.level);
    }
}
