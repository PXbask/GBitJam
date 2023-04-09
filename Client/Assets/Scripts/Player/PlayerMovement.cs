using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
/*
    Date:
    Name:
    Overview:接受输入玩家进行移动
*/

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [Header("移动速度")]
    public float speed = 4.0f;
    [Header("移动倍率")]
    public float speedMultiple = 1.00f;
    private float m_speed;

    [SerializeField]
    private FpsInput input;

    public Vector3 headDir;//玩家面向方向，用于确定发射方向,单位向量
    public Vector3 direction;//玩家运动方向,单位向量
    //输入设置
    [Serializable]
    private class FpsInput
    {
        [Tooltip("水平输入"),
         SerializeField]
        private string move = "Horizontal";

        [Tooltip("垂直输入"),
         SerializeField]
        private string strafe = "Vertical";


        public float Move
        {
            get { return Input.GetAxisRaw(move); }
        }

        public float Strafe
        {
            get { return Input.GetAxisRaw(strafe); }
        }
    }
    void Start()
    {
        m_speed = speed * speedMultiple;
        rb = GameManager.Instance.player.rb;
        headDir = transform.forward;
    }
    public void Move()
    {
        // 计算玩家的移动向量
        var dir = new Vector3(input.Strafe, 0f, -input.Move).normalized;

        if (dir.magnitude >= 0.05f)
        {
            if (Math.Abs(dir.z) >= 0.025f)
                headDir = new Vector3(0, 0, dir.z > 0 ? 1 : -1);
            var movement = dir * m_speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);
            direction= dir;
        }
        else
            direction = Vector3.zero;
    }
}
