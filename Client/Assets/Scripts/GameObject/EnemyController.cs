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
    public Creature attackTarget;


    NavMeshAgent agent;
    private bool autoNav = false;

    [Header("Battle Params")]
    public float attackDistance;
    public float offset;
    public void Init()
    {
        enemy = charBase as Enemy;
        //临时
        attackTarget = UserManager.Instance.playerlogic;
        enemy.battleStatus = BattleStatus.InBattle;

        agent = GetComponent<NavMeshAgent>();
    }
    protected override void OnAwake()
    {
        base.OnAwake();
    }

    protected override void Update()
    {
        base.Update();
        UpdateAI();
    }
    #region AI
    private void UpdateAI()
    {
        if (attackTarget == null) return;
        if (Status != BattleStatus.InBattle) return;
        if (!TryAttack())
        {
            if (autoNav)
                OnNavMove();
            else
                StartNav();
        }
    }
    private bool TryAttack()
    {
        if (Math.Abs(rb.position.x - attackTarget.controller.rb.position.x) < 0.5f)
        {
            Attack();
            StopNav();
            return true;
        }
        return false;
    }
    private void Attack()
    {
        if (attackTarget == null) return;
        rb.velocity = Vector3.zero;
        headDir = rb.position.z > attackTarget.controller.rb.position.z ? Vector3.back : Vector3.forward;
        enemy.Attack();
    }
    private void StartNav()
    {
        var target = attackTarget.controller.rb;
        //寻找相近的一点并向该方向移动
        float ran = UnityEngine.Random.Range(-offset, offset);
        float dis = ran < 0 ? -1 : 1 * (Math.Abs(offset) + attackDistance);
        Vector3 pos = target.position;
        pos.z = dis;

        agent.SetDestination(pos);
        autoNav= true;
    }
    private void StopNav()
    {
        autoNav = false;
        agent.ResetPath();
    }
    private void OnNavMove()
    {
        if (agent.pathPending) return;
        if (!agent.pathStatus.Equals(NavMeshPathStatus.PathInvalid))
        {
            StopNav();
            return;
        }
    }
    #endregion
    #region Behave
    public override void OnDeath()
    {
        base.OnDeath();
        Destroy(this.gameObject);

        GameObjectManager.Instance.RemoveCharacterObj(gameObject);
        CharacterManager.Instance.RemoveCharacter(this.charBase);
        if (UserManager.Instance.TargetEnemy == charBase) UserManager.Instance.TargetEnemy = null;
    }
    #endregion
    #region AttackMode
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
    #endregion
    public override Vector3 GetBulletHeadDirection()
    {
        return headDir;
    }
}
