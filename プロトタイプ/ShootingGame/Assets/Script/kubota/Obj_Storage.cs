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
	private GameObject Player_Missile1;                         //プレイヤー1のミサイルプレハブ
	private GameObject Bullet_Prefab_P;                         //弾のPrefab情報
	private GameObject BulletPrefab_P2;            //２P用の弾プレハブ情報
	private GameObject BulletPrefab_Option_P1;         //オプション用の球プレハブ情報１P用
	private GameObject Beam_Bullet_E_Prefab;                    //エネミーのビーム型バレットのプレハブ
    private GameObject SmallBeam_Bullet_E_Prefab;               //エネミーの小さいビーム型バレットのプレハブ
    private GameObject UfoType_Enemy_Prefab;                // UFO型エネミーのプレハブ
	private GameObject ClamChowderType_Enemy_Prefab;    // 貝型エネミーのプレハブ
	private GameObject P1_Option_Prefab;                            //オプションのプレハブ
	private GameObject Item_Prefab;                             //パワーアップのアイテムを入れえるための処理

	//-----------------------------------------------------------------------------------
	private GameObject[] Effects_Prefab = new GameObject[17];  //particleのプレハブ
	//---------------------------------------------------------------------------------
	private GameObject Boss_Middle_Prefab;                      //中ボスのプレハブ
	private GameObject Laser_Line_Prefab;						// レーザーのプレハブ

	//実際に作られたオブジェクト
	public Object_Pooling Enemy1;
	public Object_Pooling Medium_Size_Enemy1;
	public Object_Pooling Player;
	public Object_Pooling PlayerBullet;
	public Object_Pooling P1_OptionBullet;
	public Object_Pooling PlayerMissile;
	public Object_Pooling Beam_Bullet_E;
    public Object_Pooling UfoType_Enemy;
	public Object_Pooling ClamChowderType_Enemy;
	public Object_Pooling P1_Option;
	public Object_Pooling PowerUP_Item;
	public Object_Pooling Boss_Middle;
	public Object_Pooling Laser_Line;
	//effect関係-----------------------------------------------------
	public Object_Pooling[] Effects = new Object_Pooling[17];
	//サウンド関係-----------------------------------------------------
	public AudioClip[] audio_se = new AudioClip[29];    //ＳＥを読み込むための配列
	public AudioClip[] audio_voice = new AudioClip[26]; //VOICEを読み込むための配列


    //仮データ置き場（のちにプーリング化を施す）-------------------------------------------------------------
    public GameObject enemy_UFO_Group_NoneShot_prefab;
    public GameObject enemy_ClamChowder_Group_Two_Top_prefab;
    public GameObject enemy_ClamChowder_Group_Two_Under_prefab;
    public GameObject enemy_ClamChowder_Group_TwoWaveOnlyUp_prefab;
    public GameObject enemy_ClamChowder_Group_TwoWaveOnlyDown_prefab;
    public GameObject enemy_ClamChowder_Group_Three_Item_prefab;
    public GameObject enemy_ClamChowder_Group_ThreeWaveOnlyUp_prefab;
    public GameObject enemy_ClamChowder_Group_ThreeWaveOnlyDown_prefab;
    public GameObject enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item_prefab;
    public GameObject enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item_prefab;
    public GameObject enemy_ClamChowder_Group_Four_NoItem_prefab;
    public GameObject enemy_ClamChowder_Group_Five_NoItem_prefab;
    //9月13日追加

    public Object_Pooling enemy_UFO_Group_NoneShot;
    public Object_Pooling enemy_ClamChowder_Group_Two_Top;
    public Object_Pooling enemy_ClamChowder_Group_Two_Under;
    public Object_Pooling enemy_ClamChowder_Group_Three_Item;
    public Object_Pooling enemy_ClamChowder_Group_Four_NoItem;
    public Object_Pooling enemy_ClamChowder_Group_Five_NoItem;
    //9月13日追加
    public Object_Pooling enemy_ClamChowder_FourTriangle;
    public Object_Pooling enemy_ClamChowder_FourTriangle_NoItem;

    //----------------------------------------------------------
    private void Awake()
	{
		Storage_Data = GetComponent<Obj_Storage>();
	}

	void Start()
	{

		Player_Prefab = Resources.Load("Player/Player") as GameObject;
		Bullet_Prefab_P = Resources.Load("Bullet/Player_Bullet_1P") as GameObject;
		BulletPrefab_P2 = Resources.Load("Bullet/Player_Bullet_2P") as GameObject;
		BulletPrefab_Option_P1 = Resources.Load("Bullet/Option_Bullet_1P") as GameObject;
		Player_Missile1 = Resources.Load("Bullet/Player_Missile") as GameObject;
		Beam_Bullet_E_Prefab = Resources.Load("Bullet/Beam_Bullet") as GameObject;
        SmallBeam_Bullet_E_Prefab= Resources.Load("Bullet/SmallBeam_Bullet") as GameObject;
        UfoType_Enemy_Prefab = Resources.Load("Enemy/Enemy_UFO") as GameObject;
		ClamChowderType_Enemy_Prefab = Resources.Load("Enemy/ClamChowderType_Enemy") as GameObject;
		P1_Option_Prefab = Resources.Load("Option/Option") as GameObject;       //1Pオプションのロード

		Item_Prefab = Resources.Load("Item/Item_Test") as GameObject;        //アイテムのロード
		Boss_Middle_Prefab = Resources.Load("Enemy/Enemy_MiddleBoss_Father") as GameObject;		//中ボス
		Laser_Line_Prefab = Resources.Load("Bullet/LaserLine") as GameObject;

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

		audio_se[2] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_Player_BulletMode_Change");   //ラピッドとバーストの切り替え
		audio_se[4] = Resources.Load<AudioClip>("Sound/SE/gradius_SE_Player_Bullet");       //プレイヤーバレット音
		audio_se[5] = Resources.Load<AudioClip>("Sound/Teacher_SE/menesius_cupcel5 t");     //アイテム取得音(バー移動)
		audio_se[6] = Resources.Load<AudioClip>("Sound/Teacher_SE/manesius_manesius_kettei");   //アイテム使用パワーアップ音(ステータス変化)
		audio_se[7] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_Bullet_Hit");         //コアシールドヒット音
		audio_se[8] = Resources.Load<AudioClip>("Sound/SE/gradius_SE_Explosion_Small2");    //敵の爆発音
		audio_se[9] = Resources.Load<AudioClip>("Sound/SE/gradius_SE_Explosion_Moderate");  //ボスの爆発音
		audio_se[10] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_OptionCatch");           //ドロップしたオプションの回収
		audio_se[16] = Resources.Load<AudioClip>("Sound/Teacher_SE/manesius_manesius_kettei_neo");          //パワーアップの音
		audio_se[17] = Resources.Load<AudioClip>("Sound/SE/gradius_SE_Player_Laser");   //レーザーの発射音
		audio_se[18] = Resources.Load<AudioClip>("Sound/SE/gradius_SE_Explosion_1(Small)");     //小型爆発
		audio_se[19] = Resources.Load<AudioClip>("Sound/SE/gradius_SE_Explosion_2(senkan)");    //戦艦タイプの爆発音
		audio_se[20] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_Self_destruction");      //プレイヤーの死亡時の音
		audio_se[21] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_Player_Flight");         //プレイヤー登場の音
		audio_se[22] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_Explosion_3(ModerateBoss)"); //中ボス用の爆発音
		audio_se[23] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_Subtitles_Display");         //無線受信時
		audio_se[24] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_Subtitles_Display_Parmanent");   //無線のボイスの裏で流すよう
		audio_se[25] = Resources.Load<AudioClip>("Sound/SE/MANESIUS_SE_Subtitles_Display_Close");   //無線終了時


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
		audio_voice[13] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_14");				//アイテム使用時のボイス（ミサイル）
		audio_voice[14] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_15");				//アイテム使用時のボイス（ダブル）
		audio_voice[15] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_16");				//アイテム使用時のボイス（レーザー）
		audio_voice[16] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_SE_Option_Multiple");     //アイテム使用時のボイス（オプション）
		audio_voice[17] = Resources.Load<AudioClip>("Sound/VOICE/gradius_SE_PowerUp_Shield");       //アイテム使用時のボイス（フォースフィールド）
		audio_voice[18] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_19");				//アイテム使用時のボイス（マックススピード）
		audio_voice[19] = Resources.Load<AudioClip>("Sound/VOICE/Shooting_Voice_20_initial");       //アイテム使用時のボイス（イニットスピード）
		audio_voice[20] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_Scenario_013");           //モアイ1
		audio_voice[21] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_Scenario_014");           //モアイ2
		audio_voice[22] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_Scenario_015");           //前半ボス後更新1
		audio_voice[23] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_Scenario_016");           //前半ボス後更新２
		audio_voice[24] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_Scenario_017");           //了解（落ち着いた感じ）
		audio_voice[25] = Resources.Load<AudioClip>("Sound/VOICE/MANESIUS_Scenario_018");           //敵の自爆
        //--------------------------------------------------------------------------------------------------------
        enemy_UFO_Group_NoneShot_prefab = Resources.Load("Enemy/Enemy_UFO_Group_NoneShot") as GameObject;
        enemy_ClamChowder_Group_Two_Top_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_Two_Top") as GameObject;
        enemy_ClamChowder_Group_Two_Under_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_Two_Under") as GameObject;
        enemy_ClamChowder_Group_TwoWaveOnlyUp_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_TwoWaveOnlyUP") as GameObject;
        enemy_ClamChowder_Group_TwoWaveOnlyDown_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_TwoWaveOnlyDown") as GameObject;
        enemy_ClamChowder_Group_Three_Item_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_Three_Item") as GameObject;
        enemy_ClamChowder_Group_ThreeWaveOnlyUp_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_ThreeWaveOnlyUp") as GameObject;
        enemy_ClamChowder_Group_ThreeWaveOnlyDown_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_ThreeWaveOnlyDown") as GameObject;
        enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item") as GameObject;
        enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item") as GameObject;
        enemy_ClamChowder_Group_Four_NoItem_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_Four_NoItem") as GameObject;
        enemy_ClamChowder_Group_Five_NoItem_prefab = Resources.Load("Enemy/Enemy_ClamChowder_Group_Five_NoItem") as GameObject;
        //9月13日追加

        //--------------------------------------------------------------------------------------------------------

        Player = new Object_Pooling(Player_Prefab, 1, "Player");                        //プレイヤー生成
		PlayerBullet = new Object_Pooling(Bullet_Prefab_P, 5, "Player1_Bullet");         //プレイヤーのバレットを生成
		P1_OptionBullet = new Object_Pooling(BulletPrefab_Option_P1, 10, "Option_Bullet_1P");
		PlayerMissile = new Object_Pooling(Player_Missile1, 8, "Player_Missile");        //プレイヤーのミサイルの生成
		Beam_Bullet_E = new Object_Pooling(Beam_Bullet_E_Prefab, 10, "Enemy_Beam_Bullet");      // エネミーのビーム型バレットを生成
		UfoType_Enemy = new Object_Pooling(UfoType_Enemy_Prefab, 1, "UfoType_Enemy");       // UFO型エネミーを生成
		ClamChowderType_Enemy = new Object_Pooling(ClamChowderType_Enemy_Prefab, 1, "ClamChowderType_Enemy");       // 貝型エネミーを生成
		P1_Option = new Object_Pooling(P1_Option_Prefab, 4, "Option");
		PowerUP_Item = new Object_Pooling(Item_Prefab, 10, "PowerUP_Item");
		Boss_Middle = new Object_Pooling(Boss_Middle_Prefab, 1, "Middle_Boss");
		Laser_Line = new Object_Pooling(Laser_Line_Prefab, 30, "Laser_Line");


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
        enemy_UFO_Group_NoneShot = new Object_Pooling(enemy_UFO_Group_NoneShot_prefab, 2, "enemy_UFO_Group_NoneShot");
        enemy_ClamChowder_Group_Two_Top = new Object_Pooling(enemy_ClamChowder_Group_Two_Top_prefab, 1, "enemy_ClamChowder_Group_Two_Top");
        enemy_ClamChowder_Group_Two_Under = new Object_Pooling(enemy_ClamChowder_Group_Two_Under_prefab, 1, "enemy_ClamChowder_Group_Two_Under");
        //enemy_MiddleBoss_Father = new Object_Pooling(enemy_MiddleBoss_Father_prefab, 1, "enemy_MiddleBoss_Father");
        enemy_ClamChowder_Group_Three_Item = new Object_Pooling(enemy_ClamChowder_Group_Three_Item_prefab, 1, "enemy_ClamChowder_Group_Three_Item");
        enemy_ClamChowder_Group_Four_NoItem = new Object_Pooling(enemy_ClamChowder_Group_Four_NoItem_prefab, 1, "enemy_ClamChowder_Group_Four_NoItem");
        enemy_ClamChowder_Group_Five_NoItem = new Object_Pooling(enemy_ClamChowder_Group_Five_NoItem_prefab, 1, "enemy_ClamChowder_Group_Five_NoItem");
        //9月13日追加
        //-----------------------------------------------------------------------------------------------------

	}


	//private void Update()
	//{
	//	//後ほどロードするもの

	//}
	public GameObject GetPlayer()
	{
		return Player.Get_Obj()[0];
	}
	public GameObject GetOption()
	{
		return P1_Option.Get_Obj()[0];
	}

	public GameObject GetMiddleBoss()
	{
		return Boss_Middle.Get_Obj()[0];
	}
}
