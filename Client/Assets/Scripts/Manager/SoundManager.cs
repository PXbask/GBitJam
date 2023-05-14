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

    private const float PITCH_DEFAULT = 1.0f;

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

    internal void PlayMusic(string name, float pitch = 1, AudioSource source = null)
    {
        if (source == null) source = musicAudioSource;

        AudioClip clip = Resloader.Load<AudioClip>(MusicPath + name);
        if (clip == null)
        {
            Debug.LogWarningFormat("PlayMusic:{0} not exist", name);
            return;
        }
        if (source.isPlaying)
        {
            source.Stop();
        }
        source.pitch = pitch;
        source.clip = clip;
        source.Play();
    }

    internal void PlaySound(string name, float pitch = 1, AudioSource source = null)
    {
        if (source == null) source = soundAudioSource;

        AudioClip clip = Resloader.Load<AudioClip>(SoundPath + name);
        if (clip == null)
        {
            Debug.LogWarningFormat("PlaySound:{0} not exist", name);
            return;
        }
        source.pitch = pitch;
        source.PlayOneShot(clip);
    }

    public void PlayBtnClickSound() => this.PlaySound("buttonclick", Random.Range(0.9f, 1.1f));

    public void PlayEnemyShotGunSound() => this.PlaySound("shotgun", Random.Range(0.9f, 1.1f));

    public void PlayOpenChestSound() => this.PlaySound("openchest", Random.Range(0.9f, 1.1f));

    public void PlayGainItemSound() => this.PlaySound("gainitem", Random.Range(0.9f, 1.1f));

    public void PlayLevelUpSound() => this.PlaySound("levelup", Random.Range(0.9f, 1.1f));

    public void PlayGameOverSound() => this.PlaySound("gameover", Random.Range(0.9f, 1.1f));

    public void PlayPressMusic() => this.PlayMusic("press");

    public void PlayMenuMusic() => this.PlayMusic("menu");

    public void PlayBattleMusic() => this.PlayMusic("battle");

    public void PlayGameEndMusic() => this.PlayMusic("end");
}
