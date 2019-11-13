/*
 * オブジェクトやステータス情報などを保管しておく場所
 * 久保田達己
 * 更新履歴
 * 2019/06/06	とりあえずの作成
 * 2019/07/03	SEの追加
 * 2019/07/12	VOICEの追加
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Obj_Storage : MonoBehaviour
{
	public static Obj_Storage Storage_Data;

	//マップ作製に使うプレハブ
	//リソースフォルダから取得するため、インスペクターは使わない
	private GameObject Player_Prefab;                           //プレイヤーのプレハブ
	private GameObject player_2_Prefab;                         //プレイヤー2のプレハブ
	private GameObject Player_Missile_Prefab;                   //プレイヤーのミサイルプレハブ
	private GameObject Boss1_Prefab;                                //ステージ１のボスのプレハブ
	private GameObject Boss2_Prefab;                                //ステージ2のボスのプレハブ
	private GameObject Bullet_Prefab_P;                         //弾のPrefab情報
	private GameObject BulletPrefab_P2;            //２P用の弾プレハブ情報
	private GameObject BulletPrefab_Option_P1;         //オプション用の球プレハブ情報１P用
	private GameObject BulletPrefab_Option_P2;          //オプション用の弾プレハブ２P用
	private GameObject Bullet_Prefab_E;                         //エネミーの弾のPrefab情報
	private GameObject Bullet_Prefab_BattleShip;        // バトルシップタイプの弾のPrefab情報
	private GameObject Beam_Bullet_E_Prefab;                    //エネミーのビーム型バレットのプレハブ
    private GameObject SmallBeam_Bullet_E_Prefab;               //エネミーの小さいビーム型バレットのプレハブ
    private GameObject UfoType_Enemy_Prefab;                // UFO型エネミーのプレハブ
	private GameObject ClamChowderType_Enemy_Prefab;    // 貝型エネミーのプレハブ
	private GameObject OctopusType_Enemy_Prefab;            // タコ型エネミーのプレハブ
	private GameObject BeelzebubType_Enemy_Prefab;      // ハエ型エネミーのプレハブ
	private GameObject BattleShip_Enemy_Prefab;     // 戦艦型エネミーのプレハブ
	private GameObject Star_Fish_Enemy_Prefab;          //ヒトデ型のエネミーのプレハブ
	private GameObject P1_Option_Prefab;                            //オプションのプレハブ
	private GameObject P2_Option_Prefab;                        //2P用のオプションのプレハブ
	private GameObject Item_Prefab;                             //パワーアップのアイテムを入れえるための処理

	//-----------------------------------------------------------------------------------
	private GameObject[] Effects_Prefab = new GameObject[17];  //particleのプレハブ
	//---------------------------------------------------------------------------------
	private GameObject Boss_Middle_Prefab;                      //中ボスのプレハブ
	private GameObject Laser_Line_Prefab;               // レーザーのプレハブ
	private GameObject One_Boss_Laser_Prefab;                   // ボス1のレーザープレハブ
	private GameObject One_Boss_BousndBullet_Prefab;        // ボス1のバウンド弾プレハブ
	private GameObject Two_Boss_Laser_Prefab;                   //ボス２のレーザープレハブ
	private GameObject Moai_Prefab;             //モアイのプレハブ
	private GameObject Moai_Mini_Group_Prefab;              //小さいモアイグループのプレハブ
	private GameObject Moai_Bullet_Prefab;                  //モアイのバレットのプレハブ
	private GameObject Moai_Eye_Laser_Prefab;       //モアイの目のレーザーのプレハブ
	private GameObject Moai_Mouth_Laser_Prefab;     //モアイの口のレーザーのプレハブ

	//実際に作られたオブジェクト
	public Object_Pooling Enemy1;
	public Object_Pooling Medium_Size_Enemy1;
	public Object_Pooling Player;
	public Object_Pooling Player_2;
	public Object_Pooling Boss_1;
	public Object_Pooling Boss_2;
	public Object_Pooling PlayerBullet;
	public Object_Pooling Player2Bullet;
	public Object_Pooling P1_OptionBullet;
	public Object_Pooling P2_OptionBullet;
	public Object_Pooling PlayerMissile;
	public Object_Pooling PlayerMissile_TowWay;
	public Object_Pooling EnemyBullet;
	public Object_Pooling BattleShipBullet;
	public Object_Pooling Beam_Bullet_E;
    public Object_Pooling SmallBeam_Bullet_E;
    public Object_Pooling UfoType_Enemy;
	public Object_Pooling ClamChowderType_Enemy;
	public Object_Pooling OctopusType_Enemy;
	public Object_Pooling BeelzebubType_Enemy;
	public Object_Pooling BattleShipType_Enemy;
	public Object_Pooling StarFish_Enemy;
	public Object_Pooling P1_Option;
	public Object_Pooling P2_Option;
	public Object_Pooling PowerUP_Item;
	public Object_Pooling Boss_Middle;
	public Object_Pooling Laser_Line;
	public Object_Pooling One_Boss_Laser;
	public Object_Pooling One_Boss_BousndBullet;
	public Object_Pooling Two_Boss_Laser;                       //２ボスのレーザー、プーリング
	public Object_Pooling Moai;                       //モアイ
	public Object_Pooling Moai_Mini_Group;                       //小さいモアイの群れ
	public Object_Pooling Moai_Bullet;                       //モアイの弾
	public Object_Pooling Moai_Eye_Laser;                       //モアイの目のレーザー
	public Object_Pooling Moai_Mouth_Laser;                       //モアイの口のレーザー
																  //effect関係-----------------------------------------------------
	public Object_Pooling[] Effects = new Object_Pooling[17];
	//マップの作製時に使う処理
	public Vector3 pos;                                        //マップを作成するときの位置情報取得用
	private string File_name = "E_Pattern";                     //csvファイルの名前
	private string File_name2 = "E_Pattern2";
	public List<string[]> CsvData = new List<string[]>();      //csvファイルの中身を入れる変数
	private int column;                                         //配列の列を入れる変数

	public AudioClip[] audio_se = new AudioClip[29];    //ＳＥを読み込むための配列
	public AudioClip[] audio_voice = new AudioClip[26]; //VOICEを読み込むための配列


    //仮データ置き場（のちにプーリング化を施す）-------------------------------------------------------------
    public GameObject enemy_UFO_Group_prefab;
    public GameObject enemy_UFO_Group_NoneShot_prefab;
    public GameObject enemy_ClamChowder_Group_Two_Top_prefab;
    public GameObject enemy_ClamChowder_Group_Two_Under_prefab;
    public GameObject enemy_ClamChowder_Group_TwoWaveOnlyUp_prefab;
    public GameObject enemy_ClamChowder_Group_TwoWaveOnlyDown_prefab;
    public GameObject enemy_ClamChowder_Group_Three_prefab;
    public GameObject enemy_ClamChowder_Group_Three_Item_prefab;
    public GameObject enemy_ClamChowder_Group_ThreeWaveOnlyUp_prefab;
    public GameObject enemy_ClamChowder_Group_ThreeWaveOnlyDown_prefab;
    public GameObject enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item_prefab;
    public GameObject enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item_prefab;
    public GameObject enemy_ClamChowder_Group_Four_prefab;
    public GameObject enemy_ClamChowder_Group_Four_NoItem_prefab;
    public GameObject enemy_ClamChowder_Group_Five_prefab;
    public GameObject enemy_ClamChowder_Group_Five_NoItem_prefab;
    public GameObject enemy_ClamChowder_Group_Seven_prefab;
    //public GameObject enemy_MiddleBoss_Father_prefab;
    public GameObject enemy_ClamChowder_Group_Straight_prefab;
    public GameObject enemy_Beelzebub_Group_FourWide_prefab;
    public GameObject enemy_Beelzebub_Group_FourWide_Item_prefab;
    public GameObject enemy_BeetleGroup_prefab;
    public GameObject enemy_BeetleGroup_Three_prefab;
    public GameObject boundMeteors_prefab;
    public GameObject enemy_Bacula_Sixteen_prefab;
    public GameObject enemy_Bacula_FourOnly_prefab;
    //9月13日追加
    public GameObject enemy_ClamChowder_FourTriangle_prefab;
    public GameObject enemy_ClamChowder_FourTriangle_NoItem_prefab;
    public GameObject enemy_Beelzebub_Group_EightNormal_Item_prefab;
    public GameObject enemy_ClamChowder_Group_TwelveStraight_prefab;
    public GameObject enemy_UFO_Group_Five_prefab;
    public GameObject enemy_Beetle_Group_Seven_prefab;
    public GameObject enemy_ClamChowder_Group_SevenStraight_prefab;
    public GameObject enemy_ClamChowder_Group_SixStraight_prefab;
    public GameObject enemy_ClamChowder_Group_UpSevenDiagonal_prefab;
    public GameObject enemy_ClamChowder_Group_DownSevenDiagonal_prefab;
    public GameObject enemy_ClamChowder_Group_TenStraight_prefab;

    public Object_Pooling enemy_UFO_Group;
    public Object_Pooling enemy_UFO_Group_NoneShot;
    public Object_Pooling enemy_ClamChowder_Group_Two_Top;
    public Object_Pooling enemy_ClamChowder_Group_Two_Under;
    public Object_Pooling enemy_ClamChowder_Group_TwoWaveOnlyUp;
    public Object_Pooling enemy_ClamChowder_Group_TwoWaveOnlyDown;
    public Object_Pooling enemy_ClamChowder_Group_Three;
    public Object_Pooling enemy_ClamChowder_Group_Three_Item;
    public Object_Pooling enemy_ClamChowder_Group_ThreeWaveOnlyUp;
    public Object_Pooling enemy_ClamChowder_Group_ThreeWaveOnlyDown;
    public Object_Pooling enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item;
    public Object_Pooling enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item;
    public Object_Pooling enemy_ClamChowder_Group_Four;
    public Object_Pooling enemy_ClamChowder_Group_Four_NoItem;
    public Object_Pooling enemy_ClamChowder_Group_Five;
    public Object_Pooling enemy_ClamChowder_Group_Five_NoItem;
    public Object_Pooling enemy_ClamChowder_Group_Seven;
    //public Object_Pooling enemy_MiddleBoss_Father;
    public Object_Pooling enemy_ClamChowder_Group_Straight;
    public Object_Pooling enemy_Beelzebub_Group_FourWide;
    public Object_Pooling enemy_Beelzebub_Group_FourWide_Item;
    public Object_Pooling enemy_BeetleGroup;
    public Object_Pooling enemy_BeetleGroup_Three;
    public Object_Pooling boundMeteors;
    public Object_Pooling enemy_Bacula_Sixteen;
    public Object_Pooling enemy_Bacula_FourOnly;
    //9月13日追加
    public Object_Pooling enemy_ClamChowder_FourTriangle;
    public Object_Pooling enemy_ClamChowder_FourTriangle_NoItem;
    public Object_Pooling enemy_Beelzebub_Group_EightNormal_Item;
    public Object_Pooling enemy_ClamChowder_Group_TwelveStraight;
    public Object_Pooling enemy_UFO_Group_Five;
    public Object_Pooling enemy_Beetle_Group_Seven;
    public Object_Pooling enemy_ClamChowder_Group_SevenStraight;
    public Object_Pooling enemy_ClamChowder_Group_SixStraight;
    public Object_Pooling enemy_ClamChowder_Group_UpSevenDiagonal;
    public Object_Pooling enemy_ClamChowder_Group_DownSevenDiagonal;
    public Object_Pooling enemy_ClamChowder_Group_TenStraight;



    //----------------------------------------------------------
    private void Awake()
	{
		Storage_Data = GetComponent<Obj_Storage>();
	}

	void Start()
	{

		Player_Prefab = Resources.Load("Player/Player") as GameObject;
		player_2_Prefab = Resources.Load("Player/Player2") as GameObject;
		Boss1_Prefab = Resources.Load("Boss/BigCoreMk2") as GameObject;
		Boss2_Prefab = Resources.Load("Boss/bick_core_mk3") as GameObject;
		Bullet_Prefab_P = Resources.Load("Bullet/Player_Bullet_1P") as GameObject;
		BulletPrefab_P2 = Resources.Load("Bullet/Player_Bullet_2P") as GameObject;
		BulletPrefab_Option_P1 = Resources.Load("Bullet/Option_Bullet_1P") as GameObject;
		BulletPrefab_Option_P2 = Resources.Load("Bullet/Option_Bullet_2P") as GameObject;
		Player_Missile_Prefab = Resources.Load("Bullet/Player_Missile") as GameObject;
		Bullet_Prefab_E = Resources.Load("Bullet/Enemy_Bullet") as GameObject;
		Bullet_Prefab_BattleShip = Resources.Load("Bullet/CannonBullet") as GameObject;
		Beam_Bullet_E_Prefab = Resources.Load("Bullet/Beam_Bullet") as GameObject;
        SmallBeam_Bullet_E_Prefab= Resources.Load("Bullet/SmallBeam_Bullet") as GameObject;
        UfoType_Enemy_Prefab = Resources.Load("Enemy/Enemy_UFO") as GameObject;
		ClamChowderType_Enemy_Prefab = Resources.Load("Enemy/ClamChowderType_Enemy") as GameObject;
		OctopusType_Enemy_Prefab = Resources.Load("Enemy/OctopusType_Enemy") as GameObject; ;
		BeelzebubType_Enemy_Prefab = Resources.Load("Enemy/BeelzebubType_Enemy") as GameObject;
		BattleShip_Enemy_Prefab = Resources.Load("Enemy/BattleshipType_Enemy") as GameObject;
		Star_Fish_Enemy_Prefab = Resources.Load("Enemy/Enemy_hitode_type") as GameObject;       //ヒトデ型の敵のロード
		P1_Option_Prefab = Resources.Load("Option/Option") as GameObject;       //1Pオプションのロード
		P2_Option_Prefab = Resources.Load("Option/Option_2P") as GameObject;       //2Pオプションのロード

		Item_Prefab = Resources.Load("Item/Item_Test") as GameObject;        //アイテムのロード
		Boss_Middle_Prefab = Resources.Load("Enemy/Enemy_MiddleBoss_Father") as GameObject;		//中ボス
		Laser_Line_Prefab = Resources.Load("Bullet/LaserLine") as GameObject;
		One_Boss_Laser_Prefab = Resources.Load("Bullet/One_Boss_LaserLine") as GameObject;
		One_Boss_BousndBullet_Prefab = Resources.Load("Bullet/One_Boss_BousndBullet") as GameObject;
		Two_Boss_Laser_Prefab = Resources.Load("Bullet/Two_Boss_Laser") as GameObject;       //２ボスレーザー用のプレハブ修正

		Moai_Prefab = Resources.Load("Boss/Enemy_Moai") as GameObject;          //モアイのロード
		Moai_Mini_Group_Prefab = Resources.Load("Enemy/Enemy_Moai_MiniGroup") as GameObject;          //モアイの群れのロード
		Moai_Bullet_Prefab = Resources.Load("Bullet/Enemy_RingBullet") as GameObject;          //モアイの弾ロード
		Moai_Eye_Laser_Prefab = Resources.Load("Bullet/Moai_EyeLaser") as GameObject;          //モアイの目の光線ロード
		Moai_Mouth_Laser_Prefab = Resources.Load("Bullet/Moai_MouthLaser") as GameObject;          //モアイの口からのビームロード

		Effects_Prefab[0] = Resources.Load<GameObject>("Effects/Explosion/E001_1P");    //プレイヤー爆発
		Effects_Prefab[1] = Resources.Load<GameObject>("Effects/Attachment/A000");      //プレイヤー登場時に使用するジェット噴射
		Effects_Prefab[2] = Resources.Load<GameObject>("Effects/Attachment/A002");      //プレイヤーのマズルファイア
		Effects_Prefab[3] = Resources.Load<GameObject>("Effects/Attachment/A006");      //オプション回収用
		Effects_Prefab[4] = Resources.Load<GameObject>("Effects/Explosion/E100");       //敵キャラの爆発エフェクト
		Effects_Prefab[5] = Resources.Load<GameObject>("Effects/Explosion/E201");       //敵キャラコアシールドの破壊エフェクト
		Effects_Prefab[6] = Resources.Load<GameObject>("Effects/Attachment/A003");      //プレイヤーパワーアップエフェクト
		Effects_Prefab[7] = Resources.Load<GameObject>("Effects/Explosion/E104");       //中ボス爆発
		Effects_Prefab[8] = Resources.Load<GameObject>("Effects/Explosion/E001");       //バグが起きないようにプレイヤーの爆発を仮置き
		Effects_Prefab[9] = Resources.Load<GameObject>("Effects/Explosion/E011");      //使ってない（仮にはいってるだけ）
		Effects_Prefab[10] = Resources.Load<GameObject>("Effects/Explosion/E103");      //戦艦型の爆発
		Effects_Prefab[11] = Resources.Load<GameObject>("Effects/Explosion/E200");      //プレイヤーの弾の着弾時のエフェクト
		Effects_Prefab[12] = Resources.Load<GameObject>("Effects/Other/O001");          //ボス登場時のエフェクト
		Effects_Prefab[13] = Resources.Load<GameObject>("Effects/Explosion/E206");      //隕石の爆発Effect
		Effects_Prefab[14] = Resources.Load<GameObject>("Effects/Other/O005");      //ヒトデ型の出現用
		Effects_Prefab[15] = Resources.Load<GameObject>("Effects/Attachment/A003_2P");      //2Pパワーアップエフェクト
		Effects_Prefab[16] = Resources.Load<GameObject>("Effects/Explosion/E011");      //ミサイルの爆発

		audio_se[0] = Resources.Load<AudioClip>("Sound/Teacher_SE/bacura_hit");             //バキュラに当たった時の高い音用
		audio_se[1] = Resources.Load<AudioClip>("Sound/SE/gradius_SE_Explosion_Small2");    //隕石の爆発音
		audio_se[2] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_Player_BulletMode_Change");   //ラピッドとバーストの切り替え
		audio_se[3] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_Explosion_Scream");               //叫ぶ声
		audio_se[4] = Resources.Load<AudioClip>("Sound/SE/gradius_SE_Player_Bullet");       //プレイヤーバレット音
		audio_se[5] = Resources.Load<AudioClip>("Sound/Teacher_SE/menesius_cupcel5 t");     //アイテム取得音(バー移動)
		audio_se[6] = Resources.Load<AudioClip>("Sound/Teacher_SE/manesius_manesius_kettei");   //アイテム使用パワーアップ音(ステータス変化)
		audio_se[7] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_Bullet_Hit");         //コアシールドヒット音
		audio_se[8] = Resources.Load<AudioClip>("Sound/SE/gradius_SE_Explosion_Small2");    //敵の爆発音
		audio_se[9] = Resources.Load<AudioClip>("Sound/SE/gradius_SE_Explosion_Moderate");  //ボスの爆発音
		//装備セレクトで使用するもの------------------------------------------------------
		audio_se[10] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_OptionCatch");           //ドロップしたオプションの回収
		audio_se[11] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_Explosion_5(Moyai)");           //モアイの爆発
		audio_se[12] = Resources.Load<AudioClip>("Sound/SE/manesius_manesius_kettei_neo");          //ダブルの声使ってない
		audio_se[13] = Resources.Load<AudioClip>("Sound/SE/manesius_manesius_kettei_neo");     //リップルレーザーの声使ってない
		audio_se[14] = Resources.Load<AudioClip>("Sound/SE/manesius_manesius_kettei_neo");          //オプションの声使ってない
		audio_se[15] = Resources.Load<AudioClip>("Sound/SE/manesius_manesius_kettei_neo");     //フォースフィールド（シールド）使ってない
		audio_se[16] = Resources.Load<AudioClip>("Sound/Teacher_SE/manesius_manesius_kettei_neo");          //パワーアップの音(使用しない)
		audio_se[17] = Resources.Load<AudioClip>("Sound/Teacher_SE/gradius_SE_Player_Laser");   //レーザーの発射音
		audio_se[18] = Resources.Load<AudioClip>("Sound/SE/gradius_SE_Explosion_1(Small)");     //小型爆発
		audio_se[19] = Resources.Load<AudioClip>("Sound/SE/gradius_SE_Explosion_2(senkan)");    //戦艦タイプの爆発音
		audio_se[20] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_Self_destruction");      //プレイヤーの死亡時の音
		audio_se[21] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_Player_Flight");         //プレイヤー登場の音
		audio_se[22] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_Explosion_3(ModerateBoss)"); //中ボス用の爆発音
		audio_se[23] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_Subtitles_Display");         //無線受信時
		audio_se[24] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_Subtitles_Display_Parmanent");   //無線のボイスの裏で流すよう
		audio_se[25] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_Subtitles_Display_Close");   //無線終了時
		audio_se[26] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_BOSS_No_Hit");               //ボスのボディにあたった
		audio_se[27] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_CoreShield_destruction");    //ボスのコアシールドを壊したとき
		audio_se[28] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_BOSS_Core_Hit");             //ボスのコアに当たった時


		//------------------------------------------------------------------------------
		audio_voice[0] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_Scenario_001");        //開戦時
		audio_voice[1] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_Scenario_002");        //前半ボス前
		audio_voice[2] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_Scenario_003");        //前半ボス後1
		audio_voice[3] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_Scenario_004");        //前半ぼす後2
		audio_voice[4] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_Scenario_005");        //後半ボス前1
		audio_voice[5] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_Scenario_006");        //後半ボス前2
		audio_voice[6] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_Scenario_007");        //後半ボス後1
		audio_voice[7] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_Scenario_008");        //後半ボス後2
		audio_voice[8] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_Scenario_009");        //ゲームオーバー
		audio_voice[9] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_Scenario_010");        //OK
		audio_voice[10] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_Scenario_011");       //アステロイド地帯の説明
		audio_voice[11] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_Scenario_012");       //了解（ラジャー）
		audio_voice[12] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_13");       //アイテム使用時のボイス（スピードアップ）
		audio_voice[13] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_14");       //アイテム使用時のボイス（ミサイル）
		audio_voice[14] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_15");       //アイテム使用時のボイス（ダブル）
		audio_voice[15] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_16");       //アイテム使用時のボイス（レーザー）
		audio_voice[16] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_SE_Option_Multiple");     //アイテム使用時のボイス（オプション）
		audio_voice[17] = Resources.Load<AudioClip>("Sound/VOICE/gradius_SE_PowerUp_Shield");       //アイテム使用時のボイス（フォースフィールド）
		audio_voice[18] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_19");       //アイテム使用時のボイス（マックススピード）
		audio_voice[19] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_20_initial");       //アイテム使用時のボイス（イニットスピード）
		audio_voice[20] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_Scenario_013");           //モアイ1
		audio_voice[21] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_Scenario_014");           //モアイ2
		audio_voice[22] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_Scenario_015");           //前半ボス後更新1
		audio_voice[23] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_Scenario_016");           //前半ボス後更新２
		audio_voice[24] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_Scenario_017");           //了解（落ち着いた感じ）
		audio_voice[25] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_Scenario_018");           //敵の自爆
                                                                                                    //--------------------------------------------------------------------------------------------------------
        enemy_UFO_Group_prefab = Resources.Load("Enemy/Enemy_UFO_Group") as GameObject;
        enemy_UFO_Group_NoneShot_prefab = Resources.Load("Enemy/Enemy_UFO_Group_NoneShot") as GameObject;
        enemy_ClamChowder_Group_Two_Top_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_Two_Top") as GameObject;
        enemy_ClamChowder_Group_Two_Under_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_Two_Under") as GameObject;
        enemy_ClamChowder_Group_TwoWaveOnlyUp_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_TwoWaveOnlyUP") as GameObject;
        enemy_ClamChowder_Group_TwoWaveOnlyDown_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_TwoWaveOnlyDown") as GameObject;
        enemy_ClamChowder_Group_Three_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_Three") as GameObject;
        enemy_ClamChowder_Group_Three_Item_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_Three_Item") as GameObject;
        enemy_ClamChowder_Group_ThreeWaveOnlyUp_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_ThreeWaveOnlyUp") as GameObject;
        enemy_ClamChowder_Group_ThreeWaveOnlyDown_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_ThreeWaveOnlyDown") as GameObject;
        enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item") as GameObject;
        enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item") as GameObject;
        enemy_ClamChowder_Group_Four_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_Four") as GameObject;
        enemy_ClamChowder_Group_Four_NoItem_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_Four_NoItem") as GameObject;
        enemy_ClamChowder_Group_Five_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_Five") as GameObject;
        enemy_ClamChowder_Group_Five_NoItem_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_Five_NoItem") as GameObject;
        enemy_ClamChowder_Group_Seven_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_Seven") as GameObject;
        enemy_ClamChowder_Group_Straight_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_Straight") as GameObject;
        //enemy_MiddleBoss_Father_prefab = Resources.Load("Enemy/Enemy_MiddleBoss_Father") as GameObject;
        enemy_Beelzebub_Group_FourWide_prefab = Resources.Load("Enemy/Enemy_Beelzebub_Group_FourWide") as GameObject;
        enemy_Beelzebub_Group_FourWide_Item_prefab = Resources.Load("Enemy/Enemy_Beelzebub_Group_FourWide_Item") as GameObject;
        enemy_BeetleGroup_prefab = Resources.Load("Enemy/Enemy_Beetle_Group") as GameObject;
        enemy_BeetleGroup_Three_prefab = Resources.Load("Enemy/Enemy_Beetle_Group_Three") as GameObject;
        boundMeteors_prefab = Resources.Load("Enemy/BoundMeteors") as GameObject;
        enemy_Bacula_Sixteen_prefab = Resources.Load("Enemy/Enemy_Bacula_Sixteen") as GameObject;
        enemy_Bacula_FourOnly_prefab = Resources.Load("Enemy/Enemy_Bacula_FourOnly") as GameObject;
        //9月13日追加
        enemy_ClamChowder_FourTriangle_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_FourTriangle") as GameObject;
        enemy_ClamChowder_FourTriangle_NoItem_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_FourTriangle_NoItem") as GameObject;
        enemy_Beelzebub_Group_EightNormal_Item_prefab = Resources.Load("Enemy/Enemy_Beelzebub_Group_EightNormal_Item") as GameObject;
        //enemy_ClamChowder_Group_TwelveStraight_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_TwelveStraight") as GameObject;
        enemy_UFO_Group_Five_prefab = Resources.Load("Enemy/Enemy_UFO_Group_Five") as GameObject;
        enemy_Beetle_Group_Seven_prefab = Resources.Load("Enemy/Enemy_Beetle_Group_Seven") as GameObject;
        enemy_ClamChowder_Group_SevenStraight_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_SevenStraight") as GameObject;
        enemy_ClamChowder_Group_SixStraight_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_SixStraight") as GameObject;
        enemy_ClamChowder_Group_UpSevenDiagonal_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_UpSevenDiagonal") as GameObject;
        enemy_ClamChowder_Group_DownSevenDiagonal_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_DownSevenDiagonal") as GameObject;
        enemy_ClamChowder_Group_TenStraight_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_TenStraight") as GameObject;

        //--------------------------------------------------------------------------------------------------------

        Player = new Object_Pooling(Player_Prefab, 1, "Player");                        //プレイヤー生成
		Player_2 = new Object_Pooling(player_2_Prefab, 1, "Player_2");                  //プレイヤー2生成
		Boss_1 = new Object_Pooling(Boss1_Prefab, 1, "One_Boss");                              //ステージ1のボス生成
		Boss_2 = new Object_Pooling(Boss2_Prefab, 1, "Two_Boss");                               //ステージ2のボス生成
		PlayerBullet = new Object_Pooling(Bullet_Prefab_P, 5, "Player1_Bullet");         //プレイヤーのバレットを生成
		Player2Bullet = new Object_Pooling(BulletPrefab_P2, 5, "Player2_Bullet");
		P1_OptionBullet = new Object_Pooling(BulletPrefab_Option_P1, 10, "Option_Bullet_1P");
		P2_OptionBullet = new Object_Pooling(BulletPrefab_Option_P2, 10, "Option_Bullet_2P");
		PlayerMissile = new Object_Pooling(Player_Missile_Prefab, 8, "Player_Missile");        //プレイヤーのミサイルの生成
		EnemyBullet = new Object_Pooling(Bullet_Prefab_E, 10, "Enemy_Bullet");          //エネミーのバレットを生成
		Beam_Bullet_E = new Object_Pooling(Beam_Bullet_E_Prefab, 10, "Enemy_Beam_Bullet");      // エネミーのビーム型バレットを生成
        SmallBeam_Bullet_E= new Object_Pooling(SmallBeam_Bullet_E_Prefab, 20, "Enemy_SmallBeam_Bullet");      // エネミーの小さいビーム型バレットを生成
        BattleShipBullet = new Object_Pooling(Bullet_Prefab_BattleShip, 20, "BattleShip_Enemy_Bullet"); //戦艦タイプのバレットの生成
		UfoType_Enemy = new Object_Pooling(UfoType_Enemy_Prefab, 1, "UfoType_Enemy");       // UFO型エネミーを生成
		ClamChowderType_Enemy = new Object_Pooling(ClamChowderType_Enemy_Prefab, 1, "ClamChowderType_Enemy");       // 貝型エネミーを生成
		OctopusType_Enemy = new Object_Pooling(OctopusType_Enemy_Prefab, 1, "OctopusType_Enemy");                               // タコ型エネミーを生成
		BeelzebubType_Enemy = new Object_Pooling(BeelzebubType_Enemy_Prefab, 1, "BeelzebubType_Enemy");      //	 ハエ型エネミーを生成
		BattleShipType_Enemy = new Object_Pooling(BattleShip_Enemy_Prefab, 4, "BattleshipType_Enemy");          //戦艦型のエネミーを生成
		StarFish_Enemy = new Object_Pooling(Star_Fish_Enemy_Prefab, 20, "Star_Fish_Enemy");             //ヒトデ型エネミーを生成
		P1_Option = new Object_Pooling(P1_Option_Prefab, 4, "Option");
		P2_Option = new Object_Pooling(P2_Option_Prefab, 4, "P2_Option");
		PowerUP_Item = new Object_Pooling(Item_Prefab, 10, "PowerUP_Item");
		Boss_Middle = new Object_Pooling(Boss_Middle_Prefab, 1, "Middle_Boss");
		Laser_Line = new Object_Pooling(Laser_Line_Prefab, 30, "Laser_Line");
		One_Boss_Laser = new Object_Pooling(One_Boss_Laser_Prefab, 20, "One_Boss_Laser");
		One_Boss_BousndBullet = new Object_Pooling(One_Boss_BousndBullet_Prefab, 20, "One_Boss_BousndBullet");
		Two_Boss_Laser = new Object_Pooling(Two_Boss_Laser_Prefab, 20, "Two_Boss_Laser");

		Moai = new Object_Pooling(Moai_Prefab, 1, "Moai");
		Moai_Mini_Group = new Object_Pooling(Moai_Mini_Group_Prefab, 2, "Moai_Mini_Group");
		Moai_Bullet = new Object_Pooling(Moai_Bullet_Prefab, 10, "Moai_Prefab");
		Moai_Eye_Laser = new Object_Pooling(Moai_Eye_Laser_Prefab, 10, "Moai_Eye_Laser");
		Moai_Mouth_Laser = new Object_Pooling(Moai_Mouth_Laser_Prefab, 10, "Moai_Mouth_Laser");

		//effect---------------------------------------------------------------------------------------------
		Effects[0] = new Object_Pooling(Effects_Prefab[0], 1, "Player_explosion");                  //プレイヤーの爆発
		Effects[1] = new Object_Pooling(Effects_Prefab[1], 1, "Player_injection_Appearance");       //プレイヤーが登場するときのジェット噴射
		Effects[2] = new Object_Pooling(Effects_Prefab[2], 1, "Player_Fire");                       //プレイヤーのマズルフラッシュ
		Effects[3] = new Object_Pooling(Effects_Prefab[3], 1, "Player_Bullet");                     //プレイヤーの弾（使用してない）
		Effects[4] = new Object_Pooling(Effects_Prefab[4], 1, "Enemy_explosion");                   //エネミーの死亡時の爆発
		Effects[5] = new Object_Pooling(Effects_Prefab[5], 1, "Enemy_Core_Sheld_explosion");        //エネミーの中ボス以上のコアシールドの爆発エフェクト
		Effects[6] = new Object_Pooling(Effects_Prefab[6], 1, "Player_PowerUP");                    //プレイヤーのパワーアップ時のエフェクト
		Effects[7] = new Object_Pooling(Effects_Prefab[7], 1, "Boss_explosion");                    //ボス死亡時のエフェクト
		Effects[8] = new Object_Pooling(Effects_Prefab[8], 1, "Player_PowerUP_Bullet");             //プレイヤーのパワーアップした弾（使用してない）
		Effects[9] = new Object_Pooling(Effects_Prefab[9], 1, "Enemy_Grain");                       //敵の粒子
		Effects[10] = new Object_Pooling(Effects_Prefab[10], 1, "Battleship_explosion");            //戦艦の爆発
		Effects[11] = new Object_Pooling(Effects_Prefab[11], 1, "Player_Bullet_impact");            //プレイヤーの弾の着弾時のエフェクト
		Effects[12] = new Object_Pooling(Effects_Prefab[12], 1, "Boss_Appearance");                 //ボス登場時のエフェクト
		Effects[13] = new Object_Pooling(Effects_Prefab[13], 1, "Meteor_explosion");                    //隕石爆発Effect
		Effects[14] = new Object_Pooling(Effects_Prefab[14], 1, "Boss_Bullet2");                    //ボスの弾その２
		Effects[15] = new Object_Pooling(Effects_Prefab[15], 1, "P2_Powerup");                    //2Pパワーアップエフェクト
		Effects[16] = new Object_Pooling(Effects_Prefab[16], 1, "Missile_explosion");       // ミサイルの爆発
        //---------------------------------------------------------------------------------------------------
        //敵キャラのプーリング化-------------------------------------------------------------------------------
        enemy_UFO_Group = new Object_Pooling(enemy_UFO_Group_prefab, 1, "enemy_UFO_Group");
        enemy_UFO_Group_NoneShot = new Object_Pooling(enemy_UFO_Group_NoneShot_prefab, 2, "enemy_UFO_Group_NoneShot");
        enemy_ClamChowder_Group_Two_Top = new Object_Pooling(enemy_ClamChowder_Group_Two_Top_prefab, 1, "enemy_ClamChowder_Group_Two_Top");
        enemy_ClamChowder_Group_Two_Under = new Object_Pooling(enemy_ClamChowder_Group_Two_Under_prefab, 1, "enemy_ClamChowder_Group_Two_Under");
        enemy_ClamChowder_Group_TwoWaveOnlyUp = new Object_Pooling(enemy_ClamChowder_Group_TwoWaveOnlyUp_prefab, 1, "enemy_ClamChowder_Group_TwoWaveOnlyUp");
        enemy_ClamChowder_Group_TwoWaveOnlyDown = new Object_Pooling(enemy_ClamChowder_Group_TwoWaveOnlyDown_prefab, 1, "enemy_ClamChowder_Group_TwoWaveOnlyDown");
        //enemy_MiddleBoss_Father = new Object_Pooling(enemy_MiddleBoss_Father_prefab, 1, "enemy_MiddleBoss_Father");
        enemy_ClamChowder_Group_Three = new Object_Pooling(enemy_ClamChowder_Group_Three_prefab, 1, "enemy_ClamChowder_Group_Three");
        enemy_ClamChowder_Group_Three_Item = new Object_Pooling(enemy_ClamChowder_Group_Three_Item_prefab, 1, "enemy_ClamChowder_Group_Three_Item");
        enemy_ClamChowder_Group_ThreeWaveOnlyUp = new Object_Pooling(enemy_ClamChowder_Group_ThreeWaveOnlyUp_prefab, 1, "enemy_ClamChowder_Group_ThreeWaveOnlyUp");
        enemy_ClamChowder_Group_ThreeWaveOnlyDown = new Object_Pooling(enemy_ClamChowder_Group_ThreeWaveOnlyDown_prefab, 1, "enemy_ClamChowder_Group_ThreeWaveOnlyDown");
        enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item = new Object_Pooling(enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item_prefab, 1, "enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item");
        enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item = new Object_Pooling(enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item_prefab, 1, "enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item");
        enemy_ClamChowder_Group_Four = new Object_Pooling(enemy_ClamChowder_Group_Four_prefab, 1, "enemy_ClamChowder_Group_Four");
        enemy_ClamChowder_Group_Four_NoItem = new Object_Pooling(enemy_ClamChowder_Group_Four_NoItem_prefab, 1, "enemy_ClamChowder_Group_Four_NoItem");
        enemy_ClamChowder_Group_Five = new Object_Pooling(enemy_ClamChowder_Group_Five_prefab, 1, "enemy_ClamChowder_Group_Five");
        enemy_ClamChowder_Group_Five_NoItem = new Object_Pooling(enemy_ClamChowder_Group_Five_NoItem_prefab, 1, "enemy_ClamChowder_Group_Five_NoItem");
        enemy_ClamChowder_Group_Seven = new Object_Pooling(enemy_ClamChowder_Group_Seven_prefab, 1, "enemy_ClamChowder_Group_Seven");
        enemy_ClamChowder_Group_Straight = new Object_Pooling(enemy_ClamChowder_Group_Straight_prefab, 1, "enemy_ClamChowder_Group_Straight");
        enemy_Beelzebub_Group_FourWide = new Object_Pooling(enemy_Beelzebub_Group_FourWide_prefab, 2, "enemy_Beelzebub_Group_FourWide");
        enemy_Beelzebub_Group_FourWide_Item = new Object_Pooling(enemy_Beelzebub_Group_FourWide_Item_prefab, 2, "enemy_Beelzebub_Group_FourWide_Item");
        enemy_BeetleGroup = new Object_Pooling(enemy_BeetleGroup_prefab, 1, "enemy_BeetleGroup");
        enemy_BeetleGroup_Three = new Object_Pooling(enemy_BeetleGroup_Three_prefab, 1, "enemy_BeetleGroup_Three");
        boundMeteors = new Object_Pooling(boundMeteors_prefab, 2, "boundMeteors");
        enemy_Bacula_Sixteen = new Object_Pooling(enemy_Bacula_Sixteen_prefab, 1, "enemy_Bacula_Sixteen");
        enemy_Bacula_FourOnly = new Object_Pooling(enemy_Bacula_FourOnly_prefab, 1, "enemy_Bacula_FourOnly");
        //9月13日追加
        enemy_ClamChowder_FourTriangle = new Object_Pooling(enemy_ClamChowder_FourTriangle_prefab, 2, "enemy_ClamChowder_FourTriangle");
        enemy_ClamChowder_FourTriangle_NoItem = new Object_Pooling(enemy_ClamChowder_FourTriangle_NoItem_prefab, 2, "enemy_ClamChowder_FourTriangle_NoItem");
        enemy_Beelzebub_Group_EightNormal_Item = new Object_Pooling(enemy_Beelzebub_Group_EightNormal_Item_prefab, 1, "enemy_Beelzebub_Group_EightNormal_Item");
        enemy_UFO_Group_Five = new Object_Pooling(enemy_UFO_Group_Five_prefab, 2, "enemy_UFO_Group_Five");
        enemy_Beetle_Group_Seven = new Object_Pooling(enemy_Beetle_Group_Seven_prefab, 1, "enemy_Beetle_Group_Seven");
        enemy_ClamChowder_Group_SevenStraight = new Object_Pooling(enemy_ClamChowder_Group_SevenStraight_prefab, 2, "enemy_ClamChowder_Group_SevenStraight");
        enemy_ClamChowder_Group_SixStraight= new Object_Pooling(enemy_ClamChowder_Group_SixStraight_prefab, 2, "enemy_ClamChowder_Group_SixStraight");
        enemy_ClamChowder_Group_UpSevenDiagonal = new Object_Pooling(enemy_ClamChowder_Group_UpSevenDiagonal_prefab, 2, "Enemy_ClamChowder_Group_UpSevenDiagonal");
        enemy_ClamChowder_Group_DownSevenDiagonal = new Object_Pooling(enemy_ClamChowder_Group_DownSevenDiagonal_prefab, 2, "Enemy_ClamChowder_Group_DownSevenDiagonal");
        enemy_ClamChowder_Group_TenStraight = new Object_Pooling(enemy_ClamChowder_Group_TenStraight_prefab, 2, "Enemy_ClamChowder_Group_TenStraight");

        //-----------------------------------------------------------------------------------------------------
        if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eONE_PLAYER)
		{
			TextAsset Word = Resources.Load("CSV_Folder/" + File_name) as TextAsset;            //csvファイルを入れる変数
			StringReader csv = new StringReader(Word.text);                                         //読み込んだデータをcsvの変数の中に格納
			while (csv.Peek() > -1)
			{
				string line = csv.ReadLine();
				CsvData.Add(line.Split(','));               //カンマごとに割り振る
			}
		}
		else
		{
			TextAsset Word = Resources.Load("CSV_Folder/" + File_name2) as TextAsset;           //csvファイルを入れる変数
			StringReader csv = new StringReader(Word.text);                                             //読み込んだデータをcsvの変数の中に格納
			while (csv.Peek() > -1)
			{
				string line = csv.ReadLine();
				CsvData.Add(line.Split(','));                       //カンマごとに割り振る
			}
		}
	}


	//private void Update()
	//{
	//	//後ほどロードするもの

	//}
	public GameObject GetPlayer()
	{
		return Player.Get_Obj()[0];
	}
	public GameObject GetPlayer2()
	{
		return Player_2.Get_Obj()[0];
	}
	public GameObject GetOption()
	{
		return P1_Option.Get_Obj()[0];
	}

	public GameObject GetMiddleBoss()
	{
		return Boss_Middle.Get_Obj()[0];
	}

	public GameObject GetBoss(int bossID)
	{
		GameObject boss = null;
		switch (bossID)
		{
			case 1:
				boss = Boss_1.Get_Obj()[0];
				break;
			case 2:
				boss = Boss_2.Get_Obj()[0];
				break;
			case 3:
				boss = Moai.Get_Obj()[0];
				break;
			default:
				Debug.Log("引数違いますよ");
				break;
		}
		return boss;
	}
}
