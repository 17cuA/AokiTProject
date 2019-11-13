//作成日2019/06/13
// ストレージから呼び出す関数の管理
// 作成者:諸岡勇樹
/*
 * 2019/06/06	バレットの呼び出し関数の定義
 */
//----------------------------------------------------------------------------------
// using StorageReference;
// をいつもの場所に定義してください。
// 
// 使いたいタイミングで Unity の Instantiate(・・・) のように
// Object_Instantiation.Object_Reboot( 呼び出したいObjectの名前, ポジション, 向き)
// 呼び出してくださいね！！
//----------------------------------------------------------------------------------

using UnityEngine;

namespace StorageReference
{
	public class Object_Instantiation
	{
		/// <summary>
		///  オブジェクトの再起動
		/// </summary>
		/// <param name="name"> 撃ちたいバレットの名前 </param>
		/// <param name="pos"> 打ち出す場所 </param>
		/// <param name="direction"> 向き </param>
		static public GameObject Object_Reboot(Game_Master.OBJECT_NAME name, Vector3 pos, Vector3 direction)
		{
			GameObject obj = null;
			#region コピー簡単化
			switch (name)
			{
				#region Enemy_1
				case Game_Master.OBJECT_NAME.eENEMY_NUM1:
					obj = Obj_Storage.Storage_Data.Enemy1.Active_Obj();
					break;
				#endregion
				#region Player_Bullet
				case Game_Master.OBJECT_NAME.ePLAYER_BULLET:
					obj = Obj_Storage.Storage_Data.PlayerBullet.Active_Obj();
					break;
                #endregion
                    //新しく追加した部分-----------------------------
                case Game_Master.OBJECT_NAME.ePLAYER2_BULLET:
                    obj = Obj_Storage.Storage_Data.Player2Bullet.Active_Obj();
                    break;
                case Game_Master.OBJECT_NAME.eP1_OPTION_BULLET:
                    obj = Obj_Storage.Storage_Data.P1_OptionBullet.Active_Obj();
                    break;
				case Game_Master.OBJECT_NAME.eP2_OPTION_BULLET:
					obj = Obj_Storage.Storage_Data.P2_OptionBullet.Active_Obj();
					break;

				//--------------------------------------------
				#region Player_Missile
				case Game_Master.OBJECT_NAME.ePLAYER_MISSILE:
					obj = Obj_Storage.Storage_Data.PlayerMissile.Active_Obj();
					break;
					#endregion
				#region
				case Game_Master.OBJECT_NAME.ePLAYER_TowWay:
					obj = Obj_Storage.Storage_Data.PlayerMissile_TowWay.Active_Obj();
					break;
				#endregion
				#region Plauyer_Laser
				case Game_Master.OBJECT_NAME.ePLAYER_LASER:
					obj = Obj_Storage.Storage_Data.Laser_Line.Active_Obj();
					break;
				#endregion
				#region Enemy_Bullet
				case Game_Master.OBJECT_NAME.eENEMY_BULLET:
					obj = Obj_Storage.Storage_Data.EnemyBullet.Active_Obj();
					break;
				#endregion
				#region Enemy_Beam
				case Game_Master.OBJECT_NAME.eENEMY_BEAM:
					obj = Obj_Storage.Storage_Data.Beam_Bullet_E.Active_Obj();
					break;
				#endregion
				#region Enemy_Laser
				case Game_Master.OBJECT_NAME.eENEMY_LASER:
					break;
				#endregion
				case Game_Master.OBJECT_NAME.ePLAYER:
					obj = Obj_Storage.Storage_Data.Player.Active_Obj();
					break;
				case Game_Master.OBJECT_NAME.eUFOTYPE_ENEMY:
					obj = Obj_Storage.Storage_Data.UfoType_Enemy.Active_Obj();
					break;
				case Game_Master.OBJECT_NAME.eBEELZEBUBTYPE_ENEMY:
					obj = Obj_Storage.Storage_Data.BeelzebubType_Enemy.Active_Obj();
					break;
				case Game_Master.OBJECT_NAME.eCLAMCHOWDERTYPE_ENEMY:
					obj = Obj_Storage.Storage_Data.ClamChowderType_Enemy.Active_Obj();
					break;
				case Game_Master.OBJECT_NAME.eOCTOPUSTYPE_ENEMY:
					obj = Obj_Storage.Storage_Data.OctopusType_Enemy.Active_Obj();
					break;
				case Game_Master.OBJECT_NAME.ePOWERUP_ITEM:
					obj = Obj_Storage.Storage_Data.PowerUP_Item.Active_Obj();
					break;
				case Game_Master.OBJECT_NAME.eBATTLESHIP_ENEMY_PREFAB:
					obj = Obj_Storage.Storage_Data.BattleShipBullet.Active_Obj();
					break;
				case Game_Master.OBJECT_NAME.eONE_BOSS_LASER:
					obj = Obj_Storage.Storage_Data.One_Boss_Laser.Active_Obj();
					break;
				case Game_Master.OBJECT_NAME.eONE_BOSS_BOUND:
					obj = Obj_Storage.Storage_Data.One_Boss_BousndBullet.Active_Obj();
					break;
				case Game_Master.OBJECT_NAME.eTWO_BOSS_LASER:
					obj = Obj_Storage.Storage_Data.Two_Boss_Laser.Active_Obj();
					break;
				//case Game_Master.OBJECT_NAME.eMANTA_LASER:
				//	obj = Obj_Storage
				//	break;
				default:
					break;
			}
			#endregion

			if (obj == null) return obj;

			obj.transform.position = pos;
			obj.transform.right = direction;

			return obj;
		}

		/// <summary>
		///  オブジェクトの再起動
		/// </summary>
		/// <param name="name"> 撃ちたいバレットの名前 </param>
		/// <param name="pos"> 打ち出す場所 </param>
		/// <param name="direction"> 向き </param>
		static public GameObject Object_Reboot(Game_Master.OBJECT_NAME name, Vector3 pos, Quaternion direction)
		{
			GameObject obj = null;
			#region コピー簡単化
			switch (name)
			{
				#region Enemy_1
				case Game_Master.OBJECT_NAME.eENEMY_NUM1:
					obj = Obj_Storage.Storage_Data.Enemy1.Active_Obj();
					break;
				#endregion
				#region Player_Bullet
				case Game_Master.OBJECT_NAME.ePLAYER_BULLET:
					obj = Obj_Storage.Storage_Data.PlayerBullet.Active_Obj();
					break;
				#endregion
				//新しく追加した部分-----------------------------
				case Game_Master.OBJECT_NAME.ePLAYER2_BULLET:
					obj = Obj_Storage.Storage_Data.Player2Bullet.Active_Obj();
					break;
				case Game_Master.OBJECT_NAME.eP1_OPTION_BULLET:
					obj = Obj_Storage.Storage_Data.P1_OptionBullet.Active_Obj();
					break;
				case Game_Master.OBJECT_NAME.eP2_OPTION_BULLET:
					obj = Obj_Storage.Storage_Data.P2_OptionBullet.Active_Obj();
					break;

				//--------------------------------------------
				#region Player_Missile
				case Game_Master.OBJECT_NAME.ePLAYER_MISSILE:
					obj = Obj_Storage.Storage_Data.PlayerMissile.Active_Obj();
					break;
				#endregion
				#region
				case Game_Master.OBJECT_NAME.ePLAYER_TowWay:
					obj = Obj_Storage.Storage_Data.PlayerMissile_TowWay.Active_Obj();
					break;
				#endregion
				#region Plauyer_Laser
				case Game_Master.OBJECT_NAME.ePLAYER_LASER:
					obj = Obj_Storage.Storage_Data.Laser_Line.Active_Obj();
					break;
				#endregion
				#region Enemy_Bullet
				case Game_Master.OBJECT_NAME.eENEMY_BULLET:
					obj = Obj_Storage.Storage_Data.EnemyBullet.Active_Obj();
					break;
				#endregion
				#region Enemy_Beam
				case Game_Master.OBJECT_NAME.eENEMY_BEAM:
					obj = Obj_Storage.Storage_Data.Beam_Bullet_E.Active_Obj();
					break;
				#endregion
				#region Enemy_Laser
				case Game_Master.OBJECT_NAME.eENEMY_LASER:
					break;
				#endregion
				case Game_Master.OBJECT_NAME.ePLAYER:
					obj = Obj_Storage.Storage_Data.Player.Active_Obj();
					break;
				case Game_Master.OBJECT_NAME.eUFOTYPE_ENEMY:
					obj = Obj_Storage.Storage_Data.UfoType_Enemy.Active_Obj();
					break;
				case Game_Master.OBJECT_NAME.eBEELZEBUBTYPE_ENEMY:
					obj = Obj_Storage.Storage_Data.BeelzebubType_Enemy.Active_Obj();
					break;
				case Game_Master.OBJECT_NAME.eCLAMCHOWDERTYPE_ENEMY:
					obj = Obj_Storage.Storage_Data.ClamChowderType_Enemy.Active_Obj();
					break;
				case Game_Master.OBJECT_NAME.eOCTOPUSTYPE_ENEMY:
					obj = Obj_Storage.Storage_Data.OctopusType_Enemy.Active_Obj();
					break;
				case Game_Master.OBJECT_NAME.ePOWERUP_ITEM:
					obj = Obj_Storage.Storage_Data.PowerUP_Item.Active_Obj();
					break;
				case Game_Master.OBJECT_NAME.eBATTLESHIP_ENEMY_PREFAB:
					obj = Obj_Storage.Storage_Data.BattleShipBullet.Active_Obj();
					break;
				case Game_Master.OBJECT_NAME.eONE_BOSS_LASER:
					obj = Obj_Storage.Storage_Data.One_Boss_Laser.Active_Obj();
					break;
				case Game_Master.OBJECT_NAME.eONE_BOSS_BOUND:
					obj = Obj_Storage.Storage_Data.One_Boss_BousndBullet.Active_Obj();
					break;
				case Game_Master.OBJECT_NAME.eTWO_BOSS_LASER:
					obj = Obj_Storage.Storage_Data.Two_Boss_Laser.Active_Obj();
					break;
				default:
					break;
			}
			#endregion

			if (obj == null) return obj;

			obj.transform.position = pos;
			obj.transform.rotation = direction;

			return obj;
		}
	}
}