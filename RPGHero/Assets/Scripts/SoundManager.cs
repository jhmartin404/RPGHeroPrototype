using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour 
{
	public List<AudioClip> uiSounds;
	public List<AudioClip> backgroundMusic;
	public AudioSource uiSource;
	public AudioSource backgroundMusicSource;
	private static SoundManager instance = null;
	public static SoundManager Instance 
	{
		get { return instance; }
	}

	void Awake() 
	{
		if (instance != null && instance != this) 
		{
			Destroy(this.gameObject);
			return;
		} 
		else 
		{
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}

	public void PlayBackgroundMusic(string name)
	{
		if(backgroundMusicSource.clip != null && backgroundMusicSource.clip.name == name)
		{
			backgroundMusicSource.Play();
		}
		else
		{
			AudioClip sound = GetBackgroundMusic (name);
			if(sound != null)
			{
				backgroundMusicSource.clip = sound;
				backgroundMusicSource.Play();
			}
			else
			{
				Debug.LogError("backgroundmusic was not found");
			}
		}
	}

	private AudioClip GetBackgroundMusic(string name)
	{
		for(int i = 0; i < backgroundMusic.Count; ++i)
		{
			if(backgroundMusic[i].name == name)
			{
				return backgroundMusic[i];
			}
		}
		return null;
	}

	public void PlayUISound(string name)
	{
		if(uiSource.clip != null && uiSource.clip.name == name)
		{
			uiSource.Play();
		}
		else
		{
			AudioClip sound = GetUIAudio (name);
			if(sound != null)
			{
				uiSource.clip = sound;
				uiSource.Play();
			}
			else
			{
				Debug.LogError("Sound was not found");
			}
		}
	}

	private AudioClip GetUIAudio(string name)
	{
		for(int i = 0; i < uiSounds.Count; ++i)
		{
			if(uiSounds[i].name == name)
			{
				return uiSounds[i];
			}
		}
		return null;
	}
}
