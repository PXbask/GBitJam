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
    [SerializeField] GameObject novice_chest;//��������
    [SerializeField] EnemyController novice_enemy;//����
    [SerializeField] GameObject confiner_1;//��һ�����ǽ
    [SerializeField] GameObject confiner_2;//�ڶ������ǽ
    [SerializeField] GameObject endarea;//��ֹ�����ֹ���⣬̤��������ͻ�����̳�

    [SerializeField] Canvas canvas;
    [SerializeField] Transform panelRoot;//��ɫ����

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
                            UIManager.Instance.AddGainMessage("�� B ��оƬϵͳ");
                            m_waitingforB = true;
                        });
                    break;
                case NoviceStep.OpenUITitle:
                    
                    break;
                case NoviceStep.AfterEquipTitle:
                    
                    break;
                case NoviceStep.AfterDefendEnemy:
                    UIManager.Instance.AddGainMessage("�� B ��оƬϵͳ");
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
        TitleManager.Instance.OnTitleEquiped += OnEquipAttackTitle;//�����һ��װ��AttackоƬʱ(����)�����Ի�
        TitleManager.Instance.OnTitleEquiped += OnEquipAssistTitle;//�����һ��װ��AssistоƬʱ(ɱ�ֱ���)�����Ի�
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
                //�ȴ���Ұ���B������UITitle�ͳ�����
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
                //���ҵ����ɵ�UI����UI���������̳�
                if (!M_uititle)
                {
                    M_uititle = GameObject.Find("UITitle(Clone)")?.GetComponent<UITitle>();
                }
                break;
            case NoviceStep.WaitForEnemy:
                //�ȴ����˳��֣������˼�⵽��ʱ�����¼�
                if(novice_enemy.M_res == CheckDistanceResult.Detected && !enemyisdetect)
                {
                    enemyisdetect = true;
                    DialogueManager.Instance.ShowDialogue(Consts.Dialogues.Novice_Enemy_Detected, () => Step = NoviceStep.WaitForDefendEnemy);
                }
                break;
            case NoviceStep.WaitForDefendEnemy:
                //���˱����ܺ󴥷�
                if (novice_enemy.charBase.attributes.curAttribute.HP <= 0 && !enemyhasdefented)
                {
                    TitleManager.Instance.GainTitle(20);//�Զ����id=20��оƬ
                    enemyhasdefented = true;
                    DialogueManager.Instance.ShowDialogue(Consts.Dialogues.Novice_Enemy_Defended, () => {
                        Step = NoviceStep.AfterDefendEnemy; 
                        m_waitingforB = true; 
                    });
                }
                break;
            //��ܵ���֮�󣬵ȴ���UITitle
            case NoviceStep.AfterDefendEnemy:
                if (m_waitingforB)//һ���ȴ���B
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
            //�ٴδ�UITitle����Ҫ����ע���¼�
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
                //�ȴ�����ͼ��
                if (!m_uiAtla)
                {
                    m_uiAtla = GameObject.Find("UIAtla(Clone)")?.GetComponent<UIAtla>();
                    if (m_uiAtla)
                    {
                        ResetFocus();
                        Transform obj = m_uiAtla.transform.Find("UIAtla/exit");
                        m_uiAtla.OnClickExit_Atla += OnClickExit_Atla;//���ֺ�ͼ��ע�᷵���¼�
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
    //�¼�˳��: ����->װ��->Exit->��ͼ��->Exitͼ��->Mid Btn->ɱ�ֱ���->װ��->Exit
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
        //Exit�����Σ�����Ҫ���������
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
            ResetFocus();//ȡ���۽�����Ϊ�����Destroy��Ѱ�ťҲ���٣���˵���˴���
            TitleManager.Instance.OnTitleEquiped -= OnEquipAssistTitle;

            Destroy(gameObject);
            Debug.Log("<color=#FF0000>NoviceManager Destroyed</color>");
        }
    }
    private void Focus(GameObject obj)
    {
        panelRoot.gameObject.SetActive(true);//�۽���ʱ�����InputManager
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
