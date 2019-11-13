using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Helper_SceneTranslation : MonoBehaviour
{
    public Scene_Manager.SCENE_NAME sceneName;
    private bool isLoaded;
	public AudioSource audioSource; //ユニティ側にて設定
	public AudioClip audioClip;         //unity側から設定
	public AudioSource Decision;
	public AudioClip Decision_SE;           //プレイヤー数決定時の音

	public AudioSource BGM;
	public int Set_Step { get; private set; }

	public DemoMovieControl DMC;		//デモ修正のため
	List<InputManagerObject> inputManagerList = new List<InputManagerObject>();
	//private bool 
	private void Start()
	{
		isLoaded = false;
		Set_Step = 0;
		inputManagerList.AddRange(FindObjectsOfType<InputManagerObject>());
	}
	public void Update()
	{
		bool isSettingConfig = false;
		foreach (InputManagerObject manager in inputManagerList)
		{
			isSettingConfig = manager.IsInputSetting;
			if (isSettingConfig) { break; }
		}
		if (Set_Step == 0)
		{
			if(!DMC.IsPlayingMovie && !isSettingConfig)
			{
				if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("P2_Fire1"))
				{
					audioSource?.PlayOneShot(audioClip);
					DMC.IsStopDemoMovie = true;
					Set_Step++;
				}
			}
			//if (Input.anyKeyDown && !Input.GetKey(KeyCode.LeftAlt) && !Input.GetKey(KeyCode.LeftAlt) && !Input.GetKey(KeyCode.F4) && !Input.GetKey(KeyCode.LeftControl))
		}
		else if(Set_Step==1 && !isSettingConfig)
		{
			if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("P2_Fire1"))
			{
				Set_Step++;
				if (Decision.isPlaying) Decision.Stop();
				Decision.PlayOneShot(Decision_SE);
			}
			else if(Input.GetButtonDown("Fire2") || Input.GetButtonDown("P2_Fire2"))
			{
				Set_Step--;
				DMC.IsStopDemoMovie = false;
			}
		}
		else if(Set_Step == 2)
		{
			Scene_Manager.Manager.Screen_Transition_To_Stage_01();
		}
	}
}
