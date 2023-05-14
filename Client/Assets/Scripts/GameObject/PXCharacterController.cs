using Battle;
using Manager;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/*
    Date:
    Name:
    Overview:
*/

public class PXCharacterController : MonoBehaviour
{
    public Creature charBase;

    public Rigidbody rb;

    public Animator animator;

    public Vector3 headDir;

    protected Collider[] m_colliders;

    private Vector3 m_lastPos;

    [SerializeField] protected AudioSource soundSource;

    [SerializeField] protected AudioSource musicSource;
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
        Vector3 offset = transform.position - m_lastPos;
        if (Mathf.Abs(offset.z) <= 0.01f) return;
        m_lastPos = transform.position;
        headDir = offset.z > 0 ? Vector3.forward : Vector3.back;

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

        // ͨ�������ͽǶ�������ת��Ԫ��
        Quaternion randomRotation = Quaternion.AngleAxis(randomAngle, randomAxis);

        // ��ԭʼ�����������ת��˵õ����صķ���
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

        this.PlayEnemyRifleSound();
        Debug.Log("Rifle Attack");
    }
    public virtual void Melee_Attack()
    {
        this.PlaySwordSound();
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

        this.PlayShotGunSound();
        Debug.Log("ShotGun Attack");
    }
    #endregion

    protected void PlaySound(string name, float pitch) => SoundManager.Instance.PlaySound(name, pitch, soundSource);

    protected void PlayMusic(string name, float pitch = 1) => SoundManager.Instance.PlayMusic(name, pitch, soundSource);

    protected void PlayShotGunSound() => this.PlaySound("shotgun", UnityEngine.Random.Range(0.95f, 1.05f));

    protected void PlayEnemyRifleSound() => this.PlaySound("enemyrifle", UnityEngine.Random.Range(0.95f, 1.05f));

    protected void PlaySwordSound() => this.PlaySound("sword", UnityEngine.Random.Range(0.95f, 1.05f));

    protected void PlayEnemyMoveMusic1() => this.PlayMusic("enemymove1");

    protected void PlayEnemyMoveMusic2() => this.PlayMusic("enemymove2");
}
