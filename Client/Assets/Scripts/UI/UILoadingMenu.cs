using Define;
using Manager;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/*
    Date:
    Name:
    Overview:
*/

public class UILoadingMenu : MonoBehaviour
{
    public List<Sprite> illustrations = new List<Sprite>();  

    public Image image;
    public Text text;
    public Image fakeSlider;
    public Text percentText;

    List<DialogueDefine> InterludeTexts;
    IEnumerator enumerator;

    public float m_duartion;
    private void Awake()
    {
        InterludeTexts = DialogueManager.Instance.Dialogues[-1];
    }
    private void OnEnable()
    {
        enumerator = ShowInterludeText();
        StartCoroutine(enumerator);
        StartCoroutine(HandleSlider());
    }
    private void OnDisable()
    {
        StopCoroutine(enumerator);
        enumerator= null;
    }
    IEnumerator ShowInterludeText()
    {
        while(true)
        {
            int index = -1;
            int illIndex = -1;
            while (true)
            {
                var ran = UnityEngine.Random.Range(0, InterludeTexts.Count);
                if (ran != index)
                {
                    index = ran;
                    break;
                }
            }
            while (true)
            {
                var ran = UnityEngine.Random.Range(0, illustrations.Count);
                if (ran != illIndex)
                {
                    illIndex = ran;
                    break;
                }
            }
            text.text = InterludeTexts[index].Dialogue;
            image.sprite = illustrations[illIndex];
            yield return new WaitForSeconds(5f);
        }
    }
    IEnumerator HandleSlider()
    {
        fakeSlider.fillAmount = 0;

        while (fakeSlider.fillAmount < 1)
        {
            fakeSlider.fillAmount += 1f / m_duartion * Time.deltaTime;
            percentText.text = string.Format("{0}%", (fakeSlider.fillAmount*100).ToString("f0"));
            yield return null;
        }
    }
}
