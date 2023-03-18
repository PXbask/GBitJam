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
    public Dictionary<int, TitleInfo> AllUnGainedTitle = new Dictionary<int, TitleInfo>();
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
        foreach (var arr in DataManager.Instance.SaveData.alltitlesData)
        {
            if (arr[2] == 1)
                AllGainedTitle.Add(arr[0], new TitleInfo(arr[0], arr[1], true));
            else
                AllUnGainedTitle.Add(arr[0], new TitleInfo(arr[0], 0, false));
        }
        foreach (var id in DataManager.Instance.SaveData.equipedTitle)
        {
            if (AllGainedTitle.TryGetValue(id,out TitleInfo info)){
                info.equiped = true;
                EquipedTitle.Add(AllGainedTitle[id]);
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
        if(AllGainedTitle.TryGetValue(id,out info))
        {
            var temp = EquipedTitle.Where(t => t.ID == id).FirstOrDefault();
            if (temp == null)
            {
                info.equiped = true;
                EquipedTitle.Add(info);
                OnTitleEquiped?.Invoke();
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
        TitleInfo info = EquipedTitle.Where(t => t.ID== id).FirstOrDefault();
        if(info!=null)
        {
            info.equiped = false;
            EquipedTitle.Remove(info);
            OnTitleUnEquiped?.Invoke();
            Debug.LogFormat("取下了{0}", info.define.Name);
        }
        else
        {
            Debug.LogWarningFormat("Title: id:{0}不存在！", id);
        }
    }
    public void GainTitle(int id)
    {
        if (!AllGainedTitle.TryGetValue(id,out TitleInfo info))
        {
            info = AllUnGainedTitle[id];
            AllUnGainedTitle.Remove(id);

            info.gained= true;
            AllGainedTitle[id] = info;
        }
        else
        {
            //TODO:可升级
            info.level++;
        }
    }
}
