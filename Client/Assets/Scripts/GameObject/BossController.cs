using Battle;
using Manager;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.EventSystems.EventTrigger;

/*
    Date:
    Name:
    Overview:
*/

public class BossController : PXCharacterController, IVisibleinMap, IInteractable<Collider>
{
    public bool inSecondStep => charBase.attributes.curAttribute.HP * 2 <= charBase.attributes.baseAttribute.HP;
    /** melee attack if less than this, else ranged attack */
    private float separatrixAttackRange = 1f;

    public Creature attackTarget;

    private float skillTimer = 9999;

    private float specialTimer = 5f; 

    private float detectTimer = 0;

    [SerializeField] private float minVerticalDistance;

    [SerializeField] private float movementSpeed;

    [Tooltip("寻路终点距离目标平均距离")]
    [SerializeField] private float stopDistance;

    [Tooltip("平均距离浮动区间")]
    [SerializeField] private float offset;

    [SerializeField] private float detectInterval;

    [SerializeField] private float maintainDistance;

    NavMeshAgent agent;

    Boss boss;

    [SerializeField] public GameObject animationObject;

    Vector3 lastPos;

    public float animscale;

    private bool isRun = false;

    private bool normalAttackShutDown = false;

    private bool autoNav = false;

    private bool tryAttack = true;

    private bool isbacktoward = false;

    private bool isinSkill = false;

    public bool isinFoucs = false;

    private bool isStoped = false;

    public bool activited = false;

    private bool istalkd = false;

    private void Start()
    {
        MiniMapManager.Instance.Register(this);
        SetTarget(UserManager.Instance.playerlogic);

        animscale = animationObject.transform.localScale.x;
    }

    protected override void Update()
    {
        base.Update();
        HandleAnim();

        if (!activited) return;
        UpdateAI();
    }

    public override void OnDeath()
    {
        base.OnDeath();

        EventManager.OnBossDefend?.Invoke();

        DialogueManager.Instance.ShowDialogue(Consts.Dialogues.BOSS_Dialogue_Pro, () =>
        {
            Destroy(this.gameObject);
            MiniMapManager.Instance.Remove(this);
            GameObjectManager.Instance.RemoveCharacterObj(gameObject);
            CharacterManager.Instance.RemoveCharacter(this.charBase);
            if (UserManager.Instance.TargetEnemy == charBase) UserManager.Instance.TargetEnemy = null;
        });
    }

    public void SetTarget(Creature target)
    {
        attackTarget = target;
    }

    public void Init()
    {
        boss = charBase as Boss;

        agent = GetComponent<NavMeshAgent>();
        agent.speed= movementSpeed;
    }

    public void Stop(float dur)
    {
        StartCoroutine(StopAnim(dur));
    }

    IEnumerator StopAnim(float dur)
    {
        isStoped= true;
        yield return new WaitForSeconds(dur);
        isStoped= false;
    }

    private void UpdateAI()
    {
        if (isStoped) return;
        if (isinSkill) return;
        UpdateAttack();
        UpdateMovement();
    }

    private void CastDashandAttack()
    {
        float ran = UnityEngine.Random.Range(-offset, offset);
        float dis = ran < 0 ? -1 : 1 * (Math.Abs(offset) + stopDistance);
        Vector3 pos = attackTarget.controller.rb.position;
        pos.z += dis;
        Vector3 dir = (pos - rb.position).normalized;
        StartCoroutine(DashandAttack(dir));
    }

    IEnumerator DashandAttack(Vector3 dir)
    {
        Debug.Log("DashAndAttack");
        StopNav();

        isinSkill = true;

        PlayBlinkAnim();
        float timer = 0.4f;
        while (timer > 0)
        {
            timer -= Time.fixedDeltaTime;
            rb.transform.position = rb.transform.position + dir * Time.fixedDeltaTime * movementSpeed * 3f;
            //rb.MovePosition(rb.position + dir * Time.fixedDeltaTime * movementSpeed * 5f);
            yield return null;
        }
        TryAttackImmediately();
        isinSkill= false;
    }

    private void UpdateMovement()
    {
        if (attackTarget == null) return;
        if (!tryAttack)
        {
            if(autoNav)
                OnNavMove();
            else
                if (!isbacktoward)
                    StartNav();
        }
        Move();
    }

    public override void Move()
    {
        base.Move();
        detectTimer -= Time.deltaTime;
        /** Whether the Boss should stay off the player */
        if (!CheckDistance() && !isbacktoward) return;
        StopNav();
        Vector3 relatedDir = new Vector3(0, 0, attackTarget.controller.rb.position.z - rb.position.z).normalized;
        rb.MovePosition(rb.position - relatedDir * movementSpeed * Time.fixedDeltaTime);
    }

    private bool CheckDistance()
    {
        if (detectTimer > 0) return false;
        detectTimer = detectInterval;

        isbacktoward = false;
        if (attackTarget != null)
        {
            if (Math.Abs(rb.position.x - attackTarget.controller.rb.position.x) < minVerticalDistance)
            {
                if (Vector3.Distance(rb.position, attackTarget.controller.rb.position) < maintainDistance)
                {
                    isbacktoward = true;
                    return true;
                }
            }
        }

        return false;
    }

    private void StartNav()
    {
        var target = attackTarget.controller.rb;
        //寻找相近的一点并向该方向移动
        float ran = UnityEngine.Random.Range(-offset, offset);
        float dis = ran < 0 ? -1 : 1 * (Math.Abs(offset) + stopDistance);
        Vector3 pos = target.position;
        pos.z += dis;

        if (!agent.SetDestination(pos))
        {
            return;
        }
        autoNav = true;
    }

    private void OnNavMove()
    {
        if (agent.pathPending) return;
        if (agent.pathStatus == NavMeshPathStatus.PathInvalid)
        {
            StopNav();
            return;
        }
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            StopNav();
        }
    }

    private void StopNav()
    {
        autoNav = false;
        agent.ResetPath();
    }

    private void UpdateAttack()
    {
        AINormalAttack();
        AISpecialAttack();
        AISkillAttack();
    }

    private void AISpecialAttack()
    {
        specialTimer -= Time.deltaTime;

        if(specialTimer< 0)
        {
            CastDashandAttack();
            specialTimer = 5f;
        }
    }

    private void AINormalAttack()
    {
        if (normalAttackShutDown) return;
        if (attackTarget != null)
        {
            tryAttack = TryAttack();
        }
    }

    private bool TryAttack()
    {
        if (Math.Abs(rb.position.x - attackTarget.controller.rb.position.x) < minVerticalDistance)
        {
            if(Math.Abs(rb.position.z - attackTarget.controller.rb.position.z) < separatrixAttackRange)
            {
                TurntoMelee();
            }
            else
            {
                TurntoRanged();
            }
            Attack();
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool TryAttackImmediately()
    {
        if (Math.Abs(rb.position.x - attackTarget.controller.rb.position.x) < minVerticalDistance)
        {
            if (Math.Abs(rb.position.z - attackTarget.controller.rb.position.z) < separatrixAttackRange)
            {
                TurntoMelee();
            }
            else
            {
                TurntoRanged();
            }
            boss.AttackImmediately();
            Attack();
            return true;
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

        headDir = rb.position.z > attackTarget.controller.rb.position.z ? Vector3.back : Vector3.forward;
        //spriteRenderer.flipX = headDir.z > 0;

        boss.Attack();
    }

    private void TurntoMelee()
    {
        if (boss.attackStyle == EnemyAttackStyle.Melee) return;
        boss.attackStyle = EnemyAttackStyle.Melee;
        boss.weaponManager.SetWeapon((int)boss.attackStyle);
    }

    private void TurntoRanged()
    {
        if (boss.attackStyle == EnemyAttackStyle.Rifle) return;
        boss.attackStyle = EnemyAttackStyle.Rifle;
        boss.weaponManager.SetWeapon((int)boss.attackStyle);
    }

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
        Debug.Log("Rifle Attack");
    }

    private void AISkillAttack()
    {
        if (!inSecondStep) { return; }

        if (skillTimer > 1000)
        {
            skillTimer = CalculateSkillCoolDown();
        }

        skillTimer -= Time.deltaTime;

        if(skillTimer <= 0)
        {
            CastSkill();
            skillTimer = CalculateSkillCoolDown();
        }
    }

    private void CastSkill()
    {
        normalAttackShutDown = true;

        //TODO : Performing Code
        Debug.Log("Boss Skill");
        StartCoroutine(CastSkillAnim());
    }

    IEnumerator CastSkillAnim()
    {
        StopNav();

        isinSkill = true;
        isinFoucs = true;
        yield return new WaitForSeconds(1.5f);
        Vector3 destination = attackTarget.controller.rb.position;
        yield return new WaitForSeconds(0.5f);
        isinFoucs= false;

        Vector3 dir = (destination - rb.position).normalized;

        while (Vector3.Distance(rb.position, destination) >= 0.5f)
        {
            transform.position = transform.position + dir * Time.fixedDeltaTime * movementSpeed * 2f;
            yield return null;
        }

        //TODO:Bullet

        for (int i = 0; i < 8; i++)
        {
            float ranSpeed = 6f;
            Vector3 ranDir = Quaternion.AngleAxis(45 * i, Vector3.up) * headDir;
            BulletLogic bulletLogic = GameObjectManager.Instance.ShotGunPool.Get();
            bulletLogic.SetDetails(charBase, transform.position, ranDir.normalized, ranSpeed, ranDir);
        }
        Debug.Log("ShotGun Attack");

        yield return new WaitForSeconds(1f);
        isinSkill = false;

        normalAttackShutDown= false;
    }

    private float CalculateSkillCoolDown()
    {
        return UnityEngine.Random.value * 6 + 9; //the range is [9,15]
    }

    public MapIconType GetIconType() => MapIconType.Enemy;

    public string GetName() => charBase.define.Name;

    public Transform GetTransform() => transform;

    public void Interact(KeyCode code)
    {
        if (code != KeyCode.F) return;
        istalkd = true;
        ShowDialogue();
    }

    private void ShowDialogue()
    {
        DialogueManager.Instance.ShowDialogue(Consts.Dialogues.BOSS_Dialogue_Pre, () =>
        {

            if (InputManager.Instance.actObjMap[KeyCode.F] != null)
            {
                UIManager.Instance.RemoveInteractMessage(transform);
                InputManager.Instance.actObjMap[KeyCode.F] = null;
            }

            UIManager.Instance.TurntoBlack(() =>
            {
                UIManager.Instance.TurntoWhite();
                activited = true;
            });
        });
    }

    public void OnSomeTriggerEnter(Collider other)
    {
        if (istalkd) return;
        if (other.CompareTag("Player"))
        {
            if (InputManager.Instance.actObjMap[KeyCode.F] == null)
            {
                InputManager.Instance.actObjMap[KeyCode.F] = this;
                UIManager.Instance.AddInteractMessage("F 对话", transform);
            }
        }
    }

    public void OnSomeTriggerExit(Collider other)
    {
        if (istalkd) return;
        if (other.CompareTag("Player"))
        {
            if (InputManager.Instance.actObjMap[KeyCode.F] != null)
            {
                UIManager.Instance.RemoveInteractMessage(transform);
                InputManager.Instance.actObjMap[KeyCode.F] = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        OnSomeTriggerEnter(other);   
    }

    private void OnTriggerExit(Collider other)
    {
        OnSomeTriggerExit(other);
    }

    /** Handle Boss's Performance */
    private void HandleAnim()
    {
        Vector3 offset = transform.position - lastPos;
        lastPos= transform.position;

        if(Mathf.Abs(offset.z) >= 0.01f)
        {
            var xscale = offset.z > 0 ? -animscale : animscale;
            animationObject.transform.localScale = new Vector3(xscale, animscale, animscale);
        }

        bool temp = offset.sqrMagnitude >= 0.02f * 0.02f;
        if (temp != isRun)
        {
            isRun = temp;
            animator.SetBool("run", isRun);
        }
    }

    private void PlayShotGunAnim()
    {
        animator.SetTrigger("shotgun");
    }

    private void PlayBlinkAnim()
    {
        animator.SetTrigger("blink");
    }

    private void PlayMeleeAnim()
    {
        animator.SetTrigger("sword");
    }

    public void Melee_AttackPerformance()
    {

    }

    public void ShotGun_AttackPerformance()
    {
        PlayShotGunAnim();
    }
}
