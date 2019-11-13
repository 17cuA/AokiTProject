// タイトルに戻るだけ
// 作成者:佐藤翼
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scane_Title : MonoBehaviour
{
	// Update is called once per frame
	void Update()
	{
		Cursor.visible = false;
		if (Input.GetKeyDown(KeyCode.L)|| Input.GetKeyDown("joystick button 1"))
		{
			SceneManager.LoadScene("TITLE");
		}
	}
}