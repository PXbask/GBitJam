using Manager;
using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview:游戏启动临时入口
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
        //切换地图
        //UnityEngine.SceneManagement.SceneManager.LoadScene(DataManager.Instance.SaveData.sceneIndex);
        //生成玩家
        GameObject playerPre = Resloader.Load<GameObject>("Prefab/GameObject/Player");
        var obj = Instantiate(playerPre);
        obj.name = "Player";
        //脚本初始化
        player = obj.GetComponent<CharController>();
        player.charBase = new Player(DataManager.Instance.Characters[0]);
        TitleManager.Instance.OnTitleEquiped += player.charBase.attributes.Recalculate;
        TitleManager.Instance.OnTitleUnEquiped += player.charBase.attributes.Recalculate;
        player.charBase.attributes.Recalculate();
        player.transform.position = DataManager.Instance.SaveData.playerPos;
        //玩家数据应用
        PlayerMovement movement = obj.GetComponent<PlayerMovement>();
        movement.speed = player.charBase.attributes.curAttribute.MoveVelocityRatio * 4f;
    }
}
