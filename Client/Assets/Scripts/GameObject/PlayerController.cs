using Battle;
using Manager;
using Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
    Date:
    Name:
    Overview:人物表现层
*/

public class PlayerController : PXCharacterController, IVisibleinMap
{
    public Camera mainCamera;
    public PlayerMovement movement;
    public SpriteRenderer render;
    [SerializeField] PlayerState state;
    public PlayerState State
    {
        get { return state; }
        set
        {
            switch (value)
            {
                case PlayerState.None:
                    InputManager.Instance.PlayerMovementEnabled(true);
                    break;
                case PlayerState.Jump:
                    InputManager.Instance.PlayerMovementEnabled(false);
                    break;
                default:
                    break;
            }
            state = value;
        }
    }

    public Transform effectRoot;
    public CapsuleCollider _collider;
    private readonly RaycastHit[] _groundCastResults = new RaycastHit[8];
    private bool isground = true;
    public bool Isground
    {
        get => isground;
        set
        {
            State = value ? PlayerState.None : PlayerState.Jump;
            isground = value;
        }
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        state = PlayerState.None;
        mainCamera = Camera.main;

        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        MiniMapManager.Instance.Register(this);
    }
    private void FixedUpdate()
    {
        CheckGround();
    }
    public override void Move()
    {
        movement.Move();

        render.flipX = movement.headDir.z > 0;
    }
    private void CheckGround()
    {
        var bounds = _collider.bounds;
        var extents = bounds.extents;
        var radius = extents.x - 0.01f;
        Physics.SphereCastNonAlloc(bounds.center, radius, Vector3.down,
            _groundCastResults, extents.y - radius * 0.5f, 1 << 11, QueryTriggerInteraction.Ignore);
        if (!_groundCastResults.Any(hit => hit.collider != null && hit.collider != _collider))
        {
            Isground = false;
            return;
        }
        for (var i = 0; i < _groundCastResults.Length; i++)
        {
            _groundCastResults[i] = new RaycastHit();
        }

        Isground = true;
    }
    public override void Melee_Attack()
    {
        base.Melee_Attack();
        m_colliders = Physics.OverlapSphere(transform.position, charBase.weaponManager.WeaponConfig.define.Range, LayerMask.GetMask("Enemy"));
        foreach (var c in m_colliders)
        {
            var ctr = c.gameObject.GetComponent<EnemyController>();
            ctr.charBase.ReceiveDamage(new BattleContext
            {
                attacker = charBase,
                weapon = charBase.weaponManager.WeaponConfig.define
            });
        }
    }
    public override Vector3 GetBulletHeadDirection()
    {
        return movement.headDir;
    }
    public string GetName() { return "Player"; }

    public Transform GetTransform() { return transform; }

    public MapIconType GetIconType() { return MapIconType.Player; }
}
