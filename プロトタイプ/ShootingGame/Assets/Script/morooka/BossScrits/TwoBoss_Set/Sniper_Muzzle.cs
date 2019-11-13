//プレイヤーの位置参照攻撃mk3
//諸岡勇樹


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class Sniper_Muzzle : character_status
{
	[SerializeField, Tooltip("１Pを狙う方")] private bool Is_Aim_1P;
	public ParticleSystem par;
	public Two_Boss_Parts core;

	private GameObject Player1_Trans { get; set; }      // プレイヤー1のトランスフォーム
	private GameObject Player2_Trans { get; set; }       // プレイヤー2のトランスフォーム

	private Vector3 Player1_Save_Pos { get; set; }      // P1の位置保存
	private Vector3 Player2_Save_Pos { get; set; }      // P2の位置保存

	private float Save_Time_Max { get; set; }       // 保存する時間の間隔
	private float Save_Time_cnt { get; set; }       //　カウンター

	private new void Start()
	{
		if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eONE_PLAYER)
		{
			Player1_Trans = Obj_Storage.Storage_Data.GetPlayer();
			Shot_DelayMax *= 2;
		}
		else if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eTWO_PLAYER)
		{
			Player1_Trans = Obj_Storage.Storage_Data.GetPlayer();
			Player2_Trans = Obj_Storage.Storage_Data.GetPlayer2();
		}

		Save_Time_Max = 1.0f;
	}

	private new void Update()
	{
		if(core.hp < 400)
		{
			Shot_DelayMax = 60;
		}

		if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eONE_PLAYER)
		{
			// 1Pが生きているとき
			if (Player1_Trans.activeSelf)
			{
				Save_Time_cnt += Time.deltaTime;
				Shot_Delay++;

				//一定時間で位置の参照
				if (Save_Time_cnt >= Save_Time_Max)
				{
					Player1_Save_Pos = Player1_Trans.transform.position;
					Save_Time_cnt = 0.0f;
				}
				// 一定間隔で攻撃
				if (Shot_Delay >= Shot_DelayMax)
				{
					Vector3 temp_1 = transform.position;
					temp_1.z = 0.0f;
					Vector2 temp_2 = Player1_Save_Pos - temp_1;
					Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET,temp_1, temp_2);
					par.Play();
					Shot_Delay = 0;
				}
			}
		}
		else if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eTWO_PLAYER)
		{
			// 1Pを狙う
			if (Is_Aim_1P)
			{
				// 1Pが生きているとき
				if (Player1_Trans.activeSelf)
				{
					Save_Time_cnt += Time.deltaTime;
					Shot_Delay++;

					//一定時間で位置の参照
					if (Save_Time_cnt >= Save_Time_Max)
					{
						Player1_Save_Pos = Player1_Trans.transform.position;
						Save_Time_cnt = 0.0f;
					}
					// 一定間隔で攻撃
					if (Shot_Delay >= Shot_DelayMax)
					{
						Vector3 temp_1 = transform.position;
						temp_1.z = 0.0f;
						Vector2 temp_2 = Player1_Save_Pos - temp_1;
						Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET, temp_1, temp_2);
						par.Play();
						Shot_Delay = 0;
						Is_Aim_1P = false;
					}
				}
				else if(!Player1_Trans.activeSelf)
				{
					Is_Aim_1P = false;
				}
			}
			// 2Pを狙う
			else if (!Is_Aim_1P)
			{
				// 2Pが生きているとき
				if (Player2_Trans.activeSelf)
				{
					Save_Time_cnt += Time.deltaTime;
					Shot_Delay++;

					//一定時間で位置の参照
					if (Save_Time_cnt >= Save_Time_Max)
					{
						Player2_Save_Pos = Player2_Trans.transform.position;
						Save_Time_cnt = 0.0f;
					}
					// 一定間隔で攻撃
					if (Shot_Delay >= Shot_DelayMax)
					{
						Vector3 temp_1 = transform.position;
						temp_1.z = 0.0f;
						Vector2 temp_2 = Player2_Save_Pos - temp_1;
						Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET, temp_1, temp_2);
						par.Play();
						Shot_Delay = 0;
						Is_Aim_1P = true;
					}
				}
				else if(!Player2_Trans.activeSelf)
				{
					Is_Aim_1P = true;
				}
			}
		}
	}
}
