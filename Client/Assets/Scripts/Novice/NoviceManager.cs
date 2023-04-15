using Manager;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
    Date:
    Name:
    Overview:
*/

public class NoviceManager : MonoSingleton<NoviceManager>
{
    public GameObject confiner_1;
    public GameObject confiner_2;

    NoviceStep step = NoviceStep.None;
    public NoviceStep Step
    {
        get => step;
        set
        {
            switch (value)
            {
                case NoviceStep.None:
                    break;
                case NoviceStep.Start:
                    DialogueManager.Instance.ShowDialogue(Consts.Dialogues.Operation_Btn_Teaching_1);
                    break;
                case NoviceStep.GainedFirstTitle:
                    StartCoroutine(IEAfterGainFirstTitle());
                    break;
                default:
                    break;
            }
            step = value;
            Debug.LogFormat("Beginner Novice Status: <color=#00FF00>[{0}]</color>", value.ToString());
        }
    }
    public void StartNovice()
    {
        Step = NoviceStep.Start;
    }
    private void Update()
    {
        switch (Step)
        {
            case NoviceStep.None:
                break;
            case NoviceStep.GainedFirstTitle:
                break;
            default:
                break;
        }
    }
    public enum NoviceStep
    {
        None,
        Start,
        GainedFirstTitle,
    }
    IEnumerator IEAfterGainFirstTitle()
    {
        bool diafinish = false;
        DialogueManager.Instance.ShowDialogue(Consts.Dialogues.Operation_Btn_Teaching_2, () => diafinish = true);
        yield return new WaitUntil(() => diafinish);
        //当玩家装载了芯片后 继续
        yield return new WaitUntil(() => TitleManager.Instance.EquipedTitle.Where(x => x.ID == 1).Any());
        yield return new WaitUntil(() => !UIManager.Instance.HasUIOverlay);

        diafinish = false;
        DialogueManager.Instance.ShowDialogue(Consts.Dialogues.Operation_Btn_Teaching_2, () => diafinish = true);
        yield return new WaitUntil(() => diafinish);
        //限制解除，玩家可以继续前进
        confiner_1.SetActive(false);


    }
}
