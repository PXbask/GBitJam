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

public class PXCharacterController : MonoBehaviour
{
    public Creature charBase;
    public Rigidbody rb;

    public Vector3 headDir;
    private void Awake()
    {
        OnAwake();
    }
    protected virtual void OnAwake()
    {
        rb = GetComponentInChildren<Rigidbody>();
    }
    protected virtual void Update()
    {
        headDir = rb.velocity.z > 0 ? Vector3.forward : Vector3.back;
    }
    public void ReceiveDamage(BattleContext context)
    {
        charBase.ReceiveDamage(context);
    }
    public virtual void OnDeath()
    {

    }
    public virtual Vector3 GetBulletHeadDirection()
    {
        return headDir;
    }
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
    public virtual void Move()
    {
        
    }
    #region AtteckMode
    public virtual void Rifle_Attack()
    {
        Vector3 dir = GetBulletHeadDirection();
        BulletLogic bulletLogic = GameObjectManager.Instance.RiflePool.Get();
        bulletLogic.SetDetails(charBase, transform.position, dir.normalized, 18f, dir);
        Debug.Log("Rifle Attack");
    }
    public virtual void Melee_Attack()
    {
        Debug.Log("Melee Attack");
    }
    public virtual void ShotGun_Attack()
    {
        int num = charBase.weaponManager.WeaponConfig.define.ClipCount;
        for (int i = 0; i < num; i++)
        {
            float ranSpeed = Random.Range(8, 12);
            var dir = GetBulletHeadDirection();
            Vector3 ranDir = RandomDirection(dir, 30);
            BulletLogic bulletLogic = GameObjectManager.Instance.ShotGunPool.Get();
            bulletLogic.SetDetails(charBase, transform.position, ranDir.normalized, ranSpeed, ranDir);
        }
        Debug.Log("ShotGun Attack");
    }
    #endregion
}
