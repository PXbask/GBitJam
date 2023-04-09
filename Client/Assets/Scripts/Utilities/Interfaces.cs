using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview:各种接口
*/

public interface IInteractable
{
    void Interact(KeyCode code);
}
public interface IAttackable
{
    void Melee_Attack();
    void Rifle_Attack();
    void ShotGun_Attack();
}