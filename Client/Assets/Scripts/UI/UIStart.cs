using Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

/*
    Date:
    Name:
    Overview:
*/

public class UIStart : MonoBehaviour
{
    public RawImage videoImage;
    public VideoPlayer videoPlayer;

    public GameObject menu;
    private void Awake()
    {
        videoPlayer.EnableAudioTrack(0, false);
        videoImage.gameObject.SetActive(true);
        videoPlayer.gameObject.SetActive(true);

        menu.SetActive(false);
    }
    private void Start()
    {
        videoPlayer.loopPointReached += OnVideoCompleted;
    }

    private void OnVideoCompleted(VideoPlayer source)
    {
        UIManager.Instance.TurntoBlack(() =>
        {
            menu.SetActive(true);
            videoImage.gameObject.SetActive(false);
            videoPlayer.gameObject.SetActive(false);

            UIManager.Instance.TurntoWhite();
        });
    }
    public void OnClickGameStartBtn()
    {
        UIManager.Instance.TurntoBlack(() => UnityEngine.SceneManagement.SceneManager.LoadScene("Test_Scene"));
    }
}
