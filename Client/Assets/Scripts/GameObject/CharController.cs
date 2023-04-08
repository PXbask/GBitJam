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

public class CharController : MonoBehaviour
{
    public Camera mainCamera;
    public PlayerMovement movement;
    public SpriteRenderer render;
    public Player charBase;
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
    [HideInInspector] public GameObject rifleBltPre;
    [HideInInspector] public GameObject shotgunBltPre;
    [HideInInspector] public GameObject meleeEffectPre;
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

    private void Awake()
    {
        rifleBltPre = Resloader.Load<GameObject>("Prefab/GameObject/rifleBullet");
        shotgunBltPre = Resloader.Load<GameObject>("Prefab/GameObject/shotgunBullet");
        meleeEffectPre = Resloader.Load<GameObject>("Prefab/GameObject/meleeEffect");
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
    public void Attack()
    {
        UserManager.Instance.CurrentWeapon?.Fire();
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
    public void ShotGunAtk()
    {
        for(int i = 0; i < 5; i++)
        {
            float ranSpeed = Random.Range(8, 12);
            var dir = GetFireDirection(ranSpeed);
            Vector3 ranDir = RandomDirection(dir, 30);
            GameObject obj = Instantiate(shotgunBltPre, transform.position, Quaternion.identity);
            obj.transform.up = ranDir;
            BulletLogic bulletLogic = obj.GetComponent<BulletLogic>();
            bulletLogic.SetInfo(ranDir.normalized, ranSpeed);
        }
        Debug.Log("ShotGunAtk");
    }
    public void RifleAtk()
    {
        Vector3 dir = GetFireDirection(18f);
        GameObject obj = Instantiate(rifleBltPre, transform.position, Quaternion.identity);
        obj.transform.up = movement.headDir;
        BulletLogic bulletLogic = obj.GetComponent<BulletLogic>();
        bulletLogic.SetInfo(dir.normalized, 18f);
        Debug.Log("RifleAtk");
    }
    Vector3 RandomDirection(Vector3 origin, float degreeRange)
    {
        float randomAngle = Random.Range(-degreeRange, degreeRange);

        Vector3 randomAxis = transform.up;

        // 通过随机轴和角度生成旋转四元数
        Quaternion randomRotation = Quaternion.AngleAxis(randomAngle, randomAxis);

        // 将原始方向与随机旋转相乘得到返回的方向
        Vector3 returnDirection = randomRotation * origin;
        return returnDirection;
    }
    private Vector3 GetFireDirection(float speed)
    {
        return movement.headDir * speed + movement.direction;
    }
    #region simpleFuncs

    #endregion 
}
