using Manager;
using Model;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

/*
    Date:
    Name:
    Overview:
*/

public class PXSceneManager : MonoSingleton<PXSceneManager>
{
    public Dictionary<int, PXSceneInfo> Scenes= new Dictionary<int, PXSceneInfo>();

    private UnityEngine.AsyncOperation m_asyncOperation;

    public GameObject uiInterlude;

    public Text text;
    public PXSceneInfo GetScene(int id)
    {
        if(Scenes.TryGetValue(id, out var pXScene)) 
            return pXScene;
        return null;
    }
    public void Init()
    {
        foreach (var scene in DataManager.Instance.Scenes)
        {
            Scenes.Add(scene.Key, new PXSceneInfo(scene.Value));
        }
    }
    public void LoadEnd()
    {
        SceneManager.LoadScene("End");
    }
    public void LoadScene(PXSceneInfo info, float duration)
    {
        StartCoroutine(LoadSceneAsync(info, duration));
    }
    IEnumerator LoadSceneAsync(PXSceneInfo info, float duration)
    {
        GameManager.Instance.Status = GameStatus.Loading;

        m_asyncOperation = SceneManager.LoadSceneAsync(info.define.Name, LoadSceneMode.Single);
        m_asyncOperation.allowSceneActivation = false;
        yield return new WaitForSeconds(duration);
        m_asyncOperation.allowSceneActivation = true;
        yield return null;
        //第一次进入场景就播放过场动画
        bool isFirstEntered = info.isFirstEntered;
        if (isFirstEntered)
        {
            info.isFirstEntered = false;
            GameManager.Instance.Status = GameStatus.BeforeGame;
            yield return StartInterludeAnim(info.define.InterludeText);
        }
        //生成角色
        GameManager.Instance.GetPlayer(info.startPosition);
        //是否进行新手引导
        if (isFirstEntered && info.isFirstScene)
        {
            GameManager.Instance.Status = GameStatus.Novice;
        }
        else
        {
            Destroy(Utils.GetNoviceInstanceObject());
            GameManager.Instance.Status = GameStatus.Game;
        }

        Debug.LogFormat("scene: changed to [{0}]", info.define.Name);
    }
    public void LoadMainMenuScene()
    {
        StartCoroutine(LoadMainMenuSceneAsync());
    }
    IEnumerator LoadMainMenuSceneAsync()
    {
        GameManager.Instance.Status = GameStatus.Loading;
        m_asyncOperation = SceneManager.LoadSceneAsync("Scenes/Menu", LoadSceneMode.Single);
        m_asyncOperation.allowSceneActivation = false;
        yield return new WaitForSeconds(Consts.Loading.Default_Loading_Interval);
        m_asyncOperation.allowSceneActivation = true;
        yield return null;
        GameManager.Instance.Status = GameStatus.Menu;
    }
    IEnumerator StartInterludeAnim(string str)
    {
        uiInterlude.SetActive(true);

        SoundManager.Instance.PlayPressMusic();
        text.text = string.Empty;
        Tween tween = text.DOText(str, 24)//150
            .SetSpeedBased()
            .SetEase(Ease.Linear)
            .SetUpdate(true)
            .OnComplete(() => SoundManager.Instance.musicAudioSource.Stop());

        bool complete = false;
        tween.onComplete += () => complete = true;
        yield return new WaitUntil(() => complete);

        yield return new WaitForSeconds(1f);
        uiInterlude.SetActive(false);
        SoundManager.Instance.PlayBattleMusic();
    }
}
