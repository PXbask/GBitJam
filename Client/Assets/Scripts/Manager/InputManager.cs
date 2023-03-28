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
    public CharController charc;
    public PlayerMovement movement;
    UIDebug uidebug = null;
    UITitle uititle = null;
    int uiCount = 0;
    private void Start()
    {
        charc = GameManager.Instance.charc;
        movement = charc.GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        //Esc打开Debug界面
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(uidebug == null)
            {
                uidebug = UIManager.Instance.Show<UIDebug>();
                uiCount++;
            }
            else
            {
                UIManager.Instance.Close<UIDebug>();
                uidebug= null;
                uiCount--;
            }
        }
        //E打开装备列表
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (uititle == null)
            {
                uititle = UIManager.Instance.Show<UITitle>();
                uiCount++;
            }
            else
            {
                UIManager.Instance.Close<UITitle>();
                uititle = null;
                uiCount--;
            }
        }
        //鼠标左键发动攻击
        if(Input.GetMouseButtonDown(0))
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
        //E键交互

    }
    public void PlayerMovementEnabled(bool enabled)
    {
        movement.enabled= enabled;
    }
    public void DisabledPlayerMovement(float dur)
    {

    }
}
