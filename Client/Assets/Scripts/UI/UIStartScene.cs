using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using Manager;

/*
    Date:
    Name:
    Overview:开始场景
*/

public class UIStartScene : MonoBehaviour
{
    public Image bgimage;
    public Image titleimage;
    public Text titletext;

    private bool isLoading;
    Tween texttween = null;

    private void Start()
    {
        isLoading = false;
        titletext.text = "按任意键继续";
        GameManager.Instance.Status = GameStatus.None;

        titleimage.color = new Color(1, 1, 1, 0);
        titletext.color = new Color(0.5f, 0.5f, 0.5f, 0);
        titleimage.DOFade(1, 1)
            .SetLink(titleimage.gameObject)
            .SetEase(Ease.Linear);
        texttween = titletext.DOFade(1, 2f)
            .SetLink(titletext.gameObject)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Yoyo);
    }
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if(!isLoading)
            {
                StartCoroutine(StartLoadingAnim());
                isLoading = true;

                SoundManager.Instance.PlayBtnClickSound();
            }
        }
    }
    IEnumerator StartLoadingAnim()
    {
        texttween.Kill();
        titletext.color = Color.gray;
        titletext.text = "正在装载芯片";

        yield return DataManager.Instance.LoadConfigDataAsync();
        DataManager.Instance.LoadUserData();

        DialogueManager.Instance.InitAync();
        PXSceneManager.Instance.Init();
        UIManager.Instance.Init();

        int count = 0;
        while (count < 1)
        {
            yield return new WaitForSeconds(0.5f);
            titletext.text = "正在装载芯片.";
            yield return new WaitForSeconds(0.5f);
            titletext.text = "正在装载芯片..";
            yield return new WaitForSeconds(0.5f);
            titletext.text = "正在装载芯片...";
            yield return new WaitForSeconds(0.5f);
            titletext.text = "正在装载芯片";

            count++;
        }

        titletext.text = "芯片装载完成";
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Scenes/Menu", LoadSceneMode.Single);
    }
}
