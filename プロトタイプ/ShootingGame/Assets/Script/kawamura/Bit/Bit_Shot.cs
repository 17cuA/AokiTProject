//作成者：川村良太
//ビットの攻撃スクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Power;
using StorageReference;

public class Bit_Shot : MonoBehaviour
{

    GameObject saveObj;

	public GameObject playerObj;        //プレイヤーオブジェクト
	public GameObject player2Obj;        //プレイヤー2オブジェクト
	public GameObject shot_Mazle;       //弾を放つための地点を指定するためのオブジェクト
	public GameObject laser_Obj;        //レーザーオブジェクト

	Player1 pl1;                        //プレイヤースクリプト
	Player2 pl2;
	Bit_Formation_3 bf;                 //オプションの全般のスクリプト
	public Quaternion Direction;        //オブジェクトの向きを変更する時に使う  
										//public ParticleSystem[] effect_Mazle_Fire = new ParticleSystem[5];  //マズルファイアのエフェクト（unity側の動き）

	public int shotNum;                        //撃った数
	int effectNum;
	public float shot_Delay;                   //撃つディレイ
	public int Shot_DelayMax;                                           // 弾を打つ時の間隔（最大値::unity側にて設定）
	public int Bullet_cnt;          //バレットの発射数をかぞえる変数
	private int Bullet_cnt_Max;     //バレットの発射数の最大値を入れる変数
	public bool isShot = true;          //撃てるか
	int missileDelayCnt = 0;            //ミサイルのディレイ
	public int shotDelayMax;                   //ショットの間隔

    public GameObject myObj;
    public Bit_Shot myShot;

    Player_Bullet pBullet;
	List<GameObject> bullet_data = new List<GameObject>();
	//bool activeLaser = true;

	//bool activeDouble = false;
	//bool activeBullet = false;

	private void OnEnable()
	{
		//プール化したため、ここでイベント発生時の処理を入れとく
		P1_PowerManager.Instance.AddFunction(P1_PowerManager.Power.PowerType.LASER, ActiveLaser);

	}
	private void OnDisable()
	{
		P1_PowerManager.Instance.RemoveFunction(P1_PowerManager.Power.PowerType.LASER, ActiveLaser);

	}

	void Start()
	{
        myObj = gameObject;
        myShot = myObj.GetComponent<Bit_Shot>();
		//撃つ位置取得
		shot_Mazle = gameObject.transform.Find("Bullet_Fire").gameObject;
		//Bit_Formation_3取得
		bf = gameObject.GetComponent<Bit_Formation_3>();
		//向き入れます,撃つ間隔の最大設定します,
		Direction = transform.rotation;
		shotDelayMax = 5;
        Bullet_cnt_Max = 10;

        laser_Obj.SetActive(true);       //レーザーの子供が動かないようにするための変数

	}

	void Update()
	{
		//プレイヤーオブジェクトが入っていなかったら入れてスクリプトも取得
		if (playerObj == null)
		{
			playerObj = GameObject.Find("Player");
			pl1 = playerObj.GetComponent<Player1>();
		}
		if (player2Obj == null)
		{
			player2Obj = GameObject.Find("Player_2");
			pl2 = player2Obj.GetComponent<Player2>();
		}

		//if(pl1.bullet_Type == Player1.Bullet_Type.Laser)
		//{
		//	laser_Obj.SetActive(true);
		//}
		//else
		//{
		//	laser_Obj.SetActive(false);
		//}

		//死んでないくて打てる状態なら
		if (!bf.isDead && isShot)
		{
			if (bf.bState == Bit_Formation_3.BitState.Player1)
			{
				if (pl1.bullet_Type == Player1.Bullet_Type.Laser)
				{
					//プレイヤーがレーザー状態の時
					laser_Obj.SetActive(true);
					//発射ボタンが離されたら
					if (Input.GetButtonUp(pl1.InputManager.Manager.Button["Shot"]) || Input.GetKeyUp(KeyCode.Space))
					{
						//レーザーストップ
						//laser_Obj.SetActive(false);
					}
					//発射ボタンが押されている間
					else if (Input.GetButton(pl1.InputManager.Manager.Button["Shot"]) || Input.GetKey(KeyCode.Space))
					{
						//レーザーを出す
						//laser_Obj.SetActive(true);
						//レーザー時のミサイル発射の処理
						if (pl1.activeMissile && missileDelayCnt > pl1.missile_dilay_max)
						{
							if (Input.GetButton(pl1.InputManager.Manager.Button["Shot"]) || Input.GetKey(KeyCode.Space))
							{
								Missile_Fire();
							}
						}
					}
				}
				else
				{
					laser_Obj.SetActive(false);
				}
				Bullet_Create();

				shot_Delay++;
			}
			else if (bf.bState == Bit_Formation_3.BitState.Player2)
			{
				if (pl2.bullet_Type == Player2.Bullet_Type.Laser)
				{
					//プレイヤーがレーザー状態の時
					laser_Obj.SetActive(true);
					//発射ボタンが離されたら
					if (Input.GetButtonUp(pl2.InputManager.Manager.Button["Shot"]) || Input.GetKeyUp(KeyCode.Space))
					{
						//レーザーストップ
						//laser_Obj.SetActive(false);
					}
					//発射ボタンが押されている間
					else if (Input.GetButton(pl2.InputManager.Manager.Button["Shot"]) || Input.GetKey(KeyCode.Space))
					{
						//レーザーを出す
						//laser_Obj.SetActive(true);
						//レーザー時のミサイル発射の処理
						if (pl2.activeMissile && missileDelayCnt > pl2.missile_dilay_max)
						{
							if (Input.GetButton(pl2.InputManager.Manager.Button["Shot"]) || Input.GetKey(KeyCode.Space))
							{
								Missile_Fire();
							}
						}
					}
				}
				else
				{
					laser_Obj.SetActive(false);
				}
				Bullet_Create();

				shot_Delay++;

			}
		}

		else if (bf.isDead)
		{
		}

		for (int i = 0; i < bullet_data.Count; i++)
		{
			if (!bullet_data[i].activeSelf)
			{
				bullet_data.RemoveAt(i);
			}
		}

		missileDelayCnt++;
	}

	//-----------ここから関数----------------
	public void Bullet_Create()
	{
		Shot_DelayMax = 2;

		if (bf.bState == Bit_Formation_3.BitState.Player1)
		{
			if (!pl1.Is_Change_Auto)
			{
				if (shot_Delay > Shot_DelayMax)
				{

					if (Input.GetButtonDown(pl1.InputManager.Manager.Button["Shot"]) || Input.GetKeyDown(KeyCode.Space))
					{
						shot_Delay = 0;
						switch (pl1.bullet_Type)
						{
							case Player1.Bullet_Type.Single:
								Single_Fire();
								//effect_Mazle_Fire[effectNum].Play();
								effectNum++;
								break;
							case Player1.Bullet_Type.Double:
								Double_Fire();
								//effect_Mazle_Fire[effectNum].Play();
								effectNum++;
								break;
							default:
								break;
						}
						if (effectNum > 4)
						{
							effectNum = 0;
						}
						if (pl1.activeMissile && missileDelayCnt > pl1.missile_dilay_max)
						{
							Missile_Fire();
							missileDelayCnt = 0;
						}
						shot_Delay = 0;
					}
				}
			}
			else
			{
				Shot_DelayMax = 5;
				if (shot_Delay > Shot_DelayMax)
				{

					//if (Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space))
					//{
					if (pl1.Is_Burst)
					{
						// 連続で4発まで撃てるようにした
						if (shotNum < 5)
						{
							switch (pl1.bullet_Type)
							{
								case Player1.Bullet_Type.Single:
									Single_Fire();
									//effect_Mazle_Fire[effectNum].Play();
									effectNum++;
									shotNum++;

									break;
								case Player1.Bullet_Type.Double:
									Double_Fire();
									//effect_Mazle_Fire[effectNum].Play();
									effectNum++;
									shotNum++;

									break;
								default:
									break;
							}
							if (pl1.activeMissile && missileDelayCnt > pl1.missile_dilay_max)
							{
								Missile_Fire();
								missileDelayCnt = 0;
							}
							shot_Delay = 0;

						}
						// 4発撃った後、10フレーム程置く
						else if (shotNum == 40)
						{
							shotNum = 0;
							effectNum = 0;
						}
						else
						{
							shotNum++;
						}

					}
					//}
				}
				if (!pl1.Is_Burst)
				{
					shotNum = 0;
				}
				//if (Input.GetButtonUp("Fire1") || Input.GetKey(KeyCode.Space))
				//{
				//	shotNum = 0;
				//}
				if (effectNum > 4)
				{
					effectNum = 0;
				}

			}
		}
		//プレイヤー2
		else if (bf.bState == Bit_Formation_3.BitState.Player2)
		{
			if (!pl2.Is_Change_Auto)
			{
				if (shot_Delay > Shot_DelayMax)
				{
					if (Input.GetButtonDown(pl2.InputManager.Manager.Button["Shot"]) || Input.GetKeyDown(KeyCode.Space))
					{
						//if (pl2.Is_Burst)
						//{
							shot_Delay = 0;
							switch (pl2.bullet_Type)
							{
								case Player2.Bullet_Type.Single:
									Single_Fire();
									//effect_Mazle_Fire[effectNum].Play();
									effectNum++;
									break;
								case Player2.Bullet_Type.Double:
									Double_Fire();
									//effect_Mazle_Fire[effectNum].Play();
									effectNum++;
									break;
								default:
									break;
							}
							if (effectNum > 4)
							{
								effectNum = 0;
							}
							if (pl2.activeMissile && missileDelayCnt > pl2.missile_dilay_max)
							{
								Missile_Fire();
								missileDelayCnt = 0;
							}
							shot_Delay = 0;

						//}
					}
				}
			}
			else
			{
				Shot_DelayMax = 5;
				if (shot_Delay > Shot_DelayMax)
				{

					//if (Input.GetButton("P2_Fire1") || Input.GetKey(KeyCode.Space))
					//{
					if(pl2.Is_Burst)
					{
						// 連続で4発まで撃てるようにした
						if (shotNum < 5)
						{
							switch (pl2.bullet_Type)
							{
								case Player2.Bullet_Type.Single:
									Single_Fire();
									//effect_Mazle_Fire[effectNum].Play();
									effectNum++;
									shotNum++;

									break;
								case Player2.Bullet_Type.Double:
									Double_Fire();
									//effect_Mazle_Fire[effectNum].Play();
									effectNum++;
									shotNum++;

									break;
								default:
									break;
							}
							if (pl2.activeMissile && missileDelayCnt > pl2.missile_dilay_max)
							{
								Missile_Fire();
								missileDelayCnt = 0;
							}
							shot_Delay = 0;

						}
						// 4発撃った後、10フレーム程置く
						else if (shotNum == 40)
						{
							shotNum = 0;
							effectNum = 0;
						}
						else
						{
							shotNum++;
						}

					}
					//}
				}
				//if (Input.GetButtonUp("P2_Fire1") || Input.GetKey(KeyCode.Space))
				//{
				//	shotNum = 0;
				//}
				if (!pl2.Is_Burst)
				{
					shotNum = 0;
				}
				if (effectNum > 4)
				{
					effectNum = 0;
				}
			}
		}
	}

	//単発発射関数
	private void Single_Fire()
	{
		if (bf.bState == Bit_Formation_3.BitState.Player1)
		{
			if (!pl1.Is_Change_Auto)
			{
				//if (/*Bullet_cnt < Bullet_cnt_Max*/ Bullet_cnt < 100)
				//{
                if(Bullet_cnt<8)
                {
                    saveObj = Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eP1_OPTION_BULLET, shot_Mazle.transform.position, Direction);
                    pBullet = saveObj.GetComponent<Player_Bullet>();
                    pBullet.bShot = myShot;

                    //SE_Manager.SE_Obj.SE_Active(Obj_Storage.Storage_Data.audio_se[4]);
                    Bullet_cnt += 1;
                }
                //}

            }
			else
			{
				if (Bullet_cnt < 8 && bullet_data.Count < 10)
				{
					bullet_data.Add(Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eP1_OPTION_BULLET, shot_Mazle.transform.position, Direction));
					for (int i = 0; i < bullet_data.Count; i++)
					{
						if (bullet_data[i] != null)
						{
							saveObj = bullet_data[i];
							pBullet = saveObj.GetComponent<Player_Bullet>();
							pBullet.bShot = myShot;
						}
					}

					//SE_Manager.SE_Obj.SE_Active(Obj_Storage.Storage_Data.audio_se[4]);
					Bullet_cnt += 1;
				}

				//if (Bullet_cnt < 8 && bullet_data.Count < 10)
    //            {
    //                bullet_data.Add(Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eP1_OPTION_BULLET, shot_Mazle.transform.position, Direction));
    //                for (int i = 0; i < bullet_data.Count; i++)
    //                {
    //                    if (bullet_data[i] != null)
    //                    {
    //                        saveObj = bullet_data[i];
    //                        pBullet = saveObj.GetComponent<Player_Bullet>();
    //                        pBullet.bShot = myShot;
    //                    }
    //                }

    //                SE_Manager.SE_Obj.SE_Active(Obj_Storage.Storage_Data.audio_se[4]);
				//	Bullet_cnt += 1;
				//}
			}
			if (Bullet_cnt_Max != 8)
			{
				Bullet_cnt_Max = 8;
			}

			shot_Delay = 0;
		}
		else if (bf.bState == Bit_Formation_3.BitState.Player2)
		{
			if (!pl2.Is_Change_Auto)
			{
                if (Bullet_cnt < 8)
                {
                    saveObj = Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eP2_OPTION_BULLET, shot_Mazle.transform.position, Direction);
                    pBullet = saveObj.GetComponent<Player_Bullet>();
                    pBullet.bShot = myShot;

                    //SE_Manager.SE_Obj.SE_Active(Obj_Storage.Storage_Data.audio_se[4]);
                    Bullet_cnt += 1;
                }
            }
            else
			{
                if (Bullet_cnt < 8 && bullet_data.Count < 10)
                {
                    bullet_data.Add(Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eP2_OPTION_BULLET, shot_Mazle.transform.position, Direction));
                    for (int i = 0; i < bullet_data.Count; i++)
                    {
                        if (bullet_data[i] != null)
                        {
                            saveObj = bullet_data[i];
                            pBullet = saveObj.GetComponent<Player_Bullet>();
                            pBullet.bShot = myShot;
                        }
                    }

                    //SE_Manager.SE_Obj.SE_Active(Obj_Storage.Storage_Data.audio_se[4]);
                    Bullet_cnt += 1;
                }
            }
            if (Bullet_cnt_Max != 8)
			{
				Bullet_cnt_Max = 8;
			}
			shot_Delay = 0;
		}
	}

	//ダブル発射関数
	private void Double_Fire()
	{
		if(bf.bState==Bit_Formation_3.BitState.Player1)
		{
            if (bullet_data.Count < 16)
            {
                bullet_data.Add(Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eP1_OPTION_BULLET, shot_Mazle.transform.position, Direction));
                for (int i = 0; i < bullet_data.Count; i++)
                {
                    if (bullet_data[i] != null)
                    {
                        saveObj = bullet_data[i];
                        pBullet = saveObj.GetComponent<Player_Bullet>();
                        pBullet.bShot = myShot;
                    }
                }

                saveObj = Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eP1_OPTION_BULLET, shot_Mazle.transform.position, Quaternion.Euler(0, 0, 45));
                pBullet = saveObj.GetComponent<Player_Bullet>();
                pBullet.bShot = myShot;

                SE_Manager.SE_Obj.SE_Active(Obj_Storage.Storage_Data.audio_se[4]);
				Bullet_cnt += 2;
			}
            if (Bullet_cnt_Max != 20)
            {
                Bullet_cnt_Max = 20;
            }

        }
        if (bf.bState == Bit_Formation_3.BitState.Player2)
		{
            if (bullet_data.Count < 16)
            {
                bullet_data.Add(Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eP2_OPTION_BULLET, shot_Mazle.transform.position, Direction));
                for (int i = 0; i < bullet_data.Count; i++)
                {
                    if (bullet_data[i] != null)
                    {
                        saveObj = bullet_data[i];
                        pBullet = saveObj.GetComponent<Player_Bullet>();
                        pBullet.bShot = myShot;
                    }
                }

                saveObj = Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eP2_OPTION_BULLET, shot_Mazle.transform.position, Quaternion.Euler(0, 0, 45));
                pBullet = saveObj.GetComponent<Player_Bullet>();
                pBullet.bShot = myShot;

                //SE_Manager.SE_Obj.SE_Active(Obj_Storage.Storage_Data.audio_se[4]);
				Bullet_cnt += 2;
			}
            if (Bullet_cnt_Max != 20)
            {
                Bullet_cnt_Max = 20;
            }

        }

        //if (Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space))
        //{
        //	Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eOPTION_BULLET, shot_Mazle.transform.position, Direction);
        //	Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eOPTION_BULLET, shot_Mazle.transform.position, /*new Quaternion(-8,1,45,0)Quaternion.Euler(0, 0, 45));
        //	shot_Delay = 0;
        //}
    }

	private void Missile_Fire()
	{
		GameObject obj = Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePLAYER_MISSILE, shot_Mazle.transform.position, Direction);
		obj.GetComponent<Missile>().Setting_On_Reboot(1);
		missileDelayCnt = 0;
	}

	private void ActiveLaser()
	{
		//activeLaser = true;
		//activeDouble = false;
		//activeBullet = false;
		Debug.Log("レーザーに変更");
		//bullet_Type = Bullet_Type.Laser;
	}

}
