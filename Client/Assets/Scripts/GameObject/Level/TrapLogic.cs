using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview:
*/

public abstract class TrapLogic : MonoBehaviour, IInteractable
{
    public CharController controller;
    protected KeyCode interactKey;
    [SerializeField] protected string tipStr;
    protected string targetTag;
    protected virtual void OnInit()
    {
        controller = GameManager.Instance.player;
        interactKey = KeyCode.None;
        tipStr = string.Empty;
        targetTag = "Player";
    }
    private void Start()
    {
        OnInit();
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            if (InputManager.Instance.actObjMap[interactKey] == null)
            {
                InputManager.Instance.actObjMap[interactKey] = this;
                UIManager.Instance.AddInteractMessage(tipStr, transform);
            }
        }
        //UIManager.Instance.OpenWorldTip(tipStr, transform);
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        //UIManager.Instance.CloseWorldTip();
        if (other.CompareTag(targetTag))
        {
            if (InputManager.Instance.actObjMap[interactKey] != null)
            {
                UIManager.Instance.RemoveInteractMessage(transform);
                InputManager.Instance.actObjMap[interactKey] = null;
            }
        }
    }
    protected virtual void OnInteract(KeyCode code)
    {
        if (code != interactKey) return;
    }
    public void Interact(KeyCode code)
    {
        OnInteract(code);
    }
}
