//作成日2019/07/03
// プレイヤーの残機表示
// 作成者:諸岡勇樹
/*
 * 2019/07/03：残機表示
 * 2019/08/23：１P、２PUI別け
 * 2019/09/13：残機復活適応
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TextDisplay;

public class Remaining_Display : MonoBehaviour
{
	//1P-------------------------------
	[SerializeField]
	[Header("フォントのサイズ")]
	private float font_size;
	[SerializeField]
	[Header("表示位置")]
	private Vector3 position;
	[SerializeField,Header("表示位置2")]
	private Vector3 lifePosition;
	[SerializeField,Header("表示位置3")]
	private Vector3 xRPosition;

	private GameObject[] oya_1P;
	private Player1 Player_Data_1P { get; set; }                                // プレイヤーの残機確認用
	private Character_Display Object_To_Display_1P { set; get; }       // 文字表示用
	private Character_Display[] Life_1P;                            // ライフ表示
	private int Remaining_Num_1P { get; set; }                         // 現在表示してる残機
	private Sprite[] Display_Sprite_1P { get; set; }                   // 表示したいスプライト用

	//2P----------------------------------
	[SerializeField]
	[Header("フォントのサイズ")]
	private float font_size_2P;
	[SerializeField]
	[Header("表示位置")]
	private Vector3 position_2P;
	[SerializeField,Header("表示位置2")]
	private Vector3 lifePosition_2P;
	[SerializeField,Header("表示位置3")]
	private Vector3 xRPosition_2P;

	private GameObject[] oya_2P;
	private Player2 Player_Data_2P { get; set; }								// プレイヤーの残機確認用
	private Character_Display Object_To_Display_2P { set; get; }       // 文字表示用
	private Character_Display[] Life_2P;							// ライフ表示
	private int Remaining_Num_2P { get; set; }                         // 現在表示してる残機
	private Sprite[] Display_Sprite_2P { get; set; }                   // 表示したいスプライト用

	private string Temp_String{get;set;}										// 仮置き用のストリング

	void Update()
    {
		// プレイヤーの情報がないとき
		if(Player_Data_1P == null)
		{
			oya_1P = new GameObject[3] { new GameObject(),new GameObject(),new GameObject()};
			foreach(GameObject obj in oya_1P)
			{
				obj.transform.parent = transform;
			}
			// プレイヤーのデータを保存
			Player_Data_1P = Obj_Storage.Storage_Data.GetPlayer().GetComponent<Player1>();
			Remaining_Num_1P = Player_Data_1P.Remaining;

			// リソースからアイコン用の画像取得
			Display_Sprite_1P = Resources.LoadAll<Sprite>("morooka/Remaining");

			// 「1P」の文字作製と設置
			// アンダーバーはスペース部分、1P　と　ＬＩＦＥの間にスコア表示
			Temp_String = "1P______________LIFE";
			Object_To_Display_1P = new Character_Display(Temp_String.Length, "morooka/SS", oya_1P[0], position);
			Object_To_Display_1P.Character_Preference(Temp_String);
			Object_To_Display_1P.Size_Change(new Vector3(font_size, font_size, font_size));

			// Ｘは小さく
			Life_1P = new Character_Display[2];
			Temp_String = "_X_";
			Life_1P[0] = new Character_Display(Temp_String.Length, "morooka/SS", oya_1P[1], lifePosition);
			Life_1P[0].Character_Preference(Temp_String);
			Life_1P[0].Size_Change(new Vector3(font_size / 3.0f * 2.0f, font_size / 3.0f * 2.0f, font_size / 3.0f * 2.0f));

			// プレイヤーの残機数表示
			Life_1P[1] = new Character_Display(1, "morooka/SS", oya_1P[2],xRPosition);
			Life_1P[1].Character_Preference(Remaining_Num_1P.ToString());
			Life_1P[1].Size_Change(new Vector3(font_size, font_size, font_size));


			// 2Pもいるとき
			if(Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eTWO_PLAYER)
			{
				oya_2P = new GameObject[3] { new GameObject(), new GameObject(), new GameObject() };
				foreach (GameObject obj in oya_2P)
				{
					obj.transform.parent = transform;
				}
				// プレイヤーのデータを保存
				Player_Data_2P = Obj_Storage.Storage_Data.GetPlayer2().GetComponent<Player2>();
				Remaining_Num_2P = Player_Data_2P.Remaining;

				// リソースからアイコン用の画像取得
				Display_Sprite_2P = Resources.LoadAll<Sprite>("morooka/Remaining");

				// 「2P」の文字作製と設置
				// アンダーバーはスペース部分、2P　と　ＬＩＦＥの間にスコア表示
				Temp_String = "2P______________LIFE";
				Object_To_Display_2P = new Character_Display(Temp_String.Length, "morooka/SS", oya_2P[0], position_2P);
				Object_To_Display_2P.Character_Preference(Temp_String);
				Object_To_Display_2P.Size_Change(new Vector3(font_size_2P, font_size_2P, font_size_2P));

				// Ｘは小さく
				Life_2P = new Character_Display[2];
				Temp_String = "_X_";
				Life_2P[0] = new Character_Display(Temp_String.Length, "morooka/SS", oya_2P[1], lifePosition_2P);
				Life_2P[0].Character_Preference(Temp_String);
				Life_2P[0].Size_Change(new Vector3(font_size_2P / 3.0f * 2.0f, font_size_2P / 3.0f * 2.0f, font_size_2P / 3.0f * 2.0f));

				// プレイヤーの残機数表示
				Life_2P[1] = new Character_Display(1, "morooka/SS", oya_2P[2], xRPosition_2P);
				Life_2P[1].Character_Preference(Remaining_Num_2P.ToString());
				Life_2P[1].Size_Change(new Vector3(font_size_2P, font_size_2P, font_size_2P));
			}
		}

		//表示中のアイコンの数とプレイヤーの残機の数が違うとき
		if(Player_Data_1P.Remaining != Remaining_Num_1P)
		{
			// 表示中のアイコンの非表示化
			Remaining_Num_1P = Player_Data_1P.Remaining;
			Life_1P[1].Character_Preference(Remaining_Num_1P.ToString());
		}

		// 2Pがいるとき
		if(Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eTWO_PLAYER)
		{
			if(Player_Data_2P.Remaining != Remaining_Num_2P)
			{
				// 表示中のアイコンの非表示化
				Remaining_Num_2P = Player_Data_2P.Remaining;
				Life_2P[1].Character_Preference(Remaining_Num_2P.ToString());
			}
		}
    }
}
