using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview:
*/

public class CheckPoint : MonoBehaviour
{
    public Vector3 position => transform.position;

    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer= GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        meshRenderer.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            UserManager.Instance.SetCheckPoint(position);
            gameObject.SetActive(false);
        }
    }
}
