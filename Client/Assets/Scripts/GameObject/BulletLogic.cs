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
    public Rigidbody rb;
    public Vector3 direction;
    public float speed;
    //private void Update()
    //{
    //    Vector3 pos = transform.position;
    //    pos += direction.normalized * speed * Time.deltaTime;
    //    transform.position = pos;
    //}
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer != 10)//角色
            Destroy(gameObject);
    }
    public void SetInfo(Vector3 dir, float speed)
    {
        this.direction= dir;
        this.speed = speed;
        rb.AddForce(speed * direction, ForceMode.VelocityChange);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
