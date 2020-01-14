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
	// リザルト表示用(1P)
	private Character_Display result1PScoreTextDisplay;
	private Vector2 result1PScoreTextPosition = new Vector2(-1920f / 2f + 100f * 2f + 100f, 1080f / 4f * 3f - 1080f / 2f + 50f);
	private Character_Display result1PScoreDisplay;
	private Vector2 result1PScorePosition = new Vector2(-10f, 1080f / 4f * 3f - 1080f / 2f - 50f);
	private Character_Display result1PClearbonusTextDisplay;
	private Vector2 result1PClearbonusTextPosition = new Vector2(-1920f / 2f + 100f * 2f + 100f, 1080f / 4f * 2f - 1080f / 2f + 50f);
	private Character_Display result1PClearbonusDisplay;
	private Vector2 result1PClearbonusPosition = new Vector2(-10f, 1080f / 4f * 2f - 1080f / 2f - 50f);
	private Character_Display result1PTotalScoreTextDisplay;
	private Vector2 result1PTotalScoreTextPosition = new Vector2(-1920f / 2f + 100f * 2f + 100f, 1080f / 4f * 1f - 1080f / 2f + 50f);
	private Character_Display result1PTotalScoreDisplay;
	private Vector2 result1PTotalScorePosition = new Vector2(-1920f / 2f + 100f * 6f + 150f, 1080f / 4f * 1f - 1080f / 2f - 50f);


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
	}

	/// <summary>
	/// Player1のリザルト設定
	/// </summary>
	void SettingResultDisplayPlayer1()
	{
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
}
