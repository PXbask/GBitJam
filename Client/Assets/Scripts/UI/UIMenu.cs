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
    public GameObject settingpanel;
    private void Start()
    {
        SoundManager.Instance.PlayMenuMusic();
        GameManager.Instance.Status = GameStatus.Menu;
        InputManager.Deactivate();
    }
    public void OnClickStartGame()
    {
        //��ͷ���ص�һ��
        GameManager.Instance.Reset();
        GameManager.Instance.InitAync();
        var info = PXSceneManager.Instance.GetScene(1);//Level01-1
        PXSceneManager.Instance.LoadScene(info, Consts.Loading.Default_Loading_Interval);

        SoundManager.Instance.PlayBtnClickSound();
    }
    public void OnClickSetting()
    {
        settingpanel.SetActive(true);

        SoundManager.Instance.PlayBtnClickSound();
    }
    public void OnClickMakerList()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("End");

        SoundManager.Instance.PlayBtnClickSound();
    }
    public void OnClickQuit()
    {
        MessageBox.Show("ȷ���˳���Ϸ��?", "ȷ��", "ȡ��"
            , () =>
            {
#if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
            }, null);

        SoundManager.Instance.PlayBtnClickSound();

    }
}
