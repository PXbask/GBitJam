using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview:
*/

public abstract class EntityController : MonoBehaviour
{
    public CharBase charBase;
    public GameObject rifleBltPre;
    public GameObject shotgunBltPre;
    public GameObject meleeEffectPre;
    private void Awake()
    {
        OnAwake();
    }
    protected virtual void OnAwake()
    {
        rifleBltPre = Resloader.Load<GameObject>("Prefab/GameObject/rifleBullet");
        shotgunBltPre = Resloader.Load<GameObject>("Prefab/GameObject/shotgunBullet");
        meleeEffectPre = Resloader.Load<GameObject>("Prefab/GameObject/meleeEffect");
    }
    public virtual void RifleAtk()
    {
        Vector3 dir = GetFireDirection(18f);
        GameObject obj = Instantiate(rifleBltPre, transform.position, Quaternion.identity);
        obj.transform.up = GetBulletHeadDirection();
        BulletLogic bulletLogic = obj.GetComponent<BulletLogic>();
        bulletLogic.SetInfo(dir.normalized, 18f);
        Debug.Log("Rifle Attack");
    }
    public virtual void ShotGunAtk()
    {
        for (int i = 0; i < 5; i++)
        {
            float ranSpeed = Random.Range(8, 12);
            var dir = GetFireDirection(ranSpeed);
            Vector3 ranDir = RandomDirection(dir, 30);
            GameObject obj = Instantiate(shotgunBltPre, transform.position, Quaternion.identity);
            obj.transform.up = ranDir;
            BulletLogic bulletLogic = obj.GetComponent<BulletLogic>();
            bulletLogic.SetInfo(ranDir.normalized, ranSpeed);
        }
        Debug.Log("ShotGun Attack");
    }
    public abstract void Attack();
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
}
