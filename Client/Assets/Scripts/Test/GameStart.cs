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
        DataManager.Instance.LoadData();
        CreatePlayer();
    }
    public void CreatePlayer()
    {
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
        player.charBase.attributes.baseAttribute = new Model.Attribute()
        {
            HP = 100,
            SPD = 4f,
            ATK = 100,
            DEF = 50
        };
        player.charBase.attributes.Recalculate();
        //�������Ӧ��
        PlayerMovement movement = obj.GetComponent<PlayerMovement>();
        movement.speed = player.charBase.attributes.curAttribute.SPD;
        //�Զ����ǰ�����ƺ�
        TitleManager.Instance.GainTitle(1);
        TitleManager.Instance.GainTitle(2);
    }
}
