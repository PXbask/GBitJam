using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Date:
    Name:
    Overview:
*/

public class SoundManager : Singleton<SoundManager>
{
	private float mainVolume;

	public float MainVolume
	{
		get { return mainVolume; }
		set
		{
			mainVolume = value;
			GameManager.Instance.mainVolume = value;
		}
	}
	private float musicVolume;

	public float MusicVolume
	{
		get { return musicVolume; }
		set
		{
			musicVolume = value;
            GameManager.Instance.musicVolume = value;
        }
	}
	private float sfxVolume;

	public float SFXVolume
	{
		get { return sfxVolume; }
		set
		{
			sfxVolume = value; 
			GameManager.Instance.sfxVolume = value;
		}
	}


}
