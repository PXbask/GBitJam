using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview:人物表现层
*/

public class CharController : MonoBehaviour
{
    public Camera mainCamera;

    public CharBase charBase;
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
                    status = PlayerStatus.None;
                    break;
                case PlayerStatus.Jump:
                    StartCoroutine(Status2Jump());
                    break;
            }
        }
    }

    [HideInInspector] public GameObject rifleBltPre;
    [HideInInspector] public GameObject shotgunBltPre;
    [HideInInspector] public GameObject meleeEffectPre;
    [HideInInspector] public PolygonCollider2D cldr2D;
    [HideInInspector] public Cinemachine.CinemachineConfiner2D confiner;

    public Transform effectRoot;
    public Rigidbody rb;

    private Collider[] colliders;
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
        switch (status)
        {
            case PlayerStatus.None:
                
                break;
            case PlayerStatus.Jump:
                bool isground = IsOnGround();
                if (isground) Status = PlayerStatus.None;
                InputManager.Instance.PlayerMovementEnabled(isground);
                break;
            default:
                break;
        }
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
    public bool IsOnGround()
    {
        colliders = Physics.OverlapCapsule(transform.position, transform.position, 1f, (1 << 11));
        return colliders.Length > 0;
    }
    #region simpleFuncs
    IEnumerator Status2Jump()
    {
        yield return new WaitForSeconds(0.2f); 
        status = PlayerStatus.Jump;
    }
    #endregion 
}
