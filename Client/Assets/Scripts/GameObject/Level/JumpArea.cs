using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview:
*/

public class JumpArea : MonoBehaviour
{
    [Tooltip("�����Ծʱ�ķ���,������y�᷽��")]
    public Vector3 dir;
    [Tooltip("�����Ծ�ľ���,����Ծ�ٶȳ�����")]
    public float jumpdes;
    CharController controller;
    private void Start()
    {
        controller = GameManager.Instance.charc;
    }
    private void OnTriggerEnter(Collider other)
    {
        UIManager.Instance.OpenWorldTip("Space ��Ծ", controller.transform);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                controller.rb.velocity = Vector3.ProjectOnPlane(dir, Vector3.up)*jumpdes + Vector3.up * 5f;
                controller.Status = PlayerStatus.Jump;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        UIManager.Instance.CloseWorldTip();
    }
}
