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
        //F����
        if(Input.GetKeyDown(KeyCode.F))
        {
            if (actObjMap.TryGetValue(KeyCode.F, out actObj))
            {
                actObj?.Interact(KeyCode.F);
            }
        }
        //E����
        if (Input.GetKeyDown(KeyCode.E))
        {
            //TODO: �ͷż���
            Debug.Log("�ͷż���");
        }
        //B��оƬϵͳ
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
        //Space��Ծ&����
        if (Input.GetKeyDown(KeyCode.Space))
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
        if (Input.GetKeyDown(KeyCode.E))
        {
            //TODO: �����˵�
            Debug.Log("�����˵�");
        }
        //��������������
        if (Input.GetMouseButtonDown(0))
        {
            if (UIManager.Instance.HasUIOverlay) return;
            if (charController == null) return;
            UserManager.Instance.playerlogic.Attack();
        }
    }
    private void FixedUpdate()
    {
        //WASD�ƶ�
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
