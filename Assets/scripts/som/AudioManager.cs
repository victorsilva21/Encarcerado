using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;
	public float musicVolume = 1f;
	public bool change = false;

	public AudioMixerGroup mixerGroup;

	public Sound[] sounds;

	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
		}
	}

    private void Update()
    {
        if (change)
        {
			foreach (Sound s in sounds)
			{
				s.source.volume = s.volume * musicVolume;
			}
        }
	}

    public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f)) * musicVolume;
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}

	public void Stop(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.Stop();
	}

	public void FadeOut(string sound, float fadeSpeed)
	{
		StartCoroutine(FadeOutC(sound, fadeSpeed));
		Debug.Log("working");
	}

	IEnumerator FadeOutC(string sound, float fadeSpeed)
	{
		Debug.Log("still working");
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			yield break;
		}
		Debug.Log("surprisiglyinfsdf working");
		while (s.source.volume > 0)
		{
			s.source.volume -= fadeSpeed * Time.deltaTime;
			yield return null;
		}
		Debug.Log("done");
	} 
}


