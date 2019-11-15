//----------------------------------------------------------------------------------------------
// 制作者：諸岡勇樹
// 制作日：2019/05/16
//----------------------------------------------------------------------------------------------
// ゲーム全体の管理
//----------------------------------------------------------------------------------------------
// 2019/04/20：フレーム数の格納、計算
// 2019/05/16：ボスのデータベース全ての格納
// 2019/05/24：ゲーム中の切り替え
// 2019/05/30：ボスのアニメーション許可
// 2019/08/01　プレイヤー人数保存、設定
//----------------------------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.SceneManagement;
using CSV_Management;

public class Game_Master : MonoBehaviour
{
	/// <summary>
	/// 人数管理用のイナム
	/// </summary>
	public enum PLAYER_NUM
	{
		eUNDECIDED,     //未設定
		eONE_PLAYER,        //一人
		eTWO_PLAYER,        //二人
	}

	/// <summary>
	/// ボスのデータベースにあるレコード管理
	/// </summary>
	public enum BOSS_DATA_ELEMENTS
    {
        eID,                    // BossのID
        eNAME,                  // Boss の名前
        eSCRIPT,                // 各 Boss 個別のスクリプト
        eATTACK_INTERVAL,       // 攻撃と攻撃のインターバル
        eACT_CHANGE,            // 攻撃種類の切り替えインターバル
        ePARTS_HP,              // Boos のパーツのHP
        eSCORE,                 // Boss の持つHP
        eBULLET_NAME_1,         // 攻撃パターン1
        eBULLET_NAME_2,         // 攻撃パターン2
        eBULLET_NAME_3,         // 攻撃パターン3
        eBULLET_NAME_4,         // 攻撃パターン4
        eBULLET_NAME_5,         // 攻撃パターン5
        eEFFECT                 // エフェクト
    }

    /// <summary>
    /// エネミーのデータベースにあるレコード管理
    /// </summary>
    public enum ENEMY_DATA_ELEMENTS
    {
        eID,                    // Enemy のID
        eNAME,                  // Enemy の名前
        eHP,                    // HP
        eATTACK_INTERVAL,		// 攻撃と攻撃のインターバル
        eBULLET,                // 弾
        eEFFECT					// エフェクト
    }

	///// <summary>
	///// ステージ内のセグメント管理
	///// </summary>
	public enum CONFIGURATION_IN_STAGE
	{
		eNORMAL,		//通常
		//ePOSE,			//ポーズ画面
		WIRELESS,	   //無線が入ってる
		//eCLEAR,
	}

	/// <summary>
	/// 生成オブジェクトの指定用
	/// </summary>
	public enum OBJECT_NAME
	{
		ePLAYER_BULLET,		// プレイヤーのバレット 
        //新規で追加したもの---------------------
        ePLAYER2_BULLET,
        eP1_OPTION_BULLET,
		eP2_OPTION_BULLET,
		//------------------------------------
		ePLAYER_MISSILE,	// プレイヤーのミサイル
		ePLAYER_LASER,		// プレイヤーのレーザー
		ePLAYER_TowWay,		// プレイヤーの2ウェイミサイル
		ePOWERUP_ITEM,		//パワーアップアイテム
		eENEMY_BULLET,		// エネミーのバレット
		eENEMY_BEAM,			// エネミーのビーム
		eENEMY_LASER,			// エネミーのレーザー
		eONE_BOSS_LASER,		// 1ボスレーザー
		eONE_BOSS_BOUND,		// 1ボス玉
		eTWO_BOSS_LASER,		// 2ボスレーザー
		eBATTLESHIP_ENEMY_PREFAB,     // 戦艦型エネミーのプレハブ
		/////////////////////////////////////////////////////////////////
		ePLAYER,										// プレイヤー
		eENEMY_NUM1,								// エネミー1番
		eUFOTYPE_ENEMY,                         // UFOタイプエネミー
		eUFOTYPE_ENEMY_ITEM,                // UFOタイプエネミーのアイテム落とす版
		eUFOMOTHERTYPE_ENEMY,				// UFO母艦タイプエネミー
		eBEELZEBUBTYPE_ENEMY,				// ハエ型エネミー
		eCLAMCHOWDERTYPE_ENEMY,				// 貝型エネミー
		eOCTOPUSTYPE_ENEMY,					// タコ型エネミー
		eMANTA_LASER,						//マンタ型のエネミー用レーザー
	}

	public uint Frame_Count{private set; get;}                  // ゲームが開始してからの時間をカウント
    public static Game_Master MY{get; private set;}             // 自分の情報
    public static uint display_score_1P{private set; get;}                // 表示スコア
    public static uint display_score_2P{private set; get;}                // 表示スコア
    public Record Boss_Data{private set; get;}        // ボスのデータベース
    public Record Enemy_Data{private set; get;}       // エネミーのデータベース
    public static CONFIGURATION_IN_STAGE Management_In_Stage{set; get;}// ステージ内管理
    public Score_Display _Display{private set; get;}			// スコア表示をするため用
	public bool Is_Completed_For_Warning_Animation { set; get; }							// WARNING アニメーションの終了用
	public string[] Name_List {  get; private set; }
	public static PLAYER_NUM Number_Of_People { get; private set; }             // 設定保存

	public static int[] Is_Player_Alive { get; set; }              //プレイヤーが死んでいるかどうかの判定用
	private One_Boss One_Bossinfo;      //前半ボスの情報
	private Two_Boss Two_Bossinfo;      //後半ボスの情報
	private Enemy_MiddleBoss Middle_Bossinfo;   //ビックコアの情報
	private Enemy_Moai Moai_Bossinfo;			//モアイの情報

    private void Awake()
	{
		if (MY == null)
		{
			MY = GetComponent<Game_Master>();
		}
	}

	void Start()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Title":
				display_score_1P = 0;
				display_score_2P = 0;
				Number_Of_People = PLAYER_NUM.eONE_PLAYER;
				break;
            case "Stage_01":
                Stage_Start();
                break;
            case "GameOver":
                break;
            case "GameClear":
                break;
        }
    }

    void Update()
    {
        Frame_Count++;
    }

	/// <summary>
	/// 取得スコア
	/// </summary>
	/// <param name="addition"> 加算数 </param>
	public void Score_Addition(uint addition, int Player_Num)
	{
		if (Player_Num == 1)
		{
			display_score_1P += addition;
			_Display.Display_Number_Preference_1P(display_score_1P);
		}
		else if(Player_Num == 2)
		{
			display_score_2P += addition;
			_Display.Display_Number_Preference_2P(display_score_2P);
		}
	}

    /// <summary>
    /// ステージシーンのスタート
    /// </summary>
    private void Stage_Start()
    {
        _Display = GameObject.Find("Score_Display").GetComponent<Score_Display>();
		Is_Completed_For_Warning_Animation = false;

		if(Number_Of_People == PLAYER_NUM.eUNDECIDED)
		{
			Number_Of_People = PLAYER_NUM.eONE_PLAYER;
		}
    }

	/// <summary>
	/// プレイヤー人数設定
	/// </summary>
	/// <param name="set_num"> 設定人数 </param>
	/// <returns> 設定された人数 </returns>
	public PLAYER_NUM Number_Of_Players_Confirmed(PLAYER_NUM set_num)
	{
		Number_Of_People = set_num;
		return Number_Of_People;
	}

	/// <summary>
	/// ゲームオーバーに行く前にボタンを押せば
	/// 復活ができるように
	/// </summary>
	public void Game_Continue()
	{
		var player1 = Obj_Storage.Storage_Data.GetPlayer();
		if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eONE_PLAYER)
		{
			player1.SetActive(true);
			player1.GetComponent<Player1>().ResponPreparation(5);
		}
		else
		{
			var player2 = Obj_Storage.Storage_Data.GetPlayer2();
			player2.SetActive(true);
			player2.GetComponent<Player2>().ResponPreparation(5);
			player1.SetActive(true);
			player1.GetComponent<Player1>().ResponPreparation(5);
		}
	}
}
