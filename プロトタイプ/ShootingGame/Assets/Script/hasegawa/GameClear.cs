/*
 * 20190828 作成
 * author hasegawa yuuta
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TextDisplay;

namespace Costom
{
	/// <summary>
	/// ゲームクリアシーンを管理する
	/// </summary>
	public class GameClear : MonoBehaviour
	{
		/// <summary>
		/// 画面に表示するステータス
		/// </summary>
		enum EDisplayInformation
		{
			eResult,
			eResultToRanking,
			eRankingFromResult,
			eRanking,
			eNone
		}
		[SerializeField] Canvas anyCanvas;												// いずれかのCanvas
		EDisplayInformation displayInfo = EDisplayInformation.eNone;					// 画面の情報

		[SerializeField] ResultDisplay resultDisplay;									// リザルト画面用クラス
		[SerializeField] RankingDisplay rankingDisplay;									// ランキング表示用クラス

		GameObject pressButtonDisplayParent;											// PressButtonをまとめるオブジェクト
		Character_Display pressButtonDisplay;											// PressButtonを表示するためのもの
		Vector2 pressButtonDisplayPosition = new Vector2(25f, -450f);					// PressButtonの位置
		float pressButtonSize = 0.24f;													// PressButtonの大きさ
		int blinkFrame = 0;																// 点滅するためのフレーム数
		const int kBlinkFrameMax = 60;													// 点滅させる1周期のフレーム数

		bool isInput = false;															// 入力を受け付けたか
		bool isCompletedTransition = false;												// リザルトからランキングへの切り替えが終わったか
		[SerializeField] Image displayPlane;											// 画面を覆うImage

		void Start()
		{
			// リザルト表示用クラスが設定されていなかったら取得する
			if (!resultDisplay) { resultDisplay = FindObjectOfType<ResultDisplay>(); }
			// ランキング表示用クラスが設定されていなかったら取得する
			if (!rankingDisplay) { rankingDisplay = FindObjectOfType<RankingDisplay>(); }
			// キャンバスの取得
			if (!anyCanvas) { anyCanvas = FindObjectOfType<Canvas>(); }
			// PressButtonの生成
			pressButtonDisplayParent = new GameObject();
			pressButtonDisplayParent.transform.parent = anyCanvas.transform;
			pressButtonDisplay = new Character_Display("PRESS__BUTTON".Length, "morooka/SS", pressButtonDisplayParent, pressButtonDisplayPosition);
			pressButtonDisplay.Character_Preference("PRESS__BUTTON");
			pressButtonDisplay.Size_Change(Vector2.one * pressButtonSize);
			pressButtonDisplay.Centering();
			pressButtonDisplayParent.SetActive(false);
			displayInfo = EDisplayInformation.eResult;
		}

		void Update()
		{
			// リザルト画面の表示非表示設定
			resultDisplay.gameObject.SetActive(displayInfo == EDisplayInformation.eResult || displayInfo == EDisplayInformation.eResultToRanking);
			// ランキング画面の表示非表示設定
			rankingDisplay.gameObject.SetActive(displayInfo == EDisplayInformation.eRanking || displayInfo == EDisplayInformation.eRankingFromResult);
			// PressButtonの点滅
			if (displayInfo == EDisplayInformation.eResult || displayInfo == EDisplayInformation.eResultToRanking || (rankingDisplay.IsDecision1P && rankingDisplay.IsDecision2P))
			{
				pressButtonDisplayParent.SetActive(blinkFrame > kBlinkFrameMax / 2);
				++blinkFrame;
				if (blinkFrame > kBlinkFrameMax)
				{
					blinkFrame = 0;
				}
			}
			else
			{
				pressButtonDisplayParent.SetActive(false);
			}
			// 入力を受けたらScene移行
			if ((Input.anyKeyDown && rankingDisplay.IsDecision1P && rankingDisplay.IsDecision2P && displayInfo == EDisplayInformation.eRanking) && !Input.GetKey(KeyCode.LeftAlt) && !Input.GetKey(KeyCode.LeftAlt) && !Input.GetKey(KeyCode.F4) && !Input.GetKey(KeyCode.LeftControl))
			{
				Ranking_Strage.Strage_Data.Ranking_Save();
				Scene_Manager.Manager.Screen_Transition_To_Title();
			}
			// リザルトの時に入力を受けたら画面の暗転に移る
			if (displayInfo == EDisplayInformation.eResult && Input.anyKeyDown)
			{
				displayInfo = EDisplayInformation.eResultToRanking;
			}
			// 画面切り替え
			if (displayInfo == EDisplayInformation.eResultToRanking)
			{
				displayPlane.color += new Color(0f, 0f, 0f, 5f / 60f);
				if (displayPlane.color.a >= 1f)
				{
					displayPlane.color = new Color(displayPlane.color.r, displayPlane.color.g, displayPlane.color.b, 1f);
					displayInfo = EDisplayInformation.eRankingFromResult;
				}
			}
			else if (displayInfo == EDisplayInformation.eRankingFromResult)
			{
				displayPlane.color -= new Color(0f, 0f, 0f, 5f / 60f);
				if (displayPlane.color.a <= 0f)
				{
					displayPlane.color = new Color(displayPlane.color.r, displayPlane.color.g, displayPlane.color.b, 0f);
					displayInfo = EDisplayInformation.eRanking;
				}
			}
		}
	}
}