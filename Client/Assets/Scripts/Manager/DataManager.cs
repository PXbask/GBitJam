using Model;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/*
    Date:
    Name:
    Overview:Data管理类
*/

public class DataManager : Singleton<DataManager>
{
    const string dataPath = "Data/";

    public SaveData SaveData = new SaveData();

    public Dictionary<int, TitleDefine> Titles= new Dictionary<int, TitleDefine>();
    public DataManager()
    {
        Debug.Log("<color=#FF00FF>DataManager Init</color>");
    }
    public IEnumerator LoadConfigDataAync()
    {
        string json = File.ReadAllText(dataPath + "TitleDefine.txt");
        this.Titles = JsonConvert.DeserializeObject<Dictionary<int, TitleDefine>>(json);
        yield return null;
    }
    public void LoadConfigData()
    {
        string json = File.ReadAllText(dataPath + "TitleDefine.txt");
        this.Titles = JsonConvert.DeserializeObject<Dictionary<int, TitleDefine>>(json);
    }
    public void SaveUserData()
    {
        SaveData saveData = new SaveData();
        saveData.playerAttri = GameManager.Instance.charc.charBase.attributes.curAttribute;
        saveData.gainedTitleData = GetGainedTitleData();
        saveData.sceneIndex = GetSceneIndex();
        saveData.playerPos = GetPlayerPos();
        saveData.equipedTitle = GetEquipedTitle();
#if UNITY_EDITOR
        string filePath = dataPath + "Save.json";
#endif
        if(!File.Exists(filePath)) 
            File.Create(filePath);
        using(StreamWriter sw = new StreamWriter(filePath,false))
        {
            string json = JsonConvert.SerializeObject(saveData);
            sw.WriteLine(json);
        }
        Debug.Log("数据保存成功!");
    }

    public void LoadUserData()
    {
        string json = File.ReadAllText(dataPath + "Save.json");
        SaveData = JsonConvert.DeserializeObject<SaveData>(json);
    }
    #region tmpFuncs
    private List<int[]> GetGainedTitleData()
    {
        List<int[]> data = new List<int[]>();
        foreach (var title in TitleManager.Instance.AllGainedTitle)
        {
            int[] arr = new int[] { title.Key, title.Value.level };
            if(arr!=null)
                data.Add(arr);
        }
        return data;
    }
    private int GetSceneIndex()
    {
        return UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
    }
    private Vector3 GetPlayerPos()
    {
        if (GameManager.Instance.charc)
            return GameManager.Instance.charc.transform.position;
        return Vector3.zero;
    }
    private List<int> GetEquipedTitle()
    {
        List<int> data = new List<int>();
        foreach (var item in TitleManager.Instance.EquipedTitle)
        {
            data.Add(item.ID);
        }
        return data;
    }
    #endregion
}
