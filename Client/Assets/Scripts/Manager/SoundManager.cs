using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/*
    Date:
    Name:
    Overview:
*/

public class SoundManager : MonoSingleton<SoundManager>
{
    public AudioMixer audioMixer;

    public AudioSource musicAudioSource;

    public AudioSource soundAudioSource;

    private const string MusicPath = "Music/";

    private const string SoundPath = "Sound/";

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
			if(musicVolume != value)
			{
                musicVolume = value;
                GameManager.Instance.musicVolume = value;

                this.SetVolume("MusicVolume", musicVolume);
            }
        }
	}

	private float sfxVolume;

	public float SFXVolume
	{
		get { return sfxVolume; }
		set
		{
			if(musicVolume != value)
			{
                sfxVolume = value;
                GameManager.Instance.sfxVolume = value;

                this.SetVolume("SoundVolume", sfxVolume);
            }
		}
	}

    private void SetVolume(string name, float value)
    {
        float volume = value * 0.5f - 50f;
        this.audioMixer.SetFloat(name, volume);
    }

    internal void PlayMusic(string name)
    {
        AudioClip clip = Resloader.Load<AudioClip>(MusicPath + name);
        if (clip == null)
        {
            Debug.LogWarningFormat("PlayMusic:{0} not exist", name);
            return;
        }
        if (musicAudioSource.isPlaying)
        {
            musicAudioSource.Stop();
        }
        musicAudioSource.clip = clip;
        musicAudioSource.Play();
    }

    internal void PlaySound(string name)
    {
        AudioClip clip = Resloader.Load<AudioClip>(SoundPath + name);
        if (clip == null)
        {
            Debug.LogWarningFormat("PlaySound:{0} not exist", name);
            return;
        }
        soundAudioSource.PlayOneShot(clip);
    }
}
