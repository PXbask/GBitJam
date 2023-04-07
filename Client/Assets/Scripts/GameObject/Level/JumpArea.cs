using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview:
*/

public class JumpArea : TrapLogic
{
    [Tooltip("�����Ծʱ�ķ���,������y�᷽��")]
    public Vector3 dir;
    [Tooltip("�����Ծ�ľ���,����Ծ�ٶȳ�����")]
    public float jumpdes;

    protected override void OnInit()
    {
        base.OnInit();
        interactKey = KeyCode.Space;
        tipStr = "Space ��Ծ";
    }

    protected override void OnInteract(KeyCode code)
    {
        base.OnInteract(code);
        controller.rb.velocity = Vector3.ProjectOnPlane(dir, Vector3.up) * jumpdes + Vector3.up * 5f;
        //controller.Status = PlayerStatus.Jump;
    }
}
