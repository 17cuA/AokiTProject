using System.Collections;
//----------------------------------------------------------------------------------------------
// 制作者：諸岡勇樹
// 制作日：2019/05/24
//----------------------------------------------------------------------------------------------
// Score の文字の制御
//----------------------------------------------------------------------------------------------
// 2019/05/24：スコアを表示するだけの挙動
// 2019/05/27：表示する数値を受け取る関数追加
//----------------------------------------------------------------------------------------------
using UnityEngine;
using TextDisplay;

public class Score_Display : MonoBehaviour
{
	//---------------------------------------------------------------------------
	// Unity 側調節よう変数
	//---------------------------------------------------------------------------
	[SerializeField]
	[Header("読み込みたいフォントの場所")]
	private string font_path;
	[SerializeField]
	[Header("フォントのサイズ")]
	private float font_size;
	[SerializeField]
	[Header("表示位置")]
	private Vector3 position;
	//---------------------------------------------------------------------------

	//---------------------------------------------------------------------------
	// Unity 側調節よう変数
	//---------------------------------------------------------------------------
	[SerializeField]
	[Header("読み込みたいフォントの場所")]
	private string font_path_2;
	[SerializeField]
	[Header("フォントのサイズ")]
	private float font_size_2;
	[SerializeField]
	[Header("表示位置")]
	private Vector3 position_2;
	//---------------------------------------------------------------------------
	private GameObject Score_1P{get;set;}		// １Pのスコア表示用
	private GameObject Score_2P{get;set; }      // 2Pのスコア表示用

	public Character_Display Object_To_Display_1P{private set; get;}		// 1P用
    public Character_Display Object_To_Display_2P{private set; get;}        // 2P用

	void Start()
	{
		// 1P------------------------------
		Score_1P = new GameObject();
		Score_1P.transform.SetParent(transform);
		Object_To_Display_1P = new Character_Display(10, font_path, Score_1P, position);
		Object_To_Display_1P.Character_Preference(Game_Master.display_score_1P.ToString("D10"));
		Object_To_Display_1P.Size_Change(new Vector3(font_size, font_size, font_size));
		if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eTWO_PLAYER)
		{
			// 2P------------------------------
			Score_2P = new GameObject();
			Score_2P.transform.SetParent(transform);
			Object_To_Display_2P = new Character_Display(10, font_path_2, Score_2P, position_2);
			Object_To_Display_2P.Character_Preference(Game_Master.display_score_2P.ToString("D10"));
			Object_To_Display_2P.Size_Change(new Vector3(font_size_2, font_size_2, font_size_2));
		}
	}

    /// <summary>
    /// 表示数値の設定
    /// </summary>
    /// <param name="number_to_display"> 数値 </param>
    public void Display_Number_Preference_1P(uint number_to_display)
    {
        Object_To_Display_1P.Character_Preference(number_to_display.ToString("D10"));
    }
    /// <summary>
    /// 表示数値の設定
    /// </summary>
    /// <param name="number_to_display"> 数値 </param>
    public void Display_Number_Preference_2P(uint number_to_display)
    {
        Object_To_Display_2P.Character_Preference(number_to_display.ToString("D10"));
    }
}
