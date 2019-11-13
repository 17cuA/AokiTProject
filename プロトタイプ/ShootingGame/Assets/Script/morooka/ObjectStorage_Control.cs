//作成日2019/09/05
// オブジェクトストレージの内容物管理
// 作成者:諸岡勇樹
/*
 * 2019/09/13　ボスの終了時ボス用攻撃削除
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;      //これを忘れず

public class ObjectStorage_Control : MonoBehaviour
{
	public EnemyCreate EnemyCreate_Data;        // エネミークリエイトの情報

	// ボスの情報
	public character_status Medium_Boss_Data { get; set; }		// 中ボスの情報
	public character_status One_Boss_Data { get; set; }			// 1ボスの情報
	public character_status Tow_Boss_Data { get; set; }			// 2ボスの情報
	public character_status Moai_Boss_Data { get; set; }		// モアイの情報

	private bool Is_Processed_Normal { get; set; }		// ふつうのオブジェクトの削除

	private int Boss_Frame_Cnt { get; set; }     // フレーム別けようのカウント(ボス)
	private int Normal_Frame_Cnt { get; set; }		// エネミー軍の削除用
	private int wireless_Frame_Cnt { get; set; }		// 無線時のカウンター
	private bool Is_Set_Start { get; set; }     // 初期化用
	private bool Is_Direct_End { get; set; }		// 直置きオブジェクトの削除
	private bool Pooling_End { get; set; }		// プーリングしたものの終了

	private string[] direct_placement = new string[6]
	{
		"Enemy_ClamChowder_Group_TenStraight(Clone)",
		"Enemy_ClamChowder_Group_TenStraight(Clone)",
		"Enemy_ClamChowder_Group_UpSevenDiagonal(Clone)",
		"Enemy_ClamChowder_Group_DownSevenDiagonal(Clone)",
		"Enemy_Star_Fish_Spowner(Clone)",
		"Enemy_ClamChowder_Group_ThreeStraight(Clone)",
	};

	private List<Object_Pooling> Before_The_Last_Boss { set; get; }	// ラスボス前削除セット

	void Start()
    {
		Is_Set_Start = true;
		Is_Processed_Normal = false;
		Boss_Frame_Cnt = 0;
		Normal_Frame_Cnt = 0;
		wireless_Frame_Cnt = 0;
	}

	void Update()
    {
		// 後初期化
		if(Is_Set_Start)
		{
			if (Scene_Manager.Manager.Now_Scene == Scene_Manager.SCENE_NAME.eSTAGE_01)
			{
				Medium_Boss_Data = Obj_Storage.Storage_Data.Boss_Middle.Get_Obj()[0].GetComponent<character_status>();
				One_Boss_Data = Obj_Storage.Storage_Data.GetBoss(1).GetComponent<character_status>();
				Moai_Boss_Data = Obj_Storage.Storage_Data.GetBoss(3).GetComponent<character_status>();
				Tow_Boss_Data = Obj_Storage.Storage_Data.GetBoss(2).GetComponent<character_status>();

				//----------------------------- エネミーセット ---------------------------------------
				Before_The_Last_Boss = new List<Object_Pooling>();
				Before_The_Last_Boss.Add(Obj_Storage.Storage_Data.enemy_UFO_Group);
				Before_The_Last_Boss.Add(Obj_Storage.Storage_Data.enemy_UFO_Group_NoneShot);
				Before_The_Last_Boss.Add(Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Two_Top);
				Before_The_Last_Boss.Add(Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Two_Under);
				Before_The_Last_Boss.Add(Obj_Storage.Storage_Data.enemy_ClamChowder_Group_TwoWaveOnlyUp);
				Before_The_Last_Boss.Add(Obj_Storage.Storage_Data.enemy_ClamChowder_Group_TwoWaveOnlyDown);
				Before_The_Last_Boss.Add(Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Three);
				Before_The_Last_Boss.Add(Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Three_Item);
				Before_The_Last_Boss.Add(Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyUp);
				Before_The_Last_Boss.Add(Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyDown);
				Before_The_Last_Boss.Add(Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item);
				Before_The_Last_Boss.Add(Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item);
				Before_The_Last_Boss.Add(Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Four);
				Before_The_Last_Boss.Add(Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Four_NoItem);
				Before_The_Last_Boss.Add(Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Five);
				Before_The_Last_Boss.Add(Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Five_NoItem);
				Before_The_Last_Boss.Add(Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Seven);
				Before_The_Last_Boss.Add(Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Straight);
				Before_The_Last_Boss.Add(Obj_Storage.Storage_Data.enemy_Beelzebub_Group_FourWide);
				Before_The_Last_Boss.Add(Obj_Storage.Storage_Data.enemy_Beelzebub_Group_FourWide_Item);
				Before_The_Last_Boss.Add(Obj_Storage.Storage_Data.enemy_BeetleGroup);
				Before_The_Last_Boss.Add(Obj_Storage.Storage_Data.enemy_BeetleGroup_Three);
				Before_The_Last_Boss.Add(Obj_Storage.Storage_Data.boundMeteors);
				Before_The_Last_Boss.Add(Obj_Storage.Storage_Data.enemy_Bacula_Sixteen);
				Before_The_Last_Boss.Add(Obj_Storage.Storage_Data.enemy_Bacula_FourOnly);
				Before_The_Last_Boss.Add(Obj_Storage.Storage_Data.enemy_ClamChowder_FourTriangle);
				Before_The_Last_Boss.Add(Obj_Storage.Storage_Data.enemy_ClamChowder_FourTriangle_NoItem);
				Before_The_Last_Boss.Add(Obj_Storage.Storage_Data.enemy_Beelzebub_Group_EightNormal_Item);
				Before_The_Last_Boss.Add(Obj_Storage.Storage_Data.enemy_UFO_Group_Five);
				Before_The_Last_Boss.Add(Obj_Storage.Storage_Data.enemy_Beetle_Group_Seven);
				Before_The_Last_Boss.Add(Obj_Storage.Storage_Data.enemy_ClamChowder_Group_SevenStraight);
				Before_The_Last_Boss.Add(Obj_Storage.Storage_Data.enemy_ClamChowder_Group_SixStraight);
				Before_The_Last_Boss.Add(Obj_Storage.Storage_Data.enemy_ClamChowder_Group_UpSevenDiagonal);
				Before_The_Last_Boss.Add(Obj_Storage.Storage_Data.enemy_ClamChowder_Group_DownSevenDiagonal);
				Before_The_Last_Boss.Add(Obj_Storage.Storage_Data.enemy_ClamChowder_Group_TenStraight);
				Before_The_Last_Boss.Add(Obj_Storage.Storage_Data.SmallBeam_Bullet_E);
				Before_The_Last_Boss.Add(Obj_Storage.Storage_Data.UfoType_Enemy);
				Before_The_Last_Boss.Add(Obj_Storage.Storage_Data.ClamChowderType_Enemy);
				Before_The_Last_Boss.Add(Obj_Storage.Storage_Data.OctopusType_Enemy);
				Before_The_Last_Boss.Add(Obj_Storage.Storage_Data.BeelzebubType_Enemy);
				Before_The_Last_Boss.Add(Obj_Storage.Storage_Data.BattleShipType_Enemy);
				//----------------------------- エネミーセット ---------------------------------------
			}
			else
			{
				enabled = false;
			}
			Is_Set_Start = false;
		}

		// ステージ1のとき
		if (Scene_Manager.Manager.Now_Scene == Scene_Manager.SCENE_NAME.eSTAGE_01)
		{
			#region ボスの削除
			// 中ボスの削除
			if (Medium_Boss_Data != null)
			{
				if (Medium_Boss_Data.Is_Dead)
				{
					Des_Obj_B(ref Obj_Storage.Storage_Data.Boss_Middle);
					Boss_Frame_Cnt = 0;
				}
			}

			// 1ボスの削除
			if (One_Boss_Data != null)
			{
				if (One_Boss_Data.Is_Dead)
				{
					// バウンドバレットの削除
					if (Boss_Frame_Cnt == 0)
					{
						Des_Obj_B(ref Obj_Storage.Storage_Data.One_Boss_BousndBullet);
					}
					// レーザーの削除
					else if (Boss_Frame_Cnt == 1)
					{
						Des_Obj_B(ref Obj_Storage.Storage_Data.One_Boss_Laser);
					}
					else if (Boss_Frame_Cnt == 2)
					{
						Des_Obj_B(ref Obj_Storage.Storage_Data.Boss_1);
						Boss_Frame_Cnt = 0;
					}
				}
			}
			// 2ボスの削除
			if (Tow_Boss_Data != null)
			{
				if (Tow_Boss_Data.Is_Dead)
				{
					// レーザーの削除
					if (Boss_Frame_Cnt == 0)
					{
						Des_Obj_B(ref Obj_Storage.Storage_Data.Two_Boss_Laser);
					}
					else if(Boss_Frame_Cnt == 1)
					{
						Des_Obj_B(ref Obj_Storage.Storage_Data.Boss_2);
					}
					else if(Boss_Frame_Cnt == 2)
					{
						Des_Obj_B(ref Obj_Storage.Storage_Data.Beam_Bullet_E);
					}
					else if(Boss_Frame_Cnt == 3)
					{
						Des_Obj_B(ref Obj_Storage.Storage_Data.BattleShipBullet);
					}
					else if(Boss_Frame_Cnt == 4)
					{
						Des_Obj_B(ref Obj_Storage.Storage_Data.EnemyBullet);
					}
				}
			}

			// モアイの削除
			if (Moai_Boss_Data != null)
			{
				if (Moai_Boss_Data.Is_Dead)
				{
					// バレットの削除
					if (Boss_Frame_Cnt == 0)
					{
						Des_Obj_B(ref Obj_Storage.Storage_Data.Moai_Bullet);
					}
					// 目レーザーの削除
					else if (Boss_Frame_Cnt == 1)
					{
						Des_Obj_B(ref Obj_Storage.Storage_Data.Moai_Eye_Laser);
					}
					// モアイミニの削除
					else if (Boss_Frame_Cnt == 2)
					{
						Des_Obj_B(ref Obj_Storage.Storage_Data.Moai_Mini_Group);
					}
					// 口レーザーの削除
					else if (Boss_Frame_Cnt == 3)
					{
						Des_Obj_B(ref Obj_Storage.Storage_Data.Moai_Mouth_Laser);
					}
					else if(Boss_Frame_Cnt == 4)
					{
						Des_Obj_B(ref Obj_Storage.Storage_Data.Moai);
						Boss_Frame_Cnt = 0;
					}
				}
			}
			#endregion

			#region　無線時に毎回処理する
			if (Wireless_sinario.Is_using_wireless && (!Is_Direct_End || !Pooling_End))
			{
				//----------------------- 　プーリングされてない-----------------------
				if (wireless_Frame_Cnt == 0)
				{
					foreach (var s in direct_placement)
					{
						GameObject obj = GameObject.Find(s);
						if (obj != null)
						{
							//Debug.Log(" obj.name");
							Destroy(obj);
						}
					}
					Is_Direct_End = true;
				}
				//----------------------- 　プーリングされてない-----------------------

				//----------------------- エフェクト関係------------------------------
				var temp = Obj_Storage.Storage_Data.Effects[wireless_Frame_Cnt].Get_Obj().Count;
				for (int i = 1; i < Obj_Storage.Storage_Data.Effects[wireless_Frame_Cnt].Get_Obj().Count; i++)
				{
					Destroy(Obj_Storage.Storage_Data.Effects[wireless_Frame_Cnt].Get_Obj()[i]);
					Obj_Storage.Storage_Data.Effects[wireless_Frame_Cnt].Get_Obj().RemoveAt(i);
				}

				Normal_Frame_Cnt++;

				if(wireless_Frame_Cnt == Obj_Storage.Storage_Data.Effects.Length - 1)
				{
					wireless_Frame_Cnt = 0;
					Pooling_End = true;
				}
				//----------------------- エフェクト関係------------------------------
			}
			else
			{
				Is_Direct_End = false;
				Pooling_End = false;
			}
			#endregion

			#region ラスボス前に消すもの
			if (EnemyCreate_Data.GetLastBossWireless() && !Is_Processed_Normal)
			{
				// リストを各フレームで消していく
				if (Normal_Frame_Cnt < Before_The_Last_Boss.Count)
				{
					Destroy(Before_The_Last_Boss[Normal_Frame_Cnt].Get_Parent_Obj());
					Before_The_Last_Boss[Normal_Frame_Cnt].Get_Obj().Clear();
					Normal_Frame_Cnt++;
				}
				else if(Normal_Frame_Cnt == Before_The_Last_Boss.Count)
				{
					Before_The_Last_Boss.Clear();
					Normal_Frame_Cnt = 0;
					Is_Processed_Normal = true;
				}
				//if (Normal_Frame_Cnt == 0)          { Des_Obj(ref Obj_Storage.Storage_Data.enemy_UFO_Group); }
				//else if (Normal_Frame_Cnt == 1)   { Des_Obj(ref Obj_Storage.Storage_Data.enemy_UFO_Group_NoneShot); }
				//else if (Normal_Frame_Cnt == 2)   { Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Two_Top); }
				//else if (Normal_Frame_Cnt == 3)   { Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Two_Under); }
				//else if (Normal_Frame_Cnt == 4)   { Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_TwoWaveOnlyUp); }
				//else if (Normal_Frame_Cnt == 5)   { Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_TwoWaveOnlyDown); }
				//else if (Normal_Frame_Cnt == 6)   { Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Three); }
				//else if (Normal_Frame_Cnt == 7)   { Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Three_Item); }
				//else if (Normal_Frame_Cnt == 8)   { Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyUp); }
				//else if (Normal_Frame_Cnt == 9)   { Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyDown); }
				//else if (Normal_Frame_Cnt == 10) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyUp_Item); }
				//else if (Normal_Frame_Cnt == 11) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_ThreeWaveOnlyDown_Item); }
				//else if (Normal_Frame_Cnt == 12) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Four); }
				//else if (Normal_Frame_Cnt == 13) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Four_NoItem); }
				//else if (Normal_Frame_Cnt == 14) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Five); }
				//else if (Normal_Frame_Cnt == 15) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Five_NoItem); }
				//else if (Normal_Frame_Cnt == 16) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Seven); }
				//else if (Normal_Frame_Cnt == 17) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_Straight); }
				//else if (Normal_Frame_Cnt == 18) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_Beelzebub_Group_FourWide); }
				//else if (Normal_Frame_Cnt == 19) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_Beelzebub_Group_FourWide_Item); }
				//else if (Normal_Frame_Cnt == 20) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_BeetleGroup); }
				//else if (Normal_Frame_Cnt == 21) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_BeetleGroup_Three); }
				//else if (Normal_Frame_Cnt == 22) { Des_Obj(ref Obj_Storage.Storage_Data.boundMeteors); }
				//else if (Normal_Frame_Cnt == 23) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_Bacula_Sixteen); }
				//else if (Normal_Frame_Cnt == 24) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_Bacula_FourOnly); }
				//else if (Normal_Frame_Cnt == 25) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_FourTriangle); }
				//else if (Normal_Frame_Cnt == 26) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_FourTriangle_NoItem); }
				//else if (Normal_Frame_Cnt == 27) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_Beelzebub_Group_EightNormal_Item); }
				//else if (Normal_Frame_Cnt == 28) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_UFO_Group_Five); }
				//else if (Normal_Frame_Cnt == 29) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_Beetle_Group_Seven); }
				//else if (Normal_Frame_Cnt == 30) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_SevenStraight); }
				//else if (Normal_Frame_Cnt == 31) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_SixStraight); }
				//else if (Normal_Frame_Cnt == 32) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_UpSevenDiagonal); }
				//else if (Normal_Frame_Cnt == 33) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_DownSevenDiagonal); }
				//else if (Normal_Frame_Cnt == 34) { Des_Obj(ref Obj_Storage.Storage_Data.enemy_ClamChowder_Group_TenStraight); }
				//else if (Normal_Frame_Cnt == 35) { Des_Obj(ref Obj_Storage.Storage_Data.SmallBeam_Bullet_E); }
				//else if (Normal_Frame_Cnt == 36) { Des_Obj(ref Obj_Storage.Storage_Data.UfoType_Enemy); }
				//else if (Normal_Frame_Cnt == 37) { Des_Obj(ref Obj_Storage.Storage_Data.ClamChowderType_Enemy); }
				//else if (Normal_Frame_Cnt == 38) { Des_Obj(ref Obj_Storage.Storage_Data.OctopusType_Enemy); }
				//else if (Normal_Frame_Cnt == 39) { Des_Obj(ref Obj_Storage.Storage_Data.BeelzebubType_Enemy); }
				//else if (Normal_Frame_Cnt == 40) { Des_Obj(ref Obj_Storage.Storage_Data.BattleShipType_Enemy);
				//Is_Processed_Normal = true;
				//}
			}
			#endregion
		}
	}

	private void Des_Obj(ref Object_Pooling poo)
	{
		Destroy(poo.Get_Parent_Obj().gameObject);
		poo.Get_Obj().Clear();
		Normal_Frame_Cnt++;
	}
	private void Des_Obj_B(ref Object_Pooling poo)
	{
		Destroy(poo.Get_Parent_Obj().gameObject);
		poo.Get_Obj().Clear();
		Boss_Frame_Cnt++;
	}
}
