using Manager;
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
        DataManager.Instance.LoadData();
        CreatePlayer();
    }
    public void CreatePlayer()
    {
        //生成玩家
        GameObject playerPre = Resloader.Load<GameObject>("Prefab/GameObject/Player");
        var obj = Instantiate(playerPre);
        obj.name = "Player";
        //脚本初始化
        player = obj.GetComponent<CharController>();
        player.charBase = new CharBase();
        player.charBase.attributes = new Model.Attributes();
        TitleManager.Instance.OnTitleEquiped += player.charBase.attributes.Recalculate;
        TitleManager.Instance.OnTitleUnEquiped += player.charBase.attributes.Recalculate;
        player.charBase.attributes.baseAttribute = new Model.Attribute()
        {
            HP = 100,
            SPD = 4f,
            ATK = 100,
            DEF = 50
        };
        player.charBase.attributes.Recalculate();
        //玩家数据应用
        PlayerMovement movement = obj.GetComponent<PlayerMovement>();
        movement.speed = player.charBase.attributes.curAttribute.SPD;
        //自动获得前两个称号
        TitleManager.Instance.GainTitle(1);
        TitleManager.Instance.GainTitle(2);
    }
}
