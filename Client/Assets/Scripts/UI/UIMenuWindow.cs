using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

/*
    Date:
    Name:
    Overview:
*/

public class UIMenuWindow : UIWindow
{
    [SerializeField] RenderTexture renderTexture;
    public override void OnStart()
    {
        OnClose += OnCloseFunc;
    }
    public override void OnReopen()
    {
        base.OnReopen();
        GameManager.Instance.backgroundCamera.targetTexture= renderTexture;
    }
    private void OnCloseFunc(UIWindow sender, WindowResult result)
    {
        var backgroundCamera = GameManager.Instance.backgroundCamera;
        backgroundCamera.GetUniversalAdditionalCameraData().cameraStack.Clear();
        backgroundCamera.targetTexture = null;
        backgroundCamera.GetUniversalAdditionalCameraData().cameraStack.Add(Camera.main);
    }
    public void OnClickContinue()
    {
        Close();

        SoundManager.Instance.PlayBtnClickSound();
    }
    public void OnClickStartGame()
    {
        MessageBox.Show("确认要重新开始游戏吗?", "确定", "取消", () =>
        {
            Close();
            GameManager.Instance.Reset();
            GameManager.Instance.InitAync();
            var info = PXSceneManager.Instance.GetScene(1);//Level01-1
            PXSceneManager.Instance.LoadScene(info, Consts.Loading.Default_Loading_Interval);
        }, null);

        SoundManager.Instance.PlayBtnClickSound();
    }
    public void OnClickSetting()
    {
        UIManager.Instance.Show<UISetting>();

        SoundManager.Instance.PlayBtnClickSound();
    }
    public void OnClickMakerList()
    {
        //UnityEngine.SceneManagement.SceneManager.LoadScene("End");

        SoundManager.Instance.PlayBtnClickSound();
    }
    public void OnClickQuit()
    {
        MessageBox.Show("确认要退出游戏吗?", "确定", "取消", () =>
        {
            Close();
            PXSceneManager.Instance.LoadMainMenuScene();
            GameManager.Instance.Reset();
        }, null);

        SoundManager.Instance.PlayBtnClickSound();
    }
}
