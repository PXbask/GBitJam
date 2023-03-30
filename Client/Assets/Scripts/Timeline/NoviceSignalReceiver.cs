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
        DialogueManager.Instance.ShowDialogue(Consts.Dialogues.Operation_Btn_Teaching, 
            ()=> { Director.playableGraph.GetRootPlayable(0).SetSpeed(0); });
    }
    public void PlayBagTeachingPre()
    {
        StartCoroutine(IEPlayBagTeachingPre());
    }
    IEnumerator IEPlayBagTeachingPre()
    {
        Director.playableGraph.GetRootPlayable(0).SetSpeed(0);
        yield return StartCoroutine(DialogueManager.Instance.IEShowDialogue(Consts.Dialogues.Pre_TitleEqu_Teaching));
    }
    IEnumerator IEPressButton(KeyCode code)
    {
        yield return new WaitUntil(() => Input.GetKeyDown(code));
        yield return new WaitUntil(() => Input.GetKeyUp(code));
    }
}
