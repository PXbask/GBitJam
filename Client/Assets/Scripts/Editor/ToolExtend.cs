using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
}
