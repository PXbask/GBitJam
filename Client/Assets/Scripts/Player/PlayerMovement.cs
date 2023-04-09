using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
/*
    Date:
    Name:
    Overview:����������ҽ����ƶ�
*/

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [Header("�ƶ��ٶ�")]
    public float speed = 4.0f;
    [Header("�ƶ�����")]
    public float speedMultiple = 1.00f;
    private float m_speed;

    [SerializeField]
    private FpsInput input;

    public Vector3 headDir;//�������������ȷ�����䷽��,��λ����
    public Vector3 direction;//����˶�����,��λ����
    //��������
    [Serializable]
    private class FpsInput
    {
        [Tooltip("ˮƽ����"),
         SerializeField]
        private string move = "Horizontal";

        [Tooltip("��ֱ����"),
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
        // ������ҵ��ƶ�����
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
