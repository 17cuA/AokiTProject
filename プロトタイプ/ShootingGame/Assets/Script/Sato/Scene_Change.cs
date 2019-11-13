// タイトルからのシーン移動
// 作成者:佐藤翼
// 変更者:諸岡
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Change : MonoBehaviour
{
	private bool isLoaded;
	[Header("パワーアップ用の物")]
	public AudioSource audioSource; //ユニティ側にて設定
	[Header("無線用のもの")]
	public AudioClip audioClip;         //unity側から設定
	private void Start()
	{
		isLoaded = false;
	}
	public void Update()
	{
		if (!isLoaded)
		{
			if (/*Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("joystick button 7") || Input.GetKeyDown("joystick button 0")*/
				Input.anyKey && !Input.GetKey(KeyCode.LeftAlt) && !Input.GetKey(KeyCode.LeftAlt) && !Input.GetKey(KeyCode.F4) && !Input.GetKey(KeyCode.LeftControl))
			{
				//isLoaded = !isLoaded;
				//if (isLoaded)
				//{
				//             //Application.LoadLevelAdditive("MENU");
				//             SceneManager.LoadScene("MENU", LoadSceneMode.Additive); // OK
				//             //SceneManager.UnloadSceneAsync("Title");
				//             gameObject.SetActive(false);

				//         }
				//else
				//{
				//             SceneManager.UnloadSceneAsync("Title");
				//             Resources.UnloadUnusedAssets();
				//             gameObject.SetActive(true);
				//         }
				audioSource.PlayOneShot(audioClip);
				isLoaded = true;
			}
		}
		else
		{
			Scene_Manager.Manager.Screen_Transition_To_Stage_01();
		}
	}
}
