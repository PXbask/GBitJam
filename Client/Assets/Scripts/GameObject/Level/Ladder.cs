using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview:梯子
*/

public class Ladder : TrapLogic
{
    public int Length;
    public bool isClimbing = false;
    protected override void OnInit()
    {
        base.OnInit();
        interactKey = KeyCode.Space;
        tipStr = "Space 上升";
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            if (InputManager.Instance.actObjMap[interactKey] == null)
            {
                if(Vector3.Distance(controller.rb.position, transform.position) <= 1f)
                {
                    InputManager.Instance.actObjMap[interactKey] = this;
                    UIManager.Instance.AddInteractMessage(tipStr, transform);
                }
            }
        }
    }
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        controller.rb.useGravity = true;
    }
    protected override void OnInteract(KeyCode code)
    {
        base.OnInteract(code);
        if (isClimbing) return;
        UIManager.Instance.RemoveInteractMessage(transform);
        StartCoroutine(ClimbLadderAnim());
    }
    IEnumerator ClimbLadderAnim()
    {
        //把大象装进冰箱需要几步?
        controller.rb.useGravity = false;
        isClimbing= true;
        controller?.rb.MovePosition(transform.position);
        while (!IsExitLadder())
        {
            controller?.rb.MovePosition(controller.rb.position + Vector3.up * Time.deltaTime * 2f);
            yield return null;
        }
        controller.rb.velocity = (Vector3.up - transform.right * 0.25f) * 5;
        //float dis = 0;
        //while (dis <= 1f)
        //{
        //    controller?.rb.MovePosition(controller.rb.position - transform.right * Time.deltaTime);
        //    dis += (Time.deltaTime);
        //    yield return null;
        //}
        controller.rb.useGravity = true;
        isClimbing= false;
        yield return null;
    }
    private bool IsExitLadder()
    {
        float y = controller._collider.bounds.extents.y;
        Ray ray = new Ray(controller.rb.position + Vector3.down * (y - 0.05f), -transform.right);
        return !Physics.Raycast(ray, 0.25f, ~(1<<10), QueryTriggerInteraction.Collide);        
    }
}
