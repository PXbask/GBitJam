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
    public Image image;
    public Text text;
    public Slider loadSlider;
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
            while (true)
            {
                var ran = UnityEngine.Random.Range(0, InterludeTexts.Count);
                if (ran != index)
                {
                    index = ran;
                    break;
                }
            }
            text.text = InterludeTexts[index].Dialogue;
            yield return new WaitForSeconds(5f);
        }
    }
    IEnumerator HandleSlider()
    {
        loadSlider.value = 0;
        loadSlider.maxValue = 100;

        while (loadSlider.value<loadSlider.maxValue)
        {
            loadSlider.value += 100f / m_duartion * Time.deltaTime;
            percentText.text = string.Format("{0}%", loadSlider.value.ToString("f0"));
            yield return null;
        }
    }
}
