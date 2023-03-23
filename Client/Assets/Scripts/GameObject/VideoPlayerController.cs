using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/*
    Date:
    Name:
    Overview:
*/

public class VideoPlayerController : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        //������Ƶ�ļ���·��
        videoPlayer.url = "Assets/StreamingAssets/start.mp4";

        //����ģʽΪѭ������
        videoPlayer.isLooping = true;

        //��ʼ����
        videoPlayer.Play();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //���¿ո����ͣ����
            if (videoPlayer.isPlaying)
            {
                videoPlayer.Pause();
            }
            //���¿ո����������
            else
            {
                videoPlayer.Play();
            }
        }
    }
}
