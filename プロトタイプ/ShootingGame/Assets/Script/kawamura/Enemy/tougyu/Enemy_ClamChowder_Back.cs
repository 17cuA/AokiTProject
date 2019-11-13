//作成者：川村良太
//画面奥からくる敵　敵の出現方法を変えるために新しく作ったやつ

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class Enemy_ClamChowder_Back : character_status
{
	public enum State
	{
		WaveUp,
		WaveDown,
	}
	public State eState;

	GameObject childObj;        //子供入れる
	public GameObject childObj_Shot;
	public GameObject childObj_Angle;
	GameObject item;            //アイテム入れる
	GameObject parentObj;       //親入れる（群れの時のため）
	GameObject poolingParentObj;

	EnemyGroupManage groupManage;           //群れの時の親スクリプト
	Enemy_ClamChowder_Spawner clamSpawner_Script;
	Find_Angle fd;

	Vector3 velocity;
	Vector3 defaultPos;
	public Quaternion diedAttackRota;

	//----------
	public Vector3 startMarker;
	public Vector3 endMarker;
	float startTime;
	public float slaep_IncValue;
	float present_Location;
	public float testSpeed = 1.0f;

	private float distance_two;
	//----------

	public float speedX;            //Xスピード
	public float speedY;            //Yスピード
	public float speedZ;            //Zスピード（移動時）
	public float speedZ_Value;      //Zスピードの値だけ
	float startPosY;                //最初のY座標値
	float rotaY;                    //Y角度
	public float amplitude;         //画面奥から出てこない時の上下の振れ幅
	public float defaultSpeedY;         //Yスピードの初期値（最大値でもある）を入れておく
	public float addAndSubValue;        //Yスピードを増減させる値

	public float sin;
	[Header("死亡時の弾発射の角度範囲()")]
	public float diedAttack_RotaValue;

	public bool isAddSpeedY = false;    //Yスピードを増加させるかどうか
	public bool isSubSpeedY = false;    //Yスピードを減少させるかどうか

	public bool once = true;            //updateで一回だけ呼び出す処理用
	public bool isWave = false;         //奥からくる敵を上下移動に変える用
	public bool isOnlyWave;             //上下移動のみか（左へ進みながら）
	public bool haveItem = false;

	public bool isSlerp = false;
	public bool isNoSlerp = false;
	public bool utsuttemasuyo = false;
	public bool Died_Attack = false;
	public bool isFromBack = false;             //奥からくるやつ用
	public bool isBehind = false;
	//float present_Location = 0;
	//---------------------------------------------------------

	private void Awake()
	{
		defaultPos = transform.localPosition;

		if (gameObject.GetComponent<DropItem>())
		{
			DropItem dItem = gameObject.GetComponent<DropItem>();
			haveItem = true;
		}
		switch (eState)
		{
			case State.WaveUp:
				isFromBack = true;
				isBehind = false;
				endMarker = new Vector3(12.0f, transform.position.y, 0);
				distance_two = Vector3.Distance(startMarker, endMarker);
				break;

			case State.WaveDown:
				isFromBack = true;
				isBehind = false;
				endMarker = new Vector3(12.0f, transform.position.y, 0);
				distance_two = Vector3.Distance(startMarker, endMarker);
				break;
		}
	}


	new void Start()
    {
		//startMarker = new Vector3(12.0f, transform.position.y, 40.0f);
		item = Resources.Load("Item/Item_Test") as GameObject;

		childObj = transform.GetChild(0).gameObject;            //モデルオブジェクトの取得（3Dモデルを子供にしているので）
		childObj_Angle = transform.GetChild(1).gameObject;
		//childCnt = transform.childCount;
		fd = childObj_Angle.GetComponent<Find_Angle>();

		if (transform.parent)
		{
			parentObj = transform.parent.gameObject;
			groupManage = parentObj.GetComponent<EnemyGroupManage>();
		}
		else
		{
			parentObj = GameObject.Find("TemporaryParent");
			transform.parent = parentObj.transform;
		}

		speedZ = 0;
		//posX = transform.position.x;
		startPosY = transform.position.y;
		//posZ = -5.0f;
		//defPosX = (13.0f - transform.position.x) / 120.0f;         //13.0fはとりあえず敵が右へ向かう限界の座標
		startTime = 0.0f;
		HP_Setting();
		base.Start();

	}

	new void Update()
	{
		if (once)
		{
			clamSpawner_Script = parentObj.GetComponent<Enemy_ClamChowder_Spawner>();
			//transform.localPosition = defaultPos;
			//状態によって値を変える
			switch (eState)
			{

				//画面左から右へ、後ろからきて上下移動は上からし始める
				case State.WaveUp:
					isOnlyWave = false;
					isBehind = false;
					if (defaultSpeedY < 0)
					{
						defaultSpeedY *= -1;
					}
					isSubSpeedY = true;
					isAddSpeedY = false;
					speedX = 18;
					speedZ_Value = 40;
					//transform.position = new Vector3(transform.position.x, transform.position.y, 40.0f);
					isWave = false;
					HSV_Change();
					break;

				//画面左から右へ、後ろからきて上下移動は下からし始める
				case State.WaveDown:
					isOnlyWave = false;
					isBehind = false;
					if (defaultSpeedY > 0)
					{
						defaultSpeedY *= -1;
					}
					isAddSpeedY = true;
					isSubSpeedY = false;
					speedX = 18;
					speedZ_Value = 40;
					//transform.position = new Vector3(transform.position.x, transform.position.y, 40.0f);
					isWave = false;
					HSV_Change();
					break;
			}
			once = false;
		}

		if (!isWave)
		{
			if (isSlerp)
			{
				present_Location = (Time.time * testSpeed) / distance_two;
				transform.position = Vector3.Slerp(startMarker, endMarker, startTime);
				startTime += slaep_IncValue;
				if (startTime > 1)
				{
					startTime = 1;
				}
				//startTime++;
				HSV_Change();

				if (transform.position == endMarker)
				{
					isWave = true;
					transform.position = new Vector3(transform.position.x, transform.position.y, 0);

					//前からなら
					if (!isBehind)
					{
						speedX = 7.5f;
					}
					//後ろからなら
					else if (isBehind)
					{
						speedX = -7.5f;
					}
					speedY = defaultSpeedY;

				}
			}
			else if (isNoSlerp)
			{
				velocity = gameObject.transform.rotation * new Vector3(speedX, 0, -speedZ);
				gameObject.transform.position += velocity * Time.deltaTime;

				//前からなら
				if (!isBehind)
				{
					if (transform.position.z < 0)
					{
						transform.position = new Vector3(transform.position.x, transform.position.y, 0);
						speedX = 7.5f;
						speedY = defaultSpeedY;
						isWave = true;
					}

					if (transform.position.x >= 12)
					{
						isSlerp = true;
						startMarker = transform.position;
						//sonicBoom.Play();
					}

					if (transform.position.x > 7)
					{
						//明るさを変える関数
						HSV_Change();

					}
				}
			}
		}
		else if (isWave)
		{
			speedX = 7.5f;
			//sin =posY + Mathf.Sin(Time.time*5);

			SpeedY_Check();
			SpeedY_Calculation();

			//this.transform.position = new Vector3(transform.position.x, sin, 0);
			//transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.frameCount * 0.1f), transform.position.z);
			velocity = gameObject.transform.rotation * new Vector3(-speedX, speedY, 0);
			gameObject.transform.position += velocity * Time.deltaTime;
		}
		if (hp < 1)
		{
			if (haveItem)
			{
				//Instantiate(item, this.transform.position, transform.rotation);
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePOWERUP_ITEM, this.transform.position, Quaternion.identity);

			}
			if (Died_Attack)
			{
				diedAttackRota = Quaternion.Euler(0, 0, Random.Range(fd.degree - diedAttack_RotaValue, fd.degree + diedAttack_RotaValue));
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET, transform.position, diedAttackRota);

			}

			if (parentObj)
			{
				if (parentObj.name != "TemporaryParent")
				{
					//群を管理している親の残っている敵カウントマイナス
					//groupManage.remainingEnemiesCnt--;
					clamSpawner_Script.remainingEnemiesCnt--;
					//倒された敵のカウントプラス
					//groupManage.defeatedEnemyCnt++;
					clamSpawner_Script.defeatedEnemyCnt++;
					//群に残っている敵がいなくなったとき
					//if (groupManage.remainingEnemiesCnt == 0)
					//{
					//	//倒されずに画面外に出た敵がいなかったとき(すべての敵が倒されたとき)
					//	if (groupManage.notDefeatedEnemyCnt == 0 && groupManage.isItemDrop)
					//	{
					//		//アイテム生成
					//		//Instantiate(item, this.transform.position, transform.rotation);
					//		Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePOWERUP_ITEM, this.transform.position, transform.rotation);
					//	}
					//	//一体でも倒されていないのがいたら
					//	else
					//	{
					//		//なにもしない
					//	}
					//	groupManage.itemPos = transform.position;
					//	groupManage.itemTransform = this.transform;
					//}
					if (clamSpawner_Script.remainingEnemiesCnt == 0)
					{
						//倒されずに画面外に出た敵がいなかったとき(すべての敵が倒されたとき)
						if (clamSpawner_Script.notDefeatedEnemyCnt == 0 && clamSpawner_Script.isItemDrop)
						{
							//アイテム生成
							Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePOWERUP_ITEM, this.transform.position, transform.rotation);
						}
						//一体でも倒されていないのがいたら
						else
						{
							//なにもしない
						}
					}
				}
			}
			Enemy_Reset();
			//Reset_Status();
			Died_Process();
		}
		base.Update();

	}

	void SpeedY_Check()
	{
		if (defaultSpeedY >= 0)
		{
			//スピードが初期値以上になった時
			if (speedY > defaultSpeedY)
			{
				//増加をfalse 減少をtrue
				isAddSpeedY = false;
				isSubSpeedY = true;
			}
			//スピードが0以下になったとき
			else if (speedY < -defaultSpeedY)
			{
				//減少をfalse 増加をtrue
				isSubSpeedY = false;
				isAddSpeedY = true;
			}
		}
		else if (defaultSpeedY < 0)
		{
			//スピードが初期値以上になった時
			if (speedY > -defaultSpeedY)
			{
				//増加をfalse 減少をtrue
				isAddSpeedY = false;
				isSubSpeedY = true;
			}
			//スピードが0以下になったとき
			else if (speedY < defaultSpeedY)
			{
				//減少をfalse 増加をtrue
				isSubSpeedY = false;
				isAddSpeedY = true;
			}
		}
	}

	//スピードを増減させる
	void SpeedY_Calculation()
	{
		//増加がtrueなら
		if (isAddSpeedY)
		{
			//Yスピードを増加
			speedY += addAndSubValue;
		}
		//減少がtrueなら
		else if (isSubSpeedY)
		{
			//Yスピードを減少
			speedY -= addAndSubValue;
		}
	}
	void Enemy_Reset()
	{
		startTime = 0;
		speedZ = 0;
		speedY = 0;
		once = true;
		isSlerp = false;
		isWave = false;
	}

	private void OnTriggerExit(Collider col)
	{
		if (col.gameObject.name == "BattleshipType_Enemy(Clone)"|| col.gameObject.name == "BattleshipType_Enemy")
		{
			hp = 0;
		}
	}

}
