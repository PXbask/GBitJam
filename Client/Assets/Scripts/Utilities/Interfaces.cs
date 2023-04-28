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
public interface IVisibleinMap
{
    string GetName();
    Transform GetTransform();
    MapIconType GetIconType();
}