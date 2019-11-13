//作成者：川村良太
//敵を出すスクリプト

//2019/08/03改修
using System;
using UnityEngine;

public class EnemyCreate : MonoBehaviour
{
	//━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
	//
	// 生成位置の変数
	//
	//━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
	//生成位置上側（Tなので上側、mTはマイナスなのでmがついてる、17が左右の最大）
	#region CreatePosTop
	private Transform createPosT17;
    private Transform createPosT16;
    private Transform createPosT15;
    private Transform createPosT14;
    private Transform createPosT13;
    private Transform createPosT12;
    private Transform createPosT11;
    private Transform createPosT10;
    private Transform createPosT9;
    private Transform createPosT8;
    private Transform createPosT7;
    private Transform createPosT6;
    private Transform createPosT5;
    private Transform createPosT4;
    private Transform createPosT3;
    private Transform createPosT2;
    private Transform createPosT1;
    private Transform createPosT0;
    private Transform createPosTm1;
    private Transform createPosTm2;
    private Transform createPosTm3;
    private Transform createPosTm4;
    private Transform createPosTm5;
    private Transform createPosTm6;
    private Transform createPosTm7;
    private Transform createPosTm8;
    private Transform createPosTm9;
    private Transform createPosTm10;
    private Transform createPosTm11;
    private Transform createPosTm12;
    private Transform createPosTm13;
    private Transform createPosTm14;
    private Transform createPosTm15;
    private Transform createPosTm16;
    private Transform createPosTm17;
    #endregion
    //生成位置下側（Uなので上側、mUはマイナスなのでmがついてる、17が左右の最大）
    #region CreatePosUnder
    private Transform createPosU17;
    private Transform createPosU16;
    private Transform createPosU15;
    private Transform createPosU14;
    private Transform createPosU13;
    private Transform createPosU12;
    private Transform createPosU11;
    private Transform createPosU10;
    private Transform createPosU9;
    private Transform createPosU8;
    private Transform createPosU7;
    private Transform createPosU6;
    private Transform createPosU5;
    private Transform createPosU4;
    private Transform createPosU3;
    private Transform createPosU2;
    private Transform createPosU1;
    private Transform createPosU0;
    private Transform createPosUm1;
    private Transform createPosUm2;
    private Transform createPosUm3;
    private Transform createPosUm4;
    private Transform createPosUm5;
    private Transform createPosUm6;
    private Transform createPosUm7;
    private Transform createPosUm8;
    private Transform createPosUm9;
    private Transform createPosUm10;
    private Transform createPosUm11;
    private Transform createPosUm12;
    private Transform createPosUm13;
    private Transform createPosUm14;
    private Transform createPosUm15;
    private Transform createPosUm16;
    private Transform createPosUm17;
    #endregion
    //生成位置右側（Rなので右側、mRはマイナスなのでmがついてる、5が上下の最大）
    #region CreatePosRight
    private Transform createPosR5;
    private Transform createPosR4;
    private Transform createPosR3;
    private Transform createPosR2;
    private Transform createPosR1;
    private Transform createPosR0;
    private Transform createPos_FourGroupR;
    private Transform createPosRm1;
    private Transform createPosRm2;
    private Transform createPosRm3;
    private Transform createPosRm4;
    private Transform createPosRm5;
    #endregion
    //生成位置左側
    #region CreatePosLeft
    private Transform createPosL5;
    private Transform createPosL4;
    private Transform createPosL3;
    private Transform createPosL2;
    private Transform createPosL1;
    private Transform createPosL0;
    private Transform createPos_FourGroupL;
    private Transform createPosLm1;
    private Transform createPosLm2;
    private Transform createPosLm3;
    private Transform createPosLm4;
    private Transform createPosLm5;
	#endregion
	//中ボス位置と戦艦位置
	#region createMiddlePos
	private Transform createMiddleBossPos;
    private Transform createBattleShipPos;
	#endregion
	//バキュラ位置
	#region CreateBacula
	private Transform createBaculaGroupPos;
	#endregion
	//隕石生成位置
	#region CreateMeteor
	private Transform createMeteorPosR0;
	#endregion

	//━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
	//
	// 敵グループプレハブのオブジェクト変数
	//
	//━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
	// UFOオブジェクト
	#region UFO
	private GameObject enemy_UFO_Group_Five;
	#endregion
	// 闘牛オブジェクト
	#region ClamChowder
	private GameObject enemy_ClamChowder_Group_Four;
	private GameObject enemy_ClamChowder_Group_Four_NoItem;
    private GameObject enemy_ClamChowder_Group_Five;
    private GameObject enemy_ClamChowder_Group_Five_NoItem;
    private GameObject enemy_ClamChowder_Group_FourBehind;
    private GameObject enemy_ClamChowder_Group_Two;
    private GameObject enemy_ClamChowder_Group_Two_Top;
    private GameObject enemy_ClamChowder_Group_Two_Under;
    private GameObject enemy_ClamChowder_Group_TwoWaveOnlyUp;
    private GameObject enemy_ClamChowder_Group_TwoWaveOnlyDown;
    private GameObject enemy_ClamChowder_Group_TwoWaveOnlyUp_Item;
    private GameObject enemy_ClamChowder_Group_TwoWaveOnlyDown_Item;
    private GameObject enemy_ClamChowder_Group_Three;
    private GameObject enemy_ClamChowder_Group_Three_Item;
	private GameObject enemy_ClamChowder_Group_ThreeStraight;
	private GameObject enemy_ClamChowder_Group_SevenWave;
    private GameObject enemy_ClamChowder_Group_Straight;
    private GameObject enemy_ClamChowder_Group_StraightBehind;
    private GameObject enemy_ClamChowder_Group_FourTriangle;
    private GameObject enemy_ClamChowder_Group_FourTriangle_B;
    private GameObject enemy_ClamChowder_Group_FourTriangle_C;
    private GameObject enemy_ClamChowder_Group_FourTriangle_NoItem;
    private GameObject enemy_ClamChowder_Group_TwelveStraight;
    private GameObject enemy_ClamChowder_Group_SevenStraight;
    private GameObject enemy_ClamChowder_Group_SixStraight;
    private GameObject enemy_ClamChowder_Group_UpSevenDiagonal;
    private GameObject enemy_ClamChowder_Group_DownSevenDiagonal;
    private GameObject enemy_ClamChowder_Group_TenStraight;
    private GameObject enemy_ClamChowder_Group_FourVerticalAttack;
    private GameObject enemy_ClamChowder_Group_FourVerticalStraight;
    private GameObject enemy_ClamChowder_Group_Seven;
	//private GameObject enemy_ClamChowder_Group_ThreeWaveOnlyUp;
	//private GameObject enemy_ClamChowder_Group_ThreeWaveOnlyDown;
	//private GameObject enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item;
	//private GameObject enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item;
	#endregion
	// ハエオブジェクト
	#region Beelzebub
	private GameObject enemy_Beelzebub_Group_FourNomal;
	private GameObject enemy_Beelzebub_Group_FourBack;
	private GameObject enemy_Beelzebub_Group_FourWide;
	private GameObject enemy_Beelzebub_Group_FourWide_Item;
	private GameObject enemy_Beelzebub_Group_EightNormal_Item;
	private GameObject enemy_Beelzebub_Group_TwoWide;
	#endregion
	// ビートルオブジェクト
	#region Beetle
	private GameObject enemy_Beetle_Group;
	private GameObject enemy_Beetle_Group_Three;
	private GameObject enemy_Beetle_Group_Five;
	private GameObject enemy_Beetle_Group_Seven;
	#endregion
	// バキュラオブジェクト
	#region　Bacula
	private GameObject enemy_Bacula_Sixteen;
    private GameObject enemy_Bacula_FourOnly;
    private GameObject enemy_Bacula_Group_Two;
    private GameObject enemy_Bacula_Group_Six;
	#endregion
	// 隕石オブジェクト
	#region Meteor
	private GameObject Enemy_BoundMeteors;
	//private GameObject enemy_Meteor;
	//private GameObject enemy_Meteor_Top;
	//private GameObject enemy_Meteor_Under;
	//private GameObject enemy_Meteors;
	//private GameObject enemy_Meteor_Mini;
	//private GameObject enemy_MeteorWaveGroup;
	#endregion
	// ヒトデオブジェクト
	#region StarFish
	private GameObject enemy_Star_Fish_Spowner;
	#endregion
	// 中ボスオブジェクト
	#region Middle
	private GameObject enemy_MoaiBossGroup;
	private GameObject middleBossOBj;
	private Enemy_MiddleBoss middleBoss_Script;
	//private GameObject enemy_SlowFollow;
	#endregion
	// 保管オブジェクト情報
	#region Temp
	private GameObject saveEnemyObj;
	#endregion

	//━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
	//
	// カウント変数
	//
	//━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
	[SerializeField, NonEditable] private int PreviousCount = 0;
	[SerializeField, NonEditable] private int frameCnt = 0;    //フレームカウント：これの値で生成のタイミングをはかる
	[SerializeField, NonEditable] private int groupCnt = 1;    //画面に出す群れのカウント
	[SerializeField, NonEditable] private int turning_frame = 180;
	[SerializeField, NonEditable] private string nextEnemy;
    private EnemyGroupManage group_Script;
	private GameObject oneBossOBj;
	private One_Boss oneBoss_Script;
	private GameObject mistEffectObj;
	private ParticleSystem mistParticle;
	private BackgroundActivation backActive_Script;
	private GameObject twoBossObj;
	private Two_Boss twoBoss_Script;
	private GameObject enemy_BattleShip;
	private GameObject moaiObj;
	private Enemy_Moai moai_Script;
	private bool isCreate;       //表示するときにtrueにする
	private bool isBaculaDestroy = false;
	private bool isMiddleBossDead = false;
	private bool isOneBossAlive = false;
	private bool isTwoBossAlive = false;
	private bool isMoaiAlive = false;
	private bool isMiddleBossSkip = true;
	private bool isNowOneBoss = false;
	private bool isNowTwoBoss = false;
	private bool isNowMoai = false;
	private bool isDebug = false;
	private bool isLastBossWireless = false;
	// ビッグコアの出現グループ番号と経過フレーム
    private int bigCoreGroupNum = 17;
    private int bigCoreGroupFrame = 2580;
	// ビッグコア後の敵グループの出現グループ番号と経過フレーム
	private int bigCoreNextGroupNum = 26;
    private int bigCoreNextGroupFrame = 3750;
	// ビッグコア2とビッグコア2後の敵グループの出現グループ番号と経過フレーム
	private int bigCoreMK2GrouNum = 43;
    private int bigCoreMK2GroupFrame = 6345;
    private int bigCoreMK2NextGroupFrame = 6465;
	// モアイとモアイ後の敵グループの出現グループ番号と経過フレーム
	private int moaiGroupNum = 49;
    private int moaiGroupFrame = 8205;
    private int moaiGroupNextGroupFrame = 8325;
	// ビッグコア3の出現グループ番号と経過フレーム
    private int bigCoreMK3GroupNum = 97;
    private int bigCoreMK3GroupFrame = 12185;

	// Debug
	// groupFrameCheckDebugFlagをオンにしている時
	// 中ボス・大ボスの出現を無効にし、出現する瞬間の情報をDebugで表示
	private bool groupFrameCheckDebugFlag = false;
	// 敵グループの種類名・グループ番号・経過フレーム
	void GroupFrameCheckDebug(string groupName, int groupNum, int groupFrame)
    {
        if(groupFrameCheckDebugFlag) Debug.Log("■■■" + groupName + "は番号：" + groupNum + " 総フレーム数：" + groupFrame);
    }

	// 最初のフレーム
    void Start()
    {
		//群れカウント初期化
		PreviousCount = 0;
		frameCnt = 0;//フレームカウント：これの値で生成のタイミングをはかる
		groupCnt = 1;    //画面に出す群れのカウント
		nextEnemy = "";
		turning_frame = enemyGroups[0].nextGroupFrame;
		EnemyDebugNumberUpload();
		CreatePosUpload();
		ResourcesUpload();
	}

	private void EnemyDebugNumberUpload()
	{
		int allFrame = 0;

		for (int num = 0; num < enemyGroups.Length; num++)
		{
			switch(enemyGroups[num].enemyType)
			{
				case EnemyType.BIGCORE:
					bigCoreGroupNum = num;
					bigCoreGroupFrame = allFrame;
					break;

				case EnemyType.BIGCOREENDGROUP:
					bigCoreNextGroupNum = num;
					bigCoreNextGroupFrame = allFrame;
					break;

				case EnemyType.BIGCOREMK2:
					bigCoreMK2GrouNum = num;
					bigCoreMK2GroupFrame = allFrame;
					bigCoreMK2NextGroupFrame = allFrame + enemyGroups[num].nextGroupFrame;
					break;

				case EnemyType.MOAI:
					moaiGroupNum = num;
					moaiGroupFrame = allFrame;
					moaiGroupNextGroupFrame = allFrame + enemyGroups[num].nextGroupFrame;
					break;

				case EnemyType.BIGCOREMK3:
					bigCoreMK3GroupNum = num;
					bigCoreNextGroupFrame = allFrame;
					break;

				case EnemyType.GAMECLEAR:
					return;
			}

			allFrame += enemyGroups[num].nextGroupFrame;
		}
	}

	private void EnemyDebugNumberUpdate(EnemyType e, bool isNextGroup)
	{
		int allFrame = 0;

		for (int num = 0; num < enemyGroups.Length; num++)
		{
			if (e == enemyGroups[num].enemyType)
			{
				if(!isNextGroup)
				{
					groupCnt = num;
					turning_frame = allFrame;
				}
				else
				{
					groupCnt = num + 1;
					turning_frame = allFrame + enemyGroups[num].nextGroupFrame;
				}
				frameCnt = turning_frame - 60;
				return;
			}
			allFrame += enemyGroups[num].nextGroupFrame;
		}
	}

	private void CreatePosUpload()
	{
		//位置オブジェクト取得
        //上側取得
        #region CreatePosTop
        createPosT17 = GameObject.Find("CreatePos_Top_17").transform;
        createPosT16 = GameObject.Find("CreatePos_Top_16").transform;
        createPosT15 = GameObject.Find("CreatePos_Top_15").transform;
        createPosT14 = GameObject.Find("CreatePos_Top_14").transform;
        createPosT13 = GameObject.Find("CreatePos_Top_13").transform;
        createPosT12 = GameObject.Find("CreatePos_Top_12").transform;
        createPosT11 = GameObject.Find("CreatePos_Top_11").transform;
        createPosT10 = GameObject.Find("CreatePos_Top_10").transform;
        createPosT9 = GameObject.Find("CreatePos_Top_9").transform;
        createPosT8 = GameObject.Find("CreatePos_Top_8").transform;
        createPosT7 = GameObject.Find("CreatePos_Top_7").transform;
        createPosT6 = GameObject.Find("CreatePos_Top_6").transform;
        createPosT5 = GameObject.Find("CreatePos_Top_5").transform;
        createPosT4 = GameObject.Find("CreatePos_Top_4").transform;
        createPosT3 = GameObject.Find("CreatePos_Top_3").transform;
        createPosT2 = GameObject.Find("CreatePos_Top_2").transform;
        createPosT1 = GameObject.Find("CreatePos_Top_1").transform;
        createPosT0 = GameObject.Find("CreatePos_Top_0").transform;
        createPosTm1 = GameObject.Find("CreatePos_Top_-1").transform;
        createPosTm2 = GameObject.Find("CreatePos_Top_-2").transform;
        createPosTm3 = GameObject.Find("CreatePos_Top_-3").transform;
        createPosTm4 = GameObject.Find("CreatePos_Top_-4").transform;
        createPosTm5 = GameObject.Find("CreatePos_Top_-5").transform;
        createPosTm6 = GameObject.Find("CreatePos_Top_-6").transform;
        createPosTm7 = GameObject.Find("CreatePos_Top_-7").transform;
        createPosTm8 = GameObject.Find("CreatePos_Top_-8").transform;
        createPosTm9 = GameObject.Find("CreatePos_Top_-9").transform;
        createPosTm10 = GameObject.Find("CreatePos_Top_-10").transform;
        createPosTm11 = GameObject.Find("CreatePos_Top_-11").transform;
        createPosTm12 = GameObject.Find("CreatePos_Top_-12").transform;
        createPosTm13 = GameObject.Find("CreatePos_Top_-13").transform;
        createPosTm14 = GameObject.Find("CreatePos_Top_-14").transform;
        createPosTm15 = GameObject.Find("CreatePos_Top_-15").transform;
        createPosTm16 = GameObject.Find("CreatePos_Top_-16").transform;
        createPosTm17 = GameObject.Find("CreatePos_Top_-17").transform;
        #endregion

        //下側取得
        #region CreatePosUnder
        //createPosU17 = GameObject.Find("CreatePos_Under_17").transform;
        //createPosU16 = GameObject.Find("CreatePos_Under_16").transform;
        //createPosU15 = GameObject.Find("CreatePos_Under_15").transform;
        //createPosU14 = GameObject.Find("CreatePos_Under_14").transform;
        //createPosU13 = GameObject.Find("CreatePos_Under_13").transform;
        //createPosU12 = GameObject.Find("CreatePos_Under_12").transform;
        //createPosU11 = GameObject.Find("CreatePos_Under_11").transform;
        //createPosU10 = GameObject.Find("CreatePos_Under_10").transform;
        //createPosU9 = GameObject.Find("CreatePos_Under_9").transform;
        //createPosU8 = GameObject.Find("CreatePos_Under_8").transform;
        //createPosU7 = GameObject.Find("CreatePos_Under_7").transform;
        //createPosU6 = GameObject.Find("CreatePos_Under_6").transform;
        //createPosU5 = GameObject.Find("CreatePos_Under_5").transform;
        //createPosU4 = GameObject.Find("CreatePos_Under_4").transform;
        //createPosU3 = GameObject.Find("CreatePos_Under_3").transform;
        //createPosU2 = GameObject.Find("CreatePos_Under_2").transform;
        //createPosU1 = GameObject.Find("CreatePos_Under_1").transform;
        //createPosU0 = GameObject.Find("CreatePos_Under_0").transform;
        //createPosUm1 = GameObject.Find("CreatePos_Under_-1").transform;
        //createPosUm2 = GameObject.Find("CreatePos_Under_-2").transform;
        //createPosUm3 = GameObject.Find("CreatePos_Under_-3").transform;
        //createPosUm4 = GameObject.Find("CreatePos_Under_-4").transform;
        //createPosUm5 = GameObject.Find("CreatePos_Under_-5").transform;
        //createPosUm6 = GameObject.Find("CreatePos_Under_-6").transform;
        //createPosUm7 = GameObject.Find("CreatePos_Under_-7").transform;
        //createPosUm8 = GameObject.Find("CreatePos_Under_-8").transform;
        //createPosUm9 = GameObject.Find("CreatePos_Under_-9").transform;
        //createPosUm10 = GameObject.Find("CreatePos_Under_-10").transform;
        //createPosUm11 = GameObject.Find("CreatePos_Under_-11").transform;
        //createPosUm12 = GameObject.Find("CreatePos_Under_-12").transform;
        //createPosUm13 = GameObject.Find("CreatePos_Under_-13").transform;
        //createPosUm14 = GameObject.Find("CreatePos_Under_-14").transform;
        //createPosUm15 = GameObject.Find("CreatePos_Under_-15").transform;
        //createPosUm16 = GameObject.Find("CreatePos_Under_-16").transform;
        //createPosUm17 = GameObject.Find("CreatePos_Under_-17").transform;
        #endregion

        //右側取得
        #region CreatePosRight
        createPosR5 = GameObject.Find("CreatePos_Right_5").transform;
        createPosR4 = GameObject.Find("CreatePos_Right_4").transform;
        createPosR3 = GameObject.Find("CreatePos_Right_3").transform;
        createPosR2 = GameObject.Find("CreatePos_Right_2").transform;
        createPosR1 = GameObject.Find("CreatePos_Right_1").transform;
        createPosR0 = GameObject.Find("CreatePos_Right_0").transform;
        createPos_FourGroupR = GameObject.Find("CreatePos_FourGroupR").transform;
        createPosRm1 = GameObject.Find("CreatePos_Right_-1").transform;
        createPosRm2 = GameObject.Find("CreatePos_Right_-2").transform;
        createPosRm3 = GameObject.Find("CreatePos_Right_-3").transform;
        createPosRm4 = GameObject.Find("CreatePos_Right_-4").transform;
        createPosRm5 = GameObject.Find("CreatePos_Right_-5").transform;
        #endregion

        //左側取得
        #region CreatePosLeft
        createPosL5 = GameObject.Find("CreatePos_Left_5").transform;
        createPosL4 = GameObject.Find("CreatePos_Left_4").transform;
        createPosL3 = GameObject.Find("CreatePos_Left_3").transform;
        createPosL2 = GameObject.Find("CreatePos_Left_2").transform;
        createPosL1 = GameObject.Find("CreatePos_Left_1").transform;
        createPosL0 = GameObject.Find("CreatePos_Left_0").transform;
        createPos_FourGroupL = GameObject.Find("CreatePos_FourGroupL").transform;
        createPosLm1 = GameObject.Find("CreatePos_Left_-1").transform;
        createPosLm2 = GameObject.Find("CreatePos_Left_-2").transform;
        createPosLm3 = GameObject.Find("CreatePos_Left_-3").transform;
        createPosLm4 = GameObject.Find("CreatePos_Left_-4").transform;
        createPosLm5 = GameObject.Find("CreatePos_Left_-5").transform;
		#endregion
	}

	private void ResourcesUpload()
	{
		#region リソース取得
		createMiddleBossPos = GameObject.Find("CreateMiddleBossPos").transform;
		createBattleShipPos = GameObject.Find("CreateBattleshipPos").transform;

		createBaculaGroupPos = GameObject.Find("CreateBaculaGroupPos").transform;

		//createMeteorPosR4_814 = GameObject.Find("CreateMeteorPos_Right_4.814");
		//createMeteorPosR2_988 = GameObject.Find("CreateMeteorPos_Right_2.988");
		//createMeteorPosR1_494 = GameObject.Find("CreateMeteorPos_Right_1.494");
		createMeteorPosR0 = GameObject.Find("CreateMeteorPos_Right_0").transform;
		//createMeteorPosRm1_162 = GameObject.Find("CreateMeteorPos_Right_-1.162");
		//createMeteorPosRm2_822 = GameObject.Find("CreateMeteorPos_Right_-2.822");
		//createMeteorPosRm3_57 = GameObject.Find("CreateMeteorPos_Right_-3.57");
		//createMeteorPosRm4_814 = GameObject.Find("CreateMeteorPos_Right_-4.814");

		//enemy_UFO_Group = Resources.Load("Enemy/Enemy_UFO_Group") as GameObject;
		enemy_UFO_Group_Five = Resources.Load("Enemy/Enemy_UFO_Group_Five") as GameObject;
        enemy_ClamChowder_Group_Four = Resources.Load("Enemy/Enemy_ClamChowder_Group_Four") as GameObject;
        enemy_ClamChowder_Group_Four_NoItem = Resources.Load("Enemy/Enemy_ClamChowder_Group_Four_NoItem") as GameObject;
        enemy_ClamChowder_Group_Five = Resources.Load("Enemy/Enemy_ClamChowder_Group_Five") as GameObject;
        enemy_ClamChowder_Group_Five_NoItem = Resources.Load("Enemy/Enemy_ClamChowder_Group_Five_NoItem") as GameObject;
        enemy_ClamChowder_Group_FourBehind = Resources.Load("Enemy/Enemy_ClamChowder_Group_FourBehind") as GameObject;
        enemy_ClamChowder_Group_Two = Resources.Load("Enemy/Enemy_ClamChowder_Group_Two") as GameObject;
        enemy_ClamChowder_Group_Two_Top = Resources.Load("Enemy/Enemy_ClamChowder_Group_Two_Top") as GameObject;
        enemy_ClamChowder_Group_Two_Under = Resources.Load("Enemy/Enemy_ClamChowder_Group_Two_Under") as GameObject;
        enemy_ClamChowder_Group_TwoWaveOnlyUp = Resources.Load("Enemy/Enemy_ClamChowder_Group_TwoWaveOnlyUp") as GameObject;
        enemy_ClamChowder_Group_TwoWaveOnlyDown = Resources.Load("Enemy/Enemy_ClamChowder_Group_TwoWaveOnlyDown") as GameObject;
        enemy_ClamChowder_Group_TwoWaveOnlyUp_Item = Resources.Load("Enemy/Enemy_ClamChowder_Group_TwoWaveOnlyUp_Item") as GameObject;
        enemy_ClamChowder_Group_TwoWaveOnlyDown_Item = Resources.Load("Enemy/Enemy_ClamChowder_Group_TwoWaveOnlyDown_Item") as GameObject;
        enemy_ClamChowder_Group_Three = Resources.Load("Enemy/Enemy_ClamChowder_Group_Three") as GameObject;
        enemy_ClamChowder_Group_Three_Item = Resources.Load("Enemy/Enemy_ClamChowder_Group_Three_Item") as GameObject;
		enemy_ClamChowder_Group_ThreeStraight = Resources.Load("Enemy/Enemy_ClamChowder_Group_ThreeStraight") as GameObject;
		enemy_ClamChowder_Group_SevenWave = Resources.Load("Enemy/Enemy_ClamChowder_Group_Seven") as GameObject;
        enemy_ClamChowder_Group_Straight = Resources.Load("Enemy/Enemy_ClamChowder_Group_Straight") as GameObject;
        enemy_ClamChowder_Group_StraightBehind = Resources.Load("Enemy/Enemy_ClamChowder_Group_StraightBehind") as GameObject;
        enemy_ClamChowder_Group_FourTriangle = Resources.Load("Enemy/Enemy_ClamChowder_Group_FourTriangle") as GameObject;
        enemy_ClamChowder_Group_FourTriangle_B = Resources.Load("Enemy/Enemy_ClamChowder_Group_FourTriangle_B") as GameObject;
        enemy_ClamChowder_Group_FourTriangle_C = Resources.Load("Enemy/Enemy_ClamChowder_Group_FourTriangle_C") as GameObject;
        enemy_ClamChowder_Group_FourTriangle_NoItem = Resources.Load("Enemy/Enemy_ClamChowder_Group_FourTriangle_NoItem") as GameObject;
        enemy_ClamChowder_Group_TwelveStraight = Resources.Load("Enemy/Enemy_ClamChowder_Group_TwelveStraight") as GameObject;
        enemy_ClamChowder_Group_SevenStraight = Resources.Load("Enemy/Enemy_ClamChowder_Group_SevenStraight") as GameObject;
        enemy_ClamChowder_Group_SixStraight = Resources.Load("Enemy/Enemy_ClamChowder_Group_SixStraight") as GameObject;
        enemy_ClamChowder_Group_UpSevenDiagonal = Resources.Load("Enemy/Enemy_ClamChowder_Group_UpSevenDiagonal") as GameObject;
        enemy_ClamChowder_Group_DownSevenDiagonal = Resources.Load("Enemy/Enemy_ClamChowder_Group_DownSevenDiagonal") as GameObject;
        enemy_ClamChowder_Group_TenStraight = Resources.Load("Enemy/Enemy_ClamChowder_Group_TenStraight") as GameObject;
        enemy_ClamChowder_Group_FourVerticalStraight = Resources.Load("Enemy/Enemy_ClamChowder_Group_FourVerticalStraight") as GameObject;
        enemy_ClamChowder_Group_FourVerticalAttack = Resources.Load("Enemy/Enemy_ClamChowder_Group_FourVerticalAttack") as GameObject;
        enemy_ClamChowder_Group_Seven = Resources.Load("Enemy/Enemy_ClamChowder_Group_Seven") as GameObject;
        //enemy_MiddleBoss_Father = Resources.Load("Enemy/Enemy_MiddleBoss_Father") as GameObject;
        //enemy_ClamChowder_Group_ThreeWaveOnlyUp = Resources.Load("Enemy/Enemy_ClamChowder_Group_ThreeWaveOnlyUp") as GameObject;
        //enemy_ClamChowder_Group_ThreeWaveOnlyDown = Resources.Load("Enemy/Enemy_ClamChowder_Group_ThreeWaveOnlyDown") as GameObject;
        //enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item = Resources.Load("Enemy/Enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item") as GameObject;
        //enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item = Resources.Load("Enemy/Enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item") as GameObject;
        enemy_BattleShip = Resources.Load("Enemy/BattleshipType_Enemy") as GameObject;
        enemy_Beelzebub_Group_FourNomal = Resources.Load("Enemy/Enemy_Beelzebub_Group_FourNomal") as GameObject;
        enemy_Beelzebub_Group_FourBack = Resources.Load("Enemy/Enemy_Beelzebub_Group_FourBack") as GameObject;
        enemy_Beelzebub_Group_FourWide = Resources.Load("Enemy/Enemy_Beelzebub_Group_FourWide") as GameObject;
        enemy_Beelzebub_Group_FourWide_Item = Resources.Load("Enemy/Enemy_Beelzebub_Group_FourWide_Item") as GameObject;
        enemy_Beelzebub_Group_EightNormal_Item = Resources.Load("Enemy/Enemy_Beelzebub_Group_EightNormal_Item") as GameObject;
        enemy_Beelzebub_Group_TwoWide = Resources.Load("Enemy/Enemy_Beelzebub_Group_TwoWide_Item") as GameObject;
        enemy_Bacula_Sixteen = Resources.Load("Enemy/Enemy_Bacula_Sixteen") as GameObject;
        enemy_Bacula_FourOnly = Resources.Load("Enemy/Enemy_Bacula_FourOnly") as GameObject;
        enemy_Bacula_Group_Two = Resources.Load("Enemy/Enemy_Bacula_Group_Two") as GameObject;
        enemy_Bacula_Group_Six = Resources.Load("Enemy/Enemy_Bacula_Group_Six") as GameObject;
        //enemy_Meteor = Resources.Load("Enemy/Enemy_Meteor") as GameObject;
        //enemy_Meteor_Mini = Resources.Load("Enemy/Enemy_Meteor_Mini") as GameObject;
        //enemy_Meteor_Top = Resources.Load("Enemy/Enemy_Meteor_Top") as GameObject;
        //enemy_Meteor_Under = Resources.Load("Enemy/Enemy_Meteor_Under") as GameObject;
        //enemy_Meteors = Resources.Load("Enemy/Meteors") as GameObject;
        //enemy_MeteorWaveGroup = Resources.Load("Enemy/Enemy_MeteorWaveGroup") as GameObject;
        //enemy_SlowFollow = Resources.Load("Enemy/Enemy_SlowFollow") as GameObject;
        Enemy_BoundMeteors = Resources.Load("Enemy/BoundMeteors") as GameObject;
        enemy_Star_Fish_Spowner = Resources.Load("Enemy/Enemy_Star_Fish_Spowner") as GameObject;
        enemy_Beetle_Group = Resources.Load("Enemy/Enemy_Beetle_Group") as GameObject;
        enemy_Beetle_Group_Three = Resources.Load("Enemy/Enemy_Beetle_Group_Three") as GameObject;
        enemy_Beetle_Group_Five = Resources.Load("Enemy/Enemy_Beetle_Group") as GameObject;
        enemy_Beetle_Group_Seven = Resources.Load("Enemy/Enemy_Beetle_Group_Seven") as GameObject;

        enemy_MoaiBossGroup = Resources.Load("Enemy/Enemy_MoaiBossGroup") as GameObject;

        mistEffectObj = Resources.Load("Effects/Other/O004") as GameObject;
        mistParticle = mistEffectObj.GetComponent<ParticleSystem>();
        //backActive_Script = mistEffectObj.GetComponent<BackgroundActivation>();


        middleBossOBj = Obj_Storage.Storage_Data.GetMiddleBoss();
        middleBoss_Script = middleBossOBj.GetComponent<Enemy_MiddleBoss>();

        oneBossOBj = Obj_Storage.Storage_Data.GetBoss(1);
        oneBoss_Script = oneBossOBj.GetComponent<One_Boss>();
        isOneBossAlive = true;

        twoBossObj = Obj_Storage.Storage_Data.GetBoss(2);
        twoBoss_Script = twoBossObj.GetComponent<Two_Boss>();
        isTwoBossAlive = true;

        moaiObj = Obj_Storage.Storage_Data.GetBoss(3);
        moai_Script = moaiObj.GetComponent<Enemy_Moai>();
        isMoaiAlive = true;
		#endregion
	}

	// 毎フレーム更新
	void Update()
    {
        if (Game_Master.Management_In_Stage == Game_Master.CONFIGURATION_IN_STAGE.WIRELESS)
        {
            return;
        }

        if (!isNowOneBoss && !isNowTwoBoss && !isNowMoai)
        {
            frameCnt++;
        }

		DebugKeyUpdate();

		//中ボス撃破
		if (middleBoss_Script != null && isMiddleBossSkip)
        {
            if (middleBoss_Script.Is_Dead)
            {
                turning_frame = bigCoreNextGroupFrame; //←今爆発がでかいのでちょっと間を空けます
                frameCnt = bigCoreNextGroupFrame - 60;
                groupCnt = bigCoreNextGroupNum;
                isMiddleBossSkip = false;
            }
        }

        //第一ボス出現時に無線をONにする🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲
        if (groupCnt == bigCoreMK2GrouNum && frameCnt == turning_frame - 60f)
        {
            Wireless_sinario.Is_using_wireless = true;
        }

        //第二ボス出現時に無線をONにする🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲
        if (groupCnt == bigCoreMK3GroupNum && frameCnt == turning_frame - 60f)
        {
            isLastBossWireless = true;
            Wireless_sinario.Is_using_wireless = true;
        }

        //第一ボスを撃破したら間隔を詰める🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲
        if (oneBoss_Script != null)
        {
            if (oneBoss_Script.Is_Dead)
            {
                if (isOneBossAlive)
                {
                    if (backActive_Script)
                    {
                        backActive_Script.TransparencyChangeTrigger();
                        Wireless_sinario.Is_using_wireless = true;
                    }
                    isNowOneBoss = false;
                    isOneBossAlive = false;
                }
            }
        }

        if (moai_Script != null)
        {
            if (moai_Script.Is_Dead)
            {
                if (isMoaiAlive)
                {
                    isNowMoai = false;
                    isMoaiAlive = false;
                }
            }
        }

        //第二ボスを撃破🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲🔲
        if (twoBoss_Script != null)
        {
            if (twoBoss_Script.Is_Dead)
            {
                if (isTwoBossAlive)
                {
                    backActive_Script.TransparencyChangeTrigger();
                    isNowTwoBoss = false;
                    Wireless_sinario.Is_using_wireless = true;
                    isTwoBossAlive = false;

                }
            }
        }
		
        CreateEnemyGroup_01();
    }

	public bool GetLastBossWireless()
	{
		return isLastBossWireless;
	}

	public void SetBaculaDestroy(bool flag)
	{
		isBaculaDestroy = flag;

		if (isBaculaDestroy)
		{
			if (frameCnt < turning_frame)
			{
				frameCnt = turning_frame;
			}
			isBaculaDestroy = false;
		}
	}

	// 指定の敵グループを出す
	private void DebugKeyUpdate()
	{
		// ステージ移動キー
		if(Input.GetKey(KeyCode.R))
		{
			if (Input.GetKeyDown(KeyCode.H)) { Scene_Manager.Manager.Screen_Transition_To_Stage_01(); }
			if (Input.GetKeyDown(KeyCode.J)) { Scene_Manager.Manager.Screen_Transition_To_Stage_02(); }
			if (Input.GetKeyDown(KeyCode.K)) { Scene_Manager.Manager.Screen_Transition_To_Stage_03(); }
			if (Input.GetKeyDown(KeyCode.L)) { Scene_Manager.Manager.Screen_Transition_To_Stage_04(); }
			if (Input.GetKeyDown(KeyCode.B)) { Scene_Manager.Manager.Screen_Transition_To_Stage_05(); }
			if (Input.GetKeyDown(KeyCode.N)) { Scene_Manager.Manager.Screen_Transition_To_Stage_06(); }
			if (Input.GetKeyDown(KeyCode.M)) { Scene_Manager.Manager.Screen_Transition_To_Stage_07(); }
		}
		else
		{
			// 次の敵グループ
			if (Input.GetKeyDown(KeyCode.N)) { if (groupCnt < enemyGroups.Length - 1) frameCnt = turning_frame; }
			// ビッグコア
			if (Input.GetKeyDown(KeyCode.J)) { EnemyDebugNumberUpdate(EnemyType.BIGCORE, false); }
			// ビッグコア後
			if (Input.GetKeyDown(KeyCode.K)) { EnemyDebugNumberUpdate(EnemyType.BIGCOREENDGROUP, false); }
			// ビッグコアMK2
			if (Input.GetKeyDown(KeyCode.M)) { EnemyDebugNumberUpdate(EnemyType.BIGCOREMK2, false); }
			// ビッグコアMK2後
			if (Input.GetKeyDown(KeyCode.B)) { EnemyDebugNumberUpdate(EnemyType.BIGCOREMK2, true); }
			// モアイ
			if (Input.GetKeyDown(KeyCode.B) & Input.GetKey(KeyCode.H)) { EnemyDebugNumberUpdate(EnemyType.MOAI, false); }
			// モアイ後
			if (Input.GetKeyDown(KeyCode.B) & Input.GetKey(KeyCode.U)) { EnemyDebugNumberUpdate(EnemyType.MOAI, true); }
			// ビッグコアMK3
			if (Input.GetKeyDown(KeyCode.L)) { EnemyDebugNumberUpdate(EnemyType.BIGCOREMK3, false); isDebug = true; }
		}
	}

	//--------------------------------------------------------------------
	// 敵グループの種類の情報
	public enum EnemyType
	{
		NONE,
		UFO_GROUP,
		UFO_GROUP_NONESHOT,
		UFO_GROUP_FIVE,
		CLAMCHOWDER_GROUP_STRAIGHT,
		CLAMCHOWDER_GROUP_FOUR,
		CLAMCHOWDER_GROUP_FOURTRIANGLE,
		CLAMCHOWDER_GROUP_FOURTRIANGLE_B,
		CLAMCHOWDER_GROUP_FOURTRIANGLE_C,
		CLAMCHOWDER_GROUP_THREE,
		CLAMCHOWDER_GROUP_THREESTRAIGHT,
		CLAMCHOWDER_GROUP_TOPANDUNEDR,
		CLAMCHOWDER_GROUP_FIVE,
		CLAMCHOWDER_GROUP_TWOWAVEONLYDOWN,
		CLAMCHOWDER_GROUP_TWOWAVEONLYUP,
		CLAMCHOWDER_GROUP_TENSTRAIGHT,
		CLAMCHOWDER_GROUP_SEVEN,
		CLAMCHOWDER_GROUP_UPSEVENDIAGONAL,
		CLAMCHOWDER_GROUP_DOWNSEVENDIAGONAL,
		BEETLE_GROUP_THREE,
		BEETLE_GROUP_SEVEN,
		BEETLE_GROUP_FIVE,
		BIGCORE,
		BIGCOREMK2,
		BIGCOREMK3,
		BIGCOREENDGROUP,
		BEELZEBUB_GROUP_FOUR,
		BEELZEBUB_GROUP_FOURWIDE,
		BATTLESHIP,
		BATTLESHIP_TOPANDUNDER,
		BATTLESHIP_TOP,
		BATTLESHIP_UNDER,
		BACULA_GROUP_TWO,
		BACULA_GROUP_SIX,
		CLAMCHOWDER_GROUP_FOURVERTICALATTACK,
		CLAMCHOWDER_GROUP_FOURSTRAIGHT,
		BEELZEBUB_GROUP_TWOWIDE,
		BEELZEBUB_GROUP_EIGHTNORMAL,
		STARFISH,
		BOUNDMETEORS,
		MOAI,
		GAMECLEAR,
		STAGE_01,
		STAGE_02,
		STAGE_03,
		STAGE_04,
		STAGE_05,
		STAGE_06,
		STAGE_07,
	}

	// 敵グループを出現させる位置情報
	public enum CreatePos
	{
		L4, L3, L2, L1, L0, Lm1, Lm2, Lm3, Lm4,
		FOURGROUPL,
		R4, R3, R2, R1, R0, Rm1, Rm2, Rm3, Rm4,
		R0PX2Y081,
		R0PX2MY081,
	}

	// 敵グループの情報
	[System.Serializable]
	public struct EnemyGroup
	{
		public string enemyGroupName;
		public EnemyType enemyType;
		public CreatePos createPos;
		public bool isItem;
		public int nextGroupFrame;

		public EnemyGroup(string enemyGroupName, EnemyType enemyType, CreatePos createPos, bool isItem, int nextGroupFrame) : this()
		{
			this.enemyGroupName = enemyGroupName;
			this.enemyType = enemyType;
			this.createPos = createPos;
			this.isItem = isItem;
			this.nextGroupFrame = nextGroupFrame;
		}
	}

	// 敵グループの種類名を受け取り生成
	private GameObject CreateEnemy(EnemyType e, CreatePos p, bool isItem = false)
    {
        Vector3 pos = Vector3.zero;

        switch (p)
        {
            case CreatePos.FOURGROUPL:pos = createPos_FourGroupL.position; break;
			case CreatePos.R4: pos = createPosR4.position; break;
			case CreatePos.R3: pos = createPosR3.position; break;
			case CreatePos.R2: pos = createPosR2.position; break;
			case CreatePos.R1: pos = createPosR1.position; break;
			case CreatePos.R0: pos = createPosR0.position; break;
			case CreatePos.Rm1: pos = createPosRm1.position; break;
			case CreatePos.Rm2: pos = createPosRm2.position; break;
			case CreatePos.Rm3: pos = createPosRm3.position; break;
			case CreatePos.Rm4: pos = createPosRm4.position; break;
			case CreatePos.R0PX2Y081: pos = createPosR0.position + new Vector3(2.0f, 0.81f * 1.0f, 0.0f); break;
			case CreatePos.R0PX2MY081: pos = createPosR0.position + new Vector3(2.0f, 0.81f * -1.0f, 0.0f); break;
			default: pos = Vector3.zero; break;
        }

        switch (e)
        {
			case EnemyType.BIGCOREENDGROUP:
				if(groupFrameCheckDebugFlag) GroupFrameCheckDebug("中ボス後", groupCnt, turning_frame);
				break;

			case EnemyType.GAMECLEAR:
				Scene_Manager.Manager.Screen_Transition_To_Clear();
				break;

			case EnemyType.STAGE_01:
				Scene_Manager.Manager.Screen_Transition_To_Stage_01();
				break;

			case EnemyType.STAGE_02:
				Scene_Manager.Manager.Screen_Transition_To_Stage_02();
				break;

			case EnemyType.STAGE_03:
				Scene_Manager.Manager.Screen_Transition_To_Stage_03();
				break;

			case EnemyType.STAGE_04:
				Scene_Manager.Manager.Screen_Transition_To_Stage_04();
				break;

			case EnemyType.STAGE_05:
				Scene_Manager.Manager.Screen_Transition_To_Stage_05();
				break;

			case EnemyType.STAGE_06:
				Scene_Manager.Manager.Screen_Transition_To_Stage_06();
				break;

			case EnemyType.STAGE_07:
				Scene_Manager.Manager.Screen_Transition_To_Stage_07();
				break;

			case EnemyType.CLAMCHOWDER_GROUP_UPSEVENDIAGONAL:
				Instantiate(enemy_ClamChowder_Group_UpSevenDiagonal, createPosR0.position, transform.rotation);
				break;

			case EnemyType.CLAMCHOWDER_GROUP_DOWNSEVENDIAGONAL:
				Instantiate(enemy_ClamChowder_Group_DownSevenDiagonal, createPosR0.position, transform.rotation);
				break;

			case EnemyType.MOAI:
				if (groupFrameCheckDebugFlag)
				{
					GroupFrameCheckDebug("モアイ", groupCnt, turning_frame);
					GroupFrameCheckDebug("モアイ後", groupCnt + 1, turning_frame + enemyGroups[groupCnt].nextGroupFrame);
				}
				else
				{
					GameObject moai = Obj_Storage.Storage_Data.Moai.Active_Obj();
					moai.transform.position = new Vector3(15.44f, -17.0f, 0.0f);
					Wireless_sinario.Is_using_wireless = true;
					isNowMoai = true;
				}
				break;

			case EnemyType.BOUNDMETEORS:
				saveEnemyObj = Obj_Storage.Storage_Data.boundMeteors.Active_Obj();
				saveEnemyObj.transform.position = createMeteorPosR0.transform.position;
				//Instantiate(Enemy_BoundMeteors, createMeteorPosR0.transform.position, transform.rotation);
				break;

			case EnemyType.STARFISH:
				Instantiate(enemy_Star_Fish_Spowner, pos, transform.rotation);
				break;

			case EnemyType.UFO_GROUP_NONESHOT:
				//Object_Pooling pEnemy_UFO_Group = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group_NoneShot") as GameObject, 1, "enemy_UFO_Group");
				//GameObject enemy_UFO_Group = pEnemy_UFO_Group.Active_Obj();
				GameObject enemy_UFO_Group = Obj_Storage.Storage_Data.enemy_UFO_Group_NoneShot.Active_Obj();
                enemy_UFO_Group.transform.position = pos + new Vector3(8.5f, 0, 0);
                enemy_UFO_Group.transform.rotation = transform.rotation;
                //Object_Pooling pEnemy_UFO_Group = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group_NoneShot") as GameObject, 1, "enemy_UFO_Group");
                //GameObject enemy_UFO_Group = Obj_Storage.Storage_Data.enemy_UFO_Group_NoneShot.Active_Obj();
                //enemy_UFO_Group.transform.position = createPosR3.transform.position + new Vector3(8.5f, 0, 0);
                //enemy_UFO_Group.transform.rotation = transform.rotation;
                break;

            case EnemyType.UFO_GROUP_FIVE:
				//Object_Pooling pEnemy_UFO_Group_Five = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group_Five") as GameObject, 1, "enemy_UFO_Group");
				//GameObject enemy_UFO_Group_Five = pEnemy_UFO_Group_Five.Active_Obj();
				GameObject save_enemy_UFO_Group_Five = Instantiate(enemy_UFO_Group_Five, transform.position, transform.rotation);
				save_enemy_UFO_Group_Five.transform.position = pos + new Vector3(8.5f, 0, 0);
				save_enemy_UFO_Group_Five.transform.rotation = transform.rotation;
                //Object_Pooling pEnemy_UFO_Group = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group_NoneShot") as GameObject, 1, "enemy_UFO_Group");
                //GameObject enemy_UFO_Group = Obj_Storage.Storage_Data.enemy_UFO_Group_NoneShot.Active_Obj();
                //enemy_UFO_Group.transform.position = createPosR3.transform.position + new Vector3(8.5f, 0, 0);
                //enemy_UFO_Group.transform.rotation = transform.rotation;
                break;

            case EnemyType.UFO_GROUP:
                //Object_Pooling pEnemy_UFO_Group = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group_NoneShot") as GameObject, 1, "enemy_UFO_Group");
                //GameObject enemy_UFO_Group = pEnemy_UFO_Group.Active_Obj();
                GameObject enemy_UFO_Group2 = Obj_Storage.Storage_Data.enemy_UFO_Group.Active_Obj();
                enemy_UFO_Group2.transform.position = pos + new Vector3(8.5f, 0, 0);
                enemy_UFO_Group2.transform.rotation = transform.rotation;
                //Object_Pooling pEnemy_UFO_Group = new Object_Pooling(Resources.Load("Enemy/Enemy_UFO_Group_NoneShot") as GameObject, 1, "enemy_UFO_Group");
                //GameObject enemy_UFO_Group = Obj_Storage.Storage_Data.enemy_UFO_Group_NoneShot.Active_Obj();
                //enemy_UFO_Group.transform.position = createPosR3.transform.position + new Vector3(8.5f, 0, 0);
                //enemy_UFO_Group.transform.rotation = transform.rotation;
                break;

            case EnemyType.CLAMCHOWDER_GROUP_STRAIGHT:
                saveEnemyObj = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Straight.Active_Obj();
                saveEnemyObj.transform.position = pos;
                //Instantiate(enemy_Clamchowder_Group_Straight, pos, transform.rotation);
                break;

            case EnemyType.CLAMCHOWDER_GROUP_FOUR:
                if (!isItem)
                {
                    saveEnemyObj = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Four.Active_Obj();
                    saveEnemyObj.transform.position = createPos_FourGroupL.transform.position;
                    //Instantiate(enemy_ClamChowder_Group_Four_NoItem, pos, transform.rotation);
                }
                else
                {
                    saveEnemyObj = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Four_NoItem.Active_Obj();
                    saveEnemyObj.transform.position = createPos_FourGroupL.transform.position;
                    //GameObject saveObj = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Four_NoItem.Active_Obj();
                    //GameObject saveObjB = Instantiate(enemy_ClamChowder_Group_Four, createPos_FourGroupL.transform.position, transform.rotation);
                    //saveObjB.transform.position = createPos_FourGroupL.transform.position;
                }
                break;

            case EnemyType.CLAMCHOWDER_GROUP_FIVE:
                saveEnemyObj = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Five_NoItem.Active_Obj();
                saveEnemyObj.transform.position= createPos_FourGroupL.transform.position;
                //Instantiate(enemy_ClamChowder_Group_Five_NoItem, createPos_FourGroupL.transform.position, transform.rotation);
                break;

            case EnemyType.CLAMCHOWDER_GROUP_FOURTRIANGLE:
                if (!isItem)
                {
                    saveEnemyObj = Obj_Storage.Storage_Data.enemy_ClamChowder_FourTriangle_NoItem.Active_Obj();
                    saveEnemyObj.transform.position = createPos_FourGroupL.transform.position;
                    //Instantiate(enemy_ClamChowder_Group_FourTriangle_NoItem, createPos_FourGroupL.transform.position, transform.rotation);
                }
                else
                {
                    saveEnemyObj = Obj_Storage.Storage_Data.enemy_ClamChowder_FourTriangle.Active_Obj();
                    saveEnemyObj.transform.position = createPos_FourGroupL.transform.position;
                    //Instantiate(enemy_ClamChowder_Group_FourTriangle, createPos_FourGroupL.transform.position, transform.rotation);
                }
                break;

            case EnemyType.CLAMCHOWDER_GROUP_FOURTRIANGLE_B:
                Instantiate(enemy_ClamChowder_Group_FourTriangle_B, createPos_FourGroupL.transform.position, transform.rotation);
                break;

            case EnemyType.CLAMCHOWDER_GROUP_FOURTRIANGLE_C:
                Instantiate(enemy_ClamChowder_Group_FourTriangle_C, createPos_FourGroupL.transform.position, transform.rotation);
                break;

            case EnemyType.CLAMCHOWDER_GROUP_THREE:
                if (!isItem)
                {
                    saveEnemyObj = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Three.Active_Obj();
                    saveEnemyObj.transform.position = createPos_FourGroupL.transform.position+pos;
                    //Instantiate(enemy_ClamChowder_Group_FourTriangle_NoItem, pos, transform.rotation);
                }
                else
                {
                    saveEnemyObj = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Three_Item.Active_Obj();
                    saveEnemyObj.transform.position = createPos_FourGroupL.transform.position + pos;
                    //Instantiate(enemy_ClamChowder_Group_Three_Item, createPos_FourGroupL.transform.position, transform.rotation);
                }
                break;

			case EnemyType.CLAMCHOWDER_GROUP_THREESTRAIGHT:
                
                Instantiate(enemy_ClamChowder_Group_ThreeStraight, pos, transform.rotation);
                break;

            case EnemyType.CLAMCHOWDER_GROUP_FOURSTRAIGHT:
                Instantiate(enemy_ClamChowder_Group_FourVerticalStraight, pos, transform.rotation);
                break;

            case EnemyType.CLAMCHOWDER_GROUP_TOPANDUNEDR:
                saveEnemyObj = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Two_Top.Active_Obj();
                saveEnemyObj.transform.position = createPos_FourGroupL.transform.position;

                saveEnemyObj = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Two_Under.Active_Obj();
                saveEnemyObj.transform.position = createPos_FourGroupL.transform.position;
                //Instantiate(enemy_ClamChowder_Group_Two_Top, createPos_FourGroupL.transform.position, transform.rotation);
                //Instantiate(enemy_ClamChowder_Group_Two_Under, createPos_FourGroupL.transform.position, transform.rotation);
                break;

            case EnemyType.CLAMCHOWDER_GROUP_TWOWAVEONLYUP:
                saveEnemyObj = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_TwoWaveOnlyUp.Active_Obj();
                saveEnemyObj.transform.position = pos;
				//Instantiate(enemy_ClamChowder_Group_TwoWaveOnlyUp, pos, transform.rotation);
				break;

            case EnemyType.CLAMCHOWDER_GROUP_TWOWAVEONLYDOWN:
                saveEnemyObj = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_TwoWaveOnlyDown.Active_Obj();
                saveEnemyObj.transform.position = pos;
				//Instantiate(enemy_ClamChowder_Group_TwoWaveOnlyDown, pos, transform.rotation);
				break;

			case EnemyType.CLAMCHOWDER_GROUP_TENSTRAIGHT:
				//saveEnemyObj = Instantiate(enemy_ClamChowder_Group_TenStraight, pos, transform.rotation);
				saveEnemyObj = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_TenStraight.Active_Obj();
				saveEnemyObj.transform.position = pos;
				group_Script = saveEnemyObj.GetComponent<EnemyGroupManage>();
				group_Script.isItemDrop = isItem;
				break;

            case EnemyType.CLAMCHOWDER_GROUP_FOURVERTICALATTACK:
                Instantiate(enemy_ClamChowder_Group_FourVerticalAttack, createPos_FourGroupL.transform.position, transform.rotation);
                break;

            case EnemyType.CLAMCHOWDER_GROUP_SEVEN:
                saveEnemyObj = Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Seven.Active_Obj();
                saveEnemyObj.transform.position = pos;
                //Instantiate(enemy_ClamChowder_Group_Seven, pos, transform.rotation);
                return saveEnemyObj;

            case EnemyType.BEETLE_GROUP_THREE:
                saveEnemyObj = Obj_Storage.Storage_Data.enemy_BeetleGroup_Three.Active_Obj();
                saveEnemyObj.transform.position = new Vector3(15, -8, 0);
                //GameObject beetleGroup_Three = Instantiate(enemy_Beetle_Group_Three, createPosRm3.transform.position, transform.rotation);
                //beetleGroup_Three.transform.position = new Vector3(15, -8, 0);
                break;

            case EnemyType.BEETLE_GROUP_FIVE:
                GameObject beetleGroup_Five = Instantiate(enemy_Beetle_Group_Five, createPosRm3.transform.position, transform.rotation);
                beetleGroup_Five.transform.position = new Vector3(15, -8, 0);
                break;

            case EnemyType.BEETLE_GROUP_SEVEN:
                saveEnemyObj = Obj_Storage.Storage_Data.enemy_Beetle_Group_Seven.Active_Obj();
                saveEnemyObj.transform.position = new Vector3(13, -8, 0);
                //GameObject beetleGroup_Seven = Instantiate(enemy_Beetle_Group_Seven, createPosRm3.transform.position, transform.rotation);
                //beetleGroup_Seven.transform.position = new Vector3(15, -8, 0);
                break;

            case EnemyType.BEELZEBUB_GROUP_FOURWIDE:
				if (!isItem)
				{
                    //saveEnemyObj = Obj_Storage.Storage_Data.enemy_Beelzebub_Group_FourWide.Active_Obj();
                    //saveEnemyObj.transform.position = pos;
                    Instantiate(enemy_Beelzebub_Group_FourWide, pos, transform.rotation);
                }
				else
				{
                    //saveEnemyObj = Obj_Storage.Storage_Data.enemy_Beelzebub_Group_FourWide_Item.Active_Obj();
                    //saveEnemyObj.transform.position = pos;
                    Instantiate(enemy_Beelzebub_Group_FourWide_Item, pos, transform.rotation);
                }
                break;

            case EnemyType.BEELZEBUB_GROUP_TWOWIDE:
                Instantiate(enemy_Beelzebub_Group_TwoWide, pos, transform.rotation);
                break;


            case EnemyType.BACULA_GROUP_TWO:
                Instantiate(enemy_Bacula_Group_Two, createBaculaGroupPos.transform.position, transform.rotation);
                break;

            case EnemyType.BACULA_GROUP_SIX:
                Instantiate(enemy_Bacula_Group_Six, createBaculaGroupPos.transform.position, transform.rotation);
                break;

            case EnemyType.BATTLESHIP:
                GameObject Battle_Ship1 = Instantiate(enemy_BattleShip, createPosR0.position, enemy_BattleShip.transform.rotation);
                Battle_Ship1.transform.position = createPosR0.position;
                BattleshipType_Enemy b1 = Battle_Ship1.GetComponent<BattleshipType_Enemy>();
                b1.is_sandwich = false;
                b1.Is_up = false;
                //            saveEnemyObj = Obj_Storage.Storage_Data.BattleShipType_Enemy.Active_Obj();
                //            saveEnemyObj.transform.position = pos;
                ////GameObject Battle_Ship1 = Instantiate(enemy_BattleShip, pos, enemy_BattleShip.transform.rotation);
                //BattleshipType_Enemy b1 = saveEnemyObj.GetComponent<BattleshipType_Enemy>();
                //b1.is_sandwich = false;
                //b1.Is_up = false;
                break;

			case EnemyType.BATTLESHIP_TOP:
                GameObject Battle_Ship6 = Instantiate(enemy_BattleShip, createBattleShipPos.transform.position, enemy_BattleShip.transform.rotation);
                BattleshipType_Enemy b6 = Battle_Ship6.GetComponent<BattleshipType_Enemy>();
                b6.Is_up = false;
                //            saveEnemyObj = Obj_Storage.Storage_Data.BattleShipType_Enemy.Active_Obj();
                //            saveEnemyObj.transform.position = createBattleShipPos.transform.position;
                //            //GameObject Battle_Ship4 = Instantiate(enemy_BattleShip, createBattleShipPos.transform.position, enemy_BattleShip.transform.rotation);
                //BattleshipType_Enemy b4 = saveEnemyObj.GetComponent<BattleshipType_Enemy>();
                //            b4.is_sandwich = true;
                //            b4.Is_up = false;
                break;

            case EnemyType.BATTLESHIP_UNDER:
                GameObject Battle_Ship2 = Instantiate(enemy_BattleShip, createBattleShipPos.transform.position, enemy_BattleShip.transform.rotation);
                BattleshipType_Enemy b2 = Battle_Ship2.GetComponent<BattleshipType_Enemy>();
                b2.Is_up = true;
                //saveEnemyObj = Obj_Storage.Storage_Data.BattleShipType_Enemy.Active_Obj();
                //saveEnemyObj.transform.position = createBattleShipPos.transform.position;
                ////GameObject Battle_Ship5 = Instantiate(enemy_BattleShip, createBattleShipPos.transform.position, enemy_BattleShip.transform.rotation);
                //BattleshipType_Enemy b5 = saveEnemyObj.GetComponent<BattleshipType_Enemy>();
                //b5.is_sandwich = true;
                //b5.Is_up = true;
                break;

            case EnemyType.BATTLESHIP_TOPANDUNDER:
                CreateEnemy(EnemyType.BATTLESHIP_TOP, CreatePos.L0, false);
                CreateEnemy(EnemyType.BATTLESHIP_UNDER, CreatePos.L0, false);
                //saveEnemyObj = Obj_Storage.Storage_Data.BattleShipType_Enemy.Active_Obj();
                //BattleshipType_Enemy b3 = saveEnemyObj.GetComponent<BattleshipType_Enemy>();
                //b3.is_sandwich = true;
                //b3.Is_up = false;
                //saveEnemyObj.transform.position = new Vector3(25, -5, 0);

                //saveEnemyObj.transform.position = createBattleShipPos.transform.position;
                //saveEnemyObj.transform.position = b3.defautpos;

                //GameObject Battle_Ship3 = Instantiate(enemy_BattleShip, createBattleShipPos.transform.position, enemy_BattleShip.transform.rotation);

                //saveEnemyObj = Obj_Storage.Storage_Data.BattleShipType_Enemy.Active_Obj();
                //BattleshipType_Enemy b2 = saveEnemyObj.GetComponent<BattleshipType_Enemy>();
                //b2.is_sandwich = true;
                //saveEnemyObj.transform.position = b2.defautpos;
                //GameObject Battle_Ship2 = Instantiate(enemy_BattleShip, createBattleShipPos.transform.position, enemy_BattleShip.transform.rotation);
                //b2.Is_up = true;
                //            saveEnemyObj.transform.position = new Vector3(25, 5, 0);
                break;


			case EnemyType.BIGCORE:
				if (groupFrameCheckDebugFlag)
				{
					GroupFrameCheckDebug("中ボス", groupCnt, turning_frame);
				}
				else
				{
					GameObject Boss_Middle = Obj_Storage.Storage_Data.Boss_Middle.Active_Obj();
					saveEnemyObj = Boss_Middle;
					Boss_Middle.transform.position = createMiddleBossPos.transform.position;
					Boss_Middle.transform.rotation = transform.rotation;
				}
				break;

            case EnemyType.BIGCOREMK2:
				if (groupFrameCheckDebugFlag)
				{
					GroupFrameCheckDebug("ボス1", groupCnt, turning_frame);
					GroupFrameCheckDebug("ボス1後", groupCnt + 1, turning_frame + enemyGroups[groupCnt].nextGroupFrame);
				}
				else
				{
					GameObject Boss_01 = Obj_Storage.Storage_Data.Boss_1.Active_Obj();
					Boss_01.transform.position = new Vector3(10.0f, 0.0f, 0.0f);
					GameObject mistSaveObj = Instantiate(mistEffectObj, transform.position, transform.rotation);
					mistSaveObj.transform.position = new Vector3(0, 0, 3);
					mistParticle = mistSaveObj.GetComponent<ParticleSystem>();
					backActive_Script = mistSaveObj.GetComponent<BackgroundActivation>();
					mistParticle.Play();
					backActive_Script.TransparencyChangeTrigger();
					isNowOneBoss = true;
				}
                break;

			case EnemyType.BIGCOREMK3:
				if (groupFrameCheckDebugFlag)
				{
					GroupFrameCheckDebug("ボス2", groupCnt, turning_frame);
				}
				else
				{
					GameObject Boss_02 = Obj_Storage.Storage_Data.Boss_2.Active_Obj();
					Boss_02.transform.position = new Vector3(13.0f, 0.0f, 0.0f);
					isNowTwoBoss = true;

					//GameObject mistSaveObj = Instantiate(mistEffectObj, transform.position, transform.rotation);
					//backActive_Script.TransparencyChangeTrigger();
					if (isDebug)
					{
						GameObject mistSaveObj = Instantiate(mistEffectObj, transform.position, transform.rotation);
						backActive_Script = mistSaveObj.GetComponent<BackgroundActivation>();

						mistEffectObj.transform.position = new Vector3(0, 0, 3);
						mistParticle = mistSaveObj.GetComponent<ParticleSystem>();
						mistParticle.Play();
					}
					backActive_Script.TransparencyChangeTrigger();
				}
				break;


			case EnemyType.BEELZEBUB_GROUP_FOUR:
                //Instantiate(enemy_Beelzebub_Group_EightNormal_Item, pos, transform.rotation);
                break;

            case EnemyType.BEELZEBUB_GROUP_EIGHTNORMAL:
                Instantiate(enemy_Beelzebub_Group_EightNormal_Item, pos, transform.rotation);
                break;

			default:
                break;
        }

        return null;
    }

	// 出現する敵グループ全体の情報
	public EnemyGroup[] enemyGroups = new EnemyGroup[150]
	{
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 120),
		new EnemyGroup("円盤上10", EnemyType.UFO_GROUP_NONESHOT, CreatePos.R3, true, 240),
		new EnemyGroup("円盤下10", EnemyType.UFO_GROUP_NONESHOT, CreatePos.Rm3, true, 300),
		new EnemyGroup("闘牛斜め配置中央アイテム", EnemyType.CLAMCHOWDER_GROUP_FOUR, CreatePos.L0, true, 390),
		new EnemyGroup("円盤上", EnemyType.UFO_GROUP_NONESHOT, CreatePos.R3, true, 0),
		new EnemyGroup("円盤下", EnemyType.UFO_GROUP_NONESHOT, CreatePos.Rm3, true, 240),
		new EnemyGroup("円盤上狭", EnemyType.UFO_GROUP_NONESHOT, CreatePos.R1, true, 0),
		new EnemyGroup("円盤下狭", EnemyType.UFO_GROUP_NONESHOT, CreatePos.Rm1, true, 240),
		new EnemyGroup("闘牛突進三角B", EnemyType.CLAMCHOWDER_GROUP_FOURTRIANGLE_B, CreatePos.L0, true, 240),
		new EnemyGroup("闘牛突進三角C", EnemyType.CLAMCHOWDER_GROUP_FOURTRIANGLE_C, CreatePos.L0, false, 240),
		new EnemyGroup("闘牛縦3アイテム", EnemyType.CLAMCHOWDER_GROUP_THREE, CreatePos.L0, true, 45),
		new EnemyGroup("闘牛上2下2", EnemyType.CLAMCHOWDER_GROUP_TOPANDUNEDR, CreatePos.FOURGROUPL, false, 45),
		new EnemyGroup("闘牛縦3アイテム", EnemyType.CLAMCHOWDER_GROUP_THREE, CreatePos.L0, true, 45),
		new EnemyGroup("闘牛上2下2", EnemyType.CLAMCHOWDER_GROUP_TOPANDUNEDR, CreatePos.FOURGROUPL, false, 45),
		new EnemyGroup("闘牛縦7", EnemyType.CLAMCHOWDER_GROUP_FIVE, CreatePos.FOURGROUPL, false, 45),
		new EnemyGroup("闘牛縦7", EnemyType.CLAMCHOWDER_GROUP_FIVE, CreatePos.FOURGROUPL, false, 45),
		new EnemyGroup("闘牛縦7", EnemyType.CLAMCHOWDER_GROUP_FIVE, CreatePos.FOURGROUPL, false, 360),
		new EnemyGroup("🔲🔲🔲ビッグコア🔲🔲🔲", EnemyType.BIGCORE, CreatePos.L0, false, 180),
		new EnemyGroup("闘牛上2", EnemyType.CLAMCHOWDER_GROUP_TWOWAVEONLYDOWN, CreatePos.R4, false, 0),
		new EnemyGroup("闘牛下2", EnemyType.CLAMCHOWDER_GROUP_TWOWAVEONLYUP, CreatePos.Rm4, false, 180),
		new EnemyGroup("闘牛上2", EnemyType.CLAMCHOWDER_GROUP_TWOWAVEONLYDOWN, CreatePos.R4, false, 0),
		new EnemyGroup("闘牛下2", EnemyType.CLAMCHOWDER_GROUP_TWOWAVEONLYUP, CreatePos.Rm4, false, 180),
		new EnemyGroup("闘牛上2", EnemyType.CLAMCHOWDER_GROUP_TWOWAVEONLYDOWN, CreatePos.R4, false, 0),
		new EnemyGroup("闘牛下2", EnemyType.CLAMCHOWDER_GROUP_TWOWAVEONLYUP, CreatePos.Rm4, false, 240),
		new EnemyGroup("闘牛上2", EnemyType.CLAMCHOWDER_GROUP_TWOWAVEONLYDOWN, CreatePos.R4, false, 0),
		new EnemyGroup("闘牛下2", EnemyType.CLAMCHOWDER_GROUP_TWOWAVEONLYUP, CreatePos.Rm4, false, 180),
		new EnemyGroup("闘牛上2", EnemyType.CLAMCHOWDER_GROUP_TWOWAVEONLYDOWN, CreatePos.R4, false, 0),
		new EnemyGroup("闘牛下2", EnemyType.CLAMCHOWDER_GROUP_TWOWAVEONLYUP, CreatePos.Rm4, false, 510),
		new EnemyGroup("ビッグコア後2", EnemyType.BIGCOREENDGROUP, CreatePos.L0, false, 0),
		new EnemyGroup("ハエ2", EnemyType.BEELZEBUB_GROUP_TWOWIDE, CreatePos.R0, true, 270),
		new EnemyGroup("ビートル3", EnemyType.BEETLE_GROUP_THREE, CreatePos.L0, false, 300),
		new EnemyGroup("円盤上射撃", EnemyType.UFO_GROUP, CreatePos.R3, true, 0),
		new EnemyGroup("円盤下射撃", EnemyType.UFO_GROUP, CreatePos.Rm3, true, 360),
		new EnemyGroup("戦艦上下(現在停止中)", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("戦艦", EnemyType.BATTLESHIP, CreatePos.R0, false, 0),
		new EnemyGroup("闘牛10直進上", EnemyType.CLAMCHOWDER_GROUP_TENSTRAIGHT, CreatePos.R3, true, 0),
		new EnemyGroup("闘牛10直進下", EnemyType.CLAMCHOWDER_GROUP_TENSTRAIGHT, CreatePos.Rm3, true, 180),
		new EnemyGroup("闘牛10直進上", EnemyType.CLAMCHOWDER_GROUP_TENSTRAIGHT, CreatePos.R3, true, 0),
		new EnemyGroup("闘牛10直進下", EnemyType.CLAMCHOWDER_GROUP_TENSTRAIGHT, CreatePos.Rm3, true, 90),
		new EnemyGroup("戦艦上下", EnemyType.BATTLESHIP_TOPANDUNDER, CreatePos.R0, false, 150),
		new EnemyGroup("闘牛縦3直進", EnemyType.CLAMCHOWDER_GROUP_THREESTRAIGHT, CreatePos.R0, false, 120),
		new EnemyGroup("闘牛縦3直進", EnemyType.CLAMCHOWDER_GROUP_THREESTRAIGHT, CreatePos.R0, false, 120),
		new EnemyGroup("闘牛縦3直進", EnemyType.CLAMCHOWDER_GROUP_THREESTRAIGHT, CreatePos.R0, false, 210),
		new EnemyGroup("闘牛縦7", EnemyType.CLAMCHOWDER_GROUP_SEVEN, CreatePos.R0, true, 45),
		new EnemyGroup("闘牛縦7", EnemyType.CLAMCHOWDER_GROUP_SEVEN, CreatePos.R0, true, 180),
		new EnemyGroup("🔲🔲🔲🔲🔲ビッグコアマーク2🔲🔲🔲🔲🔲", EnemyType.BIGCOREMK2, CreatePos.L0, false, 120),
		new EnemyGroup("ヒトデ12", EnemyType.STARFISH, CreatePos.L0, true, 600),
		new EnemyGroup("隕石20", EnemyType.BOUNDMETEORS, CreatePos.L0, false, 210),
		new EnemyGroup("バキュラ6", EnemyType.BACULA_GROUP_SIX, CreatePos.R0, false, 360),
		new EnemyGroup("隕石20", EnemyType.BOUNDMETEORS, CreatePos.L0, false, 300),
		new EnemyGroup("隕石20", EnemyType.BOUNDMETEORS, CreatePos.L0, false, 270),
		new EnemyGroup("🔲🔲🔲モアイ🔲🔲🔲", EnemyType.MOAI, CreatePos.L0, false, 120),
		new EnemyGroup("ヒトデ12", EnemyType.STARFISH, CreatePos.L0, true, 600),
		new EnemyGroup("円盤上10狭射撃", EnemyType.UFO_GROUP, CreatePos.R1, true, 0),
		new EnemyGroup("円盤下10射撃", EnemyType.UFO_GROUP, CreatePos.Rm3, true, 75),
		new EnemyGroup("円盤上10射撃", EnemyType.UFO_GROUP, CreatePos.R3, true, 0),
		new EnemyGroup("円盤下10狭射撃", EnemyType.UFO_GROUP, CreatePos.Rm1, true, 75),
		new EnemyGroup("円盤上10狭射撃", EnemyType.UFO_GROUP, CreatePos.R1, true, 0),
		new EnemyGroup("円盤下10射撃", EnemyType.UFO_GROUP, CreatePos.Rm3, true, 75),
		new EnemyGroup("円盤上10射撃", EnemyType.UFO_GROUP, CreatePos.R3, true, 0),
		new EnemyGroup("円盤下10狭射撃", EnemyType.UFO_GROUP, CreatePos.Rm1, true, 120),
		new EnemyGroup("戦艦", EnemyType.BATTLESHIP, CreatePos.R0, false, 210),
		new EnemyGroup("円盤上10射撃", EnemyType.UFO_GROUP, CreatePos.R4, true, 0),
		new EnemyGroup("円盤下10射撃", EnemyType.UFO_GROUP, CreatePos.Rm4, true, 120),
		new EnemyGroup("戦艦", EnemyType.BATTLESHIP, CreatePos.R0, false, 0),
		new EnemyGroup("円盤上10射撃", EnemyType.UFO_GROUP, CreatePos.R4, true, 0),
		new EnemyGroup("円盤下10射撃", EnemyType.UFO_GROUP, CreatePos.Rm4, true, 120),
		new EnemyGroup("円盤上10射撃", EnemyType.UFO_GROUP, CreatePos.R4, true, 0),
		new EnemyGroup("円盤下10射撃", EnemyType.UFO_GROUP, CreatePos.Rm4, true, 120),
		new EnemyGroup("円盤上10射撃", EnemyType.UFO_GROUP, CreatePos.R4, true, 0),
		new EnemyGroup("円盤下10射撃", EnemyType.UFO_GROUP, CreatePos.Rm4, true, 420),
		new EnemyGroup("バキュラ2", EnemyType.BACULA_GROUP_TWO, CreatePos.R0, false, 120),
		new EnemyGroup("ハエ8", EnemyType.BEELZEBUB_GROUP_EIGHTNORMAL, CreatePos.R0, false, 180),
		new EnemyGroup("バキュラ2", EnemyType.BACULA_GROUP_TWO, CreatePos.R0, false, 270),
		new EnemyGroup("闘牛左上斜め配置7射撃", EnemyType.CLAMCHOWDER_GROUP_UPSEVENDIAGONAL, CreatePos.L0, false, 40),
		new EnemyGroup("闘牛左下斜め配置7射撃", EnemyType.CLAMCHOWDER_GROUP_DOWNSEVENDIAGONAL, CreatePos.L0, false, 40),
		new EnemyGroup("闘牛左上斜め配置7射撃", EnemyType.CLAMCHOWDER_GROUP_UPSEVENDIAGONAL, CreatePos.L0, false, 40),
		new EnemyGroup("闘牛左下斜め配置7射撃", EnemyType.CLAMCHOWDER_GROUP_DOWNSEVENDIAGONAL, CreatePos.L0, false, 40),
		new EnemyGroup("闘牛左上斜め配置7射撃", EnemyType.CLAMCHOWDER_GROUP_UPSEVENDIAGONAL, CreatePos.L0, false, 40),
		new EnemyGroup("闘牛左下斜め配置7射撃", EnemyType.CLAMCHOWDER_GROUP_DOWNSEVENDIAGONAL, CreatePos.L0, false, 15),
		new EnemyGroup("戦艦上下", EnemyType.BATTLESHIP_TOPANDUNDER, CreatePos.L0, false, 270),
		new EnemyGroup("闘牛10", EnemyType.CLAMCHOWDER_GROUP_TENSTRAIGHT, CreatePos.R0, false, 0),
		new EnemyGroup("闘牛10", EnemyType.CLAMCHOWDER_GROUP_TENSTRAIGHT, CreatePos.R0PX2Y081, false, 0),
		new EnemyGroup("闘牛10", EnemyType.CLAMCHOWDER_GROUP_TENSTRAIGHT, CreatePos.R0PX2MY081, false, 115),
		new EnemyGroup("闘牛10", EnemyType.CLAMCHOWDER_GROUP_TENSTRAIGHT, CreatePos.R0, false, 0),
		new EnemyGroup("闘牛10", EnemyType.CLAMCHOWDER_GROUP_TENSTRAIGHT, CreatePos.R0PX2Y081, false, 0),
		new EnemyGroup("闘牛10", EnemyType.CLAMCHOWDER_GROUP_TENSTRAIGHT, CreatePos.R0PX2MY081, false, 115),
		new EnemyGroup("闘牛10", EnemyType.CLAMCHOWDER_GROUP_TENSTRAIGHT, CreatePos.R0, false, 0),
		new EnemyGroup("闘牛10", EnemyType.CLAMCHOWDER_GROUP_TENSTRAIGHT, CreatePos.R0PX2Y081, false, 0),
		new EnemyGroup("闘牛10", EnemyType.CLAMCHOWDER_GROUP_TENSTRAIGHT, CreatePos.R0PX2MY081, false, 115),
		new EnemyGroup("円盤上10狭射撃", EnemyType.UFO_GROUP, CreatePos.R1, true, 0),
		new EnemyGroup("円盤下10狭射撃", EnemyType.UFO_GROUP, CreatePos.Rm1, true, 90),
		new EnemyGroup("ビートル7", EnemyType.BEETLE_GROUP_SEVEN, CreatePos.R0, false, 15),
		new EnemyGroup("闘牛縦4突進", EnemyType.CLAMCHOWDER_GROUP_FOURVERTICALATTACK, CreatePos.R0, false, 45),
		new EnemyGroup("円盤上10射撃", EnemyType.UFO_GROUP, CreatePos.R4, true, 0),
		new EnemyGroup("円盤下10射撃", EnemyType.UFO_GROUP, CreatePos.Rm4, true, 60),
		new EnemyGroup("闘牛縦4突進", EnemyType.CLAMCHOWDER_GROUP_FOURVERTICALATTACK, CreatePos.R0, false, 60),
		new EnemyGroup("円盤上10射撃", EnemyType.UFO_GROUP, CreatePos.R3, true, 0),
		new EnemyGroup("円盤下10射撃", EnemyType.UFO_GROUP, CreatePos.Rm3, true, 360),
		new EnemyGroup("🔲🔲🔲🔲🔲ビッグコアマーク3🔲🔲🔲🔲🔲", EnemyType.BIGCOREMK3, CreatePos.L0, false, 120),
		new EnemyGroup("ゲームクリア", EnemyType.GAMECLEAR, CreatePos.L0, false, 10000),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
		new EnemyGroup("None", EnemyType.NONE, CreatePos.L0, false, 0),
	};

	//敵を出す関数
	private void CreateEnemyGroup_01()
    {
		if (Is_A_Specified_Frame(turning_frame))
		{
			do
			{
				PreviousCount = frameCnt;
				CreateEnemy(enemyGroups[groupCnt].enemyType, enemyGroups[groupCnt].createPos, enemyGroups[groupCnt].isItem);
				Next_Condition(enemyGroups[groupCnt].nextGroupFrame);
				nextEnemy = enemyGroups[groupCnt].enemyGroupName;
			}
			// 次のフレーム経過が0以下の時繰り返し
			while (enemyGroups[groupCnt-1].nextGroupFrame <= 0);
		}
	}
	
    /// <summary>
    /// 出現フレームと経過フレームが一致またはそれ以上の時有効
    /// </summary>
    /// <param name="specified_frame"> 指定フレーム </param>
    /// <returns > あっているか </returns>
    private bool Is_A_Specified_Frame(int specified_frame)
    {
        return frameCnt >= specified_frame && specified_frame >= PreviousCount;
    }

	// 次の敵グループが出現するフレームに変更
    private void Next_Condition(int add_frame)
    {
        groupCnt++;
        turning_frame += add_frame;
    }
}
