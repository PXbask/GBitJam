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

public class Chest_Novice : Chest
{
    protected override void OpenChest()
    {
        if (isOpened) { return; }
        animat.Play();
        outline.enabled = false;
        isOpened = true;
        TitleManager.Instance.GainTitle(1);
        MiniMapManager.Instance.Remove(this);
        NoviceManager.Instance.Step = NoviceManager.NoviceStep.GainedFirstTitleDialogue;
    }
}
