// テキストや画像を点滅させる
// 作成者:佐藤翼
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TextDisplay;

public class Blink : MonoBehaviour
{
	// ボタンを押してください
	private Character_Display please_push_button;
	private GameObject please_push_button_parent;
	public Vector3 please_push_button_pos;
	public float please_push_button_size;


	private Helper_SceneTranslation HS_Step { get; set; }

	//public
	public float speed = 1.0f;

	//private
	private Text text;
	private Image image;
	private float time;

	private enum ObjType
	{
		TEXT,
		IMAGE
	};
	private ObjType thisObjType = ObjType.TEXT;

	//追加------------------------
	public AudioSource Cource;
	public AudioClip Cource_SE;
	private Vector3 temp_pos;	//移動前の位置情報
	//----------------------------
	void Start()
	{
		HS_Step = GetComponent<Helper_SceneTranslation>();

		string character = "PLAY_START";
		please_push_button_parent = new GameObject();
		please_push_button_parent.transform.parent = transform;
		please_push_button = new Character_Display(character.Length, "morooka/SS", please_push_button_parent, please_push_button_pos);
		please_push_button.Character_Preference(character);
		please_push_button.Size_Change(new Vector3(please_push_button_size, please_push_button_size, please_push_button_size));
		please_push_button.Centering();
	}

	void Update()
	{
		if (HS_Step.Set_Step == 0)
		{
			if (!please_push_button_parent.activeSelf)
			{
				Game_Master.MY.Number_Of_Players_Confirmed(Game_Master.PLAYER_NUM.eONE_PLAYER);
			}

			please_push_button.Color_Change(GetAlphaColor(please_push_button.Font_Color));
		}
	}

	//Alpha値を更新してColorを返す
	Color GetAlphaColor(Color color)
	{
		time += Time.deltaTime * 5.0f * speed;
		color.a = Mathf.Sin(time) * 10.5f + 10.5f;

		return color;
	}
}