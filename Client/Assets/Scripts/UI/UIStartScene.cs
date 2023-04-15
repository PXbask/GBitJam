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
    Overview:��ʼ����
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
        titletext.text = "�����������";
        GameManager.Instance.Status = GameStatus.None;

        texttween = titletext.DOColor(new Color(1, 1, 1, 50 / 255f), 2.5f)
            .SetLink(titletext.gameObject)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Yoyo)
            .SetOptions(true);
    }
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if(!isLoading)
            {
                StartCoroutine(StartLoadingAnim());
                isLoading = true;
            }
        }
    }
    IEnumerator StartLoadingAnim()
    {
        texttween.Kill();
        titletext.color = Color.gray;
        titletext.text = "����װ��оƬ";

        yield return DataManager.Instance.LoadConfigDataAsync();
        DataManager.Instance.LoadUserData();

        DialogueManager.Instance.InitAync();
        PXSceneManager.Instance.Init();
        UIManager.Instance.Init();

        int count = 0;
        while (count < 1)
        {
            yield return new WaitForSeconds(0.5f);
            titletext.text = "����װ��оƬ.";
            yield return new WaitForSeconds(0.5f);
            titletext.text = "����װ��оƬ..";
            yield return new WaitForSeconds(0.5f);
            titletext.text = "����װ��оƬ...";
            yield return new WaitForSeconds(0.5f);
            titletext.text = "����װ��оƬ";

            count++;
        }

        titletext.text = "оƬװ�����";
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Scenes/Menu", LoadSceneMode.Single);
    }
}
