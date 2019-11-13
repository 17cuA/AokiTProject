//作成日2019/07/08
// シーンマネージャー
// 作成者:諸岡勇樹
/*
 * 2019/07/17 フェードイン、フェードアウトする　シーン遷移
 * 2019/07/18 フェードアウト後、ディレイを入れる
 * 2019/07/19 フェードイン中にボタンが押されるとゲームが止まるバグの修正
 * 2019/07/21 説明シーン追加
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
	/// <summary>
	/// シーンの名前管理イナム
	/// </summary>
	public enum SCENE_NAME
	{
		eCAUTION,           // 0	(警告シーン)
		eROGO,                  // 1	(ロゴシーン)
		eTITLE,                 // 2	(タイトルシーン)
		eSTAGE_01,          // 3	(ステージ1シーン)
		eSTAGE_02,          // 4	(ステージ2シーン)
		eSTAGE_03,          // 5	(ステージ3シーン)
		eSTAGE_04,          // 6	(ステージ4シーン)
		eSTAGE_05,          // 7	(ステージ5シーン)
		eSTAGE_06,          // 8	(ステージ6シーン)
		eSTAGE_07,          // 9	(ステージ7シーン)
		eGAME_OVER,     // 10	(ゲームオーバーシーン)
		eGAME_CLEAR,        // 11	(ゲームクリアシーン)
	}

	static public Scene_Manager Manager { get; private set; }       // シーンマネージャー自体の保存

	[SerializeField, Header("フェードインスピード")] private float fade_in_speed;
	[SerializeField, Header("フェードアウトスピード")] private float fade_out_speed;
	[SerializeField, Header("シーン遷移の遅延時間")] private int transition_deferred;

	private Image Renderer_For_Fade { get; set; }                   // フェード用SpriteRenderer
	private float Fade_In_Quantity { get; set; }                    // 1フレームのフェードイン量
	private float Fade_Out_Quantity { get; set; }                   // 1フレームのフェードアウト量
	private int Transition_Deferred_Cnt { get; set; }               // シーン遷移の遅延時間カウント
	public SCENE_NAME Now_Scene { get; private set; }               // 今のシーン保存用
	public SCENE_NAME Next_Scene { get; private set; }              // 次のシーン保存用
	public bool Is_Fade_Finished { get; private set; }              // フェードが終わっているかどうか
	public bool Is_Fade_In_Intermediate { get; private set; }       // フェードイン中かどうか
	public bool Is_Fade_Out_Intermediate { get; private set; }      // フェードアウト中かどうか

	private void Awake()
	{
		Renderer_For_Fade = transform.GetChild(0).GetChild(0).GetComponent<Image>();
		Renderer_For_Fade.color = Color.black;
	}

	void Start()
	{
		Manager = GetComponent<Scene_Manager>();
		Next_Scene = Now_Scene = (SCENE_NAME)SceneManager.GetActiveScene().buildIndex;
		Fade_In_Quantity = (255.0f / fade_in_speed) / 255.0f;
		Fade_Out_Quantity = (255.0f / fade_out_speed) / 255.0f;

		Is_Fade_Finished = true;
		Is_Fade_In_Intermediate = true;
		Is_Fade_Out_Intermediate = false;

		Transition_Deferred_Cnt = 0;
	}

	void Update()
	{
		// フェードイン中 かつ フェードアウトしてないとき
		if (Is_Fade_In_Intermediate && !Is_Fade_Out_Intermediate)
		{
			Fade_In();
		}
		// フェードインしてないとき かつ フェードアウト中のとき
		else if (!Is_Fade_In_Intermediate && Is_Fade_Out_Intermediate)
		{
			// フェードアウト終了時
			if (Fade_Out())
			{
				// 時間を計測
				Transition_Deferred_Cnt++;
				// 指定時間を過ぎるとシーン遷移
				if (Transition_Deferred_Cnt > transition_deferred)
				{
					SceneManager.LoadScene((int)Next_Scene);
				}
			}
		}

		// 警告シーンに戻るデバッグキー
		if (Input.GetKeyDown(KeyCode.F2))
		{
			Screen_Transition_To_Caution();
		}
	}

	/// <summary>
	/// フェードイン
	/// </summary>
	/// <returns> フェードイン終了時 True </returns>
	public bool Fade_In()
	{
		Is_Fade_Finished = true;
		Is_Fade_In_Intermediate = false;

		if (Renderer_For_Fade.color.a > 0.0f)
		{
			Color color_for_fade = Renderer_For_Fade.color;
			color_for_fade.a -= Fade_In_Quantity;

			Renderer_For_Fade.color = color_for_fade;

			Is_Fade_Finished = false;
			Is_Fade_In_Intermediate = true;
		}
		return Is_Fade_Finished;
	}

	/// <summary>
	/// フェードアウト
	/// </summary>
	/// <returns> フェードアウト終了時 False </returns>
	public bool Fade_Out()
	{
		Is_Fade_Finished = true;

		if (Renderer_For_Fade.color.a < 1.0f)
		{
			Color color_for_fade = Renderer_For_Fade.color;
			color_for_fade.a += Fade_Out_Quantity;

			Renderer_For_Fade.color = color_for_fade;

			Is_Fade_Finished = false;
		}
		return Is_Fade_Finished;
	}

	/// <summary>
	/// 注意書きシーンへ移動
	/// </summary>
	public void Screen_Transition_To_Caution()
	{
		if (!Is_Fade_Out_Intermediate && Is_Fade_Finished)
		{
			Is_Fade_Out_Intermediate = true;
		}
		Next_Scene = SCENE_NAME.eCAUTION;
	}

	/// <summary>
	/// ロゴシーンに移動
	/// </summary>
	public void Screen_Transition_To_ROGO()
	{
		if (!Is_Fade_Out_Intermediate && Is_Fade_Finished)
		{
			Is_Fade_Out_Intermediate = true;
		}
		Next_Scene = SCENE_NAME.eROGO;
	}

	/// <summary>
	/// タイトルに移動
	/// </summary>
	public void Screen_Transition_To_Title()
	{
		if (!Is_Fade_Out_Intermediate && Is_Fade_Finished)
		{
			Is_Fade_Out_Intermediate = true;
		}
		Next_Scene = SCENE_NAME.eTITLE;
	}
	/// <summary>
	/// ステージ_01 に移動
	/// </summary>
	public void Screen_Transition_To_Stage_01()
	{
		if (!Is_Fade_Out_Intermediate && Is_Fade_Finished)
		{
			Is_Fade_Out_Intermediate = true;
		}
		Next_Scene = SCENE_NAME.eSTAGE_01;
	}

	/// <summary>
	/// ステージ_02 に移動
	/// </summary>
	public void Screen_Transition_To_Stage_02()
	{
		if (!Is_Fade_Out_Intermediate && Is_Fade_Finished)
		{
			Is_Fade_Out_Intermediate = true;
		}
		Next_Scene = SCENE_NAME.eSTAGE_02;
	}
	/// <summary>
	/// ステージ_03 に移動
	/// </summary>
	public void Screen_Transition_To_Stage_03()
	{
		if (!Is_Fade_Out_Intermediate && Is_Fade_Finished)
		{
			Is_Fade_Out_Intermediate = true;
		}
		Next_Scene = SCENE_NAME.eSTAGE_03;
	}
	/// <summary>
	/// ステージ_04 に移動
	/// </summary>
	public void Screen_Transition_To_Stage_04()
	{
		if (!Is_Fade_Out_Intermediate && Is_Fade_Finished)
		{
			Is_Fade_Out_Intermediate = true;
		}
		Next_Scene = SCENE_NAME.eSTAGE_04;
	}
	/// <summary>
	/// ステージ_05 に移動
	/// </summary>
	public void Screen_Transition_To_Stage_05()
	{
		if (!Is_Fade_Out_Intermediate && Is_Fade_Finished)
		{
			Is_Fade_Out_Intermediate = true;
		}
		Next_Scene = SCENE_NAME.eSTAGE_05;
	}
	/// <summary>
	/// ステージ_06 に移動
	/// </summary>
	public void Screen_Transition_To_Stage_06()
	{
		if (!Is_Fade_Out_Intermediate && Is_Fade_Finished)
		{
			Is_Fade_Out_Intermediate = true;
		}
		Next_Scene = SCENE_NAME.eSTAGE_06;
	}
	/// <summary>
	/// ステージ_07 に移動
	/// </summary>
	public void Screen_Transition_To_Stage_07()
	{
		if (!Is_Fade_Out_Intermediate && Is_Fade_Finished)
		{
			Is_Fade_Out_Intermediate = true;
		}
		Next_Scene = SCENE_NAME.eSTAGE_07;
	}

	/// <summary>
	/// ゲームオーバーに移動
	/// </summary>
	public void Screen_Transition_To_Over()
	{
		if (!Is_Fade_Out_Intermediate && Is_Fade_Finished)
		{
			Is_Fade_Out_Intermediate = true;
		}

		Next_Scene = SCENE_NAME.eGAME_OVER;
	}

	/// <summary>
	/// ゲームクリアーに移動
	/// </summary>
	public void Screen_Transition_To_Clear()
	{
		if (!Is_Fade_Out_Intermediate && Is_Fade_Finished)
		{
			Is_Fade_Out_Intermediate = true;
		}

		Next_Scene = SCENE_NAME.eGAME_CLEAR;
	}

	/// <summary>
	/// 任意のシーンに移動
	/// </summary>
	/// <param name="name"> シーンの名前 </param>
	public void Scene_Transition(SCENE_NAME name)
	{
		if (!Is_Fade_Out_Intermediate && Is_Fade_Finished)
		{
			Is_Fade_Out_Intermediate = true;
		}
		Next_Scene = name;
	}
}
