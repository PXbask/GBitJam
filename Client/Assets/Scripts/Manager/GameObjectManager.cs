using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.EventSystems.EventTrigger;

namespace Manager
{
    public class GameObjectManager : MonoSingleton<GameObjectManager>
    {
        public Camera backgroundCamera;
        [SerializeField] GameObject playerPrefab;
        [SerializeField] GameObject enemyPrefab;
        List<GameObject> Characters = new List<GameObject>();
        public void Init() { }
        public void CreatePlayer()
        {
            //PlayerController
            GameObject playerobj = Instantiate(playerPrefab);
            PlayerController controller = playerobj.GetComponent<PlayerController>();
            GameManager.Instance.player = controller;
            //CharBase
            controller.charBase = new Player(DataManager.Instance.PlayerDefine, controller);
            //Instantiate
            controller.transform.position = DataManager.Instance.SaveData.playerPos;
            CharacterManager.Instance.AddCharacter(controller.charBase);

            backgroundCamera.GetUniversalAdditionalCameraData().cameraStack.Add(Camera.main);
        }
        /// <summary>
        /// 用于生成敌人
        /// </summary>
        /// <param name="position">生成位置</param>
        /// <param name="attackStyle">攻击模式，决定敌人拿什么武器</param>
        /// <param name="gainedtitles">增益类芯片，其他类别没有用</param>
        public void CreateEnemy(Vector3 position, EnemyAttackStyle attackStyle, List<TitleInfo> gainedtitles = null)
        {
            //EnemyController
            GameObject enemyobj = Instantiate(enemyPrefab);
            EnemyController controller = enemyobj.GetComponent<EnemyController>();
            controller.transform.position = position;
            //CharBase
            Enemy enemy = new(DataManager.Instance.Characters[Consts.Character.EnemyID], controller);
            controller.charBase = enemy;
            controller.Init();
            //Title
            enemy.SetEquips(gainedtitles, attackStyle);
            enemy.attributes.Recalculate();

            //Debug
            Debug.Log(enemy.attributes.baseAttribute.ToString());
            Debug.Log(enemy.attributes.curAttribute.ToString());

            CharacterManager.Instance.AddCharacter(controller.charBase);
        }
    }
}
