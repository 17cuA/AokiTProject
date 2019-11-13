//作成日2019/09/25
// ゲームシーンの時間計測
// 作成者:諸岡勇樹
/*
 * 2019/09/25　ゲーム中の時間計測
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stopwatch : MonoBehaviour
{
	private Text Display_Text { get; set; }								// 表示テキスト
	private bool Is_OK_Measurement { get; set;  }	// 計測してもよいか
	private float Temp_Time { get; set; }
	static public int MilliSecond { get; private set; }		// ミリ秒
	static public int Second { get; private set; }				// 秒
	static public int Min { get; set; }								// 分
	static public int Hour { get; set; }								// 時間
	void Start()
    {
        if(Scene_Manager.Manager.Now_Scene == Scene_Manager.SCENE_NAME.eSTAGE_01)
		{
			Is_OK_Measurement = true;
			MilliSecond = Second = Min = Hour = 0;
		}
		else
		{
			Is_OK_Measurement = false;
		}
		Display_Text = GetComponent<Text>();
		Display_Text.enabled = false;
	}

    void Update()
    {
		if(Input.GetKey(KeyCode.H) && Input.GetKeyDown(KeyCode.T))
		{
			if (Display_Text.enabled) Display_Text.enabled = false;
			else Display_Text.enabled = true;
		}

		if(Is_OK_Measurement)
		{
			Temp_Time += Time.deltaTime;
			Min = (int)(Temp_Time / 60.0f);
			Second = (int)(Temp_Time % 60.0f);
			MilliSecond = (int)(Temp_Time * 1000.0f % 1000.0f);
			if (Min >= 60)
			{
				Hour++;
				Min = 0;
				Temp_Time -= 60.0f * 60.0f;
			}
		}

		if(Display_Text.enabled)
		{
			Display_Text.text = Hour.ToString("D2") + ":" + Min.ToString("D2") + ":" + Second.ToString("D2") + ":"+MilliSecond.ToString("D2");
		}
	}
}
