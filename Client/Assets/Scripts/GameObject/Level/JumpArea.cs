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
    [Tooltip("玩家跳跃时的方向,不考虑y轴方向")]
    public Vector3 dir;
    [Tooltip("玩家跳跃的距离,与跳跃速度成正比")]
    public float jumpdes;

    protected override void OnInit()
    {
        base.OnInit();
        interactKey = KeyCode.Space;
        tipStr = "Space 跳跃";
    }

    protected override void OnInteract(KeyCode code)
    {
        base.OnInteract(code);
        controller.rb.velocity = Vector3.ProjectOnPlane(dir, Vector3.up) * jumpdes + Vector3.up * 5f;
        //controller.Status = PlayerStatus.Jump;
    }
}
