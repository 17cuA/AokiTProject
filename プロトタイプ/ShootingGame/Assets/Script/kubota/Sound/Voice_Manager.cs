using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voice_Manager : MonoBehaviour
{
	public static Voice_Manager VOICE_Obj;
	[Header("パワーアップ用")]
	public AudioSource audiosource;
	[Header("無線用")]
	public AudioSource Sinario_audio;
	[Header("マルチプルの音声")]
	public AudioSource Maltiple_audio;


	private void Awake()
	{
		VOICE_Obj = GetComponent<Voice_Manager>();
	}
	// Start is called before the first frame update
	void Start()
	{
		//audiosource = GetComponent<AudioSource>();
	}
	//パワーアップ用のAudioSourceを使用
	public void Voice_Active(AudioClip voice)
	{
		audiosource.PlayOneShot(voice);
	}
	//マルチプル用
	public void Maltiple_Active_Voice(AudioClip voice)
	{
		Maltiple_audio.PlayOneShot(voice);
	}
	//無線用のAudioSourceを使用
	public void Sinario_Active(AudioClip voice)
	{
		if(!Sinario_audio.isPlaying)	Sinario_audio.PlayOneShot(voice);
	}

	public void Sinario_Stop()
	{
		if (Sinario_audio.isPlaying) Sinario_audio.Stop();
	}
}
