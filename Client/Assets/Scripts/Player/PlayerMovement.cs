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


    [Header("�ƶ��ٶ�")]
    public float speed = 2.0f;
    [Header("�ƶ�����")]
    public float speedMultiple = 1.00f;

    [SerializeField]
    private FpsInput input;
    void Start()
    {
        speed *= speedMultiple;
    }
    void FixedUpdate()
    {
        // ������ҵ��ƶ�����
        var direction = new Vector3(input.Strafe, 0f, -input.Move).normalized;
        //�ƶ�
        var movement = direction * speed * Time.deltaTime;
        // Ӧ���ƶ���������ɫλ��
        transform.position += movement;
    }

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
}
