using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview:接受输入玩家进行移动
*/

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f; // 玩家移动速度

    // Update is called once per frame
    void Update()
    {
        // 获取玩家输入
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // 计算玩家的移动向量
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0).normalized * speed * Time.deltaTime;

        // 应用移动向量到角色位置
        transform.position += movement;
    }
}
