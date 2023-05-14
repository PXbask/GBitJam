using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview:
*/

public class BossAnimEvent : MonoBehaviour
{
    [SerializeField] BossController bossController;
    public void Melee()
    {
        bossController.Melee_Attack();
    }

    public void ShotGun()
    {
        if(bossController.headDir.z > 0)
        {
            bossController.animationObject.transform.localScale = new Vector3(-bossController.animscale, bossController.animscale, bossController.animscale);
        }
        else
        {
            bossController.animationObject.transform.localScale = new Vector3(bossController.animscale, bossController.animscale, bossController.animscale);
        }
        bossController.Rifle_Attack();
    }
}
