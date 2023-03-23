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
        //设置视频文件的路径
        videoPlayer.url = "Assets/StreamingAssets/start.mp4";

        //播放模式为循环播放
        videoPlayer.isLooping = true;

        //开始播放
        videoPlayer.Play();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //按下空格键暂停播放
            if (videoPlayer.isPlaying)
            {
                videoPlayer.Pause();
            }
            //按下空格键继续播放
            else
            {
                videoPlayer.Play();
            }
        }
    }
}
