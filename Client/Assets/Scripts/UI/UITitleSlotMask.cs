using Manager;
using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
    Date:
    Name:
    Overview:
*/

public class UITitleSlotMask : MonoBehaviour,IPointerClickHandler
{
    const float SLOT_STEP = 17;
    const float SLOT_WIDTH = 40;
    const float SLOT_HEIGHT = 40;
    RectTransform rectTransform;
    Image childImage;
    public Image image;

    public int id;
    private TitleInfo info;
    public Vector3 Center => new Vector3(rectTransform.sizeDelta.x / 2f, -rectTransform.sizeDelta.y / 2f, 0);
    private void Awake()
    {
        rectTransform= GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }
    public void MaskReset()
    {
        rectTransform.sizeDelta = new Vector2(0, SLOT_HEIGHT);
        for (int i=0;i<transform.childCount;i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
    public void MaskApply(int size, TitleInfo info)
    {
        this.info = info;
        float _width = SLOT_WIDTH * size + SLOT_STEP * (size - 1);
        float _height = SLOT_HEIGHT;
        rectTransform.sizeDelta = new Vector2(_width, _height);
        image.color = Random.ColorHSV(0.5f, 1, 0.5f, 1, 1, 1, 1, 1);
        InitImage();
    }

    private void InitImage()
    {
        // ����һ���µ�GameObject����������Ϊ�Ӷ���
        GameObject newImageObject = new GameObject("image", typeof(Image));
        newImageObject.transform.SetParent(rectTransform);

        // ��ȡ�´�����Image���
        childImage = newImageObject.GetComponent<Image>();

        // ����Image�����Sprite����������
        childImage.rectTransform.localPosition = Center;
        childImage.rectTransform.sizeDelta = new Vector2(SLOT_WIDTH - 10, SLOT_HEIGHT - 10);
        childImage.color = Color.black;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (UserManager.Instance.isOverLoad)
        {
            UIManager.Instance.AddWarning("���ѹ��أ��޷�ж��оƬ");
            return;
        }
        TitleManager.Instance.UnEquip(info.ID);
    }
}
