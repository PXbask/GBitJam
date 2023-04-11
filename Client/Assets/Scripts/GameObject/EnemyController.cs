using Battle;
using Manager;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
    Date:
    Name:
    Overview:
*/

public class EnemyController : PXCharacterController
{
    public BattleStatus Status
    {
        get => enemy.battleStatus;
        set
        {
            enemy.battleStatus = value;
        }
    }
    public Enemy enemy;
    NavMeshAgent agent;
    public void Init()
    {
        enemy = charBase as Enemy;
        //��ʱ
        enemy.agent.Target = UserManager.Instance.playerlogic;
        enemy.battleStatus = BattleStatus.InBattle;

        agent = gameObject.AddComponent<NavMeshAgent>();
    }
    [Header("Battle")]
    public float attackDistance;
    public float offset;
    protected override void OnAwake()
    {
        base.OnAwake();
    }
    public override void Melee_Attack()
    {
        base.Melee_Attack();
    }
    public override void Rifle_Attack()
    {
        base.Rifle_Attack();
    }
    public override void ShotGun_Attack()
    {
        base.ShotGun_Attack();
    }
    public override Vector3 GetBulletHeadDirection()
    {
        return transform.forward;
    }

    public override Vector3 GetFireDirection(float speed)
    {
        return transform.forward;
    }
    public override void OnDeath()
    {
        base.OnDeath();
        Destroy(this.gameObject);

        GameObjectManager.Instance.RemoveCharacterObj(gameObject);
        CharacterManager.Instance.RemoveCharacter(this.charBase);
        if (UserManager.Instance.TargetEnemy == charBase) UserManager.Instance.TargetEnemy = null;
    }
    public override void Move()
    {
        var target = enemy.agent.Target.controller.GetRigidbody();
        //Ѱ�������һ�㲢��÷����ƶ�
        float ran = UnityEngine.Random.Range(-offset, offset);
        float dis = ran < 0 ? -1 : 1 *(Math.Abs(offset) + attackDistance);
        Vector3 pos = target.position;
        pos.z = dis;
    }
}
