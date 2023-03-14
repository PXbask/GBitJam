using Model;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/*
    Date:
    Name:
    Overview:
*/

public class DataManager : Singleton<DataManager>
{
    const string dataPath = "Data/";
    public Dictionary<int, TitleDefine> Titles= new Dictionary<int, TitleDefine>();
    public DataManager()
    {
        Debug.Log("<color=#FF00FF>DataManager Init</color>");
    }
    public IEnumerator LoadDataAync()
    {
        string json = File.ReadAllText(dataPath + "TitleDefine.txt");
        this.Titles = JsonConvert.DeserializeObject<Dictionary<int, TitleDefine>>(json);
        yield return null;
    }
    public void LoadData()
    {
        string json = File.ReadAllText(dataPath + "TitleDefine.txt");
        this.Titles = JsonConvert.DeserializeObject<Dictionary<int, TitleDefine>>(json);
    }
}
