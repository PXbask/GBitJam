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
        public UIBattle battlePanel;
        public UIDynamic dynamicPanel;
        public bool OtherUIOverlayed = false;
        public Stack<UIWindow> WindowStack = new Stack<UIWindow>();
        public bool HasUIOverlay => WindowStack.Count > 0;
        class UIElement
        {
            public string Resources;
            public bool Cache;
            public GameObject Instance;
        }
        private Dictionary<Type, UIElement> UIResources = new Dictionary<Type, UIElement>();
        public Dictionary<Type, UIWindow> UIInstance = new Dictionary<Type, UIWindow>(); //UI运行时的缓存
        public UIManager()
        {
            this.UIResources.Add(typeof(UITitle), new UIElement() { Resources = @"Prefab/UI/UITitle", Cache = true });
            //this.UIResources.Add(typeof(UIDialogue), new UIElement() { Resources = @"Prefab/UI/UIDialogue", Cache = true });
            this.UIResources.Add(typeof(UIWorldTips), new UIElement() { Resources = @"Prefab/UI/UIWorldTips", Cache = false });
            this.UIResources.Add(typeof(UIAtla), new UIElement() { Resources = @"Prefab/UI/UIAtla", Cache = false });
            //this.UIResources.Add(typeof(UIBattle), new UIElement() { Resources = @"Prefab/UI/UIBattle", Cache = false });
            //this.UIResources.Add(typeof(UIDynamic), new UIElement() { Resources = @"Prefab/UI/UIDynamic", Cache = false });
        }
        ~UIManager() { }
        public T Show<T>() where T : UIWindow
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
                T res = info.Instance.GetComponent<T>();
                UIInstance[typeof(T)] = res;
                PushToStack(res);
                return res;
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
                    UIInstance[type] = null;
                    GameObject.Destroy(uIElement.Instance);
                    uIElement.Instance = null;
                }

                PopFromStack();
            }
        }
        public void Close<T>() where T : UIWindow
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
        public T GetActiveInstance<T>() where T : UIWindow
        {
            if (this.UIInstance.ContainsKey(typeof(T)))
            {
                if(UIInstance[typeof(T)].gameObject.activeInHierarchy) 
                    return (T)UIInstance[typeof(T)];
                else
                    return null;
            }
            else
            {
                Debug.LogWarningFormat("缓存中不存在激活的类型UI:[{0}] (第一次启动可忽略)",typeof(T).Name);
                return null;
            }
        }
        private void PushToStack(UIWindow uI)
        {
            if(uI != null)
                WindowStack.Push(uI);
            Time.timeScale = HasUIOverlay ? 0 : 1;
        }
        private void PopFromStack()
        {
            WindowStack.Pop();
            Time.timeScale = HasUIOverlay ? 0 : 1;
        }
        public void AddGainMessage(string str)
        {
            dynamicPanel?.AddGainMsg(str);
        }
        public void AddInteractMessage(string str,Transform root)
        {
            battlePanel?.AddInteractMsg(str, root);
        }
        public void RemoveInteractMessage(Transform root)
        {
            battlePanel?.RemoveInteractMsg(root);
        }
        public void AddWarning(string str)
        {
            dynamicPanel?.AddWarning(str);
        }
    }
}
