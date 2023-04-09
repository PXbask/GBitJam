using Manager;
using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
