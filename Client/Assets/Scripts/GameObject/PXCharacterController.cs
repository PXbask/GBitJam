using Battle;
using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview:
*/

public abstract class PXCharacterController : MonoBehaviour, IAttackable
{
    public Creature charBase;
    public GameObject rifleBltPre;
    public GameObject shotgunBltPre;
    public GameObject meleeEffectPre;
    private void Awake()
    {
        OnAwake();
    }
    protected virtual void OnAwake()
    {

    }
    public virtual void Rifle_Attack()
    {
        Vector3 dir = GetFireDirection(18f);
        BulletLogic bulletLogic = GameObjectManager.Instance.RiflePool.Get();
        bulletLogic.SetDetails(charBase, transform.position, dir.normalized, 18f, GetBulletHeadDirection());
        Debug.Log("Rifle Attack");
    }
    public virtual void Melee_Attack()
    {
        Debug.Log("Melee Attack");
    }
    public virtual void ShotGun_Attack()
    {
        int num = charBase.weaponManager.WeaponConfig.define.ClipCount;
        for(int i=0;i<num; i++)
        {
            float ranSpeed = Random.Range(8, 12);
            var dir = GetFireDirection(ranSpeed);
            Vector3 ranDir = RandomDirection(dir, 30);
            BulletLogic bulletLogic = GameObjectManager.Instance.ShotGunPool.Get();
            bulletLogic.SetDetails(charBase,transform.position, ranDir.normalized, ranSpeed,ranDir);
        }
        Debug.Log("ShotGun Attack");
    }
    public void ReceiveDamage(BattleContext context)
    {
        charBase.ReceiveDamage(context);
    }
    public abstract Vector3 GetFireDirection(float speed);
    public abstract Vector3 GetBulletHeadDirection();
    public Vector3 RandomDirection(Vector3 origin, float degreeRange)
    {
        float randomAngle = Random.Range(-degreeRange, degreeRange);

        Vector3 randomAxis = transform.up;

        // 通过随机轴和角度生成旋转四元数
        Quaternion randomRotation = Quaternion.AngleAxis(randomAngle, randomAxis);

        // 将原始方向与随机旋转相乘得到返回的方向
        Vector3 returnDirection = randomRotation * origin;
        return returnDirection;
    }

    public virtual void OnDeath()
    {
        
    }
}
