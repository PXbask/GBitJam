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
    Camera mainCamera;
    public Vector3 direction;
    public float speed;
    private void Awake()
    {
        mainCamera= Camera.main;
        speed= 10.0f;
    }
    private void Update()
    {
        Vector3 pos = transform.position;
        pos += direction.normalized * speed * Time.deltaTime;
        transform.position = pos;

        if (IsOutOfCamera())
        {
            Destroy(gameObject);
        }
    }
    bool IsOutOfCamera()
    {
        Vector3 screenPos = mainCamera.WorldToViewportPoint(gameObject.transform.position);
        // 判断屏幕坐标是否在视口范围内
        return (screenPos.x < 0 || screenPos.x > 1 || screenPos.y < 0 || screenPos.y > 1);
    }
}
