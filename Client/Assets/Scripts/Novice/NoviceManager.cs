using Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
    Date:
    Name:
    Overview:
*/

public class NoviceManager : MonoSingleton<NoviceManager>
{
    [SerializeField] GameObject novice_chest;//新手箱子
    [SerializeField] EnemyController novice_enemy;//敌人
    [SerializeField] GameObject confiner_1;//第一层空气墙
    [SerializeField] GameObject confiner_2;//第二层空气墙
    [SerializeField] GameObject endarea;//终止区域防止意外，踏进该区域就会结束教程

    [SerializeField] Canvas canvas;
    [SerializeField] Transform panelRoot;//黑色遮罩

    NoviceStep step = NoviceStep.None;

    private bool m_waitingforB = false;
    private bool enemyisdetect = false;
    private bool enemyhasdefented = false;
    private UITitle m_uititle;
    public UITitle M_uititle
    {
        get => m_uititle;
        set
        {
            m_uititle = value;
            if (value != null)
            {
                if (Step == NoviceStep.OpenUITitle)
                {
                    Transform obj = M_uititle.transform.Find("bg/Canvasmid/mid/page1/Viewport/p1").GetChild(0);
                    Focus(obj.gameObject);
                    M_uititle.OnSelectedItemChanged += OnSelectedItemChanged;
                    M_uititle.OnClickExit += OnClickExit;
                    M_uititle.OnClickEquip += OnClickEquip;
                }
            }
        }
    }
    private UIAtla m_uiAtla;

    private Transform m_preParent;
    private GameObject m_foucsObj;
    public NoviceStep Step
    {
        get => step;
        set
        {
            if(value!=NoviceStep.OpenUITitle)
            {
                if(step == NoviceStep.OpenUITitle)
                    if(M_uititle!=null)
                        M_uititle.OnClickExit -= OnClickExit;
            }
            switch (value)
            {
                case NoviceStep.None:
                    break;
                case NoviceStep.Start:
                    DialogueManager.Instance.ShowDialogue(Consts.Dialogues.Operation_Btn_Teaching_1);
                    break;
                case NoviceStep.GainedFirstTitleDialogue:
                    DialogueManager.Instance.ShowDialogue(Consts.Dialogues.Operation_Btn_Teaching_2, 
                        () => {
                            UIManager.Instance.AddGainMessage("按 B 打开芯片系统");
                            m_waitingforB = true;
                        });
                    break;
                case NoviceStep.OpenUITitle:
                    
                    break;
                case NoviceStep.AfterEquipTitle:
                    
                    break;
                case NoviceStep.AfterDefendEnemy:
                    UIManager.Instance.AddGainMessage("按 B 打开芯片系统");
                    break;
                case NoviceStep.AfterOpenUIAtla:
                    
                    break;
                default:
                    break;
            }
            step = value;
            Debug.LogFormat("Beginner Novice Status: <color=#00FF00>[{0}]</color>", value.ToString());
        }
    }

    public void StartNovice()
    {
        var obj = GameObjectManager.Instance.CreateEnemy(new Vector3(-8f, 2f, -1f), EnemyAttackStyle.Melee, null);
        novice_enemy = obj.GetComponent<EnemyController>();

        Step = NoviceStep.Start;
        panelRoot.gameObject.SetActive(false);
        TitleManager.Instance.OnTitleEquiped += OnEquipAttackTitle;//当你第一次装备Attack芯片时(屠夫)触发对话
        TitleManager.Instance.OnTitleEquiped += OnEquipAssistTitle;//当你第一次装备Assist芯片时(杀手本能)触发对话
    }
    private void Update()
    {
        if (panelRoot.gameObject.activeInHierarchy)
            InputManager.Deactivate();


        switch (Step)
        {
            case NoviceStep.None:
                break;
            case NoviceStep.GainedFirstTitleDialogue:
                //等待玩家按下B，这样UITitle就出来了
                if (m_waitingforB)
                {
                    if (Input.GetKeyDown(KeyCode.B))
                    {
                        m_waitingforB = false;
                        Step = NoviceStep.OpenUITitle;
                    }
                }
                break;
            case NoviceStep.OpenUITitle:
                //先找到生成的UI，此UI持续整个教程
                if (!M_uititle)
                {
                    M_uititle = GameObject.Find("UITitle(Clone)")?.GetComponent<UITitle>();
                }
                break;
            case NoviceStep.WaitForEnemy:
                //等待敌人出现，当敌人检测到你时触发事件
                if(novice_enemy.M_res == CheckDistanceResult.Detected && !enemyisdetect)
                {
                    enemyisdetect = true;
                    DialogueManager.Instance.ShowDialogue(Consts.Dialogues.Novice_Enemy_Detected, () => Step = NoviceStep.WaitForDefendEnemy);
                }
                break;
            case NoviceStep.WaitForDefendEnemy:
                //敌人被击败后触发
                if (novice_enemy.charBase.attributes.curAttribute.HP <= 0 && !enemyhasdefented)
                {
                    TitleManager.Instance.GainTitle(20);//自动获得id=20的芯片
                    enemyhasdefented = true;
                    DialogueManager.Instance.ShowDialogue(Consts.Dialogues.Novice_Enemy_Defended, () => {
                        Step = NoviceStep.AfterDefendEnemy; 
                        m_waitingforB = true; 
                    });
                }
                break;
            //打败敌人之后，等待打开UITitle
            case NoviceStep.AfterDefendEnemy:
                if (m_waitingforB)//一样等待按B
                {
                    if (Input.GetKeyDown(KeyCode.B))
                    {
                        m_waitingforB = false;
                        Step = NoviceStep.BeforeOpenUIAtla;
                        m_preParent = null;
                        m_foucsObj= null;
                    }
                }
                break;
            //再次打开UITitle，需要重新注册事件
            case NoviceStep.BeforeOpenUIAtla:
                if (!m_foucsObj)
                {
                    Transform obj = M_uititle.transform.Find("bg/Canvasmid/mid/Atla");
                    Focus(obj.gameObject);

                    M_uititle.OnClickAtla += OnClickAtla;
                    M_uititle.OnClickMidBtn += OnClickMidBtn;
                    M_uititle.OnSelectedItemChanged_2 += OnSelectedItemChanged_2;
                    M_uititle.OnClickExit += OnClickExit;
                    M_uititle.OnClickEquip += OnClickEquip;
                }
                //等待出现图鉴
                if (!m_uiAtla)
                {
                    m_uiAtla = GameObject.Find("UIAtla(Clone)")?.GetComponent<UIAtla>();
                    if (m_uiAtla)
                    {
                        ResetFocus();
                        Transform obj = m_uiAtla.transform.Find("UIAtla/exit");
                        m_uiAtla.OnClickExit_Atla += OnClickExit_Atla;//出现后图鉴注册返回事件
                        DialogueManager.Instance.ShowDialogue(Consts.Dialogues.Open_Atla_First, () =>
                        {
                            Step = NoviceStep.AfterOpenUIAtla;
                            Focus(obj.gameObject);
                        });
                    }
                }
                break;
            default:
                break;
        }
    }
    //事件顺序: 屠夫->装备->Exit->打开图鉴->Exit图鉴->Mid Btn->杀手本能->装备->Exit
    private void OnSelectedItemChanged_2()
    {
        ResetFocus();
        Transform obj = M_uititle.transform.Find("bg/Canvasright/right/equip");
        Focus(obj.gameObject);

        M_uititle.OnSelectedItemChanged_2 -= OnSelectedItemChanged_2;
    }

    private void OnClickMidBtn()
    {
        ResetFocus();
        Transform obj = M_uititle.transform.Find("bg/Canvasmid/mid/page1/Viewport/p2").GetChild(0);
        Focus(obj.gameObject);

        M_uititle.OnClickMidBtn -= OnClickMidBtn;
    }

    private void OnClickExit_Atla()
    {
        ResetFocus();
        Transform obj = M_uititle.transform.Find("bg/Canvasmid/mid/btns").GetChild(1);
        Focus(obj.gameObject);
        m_uiAtla.OnClickExit_Atla -= OnClickExit_Atla;
    }

    private void OnClickAtla()
    {
        ResetFocus();
        M_uititle.OnClickAtla -= OnClickAtla;
    }

    private void OnClickEquip()
    {
        ResetFocus();
        Transform obj = M_uititle.transform.Find("bg/exit");
        Focus(obj.gameObject);

        M_uititle.OnClickEquip -= OnClickEquip;
    }

    private void OnSelectedItemChanged()
    {
        StartCoroutine(SelectedItemChanged());
    }
    IEnumerator SelectedItemChanged()
    {
        yield return null;
        ResetFocus();
        Transform obj = M_uititle.transform.Find("bg/Canvasright/right/equip");
        Focus(obj.gameObject);

        M_uititle.OnSelectedItemChanged -= OnSelectedItemChanged;
    }

    private void OnClickExit()
    {
        ResetFocus();
        //Exit有两次，所以要分情况考虑
        if(Step == NoviceStep.OpenUITitle)
        {
            DialogueManager.Instance.ShowDialogue(Consts.Dialogues.Operation_Btn_Teaching_3, () =>
            {
                Step = NoviceStep.WaitForEnemy;
                confiner_1.SetActive(false);
            });
        }
        else if(Step == NoviceStep.AfterOpenUIAtla)
        {

        }
    }
    private void OnEquipAttackTitle(int id)
    {
        if (TitleManager.Instance.GetTitleInfoByID(id).define.TitleType == TitleType.Attack && Step == NoviceStep.OpenUITitle)
        {
            DialogueManager.Instance.ShowDialogue(Consts.Dialogues.Equip_Attack_Title_First);
            TitleManager.Instance.OnTitleEquiped -= OnEquipAttackTitle;
        }
    }
    private void OnEquipAssistTitle(int id)
    {
        if (TitleManager.Instance.GetTitleInfoByID(id).define.TitleType == TitleType.Assist && Step == NoviceStep.AfterOpenUIAtla)
        {
            GameManager.Instance.Status = GameStatus.Game;
            DialogueManager.Instance.ShowDialogue(Consts.Dialogues.Equip_Assist_Title_First);
            Step = NoviceStep.End;
            ResetFocus();//取消聚焦，因为后面的Destroy会把按钮也销毁，虽说无伤大雅
            TitleManager.Instance.OnTitleEquiped -= OnEquipAssistTitle;

            Destroy(gameObject);
            Debug.Log("<color=#FF0000>NoviceManager Destroyed</color>");
        }
    }
    private void Focus(GameObject obj)
    {
        panelRoot.gameObject.SetActive(true);//聚焦的时候禁用InputManager
        InputManager.Deactivate();

        m_preParent = obj.transform.parent;
        m_foucsObj = obj;

        m_foucsObj.transform.SetParent(panelRoot);
    }
    private void ResetFocus()
    {
        panelRoot.gameObject.SetActive(false);
        InputManager.Activate();
        m_foucsObj.transform.SetParent(m_preParent);
    }
    public enum NoviceStep
    {
        None,
        Start,
        GainedFirstTitleDialogue,
        OpenUITitle,
        AfterEquipTitle,
        WaitForEnemy,
        WaitForDefendEnemy,
        AfterDefendEnemy,
        BeforeOpenUIAtla,
        AfterOpenUIAtla,
        End
    }
}
