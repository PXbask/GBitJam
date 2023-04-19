using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview:
*/

public class NoviceEndArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            GameManager.Instance.Status = GameStatus.Game;
            Destroy(NoviceManager.Instance.gameObject);
            Debug.Log("<color=#FF0000>NoviceManager Destroyed</color>");
        }
    }
}
