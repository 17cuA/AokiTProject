/*
 * 20190828 作成
 * author hasegawa yuuta
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextDisplay;

/// <summary>
/// ランキングの表示をする
/// </summary>
public class ResultDisplay : MonoBehaviour
{
	public const uint kClearbonusValue = 50000;
	const float kWholeScaleWeight = 1.4f;
	// ヘッダー
	private Character_Display resultTextDisplay;
	private Vector2 resultTextPosition = new Vector2(50f, 1080f - 1080f / 2f - 100f);
	private Character_Display player1TextDisplay;
	private Vector2 player1TextPosition = new Vector2(-3840f / 2f / 2f + 50f, 1080f - 1080f / 2f - 200f);
	private Character_Display player2TextDisplay;
	private Vector2 player2TextPosition = new Vector2(3840f / 2f / 2f + 50f, 1080f - 1080f / 2f - 200f);
	// リザルト表示用(1P)
	private Character_Display result1PScoreTextDisplay;
	private Vector2 result1PScoreTextPosition = new Vector2(-3840f / 2f + 100f * 2f + 100f, 1080f / 4f * 3f - 1080f / 2f + 50f - 100f);
	private Character_Display result1PScoreDisplay;
	private Vector2 result1PScorePosition = new Vector2(-966f, 1080f / 4f * 3f - 1080f / 2f - 50f - 100f);
	private Character_Display result1PClearbonusTextDisplay;
	private Vector2 result1PClearbonusTextPosition = new Vector2(-3840f / 2f + 100f * 2f + 100f, 1080f / 4f * 2f - 1080f / 2f + 50f - 100f);
	private Character_Display result1PClearbonusDisplay;
	private Vector2 result1PClearbonusPosition = new Vector2(-966f, 1080f / 4f * 2f - 1080f / 2f - 50f - 100f);
	private Character_Display result1PTotalScoreTextDisplay;
	private Vector2 result1PTotalScoreTextPosition = new Vector2(-3840f / 2f + 100f * 2f + 100f, 1080f / 4f * 1f - 1080f / 2f + 50f - 100f);
	private Character_Display result1PTotalScoreDisplay;
	private Vector2 result1PTotalScorePosition = new Vector2(-3840f / 2f + 100f * 6f + 150f, 1080f / 4f * 1f - 1080f / 2f - 50f - 100f);
	// リザルト表示用(2P)
	private Character_Display result2PScoreTextDisplay;
	private Vector2 result2PScoreTextPosition = new Vector2(100f * 2f + 100f, 1080f / 4f * 3f - 1080f / 2f + 50f - 100f);
	private Character_Display result2PScoreDisplay;
	private Vector2 result2PScorePosition = new Vector2(952f, 1080f / 4f * 3f - 1080f / 2f - 50f - 100f);
	private Character_Display result2PClearbonusTextDisplay;
	private Vector2 result2PClearbonusTextPosition = new Vector2(100f * 2f + 100f, 1080f / 4f * 2f - 1080f / 2f + 50f - 100f);
	private Character_Display result2PClearbonusDisplay;
	private Vector2 result2PClearbonusPosition = new Vector2(952f, 1080f / 4f * 2f - 1080f / 2f - 50f - 100f);
	private Character_Display result2PTotalScoreTextDisplay;
	private Vector2 result2PTotalScoreTextPosition = new Vector2(100f * 2f + 100f, 1080f / 4f * 1f - 1080f / 2f + 50f - 100f);
	private Character_Display result2PTotalScoreDisplay;
	private Vector2 result2PTotalScorePosition = new Vector2(100f * 6f + 150f, 1080f / 4f * 1f - 1080f / 2f - 50f - 100f);


	void Start()
	{
		// ヘッダー
		GameObject resultTextParent = new GameObject("ResultText");
		resultTextParent.transform.parent = transform;
		resultTextDisplay = new Character_Display("RESULT".Length, "morooka/SS", resultTextParent, resultTextPosition);
		resultTextDisplay.Character_Preference("RESULT");
		resultTextDisplay.Size_Change(Vector2.one / kWholeScaleWeight);
		resultTextDisplay.Centering();
		// メソッド実行
		SettingResultDisplayPlayer1();
		// 二人プレイの時はPlayer2の分も実行する
		if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eTWO_PLAYER)
		{
			SettingResultDisplayPlayer2();
		}
	}

	/// <summary>
	/// Player1のリザルト設定
	/// </summary>
	void SettingResultDisplayPlayer1()
	{
		// ヘッダー
		GameObject player1TextParent = new GameObject("Player1Text");
		player1TextParent.transform.parent = transform;
		player1TextDisplay = new Character_Display("PLAYER_1".Length, "morooka/SS", player1TextParent, player1TextPosition);
		player1TextDisplay.Character_Preference("PLAYER_1");
		player1TextDisplay.Centering();
		player1TextDisplay.Size_Change(Vector2.one / kWholeScaleWeight);
		// スコアのテキスト
		GameObject result1PScoreTextsParent = new GameObject("1PScoreText");
		result1PScoreTextsParent.transform.parent = transform;
		result1PScoreTextDisplay = new Character_Display("SCORE".Length, "morooka/SS", result1PScoreTextsParent, result1PScoreTextPosition);
		result1PScoreTextDisplay.Character_Preference("SCORE");
		result1PScoreTextDisplay.Size_Change(Vector2.one * 0.75f / kWholeScaleWeight);
		// スコア
		GameObject result1PScoresParent = new GameObject("1PScore");
		result1PScoresParent.transform.parent = transform;
		result1PScoreDisplay = new Character_Display(10, "morooka/SS", result1PScoresParent, result1PScorePosition);
		result1PScoreDisplay.Character_Preference(Game_Master.display_score_1P.ToString("D10"));
		result1PScoreDisplay.Size_Change(Vector2.one / kWholeScaleWeight);
		// クリアボーナスのテキスト
		GameObject result1PClearbonusTextsParent = new GameObject("1PClearbonusText");
		result1PClearbonusTextsParent.transform.parent = transform;
		result1PClearbonusTextDisplay = new Character_Display("BONUS".Length, "morooka/SS", result1PClearbonusTextsParent, result1PClearbonusTextPosition);
		result1PClearbonusTextDisplay.Character_Preference("BONUS");
		result1PClearbonusTextDisplay.Size_Change(Vector2.one * 0.75f / kWholeScaleWeight);
		// クリアボーナス
		uint clearbonus = kClearbonusValue;
		if (Scene_Manager.Manager.Now_Scene == Scene_Manager.SCENE_NAME.eGAME_OVER) { clearbonus = 0; }
		GameObject result1PClearbonusesParent = new GameObject("1PClearbonus");
		result1PClearbonusesParent.transform.parent = transform;
		result1PClearbonusDisplay = new Character_Display(10, "morooka/SS", result1PClearbonusesParent, result1PClearbonusPosition);
		result1PClearbonusDisplay.Character_Preference(clearbonus.ToString("D10"));
		result1PClearbonusDisplay.Size_Change(Vector2.one / kWholeScaleWeight);
		// トータルスコアのテキスト
		GameObject result1PTotalScoreTextsParent = new GameObject("1PTotalScoreText");
		result1PTotalScoreTextsParent.transform.parent = transform;
		result1PTotalScoreTextDisplay = new Character_Display("TOTAL_SCORE".Length, "morooka/SS", result1PTotalScoreTextsParent, result1PTotalScoreTextPosition);
		result1PTotalScoreTextDisplay.Character_Preference("TOTAL_SCORE");
		result1PTotalScoreTextDisplay.Size_Change(Vector2.one * 0.75f / kWholeScaleWeight);
		// トータルスコア
		uint total1PScore = Game_Master.display_score_1P + clearbonus;
		GameObject result1PTotalScoresParent = new GameObject("1PTotalScore");
		result1PTotalScoresParent.transform.parent = transform;
		result1PTotalScoreDisplay = new Character_Display(10, "morooka/SS", result1PTotalScoresParent, result1PTotalScorePosition);
		result1PTotalScoreDisplay.Character_Preference(total1PScore.ToString("D10"));
		result1PTotalScoreDisplay.Size_Change(Vector2.one * 1.3f / kWholeScaleWeight);
	}

	/// <summary>
	/// Player2のリザルト設定
	/// </summary>
	void SettingResultDisplayPlayer2()
	{
		// ヘッダー
		GameObject player2TextParent = new GameObject("Player2Text");
		player2TextParent.transform.parent = transform;
		player2TextDisplay = new Character_Display("PLAYER_2".Length, "morooka/SS", player2TextParent, player2TextPosition);
		player2TextDisplay.Character_Preference("PLAYER_2");
		player2TextDisplay.Centering();
		player2TextDisplay.Size_Change(Vector2.one / kWholeScaleWeight);
		// スコアのテキスト
		GameObject result2PScoreTextsParent = new GameObject("2PScoreText");
		result2PScoreTextsParent.transform.parent = transform;
		result2PScoreTextDisplay = new Character_Display("SCORE".Length, "morooka/SS", result2PScoreTextsParent, result2PScoreTextPosition);
		result2PScoreTextDisplay.Character_Preference("SCORE");
		result2PScoreTextDisplay.Size_Change(Vector2.one * 0.75f / kWholeScaleWeight);
		// スコア
		GameObject result2PScoresParent = new GameObject("2PScore");
		result2PScoresParent.transform.parent = transform;
		result2PScoreDisplay = new Character_Display(10, "morooka/SS", result2PScoresParent, result2PScorePosition);
		result2PScoreDisplay.Character_Preference(Game_Master.display_score_2P.ToString("D10"));
		result2PScoreDisplay.Size_Change(Vector2.one / kWholeScaleWeight);
		// クリアボーナスのテキスト
		GameObject result2PClearbonusTextsParent = new GameObject("2PClearbonusText");
		result2PClearbonusTextsParent.transform.parent = transform;
		result2PClearbonusTextDisplay = new Character_Display("CLEAR_BONUS".Length, "morooka/SS", result2PClearbonusTextsParent, result2PClearbonusTextPosition);
		result2PClearbonusTextDisplay.Character_Preference("CLEAR_BONUS");
		result2PClearbonusTextDisplay.Size_Change(Vector2.one * 0.75f / kWholeScaleWeight);
		// クリアボーナス
		uint clearbonus = kClearbonusValue;
		if (Scene_Manager.Manager.Now_Scene == Scene_Manager.SCENE_NAME.eGAME_OVER) { clearbonus = 0; }
		GameObject result2PClearbonusesParent = new GameObject("2PClearbonus");
		result2PClearbonusesParent.transform.parent = transform;
		result2PClearbonusDisplay = new Character_Display(10, "morooka/SS", result2PClearbonusesParent, result2PClearbonusPosition);
		result2PClearbonusDisplay.Character_Preference(clearbonus.ToString("D10"));
		result2PClearbonusDisplay.Size_Change(Vector2.one / kWholeScaleWeight);
		// トータルスコアのテキスト
		GameObject result2PTotalScoreTextsParent = new GameObject("2PTotalScoreText");
		result2PTotalScoreTextsParent.transform.parent = transform;
		result2PTotalScoreTextDisplay = new Character_Display("TOTAL_SCORE".Length, "morooka/SS", result2PTotalScoreTextsParent, result2PTotalScoreTextPosition);
		result2PTotalScoreTextDisplay.Character_Preference("TOTAL_SCORE");
		result2PTotalScoreTextDisplay.Size_Change(Vector2.one * 0.75f / kWholeScaleWeight);
		// トータルスコア
		uint total2PScore = Game_Master.display_score_2P + clearbonus;
		GameObject result2PTotalScoresParent = new GameObject("2PTotalScore");
		result2PTotalScoresParent.transform.parent = transform;
		result2PTotalScoreDisplay = new Character_Display(10, "morooka/SS", result2PTotalScoresParent, result2PTotalScorePosition);
		result2PTotalScoreDisplay.Character_Preference(total2PScore.ToString("D10"));
		result2PTotalScoreDisplay.Size_Change(Vector2.one * 1.3f / kWholeScaleWeight);
	}
}
