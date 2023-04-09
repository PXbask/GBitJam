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
    public Dictionary<KeyCode, IInteractable> actObjMap = new Dictionary<KeyCode, IInteractable>();
    private IInteractable actObj = null;
    public PlayerController charController;

    public bool playerMoveEnabled = true;
    private void Start()
    {
        charController = GameManager.Instance.player;

        actObjMap.Add(KeyCode.F, null);
        actObjMap.Add(KeyCode.Space, null);
        actObjMap.Add(KeyCode.None, null);
    }
    private void Update()
    {
        //F交互
        if(Input.GetKeyDown(KeyCode.F))
        {
            if (actObjMap.TryGetValue(KeyCode.F, out actObj))
            {
                actObj?.Interact(KeyCode.F);
            }
        }
        //E技能
        if (Input.GetKeyDown(KeyCode.E))
        {
            //TODO: 释放技能
            Debug.Log("释放技能");
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
                //TODO:打开游戏菜单
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
            if (charController == null) return;
            UserManager.Instance.playerlogic.Attack();
        }
    }
    private void FixedUpdate()
    {
        //WASD移动
        HandlePlayerMovement();
    }
    public void HandlePlayerMovement()
    {
        if (!playerMoveEnabled) return;
        charController.Move();
    }
    public void PlayerMovementEnabled(bool enabled)
    {
        playerMoveEnabled= enabled;
    }
    public void DisabledPlayerMovement(float dur)
    {

    }
}
