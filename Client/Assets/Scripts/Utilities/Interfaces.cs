using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview:���ֽӿ�
*/

public interface IInteractable
{
    void Interact(KeyCode code);
}
public interface IAttackable
{
    void Melee_Attack();
    void Move();
    Rigidbody GetRigidbody();
    void OnDeath();
    void Rifle_Attack();
    void ShotGun_Attack();
}