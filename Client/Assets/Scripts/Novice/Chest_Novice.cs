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

        StartCoroutine(Open());

        MiniMapManager.Instance.Remove(this);
        if(NoviceManager.Instance != null)
            NoviceManager.Instance.Step = NoviceManager.NoviceStep.GainedFirstTitleDialogue;
    }

    IEnumerator Open()
    {
        SoundManager.Instance.PlayOpenChestSound();
        TitleManager.Instance.GainTitle(1);
        yield return new WaitForSecondsRealtime(0.3f);
        TitleManager.Instance.GainTitle(2);
        yield return new WaitForSecondsRealtime(0.3f);
        TitleManager.Instance.GainTitle(3);
        yield return new WaitForSecondsRealtime(0.3f);
        TitleManager.Instance.GainTitle(80);
    }
}
