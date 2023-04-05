using Manager;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
    Date:
    Name:
    Overview:管理称号的单例类
*/

public class TitleManager : Singleton<TitleManager>
{
    public Action<int> OnTitleEquiped = null;
    public Action<int> OnTitleUnEquiped = null;

    public Dictionary<int, TitleInfo> AllTitles = new Dictionary<int, TitleInfo>();
    public Dictionary<int, List<TitleInfo>> AllTypeTitles = new Dictionary<int, List<TitleInfo>>();
    public List<TitleInfo> EquipedTitle= new List<TitleInfo>();
    public int EquipedSize
    {
        get
        {
            int size = 0;
            foreach (var item in EquipedTitle)
            {
                size += item.define.Size;
            }
            return size;
        }
    }
    public void Init()
    {
        AllTypeTitles.Add(1, new List<TitleInfo>());
        AllTypeTitles.Add(2, new List<TitleInfo>());
        AllTypeTitles.Add(3, new List<TitleInfo>());
        foreach (var arr in DataManager.Instance.SaveData.alltitlesData)
        {
            var info = new TitleInfo(arr[0], arr[1], arr[2] == 1);
            AllTitles.Add(arr[0], info);
            AllTypeTitles[info.define.Quality].Add(info);
        }
        foreach (var id in DataManager.Instance.SaveData.equipedTitle)
        {
            if (AllTitles.TryGetValue(id, out TitleInfo info))
            {
                info.equiped = true;
            }
            else
            {
                Debug.LogWarningFormat("装备了不存在的Title! id:{0}", id);
            }
        }
    }
    public void Equip(int id)
    {
        TitleInfo info;
        if (AllTitles.TryGetValue(id, out info))
        {
            var temp = EquipedTitle.Where(t => t.ID == id).FirstOrDefault();
            if (temp == null)
            {
                info.equiped = true;
                EquipedTitle.Add(info);
                OnTitleEquiped?.Invoke(id);
                Debug.LogFormat("装备了{0}", info.define.Name);
            }
        }
        else
        {
            Debug.LogWarningFormat("Title: id:{0}不存在！", id);
        }
    }
    public void UnEquip(int id)
    {
        TitleInfo info = EquipedTitle.Where(t => t.ID == id).FirstOrDefault();
        if (info != null)
        {
            info.equiped = false;
            EquipedTitle.Remove(info);
            OnTitleUnEquiped?.Invoke(id);
            Debug.LogFormat("取下了{0}", info.define.Name);
        }
        else
        {
            Debug.LogWarningFormat("Title: id:{0}不存在！", id);
        }
    }
    public void GainTitle(int id)
    {
        if (AllTitles.TryGetValue(id, out TitleInfo info))
        {
            Debug.LogFormat("获得了[{0}] id:{1}", info.define.Name, info.ID);
            if(!info.gained)
            {
                info.gained = true;
                UIManager.Instance.AddGainMessage(info.define.Name);
            }
            else
            {
                //转换成碎片
                UIManager.Instance.AddGainMessage(string.Format("获得碎片 *{0}",info.define.PartsBorn.ToString()));
            }
        }
    }
    public int RandomTitleWithTitleType(int type)
    {
        var lists = this.AllTypeTitles[type];
        int index = UnityEngine.Random.Range(0, lists.Count);
        return lists[index].ID;
    }
}
