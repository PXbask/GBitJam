using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview://游戏主管理器
*/

public class GameManager : MonoSingleton<GameManager>
{
    // 窗口宽高
    public int width = 1920;
    public int height = 1080;

    public int FPS = 60;//限帧

    public CharController charc;

    private void Awake()
    {
        Application.targetFrameRate = FPS;
        Screen.SetResolution(width, height, false);
    }
    private void Start()
    {
        charc = GameObject.Find("Player").GetComponent<CharController>();
    }
}
