using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
    Date:
    Name:
    Overview:
*/

public class UIMenu : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.Status = GameStatus.Menu;
    }
    public void OnClickStartGame()
    {
        //从头加载第一关
        GameManager.Instance.Reset();
        GameManager.Instance.InitAync();
        var info = PXSceneManager.Instance.GetScene(1);//Level01-1
        PXSceneManager.Instance.LoadScene(info, Consts.Loading.Default_Loading_Interval);
    }
    public void OnClickSetting()
    {

    }
    public void OnClickMakerList()
    {

    }
    public void OnClickQuit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
