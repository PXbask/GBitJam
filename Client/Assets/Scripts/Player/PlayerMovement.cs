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
    public float speed = 2.0f;
    [Header("移动倍率")]
    public float speedMultiple = 1.00f;

    [SerializeField]
    private FpsInput input;
    void Start()
    {
        speed *= speedMultiple;
        rb = GameManager.Instance.player.rb;
    }
    public void Move()
    {
        // 计算玩家的移动向量
        var direction = new Vector3(input.Strafe, 0f, -input.Move).normalized;
        //移动
        var movement = direction * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
    }
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
}
