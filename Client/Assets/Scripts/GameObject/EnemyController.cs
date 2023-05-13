using Battle;
using DG.Tweening;
using Manager;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

/*
    Date:
    Name:
    Overview:
*/

public sealed class EnemyController : PXCharacterController, IVisibleinMap
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

    public SpriteRenderer spriteRenderer;
    NavMeshAgent agent;
    private bool autoNav = false;
    private bool isbacktoward = false; 

    [Header("Battle Params")]
    [Tooltip("��С��ֱ���򹥻�����")]
    public float minVerticalDistance;
    [Tooltip("Ѱ·�յ����Ŀ��ƽ������")]
    public float stopDistance;
    [Tooltip("ƽ�����븡������")]
    public float offset;
    [Tooltip("����ұ��ֵ���С����")]
    public float maintainDistance;
    [Tooltip("�˶��ٶ�")]
    public float speed;
    [Tooltip("��������")]
    [Range(0f, 50f)]
    public float attackDesire;
    [Tooltip("���Ѱ·����������Ѱ·�����˴���������ʧ")]
    public int maxSeekNum;
    [Tooltip("�������룬����������ڵ��˿��ܻ�������")]
    public float maxDetectDistance;
    [Tooltip("��С�����ʧ����")]
    public float minDisattackDistance;
    [Tooltip("���Ƶ��(��/��)")]
    public float detectFrequency;

    int m_seekNum = 0;
    float m_detectInterval;
    float m_detectTimer;
    Collider[] hitColliders;
    CheckDistanceResult m_res = CheckDistanceResult.None;
    public CheckDistanceResult M_res => m_res;

    public List<Sprite> sprites = new List<Sprite>();

    public void Init()
    {
        enemy = charBase as Enemy;
        enemy.battleStatus = BattleStatus.Idle;

        agent = GetComponent<NavMeshAgent>();
        agent.speed= speed;
    }
    protected override void OnAwake()
    {
        base.OnAwake();

        m_detectInterval = 1 / detectFrequency;
    }
    private void Start()
    {
        MiniMapManager.Instance.Register(this);

        spriteRenderer.sprite = sprites[UnityEngine.Random.Range(0, sprites.Count)];
        spriteRenderer.transform.localScale = new Vector3(0.95f, 1.05f, 1) * spriteRenderer.transform.localScale.x;
        spriteRenderer.transform.DOScale(new Vector3(1.05f, 0.95f, 1) * spriteRenderer.transform.localScale.x, 1f)
                 .SetLink(gameObject)
                 .SetEase(Ease.Linear)
                 .SetLoops(-1, LoopType.Yoyo)
                 .SetUpdate(true);

        if (enemy.attackStyle == EnemyAttackStyle.Melee) maintainDistance = 1f;
    }
    protected override void Update()
    {
        base.Update();
        spriteRenderer.flipX = headDir.z > 0;
        UpdateAI();
    }
    #region AI
    private void UpdateAI()
    {
        m_detectTimer -= Time.deltaTime;


        if (Status == BattleStatus.InBattle)
        {
            //�޷�����ʱ����Ŀ�귽���ƶ�
            if (attackTarget != null && !TryAttack())
            {
                if (autoNav)
                    OnNavMove();
                else 
                    if(!isbacktoward)
                        StartNav();
            }
        }
        else
        {
            OnIdle();
        }
        Move();
    }
    private void OnIdle()
    {

    }
    public override void Move()
    {
        base.Move();
        var res = CheckDistance();
        m_res = res == CheckDistanceResult.Normal || res == CheckDistanceResult.None ? m_res : res;
        switch (m_res)
        {
            case CheckDistanceResult.NeedStayOff:        //����̫��ʱ������Զ��
                StopNav();
                Vector3 relatedDir = new Vector3(0, 0, attackTarget.controller.rb.position.z - rb.position.z).normalized;
                rb.MovePosition(rb.position - relatedDir * speed * Time.fixedDeltaTime);
                break;
            case CheckDistanceResult.TooFar:           //����̫Զʱ�����ʧ
                StopNavAndCleanTarget();
                break;
            case CheckDistanceResult.Detected:        //��⵽�ͽ��������Ϊ���Ŀ��
                SetTarget(UserManager.Instance.playerlogic);
                break;
            default:
                break;
        }
    }
    public void SetTarget(Creature target)
    {
        attackTarget = target;
        Status = BattleStatus.InBattle;
    }
    private CheckDistanceResult CheckDistance()
    {
        if (m_detectTimer > 0)
            return CheckDistanceResult.None;
        m_detectTimer = m_detectInterval;

        isbacktoward = false;
        if(attackTarget!=null)
        {
            if (Math.Abs(rb.position.x - attackTarget.controller.rb.position.x) < minVerticalDistance)
            {
                if (Vector3.Distance(rb.position, attackTarget.controller.rb.position) < maintainDistance)
                {
                    isbacktoward = true;
                    return CheckDistanceResult.NeedStayOff;
                }
            }
            if (Vector3.Distance(rb.position, attackTarget.controller.rb.position) >= minDisattackDistance)
            {
                return CheckDistanceResult.TooFar;
            }
        }
        if (EyeSimulater(LayerMask.GetMask("Player")))
        {
            return CheckDistanceResult.Detected;
        }
        return CheckDistanceResult.Normal;
    }
    private bool EyeSimulater(LayerMask mask)
    {
        hitColliders = Physics.OverlapSphere(transform.position, maxDetectDistance, mask.value);
        Collider collider = hitColliders.FirstOrDefault();
        if (collider == null) return false;
        // ���߼��
        if (Physics.Raycast(transform.position, collider.transform.position - transform.position, out RaycastHit hit, maxDetectDistance))
        {
            // ������ϰ����������
            return hit.collider == collider;
        }
        else
            return false;
    }
    private bool TryAttack()
    {

        //��������ˮƽ�������
        if (Math.Abs(rb.position.x - attackTarget.controller.rb.position.x) < minVerticalDistance)
        {
            //���빥������
            if (UnityEngine.Random.Range(0, 1f) < attackDesire * Time.fixedDeltaTime)
            {
                Attack();
                //StopNav();
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    private void Attack()
    {
        if (attackTarget == null) return;
        rb.velocity = Vector3.zero;

        enemy.Attack();
    }
    public void SetHeadDir()
    {
        headDir = rb.position.z > attackTarget.controller.rb.position.z ? Vector3.back : Vector3.forward;
        spriteRenderer.flipX = headDir.z > 0;
    }
    private void StartNav()
    {
        if (m_seekNum >= maxSeekNum)
            StopNavAndCleanTarget();

        var target = attackTarget.controller.rb;
        //Ѱ�������һ�㲢��÷����ƶ�
        float ran = UnityEngine.Random.Range(-offset, offset);
        float dis = ran < 0 ? -1 : 1 * (Math.Abs(offset) + stopDistance);
        Vector3 pos = target.position;
        pos.z += dis;

        if (!agent.SetDestination(pos))
        {
            m_seekNum++;
            return;
        }
        autoNav= true;
    }
    private void StopNav()
    {
        autoNav = false;
        agent.ResetPath();
    }
    private void StopNavAndCleanTarget()
    {
        StopNav();
        Status = BattleStatus.Idle;
        attackTarget = null;
        m_seekNum = 0;
    }
    private void OnNavMove()
    {
        if (agent.pathPending) return;
        if (agent.pathStatus==NavMeshPathStatus.PathInvalid)
        {
            StopNav();
            return;
        }
        if (agent.pathStatus == NavMeshPathStatus.PathComplete)
            m_seekNum = 0;
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            StopNav();
        }
    }
    #endregion
    #region Behave
    public override void OnDeath()
    {
        base.OnDeath();
        Destroy(this.gameObject);
        MiniMapManager.Instance.Remove(this);
        GameObjectManager.Instance.RemoveCharacterObj(gameObject);
        CharacterManager.Instance.RemoveCharacter(this.charBase);
        if (UserManager.Instance.TargetEnemy == charBase) UserManager.Instance.TargetEnemy = null;
    }
    #endregion
    #region AttackMode
    public override void Melee_Attack()
    {
        base.Melee_Attack();
        m_colliders = Physics.OverlapSphere(transform.position, charBase.weaponManager.WeaponConfig.define.Range, LayerMask.GetMask("Player"));
        Collider collider = m_colliders.FirstOrDefault();
        if (collider == null) return;
        var ctr = collider.gameObject.GetComponent<PlayerController>();
        ctr.charBase.ReceiveDamage(new BattleContext
        {
            attacker = charBase,
            weapon = charBase.weaponManager.WeaponConfig.define
        });
    }
    public override void Rifle_Attack()
    {
        Vector3 dir = GetBulletHeadDirection();
        BulletLogic bulletLogic = GameObjectManager.Instance.RiflePool.Get();
        bulletLogic.SetDetails(charBase, transform.position, dir.normalized, 10f, dir);

        this.PlayEnemyRifleSound();
        Debug.Log("Rifle Attack");
    }
    public override void ShotGun_Attack()
    {
        int num = charBase.weaponManager.WeaponConfig.define.ClipCount;
        float rediusInterval = 150f / (num - 1);
        Vector3 startNum = Quaternion.AngleAxis(75f, Vector3.up) * GetBulletHeadDirection();

        for (int i = 0; i < num; i++)
        {
            float ranSpeed = 6f;
            Vector3 ranDir = Quaternion.AngleAxis(-rediusInterval*i, Vector3.up) * startNum;
            BulletLogic bulletLogic = GameObjectManager.Instance.ShotGunPool.Get();
            bulletLogic.SetDetails(charBase, transform.position, ranDir.normalized, ranSpeed, ranDir);
        }

        SoundManager.Instance.PlayEnemyShotGunSound();
        Debug.Log("ShotGun Attack");
    }

    #endregion
    public override Vector3 GetBulletHeadDirection()
    {
        return headDir;
    }

    public string GetName() { return charBase.define.Name; }

    public Transform GetTransform() { return transform; }

    public MapIconType GetIconType() { return MapIconType.Enemy; }
}
