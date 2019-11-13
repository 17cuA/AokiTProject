// スコアランキングを管理する
// 作成者:佐藤翼
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  

public class Scene_Ranking : MonoBehaviour
{

	Game_Master game_master;
	int Score_Num;
	public GameObject score_object = null; // Textオブジェクト
	public int score_num = 0; // スコア変数

	// 初期化時の処理
	void Start()
	{
		//Score_Num = (int)game_master.display_score;
		// スコアのロード
		score_num = PlayerPrefs.GetInt("SCORE", Score_Num);
	}
	// 削除時の処理
	void OnDestroy()
	{
		// スコアを保存
		PlayerPrefs.SetInt("SCORE", score_num);
		PlayerPrefs.Save();
	}

	// 更新
	void Update()
	{
		// オブジェクトからTextコンポーネントを取得
		Text score_text = score_object.GetComponent<Text>();
		// テキストの表示を入れ替える
		score_text.text = "Score:" + score_num;

		//score_num += 1; // とりあえず1加算し続けてみる
	}
}