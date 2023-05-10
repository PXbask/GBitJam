using Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview:
*/

public class EnemySpawner : MonoBehaviour
{
    public static readonly float SPAWN_DETECTION_INTERVAL = 1;
    public static readonly float SPAWN_DISTENCE = 10f;
    private static int nextID = 1;

    [SerializeField] int ID;

    [Tooltip("敌人的攻击方式，None表示随机武器")]
    [SerializeField] EnemyAttackStyle enemyAttackStyle;

    [SerializeField] EnemyType enemyType;

    [Tooltip("敌人的难度，决定敌人芯片的数量和品质，-1代表随机生成难度")]
    [SerializeField] int difficulty;

    private Mesh mesh;
    private MeshRenderer meshRenderer;

    float m_timer = SPAWN_DETECTION_INTERVAL;

    private void Awake()
    {
        ID = nextID++;
        mesh = GetComponent<MeshFilter>().sharedMesh;
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        meshRenderer.enabled = false;
    }

    private GameObject SpawnEnemy()
    {
        if (enemyType == EnemyType.Enemy)
            return GameObjectManager.Instance.CreateEnemy(transform.position,
                enemyAttackStyle == EnemyAttackStyle.None ? (EnemyAttackStyle)UnityEngine.Random.Range(1, 4) : enemyAttackStyle
                , difficulty == -1 ? UnityEngine.Random.Range(0, 10) : difficulty);
        else
            return GameObjectManager.Instance.CreateBoss(transform.position);
    }

    private void Update()
    {
        m_timer -= Time.deltaTime;
        TrySpawn();
    }

    private void TrySpawn()
    {
        if (m_timer <= 0)
        {
            if (CheckDiatance())
            {
                SpawnEnemy();
                gameObject.SetActive(false);
            }
            m_timer = SPAWN_DETECTION_INTERVAL;
        }
    }

    private bool CheckDiatance()
    {
        PXCharacterController playerC = UserManager.Instance.playerlogic?.controller;
        return playerC != null ? Vector3.Distance(transform.position, playerC.transform.position) <= SPAWN_DISTENCE : false;
    }
    

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Vector3 pos = this.transform.position;
        Gizmos.color = Color.red;
        if (this.mesh != null)
            Gizmos.DrawWireMesh(this.mesh, pos, this.transform.rotation, this.transform.localScale);
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.ArrowHandleCap(0, pos, this.transform.rotation, 1f, EventType.Repaint);
        UnityEditor.Handles.Label(pos, "SpawnPoint:" + this.ID);
    }
#endif

    private enum EnemyType
    {
        Enemy,
        Boss,
    }
}
