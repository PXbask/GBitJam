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
    void Attack(KeyCode code);
}