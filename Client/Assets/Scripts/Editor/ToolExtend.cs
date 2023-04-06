using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Manager;

/*
    Date:
    Name:
    Overview:
*/

public class ToolExtend : UnityEditor.Editor
{
    [MenuItem("Tools/SaveData")]
    public static void Save()
    {
         DataManager.Instance?.SaveUserData();
    }
    [MenuItem("Tools/AddDynamicMsg")]
    public static void AddDynamicMsg()
    {
        UIManager.Instance.AddGainMessage("Test");
    }
    [MenuItem("Tools/PlayerLevelUp")]
    public static void PlayerLevelUp()
    {
        UserManager.Instance.OnLevelUp();
    }
}
