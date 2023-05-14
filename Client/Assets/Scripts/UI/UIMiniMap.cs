using Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

/*
    Date:
    Name:
    Overview:
*/

public class UIMiniMap : MonoBehaviour
{
    private readonly Dictionary<IVisibleinMap, GameObject> IconMaps = new Dictionary<IVisibleinMap, GameObject>();
    private readonly Vector3[] m_corners = new Vector3[4];
    [SerializeField] private Sprite iconBase;
    private Vector3 bottomLeftCorner;
    private Vector3 BottomLeftCorner
    {
        get
        {
            if (bottomLeftCorner == null)
            {
                bottomLeftCorner = m_corners[0];
                return m_corners[0];
            }
            return bottomLeftCorner;
        }
    }
    public Collider boundary;
    public Image mapImage;
    public Transform playertr;

    float Bxmin => boundary.bounds.min.x;
    float Bymin => boundary.bounds.min.z;
    float Bxsize => boundary.bounds.size.x;
    float Bysize => boundary.bounds.size.z;
    float Mapimagewidth => mapImage.rectTransform.rect.width;
    float Mapimageheight => mapImage.rectTransform.rect.height;
    IVisibleinMap m_player;

    public void UpdateMap()
    {
        if (mapImage.sprite == null)
            mapImage.sprite = MiniMapManager.Instance.LoadCurrentMiniMap();
        //mapImage.SetNativeSize();
        //mapImage.transform.localPosition = Vector3.zero;
        //this.boundary = MiniMapManager.Instance.Boundary;
    }
    void Start()
    {
        mapImage.rectTransform.GetWorldCorners(m_corners);
        MiniMapManager.Instance.uIMiniMap = this;
    }
    void Update()
    {
        if (playertr == null)
        {
            if (MiniMapManager.Instance.PlayerTransform != null)
                playertr = MiniMapManager.Instance.PlayerTransform;
            else
                return;
        }
        if (boundary != null)
            UpdateIcons();
    }

    public void Reset()
    {
        IconMaps.Clear();
    }

    private void UpdateIcons()
    {
        List<IVisibleinMap> objs = MiniMapManager.Instance.GetVisibleinMaps();
        for (int i = objs.Count-1; i >= 0; i--)
        {
            Binding(objs[i]);
        }
        if (m_player!=null)
        {
            Vector3 position = m_player.GetTransform().position;
            float relax = (position.x - Bxmin) / Bxsize;
            float relay = (position.z - Bymin) / Bysize;
            mapImage.rectTransform.pivot = new Vector2(relax, relay);
            mapImage.transform.localPosition = Vector3.zero;
        }
    }

    private void Binding(IVisibleinMap visibleinMap)
    {
        Vector3 pos = GetRelatedPosition(visibleinMap.GetTransform());

        if(IconMaps.TryGetValue(visibleinMap,out GameObject o))
        {
            o.transform.localPosition = pos;
        }
        else
        {
            var type = visibleinMap.GetIconType();
            switch (type)
            {
                case MapIconType.Player:
                    GameObject icon0 = CreateIcon(Color.blue);
                    IconMaps.Add(visibleinMap, icon0);
                    m_player = visibleinMap;
                    break;
                case MapIconType.Enemy:
                    GameObject icon1 = CreateIcon(Color.red);
                    IconMaps.Add(visibleinMap, icon1);
                    break;
                case MapIconType.Object:
                    GameObject icon2 = CreateIcon(Color.yellow);
                    IconMaps.Add(visibleinMap, icon2);
                    break;
                default:
                    break;
            }
        }
    }

    private GameObject CreateIcon(Color color)
    {
        GameObject image = new GameObject("image", typeof(Image));
        image.transform.SetParent(mapImage.transform);
        Image imagec = image.GetComponent<Image>();
        imagec.color = color;
        imagec.sprite = iconBase;
        image.transform.localScale = Vector3.one * 0.05f;
        return image;
    }

    private Vector3 GetRelatedPosition(Transform t)
    {
        Vector3 position = t.position;
        float relax = (position.x - Bxmin) / Bxsize * Mapimagewidth - mapImage.rectTransform.pivot.x * Mapimagewidth;
        float relay = (position.z - Bymin) / Bysize * Mapimageheight - mapImage.rectTransform.pivot.y * Mapimageheight;

        return new Vector3(relax, relay);
    }

    internal void Remove(IVisibleinMap v)
    {
        try
        {
            Destroy(IconMaps[v]);
            IconMaps.Remove(v);
        }
        catch(Exception e)
        {
            Debug.Log(e);
        }
    }
}
