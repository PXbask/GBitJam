using Manager;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Experimental.Audio;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

/*
    Date:
    Name:
    Overview://游戏主管理器
*/

public class GameManager : MonoSingleton<GameManager>
{
    [Header("基础设置")]
    [SerializeField] private FullScreenMode windowsMode = FullScreenMode.Windowed;
    public int width = 1920;
    public int height = 1080;
    [SerializeField] private VideoQuality videoQuality = VideoQuality.Middle;
    public float mainVolume = 100;
    public float musicVolume = 100;
    public float sfxVolume = 100;
    [SerializeField] private bool minimapEnabled = true;
    [SerializeField] private bool healthBarEnabled = true;
    [SerializeField] private bool expBarEnabled = true;
    [SerializeField] private bool skillBarEnabled = true;
    [SerializeField] private bool enemyDetailEnabled = true;
    [SerializeField] private bool tipsEnabled = true;
    public int FPS = 60;
    [Header("全局UI")]
    public Image blackMask;
    public Text maskText;
    public UILoadingMenu loadingMenu;
    [Header("运行对象")]
    public Camera backgroundCamera;
    public PlayerController player;
    public Collider boundary;
    [Header("运行参数")]
    [SerializeField] private GameStatus status;
    private bool isenterednovice = false;
    public GameStatus Status
    {
        get => status;
        set
        {
            if (value != GameStatus.Loading && status == GameStatus.Loading)
                OnGameLoadingEnd?.Invoke();

            status = value;
            Debug.LogFormat("Game Status: <color=#00FFFF>[{0}]</color>", value.ToString());

            switch (value)
            {
                case GameStatus.None:
                    InputManager.Deactivate();
                    break;
                case GameStatus.Menu:
                    InputManager.Deactivate();
                    break;
                case GameStatus.Loading:
                    InputManager.Deactivate();
                    OnGameLoadingBegin?.Invoke();
                    break;
                case GameStatus.Game:
                    SetBGCameraConfigs();
                    OnEnterGameMode?.Invoke();
                    InputManager.Activate();
                    break;
                case GameStatus.BeforeGame:
                    InputManager.Deactivate();//TODO
                    break;
                case GameStatus.Novice:
                    InputManager.Activate();
                    if (!isenterednovice)
                    {
                        status = value;
                        isenterednovice = true;
                        SetBGCameraConfigs();
                        NoviceManager.Instance.StartNovice();
                    }
                    break;
                case GameStatus.Dialoguing:
                    InputManager.Deactivate();
                    break;
                default:
                    break;
            }
        }
    }
    public Action OnEnterGameMode = null;
    public Action OnGameLoadingBegin= null;
    public Action OnGameLoadingEnd= null;
    #region Setting
    public FullScreenMode WindowsMode
    {
        get => windowsMode;
        set
        {
            if (windowsMode != value)
            {
                windowsMode = value;
                Screen.SetResolution(width, height, windowsMode);
            }
        }
    }
    private Vector2 resolution;
    public Vector2 Resolution
    {
        get { return resolution; }
        set
        {
            if(resolution != value)
            {
                resolution = value;
                width = (int)value.x;
                height = (int)value.y;
                Screen.SetResolution(width, height, windowsMode);
            }

        }
    }

    public VideoQuality VideoQuality
    {
        get => videoQuality;
        set
        {
            if (videoQuality != value)
            {
                videoQuality = value;
                //TODO:改变画质
            }
        }
    }


    public bool MiniMapEnabled
    {
        get { return minimapEnabled; }
        set { minimapEnabled = value; }
    }


    public bool HealthBarEnabled
    {
        get { return healthBarEnabled; }
        set { healthBarEnabled = value; }
    }


    public bool ExpBarEnabled
    {
        get { return expBarEnabled; }
        set { expBarEnabled = value; }
    }


    public bool SkillBarEnabled
    {
        get { return skillBarEnabled; }
        set { skillBarEnabled = value; }
    }


    public bool EnemyDetailEnabled
    {
        get { return enemyDetailEnabled; }
        set { enemyDetailEnabled = value; }
    }


    public bool TipsEnabled
    {
        get { return tipsEnabled; }
        set { tipsEnabled = value; }
    }


    #endregion


    private void Awake()
    {
        Application.targetFrameRate = FPS;
        Screen.SetResolution(width, height, false);
    }
    private void Start()
    {
        blackMask.gameObject.SetActive(false);
        loadingMenu.gameObject.SetActive(false);
    }
    private void Update()
    {
        switch (Status)
        {
            case GameStatus.None:
                break;
            case GameStatus.Menu:
                break;
            case GameStatus.Loading:
                break;
            case GameStatus.Game:
                CharacterManager.Instance.Update();
                SkillManager.Instance.Update();
                break;
            case GameStatus.BeforeGame:
                break;
            case GameStatus.Novice:
                break;
            default:
                break;
        }
        CharacterManager.Instance.Update();
        SkillManager.Instance.Update();
        MiniMapManager.Instance.UpdateBoundary(boundary);
    }
    public void GetBaseVars()
    {
        if (player == null) GetPlayer();
    }
    public void GetPlayer()
    {
        var obj = GameObject.Find("Player");
        if(!obj) GameObjectManager.Instance.CreatePlayer(DataManager.Instance.SaveData.playerPos);
    }
    public void GetPlayer(Vector3 pos)
    {
        var obj = GameObject.Find("Player");
        if (!obj) GameObjectManager.Instance.CreatePlayer(pos);
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

    internal void ShowLoadingMenu(float duration)
    {
        loadingMenu.m_duartion = duration;
        loadingMenu.gameObject.SetActive(true);
    }
    internal void HideLoadingMenu()
    {
        loadingMenu.m_duartion = 0;
        loadingMenu.gameObject.SetActive(false);
    }
    #endregion
    #region Init    
    public void Init()
    {
        TitleManager.Instance.Init();
        UserManager.Instance.Init();
        SkillManager.Instance.Init();
        CharacterManager.Instance.Init();
        GameObjectManager.Instance.Init();
    }
    public void InitAync()
    {
        StartCoroutine(InitAyncAnim());
    }
    IEnumerator InitAyncAnim()
    {
        TitleManager.Instance.Init();
        yield return null;
        UserManager.Instance.Init();
        yield return null;
        SkillManager.Instance.Init();
        yield return null;
        CharacterManager.Instance.Init();
        yield return null;
        GameObjectManager.Instance.Init();
        yield return null;
        MiniMapManager.Instance.Init();
        yield return null;
    }
    private void SetBGCameraConfigs()
    {
        backgroundCamera = GameObject.Find("Environment/Building/BGCamera")?.GetComponent<Camera>();
        backgroundCamera.GetUniversalAdditionalCameraData().cameraStack.Add(Camera.main);
    }
    internal void Reset()
    {
        TitleManager.Instance.Reset();
        UserManager.Instance.Reset();
        SkillManager.Instance.Reset();
        CharacterManager.Instance.Reset();
        GameObjectManager.Instance.Reset();
    }
    #endregion
    #region Setting

    #endregion
}
