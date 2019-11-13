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

	// プレイヤー人数選択
	private Character_Display play_select;
	private GameObject play_select_parent;
	public Vector3 play_select_pos;
	public float play_select_size;

	// 1P
	private Character_Display one_player;
	private GameObject one_player_parent;
	public Vector3 one_player_pos;
	public float one_player_size;

	// 2P
	private Character_Display two_player;
	private GameObject two_player_parent;
	public Vector3 two_player_pos;
	public float two_player_size;
	
	// 選択用アイコン
	public GameObject select_icon;

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

		string character = "FREE_PLAY";
		please_push_button_parent = new GameObject();
		please_push_button_parent.transform.parent = transform;
		please_push_button = new Character_Display(character.Length, "morooka/SS", please_push_button_parent, please_push_button_pos);
		please_push_button.Character_Preference(character);
		please_push_button.Size_Change(new Vector3(please_push_button_size, please_push_button_size, please_push_button_size));
		please_push_button.Centering();

		character = "PLAY_SELECT";
		play_select_parent = new GameObject();
		play_select_parent.transform.parent = transform;
		play_select = new Character_Display(character.Length, "morooka/SS", play_select_parent, play_select_pos);
		play_select.Character_Preference(character);
		play_select.Size_Change(new Vector3(play_select_size, play_select_size, play_select_size));
		play_select.Centering();

		character = "1_PLAYER";
		one_player_parent = new GameObject();
		one_player_parent.transform.parent = transform;
		one_player = new Character_Display(character.Length, "morooka/SS", one_player_parent, one_player_pos);
		one_player.Character_Preference(character);
		one_player.Size_Change(new Vector3(one_player_size, one_player_size, one_player_size));
		one_player.Centering();

		character = "2_PLAYERS";
		two_player_parent = new GameObject();
		two_player_parent.transform.parent = transform;
		two_player = new Character_Display(character.Length, "morooka/SS", two_player_parent, two_player_pos);
		two_player.Character_Preference(character);
		two_player.Size_Change(new Vector3(two_player_size, two_player_size, two_player_size));
		two_player.Centering();

		Vector3 temp = select_icon.transform.localPosition;
		temp.y = one_player_parent.transform.localPosition.y;
		select_icon.transform.localPosition = temp;

		// はじめ表示しない---------------------------------------------------
		play_select_parent.SetActive(false);
		one_player_parent.SetActive(false);
		two_player_parent.SetActive(false);
		select_icon.SetActive(false);
		// ---------------------------------------------------------------------
		temp_pos = select_icon.transform.localPosition;
	}

	void Update()
	{
		if (HS_Step.Set_Step == 0)
		{
			if (!please_push_button_parent.activeSelf)
			{
				please_push_button_parent.SetActive(true);
				play_select_parent.SetActive(false);
				one_player_parent.SetActive(false);
				two_player_parent.SetActive(false);
				select_icon.SetActive(false);
			}

			please_push_button.Color_Change(GetAlphaColor(please_push_button.Font_Color));
		}
		else if(HS_Step.Set_Step == 1)
		{
			if(please_push_button_parent.activeSelf)
			{
				please_push_button_parent.SetActive(false);
				play_select_parent.SetActive(true);
				one_player_parent.SetActive(true);
				two_player_parent.SetActive(true);
				select_icon.SetActive(true);
			}

			if (Input.GetAxis("Vertical") > 0 || Input.GetAxis("P2_Vertical") > 0)
			{
				Vector3 temp = select_icon.transform.localPosition;
				temp.y = one_player_parent.transform.localPosition.y;
				if(select_icon.transform.localPosition != temp)
				{
					select_icon.transform.localPosition = temp;
					//追加-------------------------
					if (Cource.isPlaying) Cource.Stop();
					Cource.PlayOneShot(Cource_SE);
				}
				Game_Master.MY.Number_Of_Players_Confirmed(Game_Master.PLAYER_NUM.eONE_PLAYER);
			}
			else if (Input.GetAxis("Vertical") < 0 || Input.GetAxis("P2_Vertical") < 0)
			{
				Vector3 temp = select_icon.transform.localPosition;
				temp.y = two_player_parent.transform.localPosition.y;
				if (select_icon.transform.localPosition != temp)
				{
					select_icon.transform.localPosition = temp;
					//追加-------------------------
					if (Cource.isPlaying) Cource.Stop();
					Cource.PlayOneShot(Cource_SE);
				}
				Game_Master.MY.Number_Of_Players_Confirmed(Game_Master.PLAYER_NUM.eTWO_PLAYER);
			}
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