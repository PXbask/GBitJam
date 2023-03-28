using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Audio;
using UnityEngine.UI;

/*
    Date:
    Name:
    Overview://游戏主管理器
*/

public class GameManager : MonoSingleton<GameManager>
{
    // 窗口宽高
    public int width = 1920;
    public int height = 1080;

    public int FPS = 60;//限帧

    public Image blackMask;
    public Text maskText;
    public CharController charc;

    private void Awake()
    {
        Application.targetFrameRate = FPS;
        UnityEngine.SceneManagement.SceneManager.activeSceneChanged += GetBaseVars;
        Screen.SetResolution(width, height, false);
    }
    private void Start()
    {
        Init();
        blackMask.gameObject.SetActive(false);
    }
    public void Init()
    {
        DataManager.Instance.LoadConfigData();
        DataManager.Instance.LoadUserData();
        TitleManager.Instance.Init();
        DialogueManager.Instance.Init();
        PXSceneManager.Instance.Init();
    }
    public void TurntoBlackAnim(Action callback)
    {
        StartCoroutine(TurntoBlack(callback));
    }
    public void TurntoWhiteAnim()
    {
        StartCoroutine(TurntoWhite());
    }

    IEnumerator TurntoBlack(Action callback)
    {
        blackMask.gameObject.SetActive(true);
        maskText.gameObject.SetActive(true);
        blackMask.color = new Color(0, 0, 0, 0);
        maskText.color = new Color(1, 1, 1, 0);
        while(blackMask.color.a< 1)
        {
            Color color = blackMask.color;
            color.a += Time.deltaTime * 0.5f;
            blackMask.color = color;

            color = maskText.color;
            color.a += Time.deltaTime * 0.5f;
            maskText.color = color;
            yield return null;
        }
        callback?.Invoke();
    }
    IEnumerator TurntoWhite()
    {
        while (blackMask.color.a > 0)
        {
            Color color = blackMask.color;
            color.a -= Time.deltaTime * 0.5f;
            blackMask.color = color;

            color = maskText.color;
            color.a -= Time.deltaTime * 0.5f;
            maskText.color = color;
            yield return null;
        }
        blackMask.gameObject.SetActive(false);
        maskText.gameObject.SetActive(false);
        yield return null;
    }
    private void GetBaseVars(UnityEngine.SceneManagement.Scene arg0, UnityEngine.SceneManagement.Scene arg1)
    {
        this.GetBaseVars();
    }
    public void GetBaseVars()
    {
        charc = GameObject.Find("Player")?.GetComponent<CharController>();
    }
}
