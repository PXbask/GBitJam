using Manager;
using Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

/*
    Date:
    Name:
    Overview:人物表现层
*/

public class CharController : EntityController
{
    public Camera mainCamera;
    public PlayerMovement movement;
    public SpriteRenderer render;
    [SerializeField] PlayerStatus status;
    public PlayerStatus Status
    {
        get { return status; }
        set
        {
            switch (value)
            {
                case PlayerStatus.None:
                    InputManager.Instance.PlayerMovementEnabled(true);
                    break;
                case PlayerStatus.Jump:
                    InputManager.Instance.PlayerMovementEnabled(false);
                    break;
                default:
                    break;
            }
            status = value;
        }
    }

    public Transform effectRoot;
    public Rigidbody rb;
    public CapsuleCollider _collider;
    private readonly RaycastHit[] _groundCastResults = new RaycastHit[8];
    private bool isground = true;
    public bool Isground
    {
        get => isground;
        set
        {
            Status = value ? PlayerStatus.None : PlayerStatus.Jump;
            isground = value;
        }
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        status = PlayerStatus.None;
        mainCamera = Camera.main;

        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        CheckGround();
    }
    public void Move()
    {
        movement.Move();

        render.flipX = movement.headDir.z > 0;
    }
    public override void Attack()
    {
        var WeaponConfig = UserManager.Instance.CurrentWeapon;
        if (WeaponConfig == null) return;
        if (WeaponConfig.IsUnderCooling) return;
        switch (WeaponConfig.ID)
        {
            case 1://刀

                break;
            case 2://步枪
                RifleAtk();
                break;
            case 3://霰弹枪
                ShotGunAtk();
                break;
            default: break;
        }
        WeaponConfig.OnFire();
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
    public override Vector3 GetFireDirection(float speed)
    {
        return movement.headDir * speed + movement.direction;
    }
    public override Vector3 GetBulletHeadDirection()
    {
        return movement.headDir;
    }
}
