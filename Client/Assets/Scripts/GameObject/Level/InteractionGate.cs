using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview:¿É½»»¥ÃÅ
*/

public class InteractionGate : MonoBehaviour
{
    [SerializeField] GameObject locks;
    public bool Islocked => locks.activeInHierarchy;
    public void OpenGate()
    {
        StartCoroutine(IEOpenGate());
    }
    IEnumerator IEOpenGate()
    {
        float yscale = 1;
        while(yscale > 0)
        {
            for (int i = 0; i < locks.transform.childCount; i++)
            {
                locks.transform.GetChild(i).localScale = new Vector3(1, yscale, 1);
            }
            yscale -= 1 * Time.deltaTime;
            yield return null;
        }
        locks.SetActive(false);
        yield return null;
    }
}
