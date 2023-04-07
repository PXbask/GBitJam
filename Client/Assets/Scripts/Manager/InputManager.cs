using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview:�����û�����
*/

public class InputManager : MonoSingleton<InputManager>
{
    public Dictionary<KeyCode,IInteractable> actObjMap = new Dictionary<KeyCode,IInteractable>();
    private IInteractable actObj = null;
    public CharController charc;
    public PlayerMovement movement;

    //UITitle uititle = null;
    //UIAtla uIAtla = null;

    public bool playerMoveEnabled = true;
    private void Start()
    {
        charc = GameManager.Instance.player;
        movement = charc.GetComponent<PlayerMovement>();

        actObjMap.Add(KeyCode.F, null);
        actObjMap.Add(KeyCode.Space, null);
    }
    private void Update()
    {
        //F����
        if(Input.GetKeyDown(KeyCode.F))
        {
            if (actObjMap.TryGetValue(KeyCode.F, out actObj))
            {
                actObj?.Interact(KeyCode.F);
            }
        }
        //E����
        //B��оƬϵͳ
        if (Input.GetKeyDown(KeyCode.B))
        {
            var uititle = UIManager.Instance.GetActiveInstance<UITitle>();
            if (uititle == null)
            {
                uititle = UIManager.Instance.Show<UITitle>();
            }
            else
            {
                uititle.Close();
                uititle = null;
            }
        }
        //Space��Ծ&����
        if (Input.GetKey(KeyCode.Space))
        {
            if (actObjMap.TryGetValue(KeyCode.Space, out actObj))
            {
                actObj?.Interact(KeyCode.Space);
            }
        }
        //Esc��Ϸ�˵�&UI�Ƴ�
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (UIManager.Instance.WindowStack.TryPeek(out var window))
            {
                window.Close();
            }
            else
            {
                //TODO:����Ϸ�˵�
            }
        }
        //H�����˵�
        /*
        //��������������
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
        //Q���л�����
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
            Debug.LogFormat("�������л���{0}", charc.atkStyle.ToString());
        }
        */
    }
    private void FixedUpdate()
    {
        //WASD�ƶ�
        HandlePlayerMovement();
    }
    public void HandlePlayerMovement()
    {
        if (!playerMoveEnabled) return;
        movement?.Move();
    }
    public void PlayerMovementEnabled(bool enabled)
    {
        playerMoveEnabled= enabled;
    }
    public void DisabledPlayerMovement(float dur)
    {

    }
    //public void OnCloseUIWindow(UIWindow window, UIWindow.WindowResult result = UIWindow.WindowResult.None)
    //{
    //    if (window as UITitle) uititle = null;
    //}
}
