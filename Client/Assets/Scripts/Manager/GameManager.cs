using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview://��Ϸ��������
*/

public class GameManager : MonoSingleton<GameManager>
{
    // ���ڿ��
    public int width = 1920;
    public int height = 1080;

    public int FPS = 60;//��֡

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
