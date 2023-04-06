using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    Date:
    Name:
    Overview:
*/

public class UITitleSlot : MonoBehaviour
{
    public Image image;
    public int id;

    public void ResetStatus()
    {
        int slotmax = UserManager.Instance.slotMax;
        image.color = slotmax > id ? Color.white : Color.gray;
    }
}
