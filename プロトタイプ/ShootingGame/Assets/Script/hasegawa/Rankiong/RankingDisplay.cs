
//----------------------------------------------------------
//　作成日　2018/11/09
//　作成者　諸岡勇樹
//----------------------------------------------------------
//　ランキング関連
//----------------------------------------------------------
//　作成時　2018/11/09　ランキングの表示
//　　追加　2018/12/11　RankingCarry クラスから情報をもらう
//　　追加　2018/12/11　ディスプレイサイズの設定
//　　変更　2019/01/19　背景の変更
//----------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TextDisplay;

public class RankingDisplay : MonoBehaviour
{
	const float kScreenWidth = 3840f;
	const float kScreenHeight = 1080f;
	private Ranking_Strage.RankingInformation[] dataArray;
	const float kWholeScaleWeight = 1.4f;

	// ヘッダー用
	const string headerText = "RANKING";
	private Character_Display headerDisplay;
	private GameObject headerParent;
	private Vector2 headerPosition = new Vector2(50f, 1080f / 2f / 7f * 5f);
	private float headerSize = 1f;

	// 1P表示テキスト
	Character_Display player1TextDisplay;
	Vector2 player1TextPosition = new Vector2(-kScreenWidth / 2f / 2f + 50f, 1080f - 1080f / 2f - 200f);
	// 2P表示テキスト
	Character_Display player2TextDisplay;
	Vector2 player2TextPosition = new Vector2(kScreenWidth / 2f / 2f + 50f, 1080f - 1080f / 2f - 200f);

	// 名前入力用
	private float inputNameSize = 0f;
	private Character_Display input1PNameDisplay;
	[SerializeField] private InputRankingName input1PNameClass;
	private Vector2 input1PNamePos = Vector2.zero;
	private string previous1PName = "";
	public bool IsDecision1P { get { return input1PNameClass.IsDecision || Ranking_Strage.Strage_Data.Player1Rank >= Ranking_Strage.Max_num; } }
	private Character_Display name1PSubscriptTextDisplay;
	private Vector2 name1PSubscriptTextPosition = new Vector2(-3840f / 2f / 2f - 80f * 3f, -1080f / 2f / 7f * 5f);

	private Character_Display input2PNameDisplay;
	[SerializeField] private InputRankingName input2PNameClass;
	private Vector2 input2PNamePos = Vector2.zero;
	private string previous2PName = "";
	public bool IsDecision2P
	{
		get
		{
			if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eONE_PLAYER)
			{
				return true;
			}
			return input2PNameClass.IsDecision || Ranking_Strage.Strage_Data.Player2Rank >= Ranking_Strage.Max_num;
		}
	}
	private Character_Display name2PSubscriptTextDisplay;
	private Vector2 name2PSubscriptTextPosition = new Vector2(3840f / 2f / 2f - 80f * 3f, -1080f / 2f / 7f * 5f);

	// ランキング用
	private float fontSize;
	private Character_Display[] ranking1PDisplay;
	private Vector3[] rank1PPosition;
	private GameObject[] rank1PParent;

	private Character_Display[] ranking2PDisplay;
	private Vector3[] rank2PPosition;
	private GameObject[] rank2PParent;

	int blinkPlayerLineFrame = 0;
	const int blinkPlayerLineFrameMax = 60;
	const int blinkPLayerLineInvisibleBorder = 58;

	private List<string> ngWordsList = new List<string>();

	// スクロール用
	const float kScrollValue = 90f / kWholeScaleWeight;
	const float kStartScrollValue = kScrollValue * 2f;
	float scroll1PValue = kStartScrollValue;
	int centerElementNum1P = 0;

	float scroll2PValue = kStartScrollValue;
	int centerElementNum2P = 0;


	void Start()
	{
	}

	public void Init()
	{
		// ヘッダー表示
		headerParent = new GameObject();
		headerParent.transform.parent = transform;
		headerSize = 1.2f;
		headerDisplay = new Character_Display(headerText.Length, "morooka/SS", headerParent, headerPosition);
		headerDisplay.Character_Preference(headerText);
		headerDisplay.Size_Change(Vector2.one * headerSize / kWholeScaleWeight);
		headerDisplay.Centering();
		Setting1PRankingDisplay();
		if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eTWO_PLAYER)
		{
			Setting2PRankingDisplay();
		}
		LoadNGWordsFromCSV();
	}
	/// <summary>
	/// NGワードのリストをロードする
	/// </summary>
	void LoadNGWordsFromCSV()
	{
		TextAsset textFile = Resources.Load("CSV_Folder/NGWordsList") as TextAsset;
		if (!textFile) { Debug.LogError("Don't found file, name NGWordsList"); return; }
		StringReader reader = new StringReader(textFile.text);
		while (reader.Peek() > -1)
		{
			string line = reader.ReadLine();
			string[] words = line.Split(',');
			ngWordsList.Add(words[0]);
		}
	}

	void Update()
	{
		// ゲームクリアシーンでなければ処理しない
		if (Scene_Manager.Manager.Now_Scene != Scene_Manager.SCENE_NAME.eGAME_CLEAR && Scene_Manager.Manager.Now_Scene != Scene_Manager.SCENE_NAME.eGAME_OVER) { return; }
		++blinkPlayerLineFrame;
		if (blinkPlayerLineFrame > blinkPlayerLineFrameMax) { blinkPlayerLineFrame = 0; }
		// 画面更新
		Update1PDisplay();
		// 1Pプレイの時は以降を処理しない
		if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eTWO_PLAYER)
		{
			Update2PDisplay();
		}
	}
	/// <summary>
	/// 1P画面更新
	/// </summary>
	void Update1PDisplay()
	{
		// 名前変更
		if (Ranking_Strage.Strage_Data.Player1Rank < Ranking_Strage.Max_num)
		{
			input1PNameClass.SelectingName();
			// NGワードであれば初めに戻す
			if (input1PNameClass.IsDecision && ngWordsList.Contains(input1PNameClass.Name))
			{
				input1PNameClass.InitSelectPosition();
			}
			// 表示の更新
			input1PNameDisplay.Character_Preference(input1PNameClass.Name);
		}
		// ランキングの中心に来るオブジェクトの補正
		if (centerElementNum1P < 2) { centerElementNum1P = 2; }
		else if (centerElementNum1P > Ranking_Strage.Max_num - 3) { centerElementNum1P = Ranking_Strage.Max_num - 3; }
		// ランキングのスクロール処理
		CorrectCenter1PRankingLine();
		// ランキングの列を中心に合わせる
		CorrectCenter1PRankingColumn();
		// 変更された名前を逐一保存していく
		if (Ranking_Strage.Strage_Data.Player1Rank < Ranking_Strage.Max_num)
		{
			Ranking_Strage.Strage[Ranking_Strage.Strage_Data.Player1Rank].name = input1PNameClass.Name;
			// 表示の変更
			string displayString = (Ranking_Strage.Strage_Data.Player1Rank + 1).ToString().PadLeft(2) + "___" + Ranking_Strage.Strage[Ranking_Strage.Strage_Data.Player1Rank].name + "__" + Ranking_Strage.Strage[Ranking_Strage.Strage_Data.Player1Rank].score.ToString("D10");
			ranking1PDisplay[Ranking_Strage.Strage_Data.Player1Rank].Character_Preference(displayString);
			if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eTWO_PLAYER)
			{
				ranking2PDisplay[Ranking_Strage.Strage_Data.Player1Rank].Character_Preference(displayString);
			}
			previous1PName = input1PNameClass.Name;
			ranking1PDisplay[Ranking_Strage.Strage_Data.Player1Rank].Set_Enable(blinkPlayerLineFrame < blinkPLayerLineInvisibleBorder);
		}
	}
	/// <summary>
	/// 2P画面更新
	/// </summary>
	void Update2PDisplay()
	{
		if (Ranking_Strage.Strage_Data.Player2Rank < Ranking_Strage.Max_num)
		{
			// 名前変更
			input2PNameClass.SelectingName();
			// ngワードであれば初めに戻す
			if (input2PNameClass.IsDecision && ngWordsList.Contains(input2PNameClass.Name))
			{
				input2PNameClass.InitSelectPosition();
			}
			// 表示の更新
			input2PNameDisplay.Character_Preference(input2PNameClass.Name);
		}
		// ランキングの中心に来るオブジェクトの補正
		if (centerElementNum2P < 2) { centerElementNum2P = 2; }
		else if (centerElementNum2P > Ranking_Strage.Max_num - 3) { centerElementNum2P = Ranking_Strage.Max_num - 3; }
		// ランキングのスクロール処理
		CorrectCenter2PRankingLine();
		// ランキングの列を中心に合わせる
		CorrectCenter2PRankingColumn();
		// 変更された名前を逐一保存していく
		if (Ranking_Strage.Strage_Data.Player2Rank < Ranking_Strage.Max_num)
		{
			Ranking_Strage.Strage[Ranking_Strage.Strage_Data.Player2Rank].name = input2PNameClass.Name;
			// 表示の変更
			string displayString = (Ranking_Strage.Strage_Data.Player2Rank + 1).ToString().PadLeft(2) + "___" + Ranking_Strage.Strage[Ranking_Strage.Strage_Data.Player2Rank].name + "__" + Ranking_Strage.Strage[Ranking_Strage.Strage_Data.Player2Rank].score.ToString("D10");
			ranking2PDisplay[Ranking_Strage.Strage_Data.Player2Rank].Character_Preference(displayString);
			ranking1PDisplay[Ranking_Strage.Strage_Data.Player2Rank].Character_Preference(displayString);
			previous2PName = input2PNameClass.Name;
			ranking2PDisplay[Ranking_Strage.Strage_Data.Player2Rank].Set_Enable(blinkPlayerLineFrame < blinkPLayerLineInvisibleBorder);
		}
	}
	/// <summary>
	/// 1P要素番号centerElementNumの要素行を1Pランキングの中心にオフセットする処理
	/// </summary>
	void CorrectCenter1PRankingLine()
	{
		int sign = rank1PPosition[centerElementNum1P].y > 0 ? -1 : 1;
		float correctValue = scroll1PValue * Time.deltaTime * sign;
		float temp = rank1PPosition[centerElementNum1P].y + correctValue;
		// yが0を通り過ぎたら、行き過ぎないようにする
		if (temp * sign > 0f)
		{
			correctValue -= (correctValue * sign - Mathf.Abs(rank1PPosition[centerElementNum1P].y)) * sign;
		}
		for (int i = 0; i < Ranking_Strage.Max_num; ++i)
		{
			rank1PPosition[i].y += correctValue;
			ranking1PDisplay[i].Position_Change(rank1PPosition[i]);
		}
	}
	/// <summary>
	/// 1Pランキングの列を中心にオフセットする処理
	/// </summary>
	void CorrectCenter1PRankingColumn()
	{
		float scrollWeight = 23.5f;
		for (int i = 0; i < Ranking_Strage.Max_num; ++i)
		{
			if (rank1PPosition[i].x > 0f)
			{
				rank1PPosition[i].x -= scroll1PValue * scrollWeight * Time.deltaTime;
				if(rank1PPosition[i].x < 0f) { rank1PPosition[i].x = 0f; }
			}
			else if (rank1PPosition[i].x < 0f)
			{
				rank1PPosition[i].x += scroll1PValue * scrollWeight * Time.deltaTime;
				if (rank1PPosition[i].x > 0f) { rank1PPosition[i].x = 0f; }
			}
			ranking1PDisplay[i].Position_Change(rank1PPosition[i]);
		}
	}
	/// <summary>
	/// 2P要素番号centerElementNumの要素行を2Pランキングの中心にオフセットする処理
	/// </summary>
	void CorrectCenter2PRankingLine()
	{
		int sign = rank2PPosition[centerElementNum2P].y > 0 ? -1 : 1;
		float correctValue = scroll2PValue * Time.deltaTime * sign;
		float temp = rank2PPosition[centerElementNum2P].y + correctValue;
		// yが0を通り過ぎたら、行き過ぎないようにする
		if (temp * sign > 0f)
		{
			correctValue -= (correctValue * sign - Mathf.Abs(rank2PPosition[centerElementNum2P].y)) * sign;
		}
		for (int i = 0; i < Ranking_Strage.Max_num; ++i)
		{
			rank2PPosition[i].y += correctValue;
			ranking2PDisplay[i].Position_Change(rank2PPosition[i]);
		}
	}
	/// <summary>
	/// 2Pランキングの列を中心にオフセットする処理
	/// </summary>
	void CorrectCenter2PRankingColumn()
	{
		float scrollWeight = 23.5f;
		for (int i = 0; i < Ranking_Strage.Max_num; ++i)
		{
			if (rank2PPosition[i].x > 0f)
			{
				rank2PPosition[i].x -= scroll2PValue * scrollWeight * Time.deltaTime;
				if (rank2PPosition[i].x < 0f) { rank2PPosition[i].x = 0f; }
			}
			else if (rank2PPosition[i].x < 0f)
			{
				rank2PPosition[i].x += scroll2PValue * scrollWeight * Time.deltaTime;
				if (rank2PPosition[i].x > 0f) { rank2PPosition[i].x = 0f; }
			}
			ranking2PDisplay[i].Position_Change(rank2PPosition[i]);
		}
	}
	/// <summary>
	/// 1Pのランキング画面セッティング
	/// </summary>
	void Setting1PRankingDisplay()
	{
		// Player1表示
		GameObject player1TextParent = new GameObject("player1Text");
		player1TextParent.transform.parent = transform;
		player1TextDisplay = new Character_Display("PLAYER_1".Length, "morooka/SS", player1TextParent, player1TextPosition);
		player1TextDisplay.Character_Preference("PLAYER_1");
		player1TextDisplay.Size_Change(Vector2.one / kWholeScaleWeight);
		player1TextDisplay.Centering();

		// 名前入力表示
		if (Ranking_Strage.Strage_Data.Player1Rank < Ranking_Strage.Max_num)
		{
			input1PNameClass.Init("Vertical", "Fire1", KeyCode.Space, "Fire2", KeyCode.X, Ranking_Strage.kDefaultName);
			GameObject inputNameParent = new GameObject("InputName");
			inputNameParent.transform.parent = transform;
			input1PNamePos.x = -3840f / 2f / 2f + 150f * 3f;
			input1PNamePos.y = -1080f / 2f / 7f * 5f;
			inputNameSize = 1.2f;
			input1PNameDisplay = new Character_Display(input1PNameClass.Name.Length, "morooka/SS", inputNameParent, input1PNamePos);
			input1PNameDisplay.Character_Preference(input1PNameClass.Name);
			input1PNameClass.NameImageList = input1PNameDisplay.Display_Characters;
			input1PNameDisplay.Size_Change(Vector3.one * inputNameSize / kWholeScaleWeight);
			input1PNameDisplay.Centering();
			previous1PName = Ranking_Strage.kDefaultName;
			// 添え字表示
			GameObject nameSubscriptTextParent = new GameObject("Name");
			nameSubscriptTextParent.transform.parent = transform;
			name1PSubscriptTextDisplay = new Character_Display("YOUR_NAME".Length, "morooka/SS", nameSubscriptTextParent, name1PSubscriptTextPosition);
			name1PSubscriptTextDisplay.Character_Preference("YOUR_NAME");
			name1PSubscriptTextDisplay.Size_Change(Vector2.one * 0.8f / kWholeScaleWeight);
			name1PSubscriptTextDisplay.Centering();
		}

		// ランキング表示
		dataArray = Ranking_Strage.Strage;

		rank1PParent = new GameObject[Ranking_Strage.Max_num];
		ranking1PDisplay = new Character_Display[Ranking_Strage.Max_num];
		rank1PPosition = new Vector3[Ranking_Strage.Max_num];

		float y_pos = (-150f / 2f * 2f + 150f / 2f * 5f) / kWholeScaleWeight * (Screen.height / kScreenHeight);
		fontSize = 0.8f;

		GameObject maskObject = new GameObject("RankingMask");
		maskObject.transform.parent = transform;
		RectMask2D maskObjectRectMask = maskObject.AddComponent<RectMask2D>();
		maskObjectRectMask.rectTransform.localPosition = new Vector3(-kScreenWidth / 2f / 2f, 0f);
		maskObjectRectMask.rectTransform.sizeDelta = new Vector2(80f * 21f * (Screen.width / kScreenWidth), 90f * 7f * (Screen.height / kScreenHeight)) / kWholeScaleWeight;

		for (int i = Ranking_Strage.Max_num - 1; i >= 0; --i)
		{
			rank1PPosition[i].y = y_pos;
			rank1PPosition[i].x = (-200f * 10f - 2100f * (Ranking_Strage.Max_num - 1 - i)) / kWholeScaleWeight * (Screen.width / kScreenWidth);
			y_pos += 180.0f / 2.0f / kWholeScaleWeight * (Screen.height / kScreenHeight);

			int ranking_num = i + 1;
			string s_temp = ranking_num.ToString().PadLeft(2) + "___" + dataArray[i].name + "__" + dataArray[i].score.ToString("D10");
			rank1PParent[i] = new GameObject();
			rank1PParent[i].transform.parent = maskObject.transform;
			rank1PParent[i].name = "Rank" + ranking_num.ToString();
			ranking1PDisplay[i] = new Character_Display(s_temp.Length, "morooka/SS", rank1PParent[i], rank1PPosition[i]);
			ranking1PDisplay[i].Character_Preference(s_temp);
			ranking1PDisplay[i].Size_Change(Vector2.one * fontSize / kWholeScaleWeight * (Screen.width / kScreenWidth));
			ranking1PDisplay[i].Centering();
		}
		if (Ranking_Strage.Strage_Data.Player1Rank < Ranking_Strage.Max_num)
		{
			centerElementNum1P = Ranking_Strage.Strage_Data.Player1Rank;
		}
		else
		{
			centerElementNum1P = 0;
		}
	}
	/// <summary>
	/// 2Pのランキング画面セッティング
	/// </summary>
	void Setting2PRankingDisplay()
	{
		// Player2表示
		GameObject player2TextParent = new GameObject("player2Text");
		player2TextParent.transform.parent = transform;
		player2TextDisplay = new Character_Display("PLAYER_2".Length, "morooka/SS", player2TextParent, player2TextPosition);
		player2TextDisplay.Character_Preference("PLAYER_2");
		player2TextDisplay.Size_Change(Vector2.one / kWholeScaleWeight);
		player2TextDisplay.Centering();

		// 名前入力表示
		if (Ranking_Strage.Strage_Data.Player2Rank < Ranking_Strage.Max_num)
		{
			input2PNameClass.Init("P2_Vertical", "P2_Fire1", KeyCode.Space, "P2_Fire2", KeyCode.X, Ranking_Strage.kDefaultName);
			GameObject inputNameParent = new GameObject("InputName");
			inputNameParent.transform.parent = transform;
			input2PNamePos.x = 3840f / 2f / 2f + 150f * 3f;
			input2PNamePos.y = -1080f / 2f / 7f * 5f;
			inputNameSize = 1.2f;
			input2PNameDisplay = new Character_Display(input2PNameClass.Name.Length, "morooka/SS", inputNameParent, input2PNamePos);
			input2PNameDisplay.Character_Preference(input2PNameClass.Name);
			input2PNameClass.NameImageList = input2PNameDisplay.Display_Characters;
			input2PNameDisplay.Size_Change(Vector3.one * inputNameSize / kWholeScaleWeight);
			input2PNameDisplay.Centering();
			previous2PName = Ranking_Strage.kDefaultName;
			// 添え字表示
			GameObject nameSubscriptTextParent = new GameObject("Name");
			nameSubscriptTextParent.transform.parent = transform;
			name2PSubscriptTextDisplay = new Character_Display("YOUR_NAME".Length, "morooka/SS", nameSubscriptTextParent, name2PSubscriptTextPosition);
			name2PSubscriptTextDisplay.Character_Preference("YOUR_NAME");
			name2PSubscriptTextDisplay.Size_Change(Vector2.one * 0.8f / kWholeScaleWeight);
			name2PSubscriptTextDisplay.Centering();
		}

		// ランキング表示
		dataArray = Ranking_Strage.Strage;

		rank2PParent = new GameObject[Ranking_Strage.Max_num];
		ranking2PDisplay = new Character_Display[Ranking_Strage.Max_num];
		rank2PPosition = new Vector3[Ranking_Strage.Max_num];

		float y_pos = (-150f / 2f * 2f + 150f / 2f * 5f) / kWholeScaleWeight * (Screen.height / kScreenHeight);
		fontSize = 0.8f;

		GameObject maskObject = new GameObject("RankingMask");
		maskObject.transform.parent = transform;
		RectMask2D maskObjectRectMask = maskObject.AddComponent<RectMask2D>();
		maskObjectRectMask.rectTransform.localPosition = new Vector3(kScreenWidth / 2f / 2f, 0f);
		maskObjectRectMask.rectTransform.sizeDelta = new Vector2(80f * 21f * (Screen.width / kScreenWidth), 90f * 7f * (Screen.height / kScreenHeight)) / kWholeScaleWeight;

		for (int i = Ranking_Strage.Max_num - 1; i >= 0; --i)
		{
			rank2PPosition[i].y = y_pos;
			rank2PPosition[i].x = (200f * 10f + 2100f * (Ranking_Strage.Max_num - 1 - i)) / kWholeScaleWeight * (Screen.width / kScreenWidth);
			y_pos += 180.0f / 2.0f / kWholeScaleWeight * (Screen.height / kScreenHeight);

			int ranking_num = i + 1;
			string s_temp = ranking_num.ToString().PadLeft(2) + "___" + dataArray[i].name + "__" + dataArray[i].score.ToString("D10");
			rank2PParent[i] = new GameObject();
			rank2PParent[i].transform.parent = maskObject.transform;
			rank2PParent[i].name = "Rank" + ranking_num.ToString();
			ranking2PDisplay[i] = new Character_Display(s_temp.Length, "morooka/SS", rank2PParent[i], rank2PPosition[i]);
			ranking2PDisplay[i].Character_Preference(s_temp);
			ranking2PDisplay[i].Size_Change(Vector2.one * fontSize / kWholeScaleWeight * (Screen.width / kScreenWidth));
			ranking2PDisplay[i].Centering();
		}
		if (Ranking_Strage.Strage_Data.Player2Rank < Ranking_Strage.Max_num)
		{
			centerElementNum2P = Ranking_Strage.Strage_Data.Player2Rank;
		}
		else
		{
			centerElementNum2P = 0;
		}
	}
}
