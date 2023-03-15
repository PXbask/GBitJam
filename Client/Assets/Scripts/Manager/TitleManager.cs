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
    public Action OnTitleEquiped = null;
    public Action OnTitleUnEquiped = null;

    public Dictionary<int, TitleInfo> AllGainedTitle = new Dictionary<int, TitleInfo>();
    public List<TitleInfo> EquipedTitle= new List<TitleInfo>();//暂定先放一个
    public void Init()
    {
        foreach (var arr in DataManager.Instance.SaveData.gainedTitleData)
        {
            AllGainedTitle.Add(arr[0], new TitleInfo(arr[0], arr[1]));
        }
    }
    public void Equip(int id)
    {
        if (EquipedTitle.Count >= 1) return;
        TitleInfo info;
        if(AllGainedTitle.TryGetValue(id,out info))
        {
            var temp = EquipedTitle.Where(t => t.ID == id).FirstOrDefault();
            if (temp == null)
            {
                EquipedTitle.Add(info);
                OnTitleEquiped?.Invoke();
            }
        }
        else
        {
            Debug.LogWarningFormat("Title: id:{0}不存在！", id);
        }
    }
    public void UnEquip(int id)
    {
        TitleInfo info = EquipedTitle.Where(t => t.ID== id).FirstOrDefault();
        if(info!=null)
        {
            EquipedTitle.Remove(info);
            OnTitleUnEquiped?.Invoke();
        }
        else
        {
            Debug.LogWarningFormat("Title: id:{0}不存在！", id);
        }
    }
    public void GainTitle(int id)
    {
        TitleInfo info;
        if (!AllGainedTitle.TryGetValue(id,out info))
        {
            AllGainedTitle.Add(id, new TitleInfo(id, 1));
        }
        else
        {
            info.level++;
        }
    }
}
