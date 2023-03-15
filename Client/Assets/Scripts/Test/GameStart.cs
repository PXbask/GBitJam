using Manager;
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
        CreatePlayer();

        TitleManager.Instance.Init();
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
        player.charBase = new CharBase();
        player.charBase.attributes = new Model.Attributes();
        TitleManager.Instance.OnTitleEquiped += player.charBase.attributes.Recalculate;
        TitleManager.Instance.OnTitleUnEquiped += player.charBase.attributes.Recalculate;
        player.charBase.attributes.baseAttribute = DataManager.Instance.SaveData.playerAttri;
        player.charBase.attributes.Recalculate();
        player.transform.position = DataManager.Instance.SaveData.playerPos;
        //�������Ӧ��
        PlayerMovement movement = obj.GetComponent<PlayerMovement>();
        movement.speed = player.charBase.attributes.curAttribute.SPD;
    }
}
