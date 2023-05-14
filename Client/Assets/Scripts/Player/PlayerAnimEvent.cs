using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview:
*/

public class PlayerAnimEvent : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    public void RifleORShotGun()
    {
        if (playerController.userifleFlag)
        {
            playerController.Rifle_Attack();
        }
        else
        {
            playerController.ShotGun_Attack();
        }
    }

    public void Melee()
    {
        playerController.Melee_Attack();
    }
}
