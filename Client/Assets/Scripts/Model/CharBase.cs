using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;
using Define;

/*
    Date:
    Name:
    Overview:玩家&敌人的基类
*/

public class CharBase
{
    public CharacterDefine define;
    public Attributes attributes;//属性总类
    public int level;
    public int gold;
    public int parts;
    public bool IsPlayer => define.ID == 0;
    public CharBase(CharacterDefine define)
    {
        this.define = define;
        attributes = new Attributes(define);
        if (IsPlayer) level = DataManager.Instance.SaveData.playerLevel;
        if (IsPlayer) gold = DataManager.Instance.SaveData.gold;
        if (IsPlayer) parts = DataManager.Instance.SaveData.parts;
    }
}
