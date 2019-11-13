using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ClamChowder_Spawner : MonoBehaviour
{
	public enum EnemyMoveState
	{
		BackWave,
		WaveOnlyUp,
		WaveOnlyDown,
		WaveAndStraight,
		Straight,
		Rush,
	}

	public EnemyMoveState moveState;

	public GameObject sideObj;
	GameObject saveObj;
	public GameObject[] saveChildObj;
	Enemy_Wave WaveScript;
	public GameObject[] waveStraightPos;
	Enemy_ClamChouder_Side side_Script_Base;
	Enemy_ClamChouder_Side side_Script_copy;

	public int createNum;                   //作り出す処理の回数（敵の数ではない）
	public int createCnt = 0;				//敵を作り出す処理の回数カウント（敵の数ではない）
	public int createDelay = 0;				//敵を作るときの間隔
	public int childNum;                    //敵(子供)の総数
	public int remainingEnemiesCnt;         //残っている敵の数
	public int defeatedEnemyCnt = 0;        //倒された敵の数
	public int notDefeatedEnemyCnt = 0;     //倒されずに画面外に出た数
	public string myName;

	bool isCreate = true;
	bool isFirstAppearance = false;     //一番はじめに出す敵が出たかどうか
	public bool isAppearanceEnd = false;       //敵を出し終わったかどうか
	public bool isItemDrop = true;


	void Start()
	{

	}

	void Update()
	{
		//敵の出現が終わっていなかったら
		if (!isAppearanceEnd)
		{
			//出現関数呼び出し
			EnemyAppearance(moveState, createNum);
			createDelay++;

		}

		//倒された敵の数と倒されずに画面外に出た敵の数の合計が最初の子供の数と同じになったら
		if ((defeatedEnemyCnt + notDefeatedEnemyCnt == childNum) && isAppearanceEnd)
		{
			//remainingEnemiesCnt = childNum;
			ResetState();
			gameObject.SetActive(false);
			//Destroy(this.gameObject);
			//gameObject.SetActive(false);

			//isDead = true;
			//Died_Process();
		}
	}

	//出現させる敵の動きと数の設定
	void AppearanceSetting(EnemyMoveState state, int num)
	{
		moveState = state;
		createNum = num;
	}

	//敵を出現させる
	void EnemyAppearance(EnemyMoveState state, int num)
	{
		switch (state)
		{
			case EnemyMoveState.BackWave:
				break;

			case EnemyMoveState.WaveOnlyUp:
				break;

			case EnemyMoveState.WaveOnlyDown:
				break;

			case EnemyMoveState.WaveAndStraight:
				if ((createCnt < num && createDelay >= 13) || !isFirstAppearance)
				{
					//敵を出現させて、子供にして、指定の位置に移動させて、挙動の状態を入れる
					//saveObj = Obj_Storage.Storage_Data.ClamChowderType_Enemy.Active_Obj();
					saveObj = Instantiate(sideObj,transform.position, transform.rotation);
					saveObj.transform.parent = gameObject.transform;
					saveObj.transform.position = waveStraightPos[0].transform.position;
					//saveObj.GetComponent<Enemy_Wave>().SetState(Enemy_Wave.State.WaveOnlyDown);
					saveObj.GetComponent<Enemy_ClamChouder_Side>().SetState(Enemy_ClamChouder_Side.State.WaveOnlyDown);
					//最初に出現した敵を保存する
					if (!isFirstAppearance)
					{
						side_Script_Base = saveObj.GetComponent<Enemy_ClamChouder_Side>();
						saveChildObj[0] = saveObj.transform.GetChild(0).gameObject;
					}
					else if (isFirstAppearance)
					{
						side_Script_copy = saveObj.GetComponent<Enemy_ClamChouder_Side>();
						side_Script_copy.speedY = side_Script_Base.speedY;
						side_Script_copy.isAddSpeedY = side_Script_Base.isAddSpeedY;
						side_Script_copy.isSubSpeedY = side_Script_Base.isSubSpeedY;
						saveObj.transform.position = new Vector3(transform.position.x, saveChildObj[0].transform.position.y, 0);
						saveObj.transform.GetChild(0).gameObject.transform.eulerAngles = saveChildObj[0].transform.eulerAngles;
					}

					//敵を出現させて、子供にして、指定の位置に移動させて、挙動の状態を入れる
					//saveObj = Obj_Storage.Storage_Data.ClamChowderType_Enemy.Active_Obj();
					saveObj = Instantiate(sideObj, transform.position, transform.rotation);
					saveObj.transform.parent = gameObject.transform;
					saveObj.transform.position = waveStraightPos[1].transform.position;
					//saveObj.GetComponent<Enemy_Wave>().SetState(Enemy_Wave.State.Straight);
					saveObj.GetComponent<Enemy_ClamChouder_Side>().SetState(Enemy_ClamChouder_Side.State.Straight);
					//最初に出現した敵を保存する
					if (!isFirstAppearance)
					{
						side_Script_Base = saveObj.GetComponent<Enemy_ClamChouder_Side>();
						saveChildObj[1] = saveObj.transform.GetChild(0).gameObject;
					}
					else if (isFirstAppearance)
					{
						side_Script_copy = saveObj.GetComponent<Enemy_ClamChouder_Side>();
						side_Script_copy.speedY = side_Script_Base.speedY;
						side_Script_copy.isAddSpeedY = side_Script_Base.isAddSpeedY;
						side_Script_copy.isSubSpeedY = side_Script_Base.isSubSpeedY;
						saveObj.transform.position = new Vector3(transform.position.x, saveChildObj[1].transform.position.y, 0);
						saveObj.transform.GetChild(0).gameObject.transform.eulerAngles = saveChildObj[1].transform.eulerAngles;
					}

					//敵を出現させて、子供にして、指定の位置に移動させて、挙動の状態を入れる
					//saveObj = Obj_Storage.Storage_Data.ClamChowderType_Enemy.Active_Obj();
					saveObj = Instantiate(sideObj, transform.position, transform.rotation);
					saveObj.transform.parent = gameObject.transform;
					saveObj.transform.position = waveStraightPos[2].transform.position;
					//saveObj.GetComponent<Enemy_Wave>().SetState(Enemy_Wave.State.WaveOnlyUp);
					saveObj.GetComponent<Enemy_ClamChouder_Side>().SetState(Enemy_ClamChouder_Side.State.WaveOnlyUp);
					//最初に出現した敵を保存する
					if (!isFirstAppearance)
					{
						side_Script_Base = saveObj.GetComponent<Enemy_ClamChouder_Side>();
						saveChildObj[2] = saveObj.transform.GetChild(0).gameObject;
					}
					else if (isFirstAppearance)
					{
						side_Script_copy = saveObj.GetComponent<Enemy_ClamChouder_Side>();
						side_Script_copy.speedY = side_Script_Base.speedY;
						side_Script_copy.isAddSpeedY = side_Script_Base.isAddSpeedY;
						side_Script_copy.isSubSpeedY = side_Script_Base.isSubSpeedY;

						saveObj.transform.position = new Vector3(transform.position.x, saveChildObj[2].transform.position.y, 0);
						saveObj.transform.GetChild(0).gameObject.transform.eulerAngles = saveChildObj[2].transform.eulerAngles;
					}

					saveObj = null;
					isFirstAppearance = true;
					createDelay = 0;
					createCnt++;
					childNum += 3;
					remainingEnemiesCnt += 3;
				}
				if (createCnt == num)
				{
					isAppearanceEnd = true;
				}

				break;

			case EnemyMoveState.Straight:
				break;

			case EnemyMoveState.Rush:
				break;
		}
	}

	//アイテムを落とすかどうか
	public void WhetherToDropTheItem(bool isDrop)
	{
		isItemDrop = isDrop;
	}

	//いろいろリセット
	void ResetState()
	{
		//倒されたのと画面外に出たカウントをリセット
		notDefeatedEnemyCnt = 0;
		defeatedEnemyCnt = 0;
		remainingEnemiesCnt = 0;
		createNum = 0;
		createCnt = 0;
		childNum = 0;
		isFirstAppearance = false;
		isAppearanceEnd = false;
	}
}
