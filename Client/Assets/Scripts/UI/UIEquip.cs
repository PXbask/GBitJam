using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/*
    Date:
    Name:
    Overview:装备界面
*/

public class UIEquip : UIWindow
{
    public ListView listMain;
    public GameObject cellPre;
    public Transform itemRoot;
    public EquipListViewCell selectedItem;

    public Text infoText;
    public Text titleInfoText;

    StringBuilder _sb = new StringBuilder();
    protected override void OnStart()
    {
        this.listMain.onItemSelected += this.OnClickTitle;
        InitCellList();
    }

    private void OnEnable()
    {
        RefreshInfoArea();
    }
    private void OnClickTitle(ListView.ListViewItem item)
    {
        this.selectedItem = item as EquipListViewCell;
        RefreshTitleInfoArea();
        RefreshInfoArea();
    }

    private void RefreshTitleInfoArea()
    {
        _sb.Clear();
        _sb.Append(string.Format("攻击加+{0}%", selectedItem.titleInfo.define.ATKratio * 100));
        _sb.Append(string.Format("防御加+{0}%", selectedItem.titleInfo.define.DEFratio * 100));
        titleInfoText.text = _sb.ToString();
    }

    private void RefreshInfoArea()
    {
        _sb.Clear();
        foreach (var title in TitleManager.Instance.EquipedTitle)
        {
            _sb.AppendLine(title.define.Name);
        }
        infoText.text = _sb.ToString();
    }

    public void InitCellList()
    {
        var list = TitleManager.Instance.EquipedTitle;
        foreach (var title in TitleManager.Instance.AllGainedTitle)
        {
            GameObject obj = Instantiate(cellPre, itemRoot);
            EquipListViewCell cell = obj.GetComponent<EquipListViewCell>();
            cell.SetInfo(title.Value);
            this.listMain.AddItem(cell);
            if (list.Contains(title.Value))
            {
                cell.OnPointerClick(null);
            }
        }
    }
    public void ClearCellList()
    {
        this.listMain.RemoveAll();
    }
    private void OnDestroy()
    {
        this.listMain.onItemSelected -= this.OnClickTitle;
    }
}
