using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class UIManager : Singleton<UIManager>
    {
        public UIDynamic dynamicPanel;
        class UIElement
        {
            public string Resources;
            public bool Cache;
            public GameObject Instance;
        }
        private Dictionary<Type, UIElement> UIResources = new Dictionary<Type, UIElement>();
        public UIManager()
        {
            this.UIResources.Add(typeof(UITitle), new UIElement() { Resources = @"Prefab/UI/UITitle", Cache = true });
            this.UIResources.Add(typeof(UIDialogue), new UIElement() { Resources = @"Prefab/UI/UIDialogue", Cache = true });
            this.UIResources.Add(typeof(UIWorldTips), new UIElement() { Resources = @"Prefab/UI/UIWorldTips", Cache = false });
            this.UIResources.Add(typeof(UIAtla), new UIElement() { Resources = @"Prefab/UI/UIAtla", Cache = false });
            this.UIResources.Add(typeof(UIBattle), new UIElement() { Resources = @"Prefab/UI/UIBattle", Cache = false });
            this.UIResources.Add(typeof(UIDynamic), new UIElement() { Resources = @"Prefab/UI/UIDynamic", Cache = false });
        }
        ~UIManager() { }
        public T Show<T>()
        {
            Type type = typeof(T);
            if (this.UIResources.ContainsKey(type))
            {
                UIElement info = this.UIResources[type];
                if (info.Instance != null)
                {
                    info.Instance.SetActive(true);
                }
                else
                {
                    UnityEngine.Object prefab = Resources.Load(info.Resources);
                    if (prefab == null)
                    {
                        Debug.LogWarningFormat("UI prefab can't find:{0}", type.Name);
                        return default(T);
                    }
                    info.Instance = (GameObject)GameObject.Instantiate(prefab);
                }
                return info.Instance.GetComponent<T>();
            }
            Debug.LogWarningFormat("UI prefab can't find:{0}", type.Name);
            return default(T);
        }
        public void Close(Type type)
        {
            if (this.UIResources.ContainsKey(type))
            {
                UIElement uIElement = this.UIResources[type];
                if (uIElement.Instance == null) return;
                if (uIElement.Cache)
                {
                    uIElement.Instance.SetActive(false);
                }
                else
                {
                    GameObject.Destroy(uIElement.Instance);
                    uIElement.Instance = null;
                }
            }
        }
        public void Close<T>()
        {
            this.Close(typeof(T));
        }
        public void TurntoBlack(Action callback)
        {
            GameManager.Instance.TurntoBlackAnim(callback);      
        }
        public void TurntoWhite()
        {
            GameManager.Instance.TurntoWhiteAnim();
        }

        public void OpenWorldTip(string text, Transform root)
        {
            var ui = Show<UIWorldTips>();
            ui.SetText(text);
            ui.SetOwner(root);
            ui.transform.SetParent(root);
        }

        internal void CloseWorldTip()
        {
            this.Close<UIWorldTips>();
        }
        public void AddGainMessage(string str)
        {
            dynamicPanel?.AddGainMsg(str);
        }
        public void AddInteractMessage(string str,Transform root)
        {
            dynamicPanel?.AddInteractMsg(str, root);
        }
        public void RemoveInteractMessage(Transform root)
        {
            dynamicPanel?.RemoveInteractMsg(root);
        }
    }
}
