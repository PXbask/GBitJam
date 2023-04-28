using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Manager
{
    public class MiniMapManager : Singleton<MiniMapManager>
    {
        private readonly List<IVisibleinMap> visibleObjs = new List<IVisibleinMap>();
        public UIMiniMap uIMiniMap;
        private Collider boundary;
        public Collider Boundary
        {
            get { return boundary; }
        }

        public void Register(IVisibleinMap v)
        {
            if (visibleObjs.Contains(v)) return;
            visibleObjs.Add(v);
            Debug.LogFormat("[{0}] register miniMap", v.GetName());
        }

        public void Remove(IVisibleinMap v)
        {
            visibleObjs.Remove(v);
            uIMiniMap.Remove(v);
        }

        public List<IVisibleinMap> GetVisibleinMaps() { return this.visibleObjs; }

        public void Init() { }
        public Transform PlayerTransform
        {
            get
            {
                if (UserManager.Instance.playerlogic == null)
                    return null;
                else
                    return UserManager.Instance.playerObject.transform;
            }
        }

        public Sprite LoadCurrentMiniMap()
        {
            string path = string.Format("UI/minimap/minimap");
            return Resloader.Load<Sprite>(path);
        }
        public void UpdateBoundary(Collider collider)
        {
            if (collider != null)
                this.boundary = collider;
            if (uIMiniMap != null)
                uIMiniMap.UpdateMap();
        }
    }
}