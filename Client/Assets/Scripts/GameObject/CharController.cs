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

    public Player charBase;
    public AtkStyle atkStyle;

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
        rifleBltPre = Resloader.Load<GameObject>("Prefab/GameObject/rifleBlt");
        shotgunBltPre = Resloader.Load<GameObject>("Prefab/GameObject/shotgunBlt");
        meleeEffectPre = Resloader.Load<GameObject>("Prefab/GameObject/meleeEffect");
        atkStyle = AtkStyle.Rifle;
        status = PlayerStatus.None;
        mainCamera = Camera.main;

        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        switch (Status)
        {
            case PlayerStatus.None:
                
                break;
            case PlayerStatus.Jump:
                
                break;
            default:
                break;
        }

    }
    private void FixedUpdate()
    {
        CheckGround();
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
    public void MeleeAtk()
    {
        Vector3 dir = Direction2Mouse();
        StartCoroutine(InitMeleeEffect(dir));

        Debug.Log("MeleeAtk");
    }
    IEnumerator InitMeleeEffect(Vector3 dir)
    {
        GameObject obj = Instantiate(meleeEffectPre, transform.position, Quaternion.identity);
        obj.transform.up = dir;
        SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
        while(renderer.color.a > 0)
        {
            Color color= renderer.color;
            color.a -= 225/225f * Time.fixedDeltaTime;
            renderer.color = color; 
            yield return null;
        }
        Destroy(obj);
    }
    public void ShotGunAtk()
    {
        Vector3 dir = Direction2Mouse();
        for(int i = 0; i < 5; i++)
        {
            float ranSpeed = Random.Range(8, 12);
            Vector3 ranDir = RandomDirection(dir, 30);
            GameObject obj = Instantiate(shotgunBltPre, transform.position, Quaternion.identity);
            obj.transform.up = ranDir;
            BulletLogic bulletLogic = obj.GetComponent<BulletLogic>();
            bulletLogic.direction = ranDir;
            bulletLogic.speed = ranSpeed;
        }
        Debug.Log("ShotGunAtk");
    }
    public void RifleAtk()
    {
        Vector3 dir = Direction2Mouse();
        GameObject obj = Instantiate(rifleBltPre,transform.position,Quaternion.identity);
        obj.transform.up = dir;
        BulletLogic bulletLogic = obj.GetComponent<BulletLogic>();
        bulletLogic.direction = dir;
        bulletLogic.speed = 18f;
        Debug.Log("RifleAtk");
    }
    Vector3 Direction2Mouse()
    {
        Vector3 pos = Input.mousePosition;
        Vector3 mousePos = new Vector3(pos.x / 1920f, pos.y / 1080f, 0);
        Vector3 screenPos = mainCamera.WorldToViewportPoint(gameObject.transform.position);
        screenPos.z = 0;
        return (mousePos-screenPos).normalized;
    }
    Vector3 RandomDirection(Vector3 origin, float degreeRange)
    {
        float randomAngle = Random.Range(-degreeRange, degreeRange);

        Vector3 randomAxis = Vector3.forward;

        // 通过随机轴和角度生成旋转四元数
        Quaternion randomRotation = Quaternion.AngleAxis(randomAngle, randomAxis);

        // 将原始方向与随机旋转相乘得到返回的方向
        Vector3 returnDirection = randomRotation * origin;
        return returnDirection;
    }
    #region simpleFuncs
    IEnumerator Status2Jump()
    {
        yield return new WaitForSeconds(0.2f); 
        status = PlayerStatus.Jump;
    }
    #endregion 
}
