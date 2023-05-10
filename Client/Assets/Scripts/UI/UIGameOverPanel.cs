using DG.Tweening;
using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    Date:
    Name:
    Overview:
*/

public class UIGameOverPanel : MonoBehaviour
{
    [SerializeField] Image background;

    [SerializeField] Text text;

    [SerializeField] Text restart;

    private bool waitingForRestart = false;
    private GameObject panel => background.gameObject;
    public void Show()
    {
        Time.timeScale = 0;
        InputManager.Deactivate();

        panel.SetActive(true);
        restart.color = new Color(1, 1, 1, 0);
        background.color = new Color(1, 0, 0, 0);
        text.color = new Color(1, 0, 0, 0);

        background.DOColor(new Color(1, 0, 0, 100 / 255f), 3f)
                  .SetOptions(true)
                  .SetUpdate(true);

        text.DOColor(Color.red, 3f)
            .SetOptions(true)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                restart.color = new Color(1, 1, 1, 0);
                restart.DOColor(Color.white, 2f)
                     .SetOptions(true)
                     .SetUpdate(true)
                     .OnComplete(() => {
                         waitingForRestart = true;
                     });
            });
    }
    private void Update()
    {
        if (waitingForRestart)
        {
            if (Input.anyKeyDown)
            {
                ShowRestart();
                waitingForRestart = false;
            }
        }
    }

    private void ShowRestart()
    {
        MessageBox.Show("要重新开始吗", "是", "否", () =>
        {
            //GameManager.Instance.Reset();
            //GameManager.Instance.InitAync();
            //var info = PXSceneManager.Instance.GetScene(1);
            //PXSceneManager.Instance.LoadScene(info, Consts.Loading.Default_Loading_Interval);

            Time.timeScale = 1;
            panel.SetActive(false);
            UserManager.Instance.ReBorn();
        },() =>
        {
            UserManager.Instance.playerObject.SetActive(false);
            PXSceneManager.Instance.LoadMainMenuScene();

            Time.timeScale = 1;
            panel.SetActive(false);
        });
    }
}
