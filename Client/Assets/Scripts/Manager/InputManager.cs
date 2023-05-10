using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview:管理用户输入
*/

public class InputManager : MonoSingleton<InputManager>
{
    public Dictionary<KeyCode, IInteractable<Collider>> actObjMap = new Dictionary<KeyCode, IInteractable<Collider>>();
    private IInteractable<Collider> actObj = null;
    public PlayerController CharController => GameManager.Instance.player;

    public bool playerMoveEnabled = true;
    public static bool active = false;
    private void Start()
    {
        actObjMap.Add(KeyCode.F, null);
        actObjMap.Add(KeyCode.Space, null);
        actObjMap.Add(KeyCode.None, null);
    }
    private void Update()
    {
        if (!active) return;
        //F交互
        if(Input.GetKeyDown(KeyCode.F))
        {
            if (actObjMap.TryGetValue(KeyCode.F, out actObj))
            {
                actObj?.Interact(KeyCode.F);
            }
        }
        //E技能
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SkillManager.Instance.CastSkill(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SkillManager.Instance.CastSkill(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SkillManager.Instance.CastSkill(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SkillManager.Instance.CastSkill(4);
        }
        //B打开芯片系统
        if (Input.GetKeyDown(KeyCode.B))
        {
            var uititle = UIManager.Instance.GetActiveInstance<UITitle>();
            if (uititle == null)
            {
                _ = UIManager.Instance.Show<UITitle>();
            }
            else
            {
                uititle.Close();
                uititle = null;
            }
        }
        //Space跳跃&攀爬
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (actObjMap.TryGetValue(KeyCode.Space, out actObj))
            {
                actObj?.Interact(KeyCode.Space);
            }
        }
        //Esc游戏菜单&UI移除
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (UIManager.Instance.WindowStack.TryPeek(out var window))
            {
                window.Close();
            }
            else
            {
                var uimenu = UIManager.Instance.GetActiveInstance<UIMenuWindow>();
                if (uimenu == null)
                {
                    _ = UIManager.Instance.Show<UIMenuWindow>();
                }
                else
                {
                    uimenu.Close();
                    uimenu = null;
                }
            }
        }
        //H帮助菜单
        if (Input.GetKeyDown(KeyCode.E))
        {
            //TODO: 帮助菜单
            Debug.Log("帮助菜单");
        }
        //鼠标左键发动攻击
        if (Input.GetMouseButtonDown(0))
        {
            if (UIManager.Instance.HasUIOverlay) return;
            if (CharController == null) return;
            UserManager.Instance.playerlogic.Attack();
        }
    }
    private void FixedUpdate()
    {
        if (!active) return;
        //WASD移动
        HandlePlayerMovement();
    }
    public void HandlePlayerMovement()
    {
        if (!playerMoveEnabled) return;
        CharController.Move();
    }
    public void PlayerMovementEnabled(bool enabled)
    {
        playerMoveEnabled= enabled;
    }
    public void DisabledPlayerMovement(float dur)
    {

    }
    public static void Activate()
    {
        active = true;
    }
    public static void Deactivate()
    {
        active= false;
    }
}
