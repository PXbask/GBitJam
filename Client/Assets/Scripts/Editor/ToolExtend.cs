using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Manager;
using UnityEditor.SceneManagement;

/*
    Date:
    Name:
    Overview:
*/
[InitializeOnLoad]
public class ToolExtend : UnityEditor.Editor
{
    [MenuItem("Tools/SaveData")]
    public static void Save()
    {
         DataManager.Instance?.SaveUserData();
    }
    [MenuItem("Tools/PlayerLevelUp")]
    public static void PlayerLevelUp()
    {
        UserManager.Instance.OnLevelUp();
    }
    [MenuItem("Tools/LoadClean")]
    public static void LoadClean()
    {
        UserManager.Instance.Load = 0;
    }
    [MenuItem("Tools/StartTest")]
    public static void StartTest()
    {
        if (!EditorApplication.isPlaying)
        {
            if (EditorSceneManager.GetActiveScene().isDirty)
            {
                EditorUtility.DisplayDialog("DIRTY", "请先保存当前场景", "ok");
                return;
            }
            else
            {
                EditorSceneManager.OpenScene("Assets/Scenes/Start.unity", OpenSceneMode.Single);
                EditorApplication.EnterPlaymode();
            }
        }
    }
}
