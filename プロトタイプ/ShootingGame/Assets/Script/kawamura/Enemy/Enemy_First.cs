//作成者：川村良太
//円盤形の敵のスクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class Enemy_First : character_status
{
	public enum State
	{
		TurnUp,				//上に曲がる
		TurnDown,			//下に曲がる
		defaultStraight,	//画面奥からくる直進
		Straight,			//排出とかで生成された時(曲がらない)
	}

	public State eState;			//移動状態
	public GameObject parentObj;	//親取得用
	GameObject childObj;			//子供取得用

	Vector3 velocity;
	public Vector3 defaultPos_Local;
    public Vector3 defaultPos_PlusZ;
	public Quaternion diedAttackRota;			//死んだ時に出す弾の角度範囲
	public Transform diedAttack_Transform;		//死んだ時に弾を出す場合の位置

	public EnemyGroupManage groupManage;		//群管理スクリプト取得用
	Find_Angle fd;								//プレイヤーの方向を向くスクリプト取得用（死亡時攻撃に使う）

	[Header("Zポジションの移動開始位置")]
	public float ZMovePos;

	public float defaultPosY_World;		//最初のワールドY
	public float defaultPosY_Local;		//最初のローカルY
	public float endPosY_Local;			//手前に来た時のYの位置
	public float localPosY;				
	//float frame = 0;
	public float speedX;
	public float speedX_Straight;	//排出された時のスピード
	public float speedY;
    public float speedZ;
    public float speedZ_Value;
	public float diedAttack_RotaZ;			//死亡時の弾の角度範囲
	[Header("死亡時の弾発射の角度範囲()")]
	public float diedAttack_RotaValue;
	bool once = true;
	bool isTurn;
	public bool haveItem = false;
	public bool Died_Attack = false;

	private void Awake()
	{

		defaultPos_Local = transform.localPosition;
        defaultPos_PlusZ = defaultPos_Local + new Vector3(0, 0, 40);
		if (gameObject.GetComponent<DropItem>())
		{
			DropItem dItem = gameObject.GetComponent<DropItem>();
			haveItem = true;
		}
	}

	new void Start()
	{
		childObj = transform.GetChild(0).gameObject;
		fd = childObj.GetComponent<Find_Angle>();

		if (transform.parent)
		{
			parentObj = transform.parent.gameObject;
			groupManage = parentObj.GetComponent<EnemyGroupManage>();
			speedX = 5;
			speedY = 5.0f;
		}

		HP_Setting();
		base.Start();

	}

	new void Update()
	{
		if(once)
		{
			if (parentObj)
			{
                transform.localPosition = defaultPos_PlusZ;

                defaultPosY_World = transform.position.y;
				defaultPosY_Local = transform.localPosition.y;

				endPosY_Local = defaultPosY_World * -0.29f;

				speedX = 5;
				speedY = 5;
			}
			once = false;
		}

		//移動関数呼び出し
		Move();
		HSV_Change();
		//倒されたとき
		if (hp < 1)
		{
			if (haveItem)
			{
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePOWERUP_ITEM, this.transform.position, transform.rotation);
			}

			//if(Died_Attack)
			//{
			//	//死亡時攻撃の処理
			//	diedAttack_Transform = childObj.transform;
			//	diedAttack_RotaZ = Random.Range(fd.degree - diedAttack_RotaValue, fd.degree + diedAttack_RotaValue);
			//	diedAttack_Transform.rotation = Quaternion.Euler(0, 0, diedAttack_RotaZ);
			//	diedAttackRota = Quaternion.Euler(0, 0, Random.Range(fd.degree - diedAttack_RotaValue, fd.degree + diedAttack_RotaValue));
			//	Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET, transform.position, diedAttackRota);

			//}

			if (parentObj != null)
			{			
				//群を管理している親の残っている敵カウントマイナス
				groupManage.remainingEnemiesCnt--;
				//倒された敵のカウントプラス
				groupManage.defeatedEnemyCnt++;
				//群に残っている敵がいなくなったとき
				if (groupManage.remainingEnemiesCnt == 0)
				{
					//倒されずに画面外に出た敵がいなかったとき(すべての敵が倒されたとき)
					if (groupManage.notDefeatedEnemyCnt == 0 && groupManage.isItemDrop)
					{
						//アイテム生成
						//Instantiate(item, this.transform.position, transform.rotation);
						Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePOWERUP_ITEM, this.transform.position, transform.rotation);
					}
					//一体でも倒されていなかったら
					else
					{
						//なにもしない
					}
				}
			}
			Enemy_Reset();
			Died_Process();
		}

		base.Update();
	}

	//---------ここから関数--------------

	//移動の関数
	void Move()
	{
		switch (eState)
		{
			//画面奥からくる直進
			case State.defaultStraight:
				MoveStraight();
				break;

			case State.TurnUp:
				if (!isTurn)
				{
					MoveStraight();
				}
				else if (isTurn)
				{
					velocity = gameObject.transform.rotation * new Vector3(speedX, speedY, 0);
					gameObject.transform.position += velocity * Time.deltaTime;

					speedX -= 0.12f;

					if (transform.position.y > 0.5)
					{
						speedX -= 0.36f;
						speedY += 0.36f;
					}
					if (speedX < -12.0f)
					{
						speedX = -12.0f;
					}
				}
				break;

			case State.TurnDown:
				if (!isTurn)
				{
					MoveStraight();
				}
				else if (isTurn)
				{
					velocity = gameObject.transform.rotation * new Vector3(speedX, -speedY, 0);
					gameObject.transform.position += velocity * Time.deltaTime;

					speedX -= 0.12f;

					if (transform.position.y < -0.5)
					{
						speedX -= 0.36f;
						speedY += 0.36f;
					}
					if (speedX < -12.0f)
					{
						speedX = -12.0f;
					}
				}
				break;

			case State.Straight:
				speedX = speedX_Straight;
				velocity = gameObject.transform.rotation * new Vector3(-speedX, 0, 0);
				gameObject.transform.position += velocity * Time.deltaTime;

				break;
		}
	}

	//直進移動関数
	void MoveStraight()
	{
		velocity = gameObject.transform.rotation * new Vector3(-speedX, 0, -speedZ);
		gameObject.transform.position += velocity * Time.deltaTime;
		//見た目上のY座標を常に一緒になるように調整
		localPosY = transform.localPosition.z * (-endPosY_Local / 20.0f) + endPosY_Local;
		//座標を代入
		gameObject.transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY_Local + localPosY, transform.localPosition.z);

		//手前に来たら
		if (transform.position.z < 0)
		{
			//Zスピード0にしたりする
			transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
			speedZ = 0;
			speedZ_Value = 0;
		}

		//手前に来始める位置に来たらZスピードを入れる
		if (transform.localPosition.x <= ZMovePos)
		{
			speedZ = speedZ_Value;
		}
		if (transform.localPosition.x <= -42)
		{
			speedX = 7.5f;
			if (eState == State.TurnDown || eState == State.TurnUp)
			{
				speedX = 5;
			}
			isTurn = true;
		}

		//else if (transform.localPosition.x < -21)
		//加速したスピードを減速
		else if (transform.localPosition.x < -25.5)
		{
			speedX -= 0.48f;
			if (speedX < 7.5f)
			{
				speedX = 7.5f;
			}

		}
		//手前に来るときにXスピードを上げてスピード感を出す
		else if (transform.localPosition.x < ZMovePos)
		{
			speedX += 0.24f;
		}

	}

	//状態を変える(主に出現時に曲がらせたくないときに使うと思われます)
	public void SetState(int num)
	{
		switch (num)
		{
			//上に曲がる
			case 0:
				eState = State.TurnUp;
				break;

			//下に曲がる
			case 1:
				eState = State.TurnDown;
				break;

			//直進
			case 2:
				eState = State.Straight;
				break;
		}
	}
	void Enemy_Reset()
	{
		speedZ_Value = 75;
		once = true;
		isTurn = false;
		Is_Dead = false;
	}
	private void OnTriggerExit(Collider col)
	{
		if (col.gameObject.name == "WallUnder" || col.gameObject.name == "WallTop")
		{
			if (parentObj)
			{
				//if (parentObj.name == "enemy_UFO_Group")
				//{
				groupManage.notDefeatedEnemyCnt++;
				groupManage.remainingEnemiesCnt -= 1;
				//}
			}
			Enemy_Reset();
			gameObject.SetActive(false);

		}
		else if(eState==State.defaultStraight&& col.gameObject.name == "WallLeft")
		{
			if (parentObj)
			{
				//if (parentObj.name == "enemy_UFO_Group")
				//{
				groupManage.notDefeatedEnemyCnt++;
				groupManage.remainingEnemiesCnt -= 1;
				//}
			}

			Enemy_Reset();
			gameObject.SetActive(false);

		}
		else if (eState == State.Straight && (col.gameObject.name == "WallLeft" || col.gameObject.name == "WallRight"))
		{
			Enemy_Reset();
			gameObject.SetActive(false);
		}
	}
}
