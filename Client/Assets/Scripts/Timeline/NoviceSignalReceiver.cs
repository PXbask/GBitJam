using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/*
    Date:
    Name:
    Overview:新手引导事件
*/

public class NoviceSignalReceiver : MonoBehaviour
{
    PlayableDirector Director;
    private void Start()
    {
        Director= GetComponent<PlayableDirector>();
    }
    public void PlayOperBtnTeaching()
    {
        Director.playableGraph.GetRootPlayable(0).SetSpeed(0);
        GameManager.Instance.Status = GameStatus.Dialoguing;
        DialogueManager.Instance.ShowDialogue(Consts.Dialogues.Operation_Btn_Teaching_3, 
            ()=> { Director.playableGraph.GetRootPlayable(0).SetSpeed(0); });
    }
    public void SequenceStop()
    {
        Director.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }
    IEnumerator IEPressButton(KeyCode code)
    {
        yield return new WaitUntil(() => Input.GetKeyDown(code));
        yield return new WaitUntil(() => Input.GetKeyUp(code));
    }
}
