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

public class UIMenuWindow : UIWindow
{
    public void OnClickContinue()
    {
        UIManager.Instance.Close<UIMenuWindow>();
    }
    public void OnClickStartGame()
    {
        MessageBox.Show("确认要重新开始游戏吗?", "确定", "取消", () =>
        {
            //从头加载第一关
            GameManager.Instance.Reset();
            GameManager.Instance.InitAync();
            var info = PXSceneManager.Instance.GetScene(1);//Level01-1
            PXSceneManager.Instance.LoadScene(info, Consts.Loading.Default_Loading_Interval);
        }, null);
    }
    public void OnClickSetting()
    {
        
    }
    public void OnClickMakerList()
    {

    }
    public void OnClickQuit()
    {
        MessageBox.Show("确认要退出游戏吗?", "确定", "取消", () =>
        {
            UIManager.Instance.Close<UIMenuWindow>();
            PXSceneManager.Instance.LoadMainMenuScene();
            GameManager.Instance.Reset();
        }, null);
    }
}
