using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    Date:
    Name:
    Overview:
*/

public class UIEndWindow : MonoBehaviour
{
    public Image titleimage;
    public List<Text> texts= new List<Text>();

    private void Start()
    {
        InputManager.Deactivate();
        StartCoroutine(TextAnimation());
    }

    IEnumerator TextAnimation()
    {
        SoundManager.Instance.PlayGameEndMusic();

        yield return ImageAnimation();

        for (int i = 0; i < texts.Count; i++)
        {
            var text = texts[i];
            text.color = new Color(1, 1, 1, 0);
            text.gameObject.SetActive(true);
            while (text.color.a < 1)
            {
                Color color = text.color;
                color.a += 1 * Time.fixedDeltaTime;
                text.color = color;
                yield return null;
            }
            yield return new WaitForSecondsRealtime(3f);
            if (i != texts.Count - 1)
            {
                while (text.color.a > 0)
                {
                    Color color = text.color;
                    color.a -= 1 * Time.fixedDeltaTime;
                    text.color = color;
                    yield return null;
                }

                text.gameObject.SetActive(false);
            }
        }
        yield return new WaitUntil(() => Input.anyKeyDown);
        PXSceneManager.Instance.LoadMainMenuScene();
    }

    IEnumerator ImageAnimation()
    {
        titleimage.color = new Color(1, 1, 1, 0);
        titleimage.gameObject.SetActive(true);
        while (titleimage.color.a < 1)
        {
            Color color = titleimage.color;
            color.a += 1 * Time.fixedDeltaTime;
            titleimage.color = color;
            yield return null;
        }
        yield return new WaitForSecondsRealtime(3f);
        while (titleimage.color.a > 0)
        {
            Color color = titleimage.color;
            color.a -= 1 * Time.fixedDeltaTime;
            titleimage.color = color;
            yield return null;
        }
        titleimage.gameObject.SetActive(false);
    }
}
