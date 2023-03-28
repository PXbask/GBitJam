using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview:ÌÝ×Ó
*/

public class Ladder : MonoBehaviour
{
    [SerializeField] CharController charController;
    private void Start()
    {
        charController = GameManager.Instance.charc;
    }
    private void OnTriggerEnter(Collider other)
    {
        UIManager.Instance.OpenWorldTip("Space ÉÏÉý", charController.transform);
    }
    private void OnTriggerExit(Collider other)
    {
        UIManager.Instance.CloseWorldTip();
        charController.rb.useGravity = true;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                charController.rb.useGravity= false;
                charController?.rb.MovePosition(charController.rb.position + Vector3.up * Time.deltaTime);
            }
        }
        //if(charController == null)
        //{
        //    if (other.gameObject.CompareTag("Player"))
        //    {
        //        charController = other.gameObject.GetComponent<CharController>();
        //    }
        //}
        //else
        //{
        //    charController.rb.MovePosition(charController.rb.position+Vector3.up*Time.deltaTime);
        //}

    }
}
