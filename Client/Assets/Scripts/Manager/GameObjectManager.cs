using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Rendering.Universal;

namespace Manager
{
    public class GameObjectManager : MonoSingleton<GameObjectManager>
    {
        [SerializeField] GameObject playerPrefab;
        [SerializeField] GameObject enemyPrefab;
        [SerializeField] GameObject riflePrefab;
        [SerializeField] GameObject shotGunPrefab;

        List<GameObject> Characters = new List<GameObject>();
        public ObjectPool<BulletLogic> RiflePool;
        public ObjectPool<BulletLogic> ShotGunPool;

        public Transform poolRoot;
        public void Init()
        {
            GameObject obj = new GameObject("GameObjectManager");
            obj.transform.SetParent(transform, false);
            poolRoot = obj.transform;
            //Object Pool Init
            RiflePool = new ObjectPool<BulletLogic>(() => OnCreatePoolItem(riflePrefab), OnGetPoolItem, OnReleasePoolItem, OnDestroyPoolItem);
            ShotGunPool = new ObjectPool<BulletLogic>(() => OnCreatePoolItem(shotGunPrefab), OnGetPoolItem, OnReleasePoolItem, OnDestroyPoolItem);
        }
        internal void Reset()
        {
            Characters.Clear();
            RiflePool?.Clear();
            ShotGunPool?.Clear();
        }
        public void CreatePlayer(Vector3 pos)
        {
            //PlayerController
            GameObject playerobj = Instantiate(playerPrefab);
            PlayerController controller = playerobj.GetComponent<PlayerController>();
            GameManager.Instance.player = controller;
            //CharBase
            controller.charBase = new Player(DataManager.Instance.PlayerDefine, controller);
            //Instantiate
            controller.transform.position = pos;
            CharacterManager.Instance.AddCharacter(controller.charBase);
            AddCharacterObj(playerobj);
        }
        public void AddCharacterObj(GameObject obj)
        {
            this.Characters.Add(obj);
        }
        public void RemoveCharacterObj(GameObject obj)
        {
            this.Characters.Remove(obj);
        }
        /// <summary>
        /// 用于生成敌人
        /// </summary>
        /// <param name="position">生成位置</param>
        /// <param name="attackStyle">攻击模式，决定敌人拿什么武器</param>
        /// <param name="gainedtitles">增益类芯片，其他类别没有用</param>
        public GameObject CreateEnemy(Vector3 position, EnemyAttackStyle attackStyle, List<TitleInfo> gainedtitles = null)
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
            AddCharacterObj(enemyobj);

            return enemyobj;
        }
        #region PoolFuncs
        private BulletLogic OnCreatePoolItem(GameObject pre)
        {
            GameObject obj = Instantiate(pre);
            return obj.GetComponent<BulletLogic>();
        }
        private void OnReleasePoolItem(BulletLogic obj)
        {
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(poolRoot);
        }
        private void OnGetPoolItem(BulletLogic obj)
        {
            obj.gameObject.SetActive(true);
        }
        private void OnDestroyPoolItem(BulletLogic obj)
        {
            Destroy(obj.gameObject);
        }
        #endregion
    }
}
