using Define;
using Manager;
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
    public Dictionary<int, Dictionary<int, TitleAffectDefine>> TitleAffects = new Dictionary<int, Dictionary<int, TitleAffectDefine>>();
    public Dictionary<int, CharacterDefine> Characters = new Dictionary<int, CharacterDefine>();
    public Dictionary<int, WeaponDefine> Weapons = new Dictionary<int, WeaponDefine>();
    public Dictionary<int, Dictionary<int, DialogueDefine>> Dialogues = new Dictionary<int, Dictionary<int, DialogueDefine>>();
    public Dictionary<int, Dictionary<int, LevelDefine>> Levels = new Dictionary<int, Dictionary<int, LevelDefine>>();
    public CharacterDefine PlayerDefine => Characters[Consts.Character.PlayerID];
    public DataManager()
    {
        Debug.Log("<color=#FF00FF>DataManager Init</color>");
    }
    public IEnumerator LoadConfigDataAync()
    {
        string json = File.ReadAllText(dataPath + "TitleDefine.txt");
        this.Titles = JsonConvert.DeserializeObject<Dictionary<int, TitleDefine>>(json);
        yield return null;

        json = File.ReadAllText(dataPath + "TitleAffectDefine.txt");
        this.TitleAffects = JsonConvert.DeserializeObject<Dictionary<int, Dictionary<int, TitleAffectDefine>>>(json);
        yield return null;

        json = File.ReadAllText(dataPath + "CharacterDefine.txt");
        this.Characters = JsonConvert.DeserializeObject<Dictionary<int, CharacterDefine>>(json);
        yield return null;

        json = File.ReadAllText(dataPath + "WeaponDefine.txt");
        this.Weapons = JsonConvert.DeserializeObject<Dictionary<int, WeaponDefine>>(json);
        yield return null;

        json = File.ReadAllText(dataPath + "DialogueDefine.txt");
        this.Dialogues = JsonConvert.DeserializeObject<Dictionary<int, Dictionary<int, DialogueDefine>>>(json);
        yield return null;

        json = File.ReadAllText(dataPath + "LevelDefine.txt");
        this.Levels = JsonConvert.DeserializeObject<Dictionary<int, Dictionary<int, LevelDefine>>>(json);
        yield return null;
    }
    public void LoadConfigData()
    {
        string json = File.ReadAllText(dataPath + "TitleDefine.txt");
        this.Titles = JsonConvert.DeserializeObject<Dictionary<int, TitleDefine>>(json);

        json = File.ReadAllText(dataPath + "TitleAffectDefine.txt");
        this.TitleAffects = JsonConvert.DeserializeObject<Dictionary<int, Dictionary<int, TitleAffectDefine>>>(json);

        json = File.ReadAllText(dataPath + "CharacterDefine.txt");
        this.Characters = JsonConvert.DeserializeObject<Dictionary<int, CharacterDefine>>(json);

        json = File.ReadAllText(dataPath + "WeaponDefine.txt");
        this.Weapons = JsonConvert.DeserializeObject<Dictionary<int, WeaponDefine>>(json);

        json = File.ReadAllText(dataPath + "DialogueDefine.txt");
        this.Dialogues = JsonConvert.DeserializeObject<Dictionary<int, Dictionary<int, DialogueDefine>>>(json);

        json = File.ReadAllText(dataPath + "LevelDefine.txt");
        this.Levels = JsonConvert.DeserializeObject<Dictionary<int, Dictionary<int, LevelDefine>>>(json);
    }
    public void SaveUserData()
    {
        SaveData saveData = new SaveData();
        saveData.alltitlesData = GetAllTitlesData();
        saveData.sceneIndex = GetSceneIndex();
        saveData.playerPos = GetPlayerPos();
        saveData.equipedTitle = GetEquipedTitle();
        saveData.Hp = GetPlayerHp();
        saveData.playerLevel = GetPlayerLevel();
        saveData.exp = GetPlayerExp();
        saveData.load = GetPlayerLoad();
        saveData.gold = GetPlayerGold();
        saveData.parts = GetPlayerParts();
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
     #region privateFuncs
    private List<int[]> GetAllTitlesData()
    {
        List<int[]> data = new List<int[]>();
        foreach (var title in TitleManager.Instance.AllTitles)
        {
            int[] arr = new int[] { title.Key, title.Value.level, title.Value.gained ? 1 : 0 };
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
        if (GameManager.Instance.player)
            return GameManager.Instance.player.transform.position;
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
    private float GetPlayerHp()
    {
        return UserManager.Instance.HP;
    }
    private int GetPlayerLevel()
    {
        return UserManager.Instance.Level;
    }
    private int GetPlayerExp()
    {
        return UserManager.Instance.Exp;
    }
    private int GetPlayerLoad()
    {
        return UserManager.Instance.Load;
    }
    private int GetPlayerParts()
    {
        return UserManager.Instance.Parts;
    }

    private int GetPlayerGold()
    {
        return UserManager.Instance.Gold;
    }
    #endregion
}
