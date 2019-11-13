//作成日2019/07/30
// 一面のボス本番
// 作成者:諸岡勇樹
/*
 * 2019/07/30　グリッド移動の適応
 * 2019/08/02　ボスにレーザー追加
 * 2019/08/14　シェーダーでAnimation
 * 2019/08/27　タイムラインで一部制御
 * 2019/08/28　シャッター制御
 * 2019/09/26　最終的に使わなくなったコードの完全削除
 * 2019/10/17　レーザーの軽量に伴うスクリプトの変更
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;
using UnityEngine.Playables;

public class One_Boss : character_status
{
	[Header("ボスの個別で動かしたい形成パーツ")]
	[Header("--------以下ボス個別---------")]
	[Header(" ")]
	[Header(" ")]
	[SerializeField, Tooltip("コア")] private One_Boss_Parts[] core;
	[SerializeField, Tooltip("ボスのコアシャッター")] private One_Boss_Parts[] core_shutter;
	[SerializeField, Tooltip("ビームまずる")] private GameObject[] muzzles;
	[SerializeField, Tooltip("レーザーのまずる")] private GameObject[] laser_muzzle;
	[SerializeField, Tooltip("エネルギーため用のパーティクル用")] private Boss_One_A111[] supply;
	[SerializeField, Tooltip("バウンドする弾の発射数(最低二個は発射)")] private int number_of_fires;
	[SerializeField, Tooltip("ノーダメージコライダー")] private Collider[] no_damage_Collider;

	[Header("ボスのアニメーション用")]
	[SerializeField, Tooltip("ワープエフェクト")] private GameObject warp_ef;
	[SerializeField, Tooltip("スタートアニメーション")] private bool Start_Flag;
	[SerializeField, Tooltip("アップデートアニメーション")] private bool Update_Flag;
	[SerializeField, Tooltip("タイムライン")] private PlayableDirector Timeline_Player;
	[SerializeField, Tooltip("死ぬとき用")] private GameObject End_Plefab;

	[Header("アニメーションタイムライン")]
	[SerializeField, Tooltip("スタートのタイムライン")] private PlayableAsset start_timeline;
	[SerializeField, Tooltip("タイムラインの保管")] private PlayableAsset layser_timeline;
	[SerializeField, Tooltip("タイムラインの保管")] private PlayableAsset Bullet_timeline;
	[SerializeField, Tooltip("タイムラインの終了判定")] private bool Is_end_of_timeline;

	[Header("レーザー用")]
	[SerializeField, Tooltip("撃つ？撃たない？")] public bool Is_Laser_Attack;

	[Header("突進攻撃用")]
	[SerializeField, Tooltip("突進中フラグ")] public bool now_rush;

	// アニメーション用
	//-------------------------------------------------
	private uint Flame { get; set; }								// ボス内でのフレーム数
	private int Attack_Step { get; set; }							// 関数内 攻撃ステップ
	//-------------------------------------------------

	private Vector3[] BoundBullet_Rotation { get; set; }    // バウンドバレットの角度
	private int Attack_Type_Instruction { get; set; }           // 攻撃タイプ支持
	private bool End_Flag { get; set; }         // 終わりのフラグ

	private int Survival_Time { get; set; }				// ボスの生存する時間
	private int Survival_Time_Cnt { get; set; }		// 生存している時間カウント
	private bool Is_Attack_Now { get; set; }			// 現在攻撃しているか

	private  List<List<Collider>> Damage_Stage_Col { get; set; }		// ダメージの段階
	private bool Is_Shoot_The_Laser { get; set; }		// レーザー撃ったか
	private int Core_Init_HP { get; set; }// コアの初期HP

	private Color[] Base_Color { get; set; }			// コアのベースの色
	private Color[] Emissive_Color { get; set; }		// コアのエミッシブの色

	private Player1 Player1_Script { get; set; }		// プレイヤー1のデータ
	private Player2 Player2_Script { get; set; }		// プレイヤー2のデータ

	// 背景遷移トリガー
	SetTimeTrigger setTimeTrigger = null;

	private Boss_One_Laser[] Laser_Manager { get; set; }

	//---------------------------------------------------------
	//レーザー音追加
	[SerializeField] private AudioClip laserBegin;
	[SerializeField] private AudioClip laserContinuing;
	[SerializeField] private AudioClip laserEnd;
	private AudioSource audioSource;
	//---------------------------------------------------------

	//---------------------------------------------------------
	//弾を消す用保存データ
	private List<GameObject> unactiveOperateBullets = new List<GameObject>();
	[SerializeField] private ParticleSystem markParticle;
	[SerializeField] private int markCount = 0;
	[SerializeField] private float markIntervalTime;
	private float markIntervalTimer;
	private bool showMark = false;
	//---------------------------------------------------------

	private new void Start()
	{
		//-------------追加--------------
		audioSource = GetComponent<AudioSource>();
		audioSource.clip = laserEnd;
		audioSource.playOnAwake = false;
		//-------------------------------

		Timeline_Player.playOnAwake = false;

		base.Start();

		Attack_Step = 0;
		Start_Flag = true;

		Survival_Time = (2 * 60 * 60);
		Survival_Time_Cnt = 0;
		Is_Attack_Now = false;

		Bullet_num_Set(number_of_fires);

		End_Flag = false;

		Collider_Set(false);

		Core_Init_HP = 255;
		Damage_Stage_Col = new List<List<Collider>>();
		Damage_Stage_Col.Add(new List<Collider> { core_shutter[0].GetComponent<Collider>(), core_shutter[1].GetComponent<Collider>(), core_shutter[2].GetComponent<Collider>(), core[0].GetComponent<Collider>() });
		Damage_Stage_Col.Add(new List<Collider> { core_shutter[3].GetComponent<Collider>(), core_shutter[4].GetComponent<Collider>(), core_shutter[5].GetComponent<Collider>(), core[1].GetComponent<Collider>() });
		Damage_Stage_Col.Add(new List<Collider> { core_shutter[6].GetComponent<Collider>(), core_shutter[7].GetComponent<Collider>(), core_shutter[8].GetComponent<Collider>(), core[2].GetComponent<Collider>() });
		Damage_Stage_Col.Add(new List<Collider> { core_shutter[9].GetComponent<Collider>(), core_shutter[10].GetComponent<Collider>(), core_shutter[11].GetComponent<Collider>(), core[3].GetComponent<Collider>() });

		for(int i = 0;i<Damage_Stage_Col.Count; i++)
		{
			for(int j = 0;j<Damage_Stage_Col[i].Count;j++)
			{
					Damage_Stage_Col[i][j].enabled = false;
			}
		}

		if(Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eONE_PLAYER)
		{
			Player1_Script = Obj_Storage.Storage_Data.GetPlayer().GetComponent<Player1>();
		}
		else if(Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eTWO_PLAYER)
		{
			Player1_Script = Obj_Storage.Storage_Data.GetPlayer().GetComponent<Player1>();
			Player2_Script = Obj_Storage.Storage_Data.GetPlayer2().GetComponent<Player2>();
		}

		Base_Color = new Color[4]
		{
			new Color(27.0f/255.0f, 0.0f, 1.0f),
			new Color(27.0f/255.0f, 0.0f, 1.0f),
			new Color(27.0f/255.0f, 0.0f, 1.0f),
			new Color(27.0f/255.0f, 0.0f, 1.0f),
		};

		Emissive_Color = new Color[4]
		{
			new Color(0.0f, 12.0f /255.0f, 1.0f ),
			new Color(0.0f, 12.0f /255.0f, 1.0f ),
			new Color(0.0f, 12.0f /255.0f, 1.0f ),
			new Color(0.0f, 12.0f /255.0f, 1.0f ),
		};

		setTimeTrigger = FindObjectOfType<SetTimeTrigger>();

		Laser_Manager = new Boss_One_Laser[2];
		Laser_Manager[0] = laser_muzzle[0].GetComponent<Boss_One_Laser>();
		Laser_Manager[1] = laser_muzzle[1].GetComponent<Boss_One_Laser>();
	}

	private new void Update()
	{

		if (PauseManager.IsPause)
		{
			Timeline_Player.Pause();
		}
		else
		{
			Timeline_Player.Resume();
		}

		if (!PauseManager.IsPause)
		{

			if (showMark)
			{
				Debug.Log("--------------突進表示------------");
				//-------------------------------------------
				markIntervalTimer += Time.deltaTime;
				if (markIntervalTimer >= markIntervalTime && markCount < 3)
				{
					markParticle.Play();
					markCount++;
					markIntervalTimer = 0;
				}
				//-------------------------------------------
				if(markCount == 3)
				{
					showMark = false;
				}
			}
			else
			{
				markCount = 0;
				markIntervalTimer = 0;
			}


			if (Survival_Time_Cnt >= Survival_Time && !Is_Attack_Now && !End_Flag)
			{
				Timeline_Player.Pause();
				Attack_Step = 0;
				End_Flag = true;
			}

			if (Start_Flag && !End_Flag && Update_Flag)
			{
				Collider_Set(true);
				for (int i = 0; i < Damage_Stage_Col.Count; i++)
				{
					Damage_Stage_Col[i][0].enabled = true;
				}
				Timeline_Player.Pause();
				Timeline_Player.time = 60.0;

				Start_Flag = false;
			}
			else if (!End_Flag && !Start_Flag && Update_Flag)
			{
				// デバッグ　HP　0か
				if(Input.GetKey(KeyCode.O) && Input.GetKey(KeyCode.H))
				{
					foreach(One_Boss_Parts core in core)
					{
						core.hp = 0;
					}
				}

				if (!Is_Shoot_The_Laser)
				{
					Laser_And_Bouncing_Bullets();
				}
				else
				{
					Rush_2();
				}

				base.Update();
				Survival_Time_Cnt++;

				// パーツのコアが壊れたら死亡
				if (!core[0].gameObject.activeSelf && !core[1].gameObject.activeSelf && !core[2].gameObject.activeSelf && !core[3].gameObject.activeSelf)
				{
					Timeline_Player.Stop();
					Attack_Step = 0;
					Timeline_Player.time = 60.0;
					End_Flag = true;

                    base.BossRemainingBouns(2);

					//-----------------追加-------------------
					//----------------弾を消す----------------
					for (var i = 0; i < unactiveOperateBullets.Count; ++i)
					{
						if (!unactiveOperateBullets[i].transform.GetChild(1).gameObject.activeSelf)
						{
							unactiveOperateBullets[i].transform.GetChild(1).gameObject.SetActive(true);
						}
					}
					unactiveOperateBullets.Clear();
                    BossRemainingBouns(2);
					//----------------弾を消す----------------
					//-----------------追加-------------------
				}
			}
			else if (End_Flag && !Start_Flag && Update_Flag)
			{
				End_Anime();
			}

			// コライダー管理
			// シャッターが壊れると次のコライダーが起動
			for (int a = 0; a < Damage_Stage_Col.Count; a++)
			{
				for (int b = 0; b < Damage_Stage_Col[a].Count - 1; b++)
				{
					if (!Damage_Stage_Col[a][b].gameObject.activeSelf && !Damage_Stage_Col[a][b + 1].enabled)
					{
						Damage_Stage_Col[a][b + 1].enabled = true;
						Damage_Stage_Col[a].RemoveAt(b);
					}
				}
			}
		}
	}

	private void OnEnable()
	{
		Update_Flag = false;
		Timeline_Player.time = 0.0;
		Timeline_Player.Play(start_timeline);
	}

	#region 終わりアニメーション
	private void End_Anime()
	{
		if(Attack_Step == 0)
		{
			Is_Dead = true;
			setTimeTrigger.Trigger = true;
			Attack_Step++;
		}
		else if(Attack_Step == 1)
		{
			Instantiate(End_Plefab, transform.position, Quaternion.identity);
			SE_Manager.SE_Obj.SE_Explosion(Obj_Storage.Storage_Data.audio_se[22]);
			gameObject.SetActive(false);
			if(Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eONE_PLAYER)
			{
				Game_Master.MY.Score_Addition(score, (int)Game_Master.PLAYER_NUM.eONE_PLAYER);
			}
			else if(Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eTWO_PLAYER)
			{
				Game_Master.MY.Score_Addition(score / 2, (int)Game_Master.PLAYER_NUM.eONE_PLAYER);
				Game_Master.MY.Score_Addition(score / 2, (int)Game_Master.PLAYER_NUM.eTWO_PLAYER);
			}
		}
	}
	#endregion

	#region 突進攻撃2
	/// <summary>
	/// 突進攻撃
	/// </summary>
	private void Rush_2()
	{
		if (Attack_Step == 0)
		{
			Timeline_Player.time = 30.0;
			Attack_Step++;
			Is_Attack_Now = true;
			showMark = true;
		}
		else if (Attack_Step == 1)
		{
			now_rush = true;
			Timeline_Player.Play(start_timeline);
			Timeline_Player.time = 30.0;
			Attack_Step++;
		}
		else if (Attack_Step == 2)
		{
			if(!now_rush)
			{
				Timeline_Player.Pause();
				Attack_Step = 0;
				Attack_Type_Instruction = 0;
				Is_Shoot_The_Laser=false;
				Is_Attack_Now = false;
			}
		}
	}
	#endregion

	/// <summary>
	/// レーザーとバウンド弾
	/// </summary>
	private void Laser_And_Bouncing_Bullets()
	{
		// レーザーチャージ
		if (Attack_Step == 0)
		{	
			// 攻撃開始
			Is_Attack_Now = true;

			// チャージ音
			if (audioSource.clip == laserEnd)
			{
				audioSource.clip = laserBegin;
				audioSource.Stop();
				audioSource.loop = true;
				audioSource.Play();
			}

			// チャージエフェクト再生
			if (!supply[0].gameObject.activeSelf && !supply[1].gameObject.activeSelf)
			{
				supply[0].gameObject.SetActive(true);
				supply[1].gameObject.SetActive(true);

				supply[0].SetUp();
				supply[1].SetUp();
			}
			// チャージエフェクト終了
			if (supply[0].Completion_Confirmation() && supply[1].Completion_Confirmation())
			{
				supply[0].gameObject.SetActive(false);
				supply[1].gameObject.SetActive(false);

				Attack_Step++;
			}
		}
		// タイムライン再生
		else if(Attack_Step == 1)
		{
			// デフォルト位置での揺れ
			Timeline_Player.Play(Bullet_timeline);
			Timeline_Player.time = 0.0f;
			// ディレイ調節
			Shot_Delay = -60;
			// 枠組み使用開始
			//framework.SetActive(true);
			// 次の動きへ
			Attack_Step++;

			// レーザー撃ちだし開始
			Laser_Manager[0].IsShoot = true;
			Laser_Manager[1].IsShoot = true;
		}
		// 攻撃
		else if(Attack_Step == 2)
		{
			//--------------追加-----------------
			if (audioSource.clip == laserBegin)
			{
				audioSource.clip = laserContinuing;
				audioSource.Stop();
				audioSource.loop = true;
				audioSource.Play();
			}
			//-----------------------------------
			Flame++;
			Shot_Delay++;

			// バウンド弾撃ちだし
			if (Flame % 80 == 0)
			{
				Bullet_num_Set(Check_Bits());
				for (int i = 0; i < BoundBullet_Rotation.Length; i++)
				{
				  　var bullet1 = Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eONE_BOSS_BOUND, muzzles[0].transform.position, Quaternion.Euler(BoundBullet_Rotation[i]));
					var bullet2 = Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eONE_BOSS_BOUND, muzzles[3].transform.position, Quaternion.Euler(BoundBullet_Rotation[i]));

					bullet1.GetComponent<One_Boss_BoundBullet>().RougeSeed();
					bullet2.GetComponent<One_Boss_BoundBullet>().RougeSeed();
					unactiveOperateBullets.Add(bullet1);
					unactiveOperateBullets.Add(bullet2);
				}
			}

			// ビーム撃ちだし
			if (Shot_Delay == (Shot_DelayMax / 3))
			{
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, muzzles[1].transform.position, muzzles[1].transform.right);
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, muzzles[2].transform.position, muzzles[2].transform.right);
			}
			else if (Shot_Delay == (Shot_DelayMax / 3) * 2)
			{
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, muzzles[1].transform.position, muzzles[1].transform.right);
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, muzzles[2].transform.position, muzzles[2].transform.right);
			}
			else if (Shot_Delay == Shot_DelayMax)
			{
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, muzzles[1].transform.position, muzzles[1].transform.right);
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, muzzles[2].transform.position, muzzles[2].transform.right);

				Shot_Delay -= Shot_DelayMax * 2;
			}

			// タイムライン終了時
			if (Is_end_of_timeline)
			{
				// 薙ぎ払い攻撃
				Timeline_Player.Play(layser_timeline);
				Timeline_Player.time = 0.0;


				//-----------------追加-------------------
				//----------------弾を消す----------------
				for (var i = 0; i < unactiveOperateBullets.Count; ++i)
				{
					if (!unactiveOperateBullets[i].transform.GetChild(1).gameObject.activeSelf)
					{
						unactiveOperateBullets[i].transform.GetChild(1).gameObject.SetActive(true);
					}
				}
				unactiveOperateBullets.Clear();
				//----------------弾を消す----------------
				//-----------------追加-------------------

				// 次のステップへ
				Attack_Step++;
				Is_end_of_timeline = false;
				Flame = 0;
				Shot_Delay = 0;
			}
		}
		else if (Attack_Step == 3)
		{
			Flame++;
			Shot_Delay++;

			if (Is_end_of_timeline)
			{
				Flame = 0;
				Timeline_Player.Stop();
				Attack_Step = 0;
				Is_Attack_Now = false;
				Is_Shoot_The_Laser = true;

				//---------------追加-----------------
				audioSource.clip = laserEnd;
				audioSource.Stop();
				audioSource.loop = false;
				audioSource.Play();
				//------------------------------------

				// レーザー撃ちだし終了
				Laser_Manager[0].IsShoot = false;
				Laser_Manager[1].IsShoot = false;
			}
		}
	}

	/// <summary>
	/// コライダーの使用未使用の切り替え
	/// </summary>
	/// <param name="State"></param>
	private void Collider_Set(bool State)
	{
		List<Collider> objCol = new List<Collider>();
		foreach(var col in no_damage_Collider)
		{
			objCol.AddRange(col.GetComponents<Collider>());
		}
		
		foreach(var col in objCol)
		{
			col.enabled = State;
		}
	}

	/// <summary>
	/// バウンド弾の数設定
	/// </summary>
	/// <param name="set_num"></param>
	private void Bullet_num_Set(int set_num )
	{
		if(set_num % 2 == 1)
		{
			set_num--;
		}

		set_num += number_of_fires;

		BoundBullet_Rotation = new Vector3[set_num + 2];
		float z_rotation = 120.0f / ((float)BoundBullet_Rotation.Length - 1.0f);
		for (int i = 0; i < BoundBullet_Rotation.Length; i++)
		{
			BoundBullet_Rotation[i] = new Vector3(0.0f, 0.0f, (z_rotation * i) + -60.0f);
		}
	}

	/// <summary>
	/// ビットの数確認
	/// </summary>
	/// <returns></returns>
	private int Check_Bits()
	{
		int num = 0;

		if(Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eONE_PLAYER)
		{
			num = Player1_Script.bitIndex;
		}
		else if(Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eTWO_PLAYER)
		{
			num = Player1_Script.bitIndex + Player2_Script.bitIndex;
		}

		return num;
	}

	private new void OnTriggerEnter(Collider col)
	{
		if (now_rush)
		{
			if (col.GetComponent<One_Boss_BoundBullet>() != null)
			{
				col.gameObject.SetActive(false);
			}
		}
	}
}