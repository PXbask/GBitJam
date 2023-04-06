using Manager;
using Model;
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
    public CharController player;

    private void Awake()
    {
        Application.targetFrameRate = FPS;
        GameManager.Instance.Init();
        UnityEngine.SceneManagement.SceneManager.activeSceneChanged += GetBaseVars;
        Screen.SetResolution(width, height, false);
    }
    private void Start()
    {
        blackMask.gameObject.SetActive(false);
    }
    public void Init()
    {
        DataManager.Instance.LoadConfigData();
        DataManager.Instance.LoadUserData();
        TitleManager.Instance.Init();
        UserManager.Instance.Init();
        DialogueManager.Instance.Init();
        PXSceneManager.Instance.Init();
    }
    private void GetBaseVars(UnityEngine.SceneManagement.Scene arg0, UnityEngine.SceneManagement.Scene arg1)
    {
        this.GetBaseVars();
    }
    public void GetBaseVars()
    {
        if (player == null) GetPlayer();
    }
    public void GetPlayer()
    {
        var obj = GameObject.Find("Player");
        player = obj.GetComponent<CharController>();
        player.charBase = new Player(DataManager.Instance.Characters[1]);
        UserManager.Instance.playerdata = player.charBase;
        TitleManager.Instance.OnTitleEquiped += player.charBase.attributes.Recalculate;
        TitleManager.Instance.OnTitleUnEquiped += player.charBase.attributes.Recalculate;
        player.charBase.attributes.Recalculate();
        player.transform.position = DataManager.Instance.SaveData.playerPos;
        PlayerMovement movement = obj.GetComponent<PlayerMovement>();
        movement.speed = player.charBase.attributes.curAttribute.MoveVelocityRatio * 4f;
    }
    #region UI
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
        while (blackMask.color.a < 1)
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
    #endregion
}
