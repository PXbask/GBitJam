using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview:
*/

public abstract class TrapLogic : MonoBehaviour, IInteractable<Collider>
{
    public PlayerController Controller => GameManager.Instance.player;
    protected KeyCode interactKey;
    [SerializeField] protected string tipStr;
    protected string targetTag;
    protected virtual void OnInit()
    {
        interactKey = KeyCode.None;
        tipStr = string.Empty;
        targetTag = "Player";
    }
    private void Start()
    {
        OnInit();
    }
    protected void OnTriggerEnter(Collider other)
    {
        OnSomeTriggerEnter(other);
        //UIManager.Instance.OpenWorldTip(tipStr, transform);
    }
    protected void OnTriggerExit(Collider other)
    {
        OnSomeTriggerExit(other);
        //UIManager.Instance.CloseWorldTip();
    }
    protected virtual void OnInteract(KeyCode code)
    {
        if (code != interactKey) return;
    }
    public void Interact(KeyCode code)
    {
        OnInteract(code);
    }

    public virtual void OnSomeTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            if (InputManager.Instance.actObjMap[interactKey] == null)
            {
                InputManager.Instance.actObjMap[interactKey] = this;
                UIManager.Instance.AddInteractMessage(tipStr, transform);
            }
        }
    }

    public virtual void OnSomeTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            if (InputManager.Instance.actObjMap[interactKey] != null)
            {
                UIManager.Instance.RemoveInteractMessage(transform);
                InputManager.Instance.actObjMap[interactKey] = null;
            }
        }
    }
}
