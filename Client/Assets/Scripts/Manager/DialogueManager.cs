using Define;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{

    /*
        Date:
        Name:
        Overview:
    */

    public class DialogueManager : MonoSingleton<DialogueManager>
    {
        public Action OnConversationStart = null;
        public Action OnConversationEnd = null;
        public Dictionary<int, List<DialogueDefine>> Dialogues = new Dictionary<int, List<DialogueDefine>>();
        public UIDialogue uiDialogue;

        private List<DialogueDefine> m_defines = null;
        private int index = 0;
        private DialogueDefine m_define = null;

        public void InitAync()
        {
            StartCoroutine(InitAyncAnim());
        }
        IEnumerator InitAyncAnim()
        {
            foreach (var diadics in DataManager.Instance.Dialogues)
            {
                var defines = new List<DialogueDefine>(diadics.Value.Count);
                index = 1;
                m_define = null;
                while (diadics.Value.TryGetValue(index++, out m_define))
                {
                    defines.Add(m_define);
                }
                Dialogues.Add(diadics.Key, defines);
            }
            yield return null;
            Debug.Log("DialogueManager Init");
            //DataManager.Instance.Dialogues.Clear();
        }
        protected override void OnAwake()
        {
            base.OnAwake();
        }
        public void ShowDialogue(int diaId, Action oncomplete = null)
        {
            OnConversationStart?.Invoke();

            Debug.Log(string.Format("开始对话: Id:{0}", diaId));
            m_defines = Dialogues[diaId];
            StartCoroutine(ShowDialogue(m_defines, oncomplete));

            OnConversationEnd?.Invoke();
        }
        IEnumerator ShowDialogue(List<DialogueDefine> def, Action callback)
        {
            uiDialogue = UIManager.Instance.Show<UIDialogue>();
            foreach (var define in def)
            {
                uiDialogue.SetInfo(define);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
                yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.Space));
            }
            UIManager.Instance.Close<UIDialogue>();
            callback?.Invoke();
        }
        public IEnumerator IEShowDialogue(int diaId, Action callback = null)
        {
            m_defines = Dialogues[diaId];
            yield return StartCoroutine(ShowDialogue(m_defines, callback));
        }
    }
}