using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview:����������ҽ����ƶ�
*/

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f; // ����ƶ��ٶ�

    // Update is called once per frame
    void Update()
    {
        // ��ȡ�������
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // ������ҵ��ƶ�����
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0).normalized * speed * Time.deltaTime;

        // Ӧ���ƶ���������ɫλ��
        transform.position += movement;
    }
}
