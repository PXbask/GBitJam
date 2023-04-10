using Battle;
using Define;
using Manager;
using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview:子弹的表现
*/

public class BulletLogic : MonoBehaviour
{
    [SerializeField] Material playerMat;
    [SerializeField] Material enemyMat;
    public Creature owner;
    public bool IsPlayer => owner.IsPlayer;
    public BattleContext context = new BattleContext();

    public VolumetricLines.VolumetricLineBehavior lineBehavior;
    public Rigidbody rb;
    public Vector3 direction;
    public float speed;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        lineBehavior = GetComponent<VolumetricLines.VolumetricLineBehavior>();
    }
    public void SetDetails(Creature owner, Vector3 startPos, Vector3 dir, float speed, Vector3 updir)
    {
        SetContext(owner, owner.weaponManager.WeaponConfig);
        SetInfo(startPos, dir, speed, updir);
    }
    private void SetContext(Creature owner, WeaponInfo info)
    {
        this.owner = owner;

        context.curAttri = owner.attributes.curAttribute;
        context.weapon = info.define;
    }
    private void OnTriggerEnter(Collider other)
    {
        //10->Player 15->Enemy 16->PlayerBullet 17->EnemyBullet
        if ((gameObject.layer == 16 && other.gameObject.layer==15) || (gameObject.layer == 17 && other.gameObject.layer == 10))
        {
            if (other.gameObject.TryGetComponent<PXCharacterController>(out var controller))
            {
                controller.ReceiveDamage(context);
            }
        }
        if ((gameObject.layer == 16 && other.gameObject.layer == 10) || (gameObject.layer == 17 && other.gameObject.layer == 15))
        {
            return;
        }
        //存入对象池
        ReleaseToPools();
    }
    private void SetInfo(Vector3 startPos, Vector3 dir, float speed, Vector3 updir)
    {
        //Layer
        gameObject.layer = IsPlayer? 16: 17;//16->PlayerBullet 17->EnemyBullet
        //Meterials
        //lineBehavior.TemplateMaterial = IsPlayer ? playerMat : enemyMat;
        lineBehavior.LineColor = IsPlayer? Color.blue: Color.red;
        //Physics
        rb.MovePosition(startPos);
        rb.velocity = Vector3.zero;
        //Calculate
        this.direction= dir;
        this.speed = speed;
        transform.up= updir;
        rb.AddForce(speed * direction, ForceMode.VelocityChange);
    }
    public void ReleaseToPools()
    {
        switch (context.weapon.ID)
        {
            case 2:
                GameObjectManager.Instance.RiflePool.Release(this);
                break;
            case 3:
                GameObjectManager.Instance.ShotGunPool.Release(this);
                break;
            default: break;
        }
    }
    private void OnBecameInvisible()
    {
        if(gameObject.activeInHierarchy)
            ReleaseToPools();
    }
}
