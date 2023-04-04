using Manager;
using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview:��Ϸ������ʱ���
*/

public class GameStart : MonoBehaviour
{
    CharController player;
    private void Awake()
    {
        DataManager.Instance.LoadConfigData();
        DataManager.Instance.LoadUserData();
        TitleManager.Instance.Init();
        DialogueManager.Instance.Init();

        CreatePlayer();
    }
    
    public void CreatePlayer()
    {
        //�л���ͼ
        //UnityEngine.SceneManagement.SceneManager.LoadScene(DataManager.Instance.SaveData.sceneIndex);
        //�������
        GameObject playerPre = Resloader.Load<GameObject>("Prefab/GameObject/Player");
        var obj = Instantiate(playerPre);
        obj.name = "Player";
        //�ű���ʼ��
        player = obj.GetComponent<CharController>();
        player.charBase = new Player(DataManager.Instance.Characters[0]);
        TitleManager.Instance.OnTitleEquiped += player.charBase.attributes.Recalculate;
        TitleManager.Instance.OnTitleUnEquiped += player.charBase.attributes.Recalculate;
        player.charBase.attributes.Recalculate();
        player.transform.position = DataManager.Instance.SaveData.playerPos;
        //�������Ӧ��
        PlayerMovement movement = obj.GetComponent<PlayerMovement>();
        movement.speed = player.charBase.attributes.curAttribute.MoveVelocityRatio * 4f;
    }
}
