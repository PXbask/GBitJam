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

public class PlayerController : PXCharacterController
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
    public override Vector3 GetBulletHeadDirection()
    {
        return movement.headDir;
    }
}
