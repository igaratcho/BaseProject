using UnityEngine;
using System;
using System.Collections;

public class SoundManager : SingletonBase<SoundManager> 
{
	
	const int MAX_SE_CHANNEL = 3;

	[SerializeField]
	AudioClip[] BGM;

	[SerializeField]
	AudioClip[] SE;

	AudioSource BGMsource;
	AudioSource[] SEsources = new AudioSource[MAX_SE_CHANNEL];

	protected override void OnAwake ()
	{
		BGMsource = this.gameObject.AddComponent<AudioSource>();
		BGMsource.loop = true;

		for(int i = 0 ; i < SEsources.Length ; i++ )
		{
			SEsources[i] = gameObject.AddComponent<AudioSource>();
		}
	}

	public void PlayBGM(BGM bgm_id)
	{
		int index = (int)bgm_id;

		if ( 0 > index || BGM.Length <= index )
			return;

		if( BGMsource.clip == BGM[index] )
			return;

		BGMsource.Stop();
		BGMsource.clip = BGM[index];
		BGMsource.Play();
	}

	public void PlayScheduledBGM(BGM bgm_id, double time)
	{
		int index = (int)bgm_id;

		if ( 0 > index || BGM.Length <= index )
			return;
		
		if( BGMsource.clip == BGM[index] )
			return;
		
		BGMsource.Stop();
		BGMsource.clip = BGM[index];
		BGMsource.PlayScheduled (time);
	}

	public void StopBGM()
	{
		BGMsource.Stop();
		BGMsource.clip = null;
	}

	public void SetBGMVolume(float volume)
	{
		BGMsource.volume = volume;
	}


	public void PlaySE(SE se_id)
	{
		int index = (int)se_id;

		if( 0 > index || SE.Length <= index )
			return;

		foreach(AudioSource source in SEsources)
		{
			if( !source.isPlaying )
			{
				source.clip = SE[index];
				source.Play();
				return;
			}
		}  
	}

	public void StopSE()
	{
		foreach(AudioSource source in SEsources)
		{
			source.Stop();
			source.clip = null;
		}  
	}

	public void SetSEVolume(float volume)
	{
		foreach(AudioSource source in SEsources)
			source.volume = volume;
	}

	public void Mute()
	{
		SetBGMVolume (0f);
		SetSEVolume  (0f);
	}

	public void FadeIn(float volume, float duration)
	{
		StartCoroutine (FadeIn_ (volume, duration));
	}
	
	public IEnumerator FadeIn_(float volume, float duration)
	{
		float elapsedTime = 0f;
		while( BGMsource.volume < volume - 0.1f )
		{
			BGMsource.volume = Mathf.Lerp( 0f, volume,  elapsedTime / duration );
			elapsedTime+=Time.deltaTime;
			yield return null;
		}
		BGMsource.volume = volume;
	}

	public void FadeOut(float duration)
	{
		StartCoroutine (FadeOut_ (duration));
	}

	public IEnumerator FadeOut_(float duration)	
	{
		float nowVolume = BGMsource.volume;

		float elapsedTime = 0f;
		while( BGMsource.volume > 0f )
		{
			BGMsource.volume = Mathf.Lerp( nowVolume, 0f, elapsedTime / duration );
			elapsedTime+=Time.deltaTime;
			yield return null;
		}
		BGMsource.volume = 0f;
	}
	
	public void Crossfade ()
	{
		// @todo. Not Implement.
//		var startTime = Time.time;
//		var endTime = startTime + duration;
//		while (Time.time < endTime) {
//			var i = (Time.time - startTime) / duration;
//			a1.volume = (1-i);
//			a2.volume = i;
//			yield;
//		}
	}
}