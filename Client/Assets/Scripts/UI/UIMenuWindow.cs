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
        MessageBox.Show("ȷ��Ҫ���¿�ʼ��Ϸ��?", "ȷ��", "ȡ��", () =>
        {
            //��ͷ���ص�һ��
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
        MessageBox.Show("ȷ��Ҫ�˳���Ϸ��?", "ȷ��", "ȡ��", () =>
        {
            UIManager.Instance.Close<UIMenuWindow>();
            PXSceneManager.Instance.LoadMainMenuScene();
            GameManager.Instance.Reset();
        }, null);
    }
}
