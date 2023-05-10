using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview:各种接口
*/

public interface IInteractable<T>
{
    void Interact(KeyCode code);
    void OnSomeTriggerEnter(T Collider);
    void OnSomeTriggerExit(T Collider);
}
public interface IVisibleinMap
{
    string GetName();
    Transform GetTransform();
    MapIconType GetIconType();
}