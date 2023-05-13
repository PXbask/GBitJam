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
    [Tooltip("最小垂直方向攻击距离")]
    public float minVerticalDistance;
    [Tooltip("寻路终点距离目标平均距离")]
    public float stopDistance;
    [Tooltip("平均距离浮动区间")]
    public float offset;
    [Tooltip("与玩家保持的最小距离")]
    public float maintainDistance;
    [Tooltip("运动速度")]
    public float speed;
    [Tooltip("攻击欲望")]
    [Range(0f, 50f)]
    public float attackDesire;
    [Tooltip("最大寻路次数，尝试寻路超过此次数后仇恨消失")]
    public int maxSeekNum;
    [Tooltip("最大检测距离，在这个距离内敌人可能会产生仇恨")]
    public float maxDetectDistance;
    [Tooltip("最小仇恨消失距离")]
    public float minDisattackDistance;
    [Tooltip("检测频率(次/秒)")]
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
            //无法攻击时就向目标方向移动
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
            case CheckDistanceResult.NeedStayOff:        //距离太近时就主动远离
                StopNav();
                Vector3 relatedDir = new Vector3(0, 0, attackTarget.controller.rb.position.z - rb.position.z).normalized;
                rb.MovePosition(rb.position - relatedDir * speed * Time.fixedDeltaTime);
                break;
            case CheckDistanceResult.TooFar:           //距离太远时仇恨消失
                StopNavAndCleanTarget();
                break;
            case CheckDistanceResult.Detected:        //检测到就将玩家设置为仇恨目标
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
        // 射线检测
        if (Physics.Raycast(transform.position, collider.transform.position - transform.position, out RaycastHit hit, maxDetectDistance))
        {
            // 如果有障碍物，跳过处理
            return hit.collider == collider;
        }
        else
            return false;
    }
    private bool TryAttack()
    {

        //计算两者水平方向距离
        if (Math.Abs(rb.position.x - attackTarget.controller.rb.position.x) < minVerticalDistance)
        {
            //掺入攻击欲望
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
        //寻找相近的一点并向该方向移动
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
