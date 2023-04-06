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
    public IInteractable actObj = null;
    public CharController charc;
    public PlayerMovement movement;
    UITitle uititle = null;
    int uiCount = 0;

    public bool playerMoveEnabled = true;
    private void Start()
    {
        charc = GameManager.Instance.player;
        movement = charc.GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        //F交互
        if(Input.GetKeyDown(KeyCode.F))
        {
            actObj?.Interact(KeyCode.F);
        }
        //E技能
        //B打开芯片系统
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (uititle == null)
            {
                uititle = UIManager.Instance.Show<UITitle>();
            }
            else
            {
                UIManager.Instance.Close<UITitle>();
                uititle = null;
            }
        }
        //Space跳跃&攀爬
        if (Input.GetKey(KeyCode.Space))
        {
            actObj?.Interact(KeyCode.Space);
        }
        //Esc游戏菜单&UI移除
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (UIManager.Instance.windowStack.TryPeek(out var window))
            {
                OnCloseUIWindow(window);
                window.Close();
            }
            else
            {
                //TODO:打开游戏菜单
            }
        }
        //H帮助菜单
        /*
        //鼠标左键发动攻击
        if (Input.GetMouseButtonDown(0))
        {
            if (uiCount != 0) return;
            if (charc == null) return;
            switch(charc.atkStyle)
            {
                case AtkStyle.Melee:
                    charc.MeleeAtk();
                    break;
                case AtkStyle.Rifle:
                    charc.RifleAtk();
                    break;
                case AtkStyle.ShotGun:
                    charc.ShotGunAtk();
                    break;
                default: break;
            }
        }
        //Q键切换武器
        if (Input.GetKeyDown(KeyCode.Q))
        {
            switch (charc.atkStyle)
            {
                case AtkStyle.Melee:
                    charc.atkStyle = AtkStyle.Rifle; break;
                case AtkStyle.Rifle:
                    charc.atkStyle = AtkStyle.ShotGun; break;
                case AtkStyle.ShotGun:
                    charc.atkStyle = AtkStyle.Melee; break;
            }
            Debug.LogFormat("武器已切换到{0}", charc.atkStyle.ToString());
        }
        */
    }
    private void FixedUpdate()
    {
        //WASD移动
        HandlePlayerMovement();
    }
    public void HandlePlayerMovement()
    {
        if (!playerMoveEnabled) return;
        movement?.Move();
    }
    public void PlayerMovementEnabled(bool enabled)
    {
        movement.enabled= enabled;
    }
    public void DisabledPlayerMovement(float dur)
    {

    }
    public void OnCloseUIWindow(UIWindow window)
    {
        if (window as UITitle) uititle = null;
    }
}
