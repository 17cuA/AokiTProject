using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AudioInfo
{
	public C_AudioType audioType;
	public AudioClip audio;
}

public enum C_AudioType
{
	BGM_Ending,
	BGM_Opening_01,
	BGM_Opening_02,
	BGM_Stage_01,
	BGM_GameOver,


	SE_Intro,
	SE_Credit,
	SE_SelectMove,
	SE_SelectOk,
	SE_Shot,
	SE_ItemGet,
	SE_Destroyed,
	SE_Shot_Hit,
	SE_Explosion,
	SE_BossExplosion,
	SE_SpeedUp,
	SE_Laser,
	SE_Double,
	SE_Liple_Laser,
	SE_Option,
	SE_Force_Field
}


public class AudioManager : MonoBehaviour
{
	private static AudioManager instance = null;
	public static AudioManager Instance
	{
		get
		{
			return instance;
		}
	}

	[Header("-----------BGMデータ----------")]
	public List<AudioInfo> bgms = new List<AudioInfo>();

	[Header("-----------SEデータ----------")]
	public List<AudioInfo> ses = new List<AudioInfo>();

	private AudioSource bgmPlayer;

	private GameObject seGroupFather;
	private List<AudioSource> canUseSePlayers;

	[Header("デフォルト設定")]
	public float defaultBgmVolume = 1.0f;
	public float defaultSeVolume = 1.0f;
	public float audioFadeInOutDefaultTime = 1f;
	public float audioFadeInDefaultVolume = 1f;

	private float audioFadeInTarget;
	private bool isAudioFadeIn = false;
	private float audioFadeInTime;
	private float audioFadeInCount;

	private bool isAudioFadeOut = false;
	private float audioFadeOutStart;
	private float audioFadeOutTime;
	private float audioFadeOutCount;

	private bool prepartToPause = false;
	private bool prepartToStop = false;

	private bool isPausing = false;
	private bool isSePausing = false;
	private void Awake()
	{
		DontDestroyOnLoad(this.gameObject);

		if(Instance == null)
		{
			instance = this;
		}
		else
		{
			if(Instance != this)
			{
				Destroy(this.gameObject);
			}
		}

		bgmPlayer = transform.Find("Bgm").GetComponent<AudioSource>();
		seGroupFather = transform.Find("Se").gameObject;
		canUseSePlayers = new List<AudioSource>();
	}

	
	private void Update()
	{
		if(isAudioFadeIn)
		{
			bgmPlayer.volume = Mathf.Lerp(0, audioFadeInTarget, audioFadeInCount / audioFadeInTime);
			audioFadeInCount += Time.deltaTime;
			if(bgmPlayer.volume >= 1)
			{
				audioFadeInCount = 0;
				isAudioFadeIn = false;
			}
		}

		if(isAudioFadeOut)
		{
			bgmPlayer.volume = Mathf.Lerp(audioFadeOutStart, 0, audioFadeOutCount / audioFadeOutTime);
			audioFadeOutCount += Time.deltaTime;
			if(bgmPlayer.volume <= 0)
			{
				isAudioFadeOut = false;
				audioFadeOutCount = 0;
				if(prepartToPause)
				{
					PauseBGM();
					prepartToPause = false;
				}
				if(prepartToStop)
				{
					StopCurrentAudioPlayer();
					prepartToStop = false;
				}
			}
		}

		for(var i = 0; i < canUseSePlayers.Count; ++i)
		{
			if(!canUseSePlayers[i].isPlaying && !isSePausing)
			{
				canUseSePlayers[i].gameObject.SetActive(false);
			}
		}
	}

	private void OnGUI()
	{
		GUIStyle fontStyle = new GUIStyle();  
	    fontStyle.alignment=TextAnchor.MiddleCenter;
        fontStyle.fontSize=25;

		if(GUI.Button (new Rect (60,60,240,120),"Fade In再生,Opening",fontStyle))
		{
			PlayBGMFadeIn(C_AudioType.BGM_Opening_01);
		}

		if(GUI.Button (new Rect (60,180,240,120),"再生,Opening",fontStyle))
		{
			PlayBGM(C_AudioType.BGM_Opening_01);
		}

		if(GUI.Button (new Rect (60,300,240,120),"停止,Opening",fontStyle))
		{
			StopCurrentAudioPlayer();
		}

		if(GUI.Button (new Rect (60,420,240,120),"Fade Out停止,Opening",fontStyle))
		{
			StopCurrentAudioPlayerFadeOut();
		}

		if(GUI.Button (new Rect (60,540,240,120),"一時停止,Opening",fontStyle))
		{
			PauseBGM();
		}

		if(GUI.Button (new Rect (60,660,240,120),"Fade Out 一時停止,Opening",fontStyle))
		{
			PauseBGMFadeOut();
		}

		if(GUI.Button (new Rect (60,780,240,120),"再開,Opening",fontStyle))
		{
			ResumeBGM();
		}

		if(GUI.Button (new Rect (60,900,240,120),"Fade In再開,Opening",fontStyle))
		{
			ResumeBGMFadeIn();
		}



		if(GUI.Button (new Rect (500,60,240,120),"アイテムレーザー",fontStyle))
		{
			PlaySE(C_AudioType.SE_Laser);
		}

		if(GUI.Button (new Rect (500,180,240,120),"アイテムダブル、OneShot",fontStyle))
		{
			PlaySEOneShot(C_AudioType.SE_Double);
		}

		if(GUI.Button (new Rect (500,300,240,120),"爆発音",fontStyle))
		{
			PlaySE(C_AudioType.SE_Explosion);
		}

		if(GUI.Button (new Rect (500,420,240,120),"全部停止",fontStyle))
		{
			StopCurrentAudioPlayer();
		}

		if(GUI.Button (new Rect (500,540,240,120),"一時停止,SE",fontStyle))
		{
			PauseSE();
		}

		if(GUI.Button (new Rect (500,780,240,120),"再開,SE",fontStyle))
		{
			ResumeSE();
		}

	}

	/// <summary>
	/// BGM再生1
	/// </summary>
	/// <param name="type"></param>
	/// <param name="volume"></param>
	/// <param name="isLoop"></param>
	public void PlayBGM(C_AudioType type,float volume, bool isLoop)
	{
		if(isPausing)
		{
			Debug.Log("Pause、再生処理は実行しない、Resumeを使用してください。");
			return;
		}

		AudioClip tempBgmChip = null;
		for(var i = 0; i < bgms.Count; ++i)
		{
			if(bgms[i].audioType == type)
			{
				tempBgmChip = bgms[i].audio;
				break;
			}
		}
		if(tempBgmChip == null)
		{
			Debug.LogError("再生するBGMタイプは存在しない");
			return;
		}

		bgmPlayer.clip = tempBgmChip;
		bgmPlayer.volume = volume;
		bgmPlayer.loop = isLoop;
		bgmPlayer.Play();
	}

	/// <summary>
	/// BGM再生2
	/// </summary>
	/// <param name="type"></param>
	/// <param name="volume"></param>
	public void PlayBGM(C_AudioType type, float volume)
	{
		PlayBGM(type, volume, true);
	}

	/// <summary>
	/// BGM再生3
	/// </summary>
	/// <param name="type"></param>
	public void PlayBGM(C_AudioType type)
	{
		PlayBGM(type, defaultBgmVolume, true);
	}

	/// <summary>
	/// SE再生1
	/// </summary>
	/// <param name="type"></param>
	/// <param name="volume"></param>
    public void PlaySE(C_AudioType type,float volume)
	{
		if(isSePausing)
		{
			Debug.Log("Se一時停止、Resumeを使用してください");
			return;
		}

		AudioSource GetSePlayer = null;
	
		if(canUseSePlayers.Count <= 0)
		{
			var newSePlayer = new GameObject("SePlayer");
			var audioSource = newSePlayer.AddComponent<AudioSource>();
			newSePlayer.transform.SetParent(seGroupFather.transform);
			GetSePlayer = audioSource;
			canUseSePlayers.Add(GetSePlayer);
		}
		else
		{
			for(var i = 0; i < canUseSePlayers.Count; ++i)
			{
				if(!canUseSePlayers[i].gameObject.activeSelf)
				{
					GetSePlayer = canUseSePlayers[i];
					GetSePlayer.gameObject.SetActive(true);
					break;
				}
			}
			if(GetSePlayer == null)
			{
				var newSePlayer = new GameObject("SePlayer");
				newSePlayer.transform.SetParent(seGroupFather.transform);
				var audioSource = newSePlayer.AddComponent<AudioSource>();
				GetSePlayer = audioSource;
				canUseSePlayers.Add(GetSePlayer);
			}
		}
		

		AudioClip tempSeChip = null;
		for(var i = 0; i < ses.Count; ++i)
		{
			if(ses[i].audioType == type)
			{
				tempSeChip = ses[i].audio;
				break;
			}
		}
		if(tempSeChip == null)
		{
			Debug.LogError("再生するSEタイプは存在しない");
			return;
		}

		GetSePlayer.clip = tempSeChip;
		GetSePlayer.volume = volume;
		GetSePlayer.loop = false;
		GetSePlayer.Play();
	}

	/// <summary>
	/// SE再生2
	/// </summary>
	/// <param name="type"></param>
	public void PlaySE(C_AudioType type)
	{
		PlaySE(type, defaultSeVolume);
	}

	/// <summary>
	///  SE　Play One shot1
	/// </summary>
	/// <param name="type"></param>
	/// <param name="volume"></param>
	public void PlaySEOneShot(C_AudioType type,float volume)
	{
		if(isSePausing)
		{
			Debug.Log("Se一時停止、Resumeを使用してください");
			return;
		}

		AudioSource GetSePlayer = null;
	
		if(canUseSePlayers.Count <= 0)
		{
			var newSePlayer = new GameObject("SePlayer");
			newSePlayer.transform.SetParent(seGroupFather.transform);
			var audioSource = newSePlayer.AddComponent<AudioSource>();
			GetSePlayer = audioSource;
			canUseSePlayers.Add(GetSePlayer);
		}
		else
		{
			for(var i = 0; i < canUseSePlayers.Count; ++i)
			{
				if(!canUseSePlayers[i].gameObject.activeSelf)
				{
					GetSePlayer = canUseSePlayers[i];
					GetSePlayer.gameObject.SetActive(true);
					break;
				}
			}
			if(GetSePlayer == null)
			{
				var newSePlayer = new GameObject("SePlayer");
				newSePlayer.transform.SetParent(seGroupFather.transform);
				var audioSource = newSePlayer.AddComponent<AudioSource>();
				GetSePlayer = audioSource;
				canUseSePlayers.Add(GetSePlayer);
			}
		}

		AudioClip tempSeChip = null;
		for(var i = 0; i < ses.Count; ++i)
		{
			if(ses[i].audioType == type)
			{
				tempSeChip = ses[i].audio;
				break;
			}
		}
		if(tempSeChip == null)
		{
			Debug.LogError("再生するSEタイプは存在しない");
			return;
		}

		GetSePlayer.clip = tempSeChip;
		GetSePlayer.volume = volume;
		GetSePlayer.loop = false;
		GetSePlayer.PlayOneShot(tempSeChip, volume);
	}


	/// <summary>
	/// SE　Play One shot2
	/// </summary>
	/// <param name="type"></param>
	public void PlaySEOneShot(C_AudioType type)
	{
		PlaySEOneShot(type, defaultSeVolume);
	}

	/// <summary>
	///　BGM　PauseとResume
	/// </summary>
	/// <param name="isPause"></param>
	public void PauseAndResumeBGM(bool isPause)
	{
		if(!isPausing　&& !isPause)
		{
			Debug.Log("Pauseではない中、再生再開処理は実行しない");
			return;
		}

		if(isPausing　&& isPause)
		{
			Debug.Log("Pause中、再生一時停止処理は実行しない");
			return;
		}


		if (isPause)
		{
			bgmPlayer.Pause();
			isPausing = true;
		}
		else
		{
			bgmPlayer.UnPause();
			isPausing = false;
			bgmPlayer.volume = 1f;
		}
	}

	public void PauseBGM()
	{
		PauseAndResumeBGM(true);
	}

	public void PauseBGMFadeOut()
	{
		PlayBGMFadeOut();
		prepartToPause = true;
		isPausing = true;
	}

	public void ResumeBGMFadeIn(float targetVolume,float fadeTime)
	{
		if(!isPausing)
		{
			Debug.Log("Pause中ではない、再生再開処理は実行しない");
			return;
		}

		isPausing = false;
		bgmPlayer.UnPause();
		isAudioFadeIn = true;
		audioFadeInTarget = targetVolume;
		audioFadeInTime = fadeTime;
	}

	public void ResumeBGMFadeIn()
	{
		ResumeBGMFadeIn(audioFadeInDefaultVolume, audioFadeInOutDefaultTime);
	}

	public void ResumeBGM()
	{
		PauseAndResumeBGM(false);
	}

	/// <summary>
	/// SE　PauseとResume
	/// </summary>
	/// <param name="isPause"></param>
	public void PauseAndResumeSE(bool isPause)
	{
		if (isPause)
		{
			for (var i = 0; i < canUseSePlayers.Count; ++i)
			{
				if (canUseSePlayers[i].isPlaying)
				{
					canUseSePlayers[i].Pause();
				}
			}
			isSePausing = true;
		}
		else
		{
			for (var i = 0; i < canUseSePlayers.Count; ++i)
			{
				if (!canUseSePlayers[i].isPlaying && canUseSePlayers[i].gameObject.activeSelf)
				{
					canUseSePlayers[i].UnPause();
				}
			}
			isSePausing = false;
		}
	}

	public void PauseSE()
	{
		PauseAndResumeSE(true);
	}

	public void ResumeSE()
	{
		PauseAndResumeSE(false);
	}

	public void StopCurrentAudioPlayer()
	{
		bgmPlayer.Stop();
		for(var i = 0; i < canUseSePlayers.Count; ++i)
		{
			if(canUseSePlayers[i].isPlaying)
			{
				canUseSePlayers[i].Stop();
			}
		}
		isSePausing = false;
		
	}

	public void StopCurrentAudioPlayerFadeOut(float fadeTime)
	{
		PlayBGMFadeOut(fadeTime);
		prepartToStop = true;
	}

	public void StopCurrentAudioPlayerFadeOut()
	{
		StopCurrentAudioPlayerFadeOut(audioFadeInOutDefaultTime);
	}

	public void PlayBGMFadeIn(C_AudioType type, float targetVolume,float fadeTime)
	{
		if(isPausing)
		{
			Debug.Log("Pause、再生処理は実行しない、Resumeを使用してください。");
			return;
		}

		isAudioFadeIn = true;
		audioFadeInTarget = targetVolume;
		audioFadeInTime = fadeTime;
		
		AudioClip tempBgmChip = null;
		for(var i = 0; i < bgms.Count; ++i)
		{
			if(bgms[i].audioType == type)
			{
				tempBgmChip = bgms[i].audio;
				break;
			}
		}
		if(tempBgmChip == null)
		{
			Debug.LogError("再生するBGMタイプは存在しない");
			return;
		}
		bgmPlayer.clip = tempBgmChip;
		bgmPlayer.Play();
	}

	public void PlayBGMFadeIn(C_AudioType type)
	{
		PlayBGMFadeIn(type, audioFadeInDefaultVolume, audioFadeInOutDefaultTime);
	}

	private void PlayBGMFadeOut(float fadeTime)
	{
		if(!bgmPlayer.isPlaying)
		{
			Debug.Log("再生しないため、再生一時停止処理は実行しない");
			return;
		}


		isAudioFadeOut = true;
		audioFadeOutStart = bgmPlayer.volume;
		audioFadeOutTime = fadeTime;

	}

	private void PlayBGMFadeOut()
	{
		PlayBGMFadeOut(audioFadeInOutDefaultTime);
	}
}
