//作成者：川村良太
//1つ前のオプションと同じ動きで追従する位置用オブジェクトのスクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowToPreviousBit : MonoBehaviour
{
	public GameObject playerObj;
	Player1 pl1;
	Player2 pl2;
	GameObject previousBitObj;
	public GameObject parentObj;
	public string parentName;
	FollowPositions followParent_Script;    //t4つの追従位置の親スクリプト
	FollowToPlayer_SameMotion firstPos_Script;
	FollowToPreviousBit followBit_Script;

	public Vector3[] previousBitPos;
	public Vector3 pos;
    Vector3 savePos;                //フリーズ開始時の座標を入れる
    Vector3 defPos;                 //フリーズ解除時、開始時と今の座標との差を入れる

	Vector3 freesePos;

    public int cnt;
	int array_Num;
	public int childCnt;

	public string myName;

	//bool one = true;
	//bool once = true;
	public bool check = false;      //配列すべてに値が入っているかの判定
	public bool isMove = false;
	public bool hasOption = false;
	bool defCheck = false;
	public bool isFreeze = false;
	public bool isFollow1P;
	public bool isFollow2P;
	public bool isPlayerLive;       //プレイヤーを取得したらtrue
	public bool isResetPos;         //リスポーン終了時に位置をリセットしたかどうか
	public bool endDDDDDDDDDDDDDDDDDDDDDDDDDDD = false;
	void Start()
	{
		isPlayerLive = false;
		myName = gameObject.name;

		parentObj = transform.parent.gameObject;
		parentName = parentObj.name;
		followParent_Script = parentObj.GetComponent<FollowPositions>();

		if (parentName == "Four_FollowPos_1P")
		{
			isFollow1P = true;
			isFollow2P = false;
		}
		else if (parentName == "Four_FollowPos_2P")
		{
			isFollow2P = true;
			isFollow1P = false;
		}

		if (isFollow1P)
		{
			if (myName == "FollowPosSecond_1P")
			{
				previousBitObj = GameObject.Find("FollowPosFirst_1P");
				firstPos_Script = previousBitObj.GetComponent<FollowToPlayer_SameMotion>();
			}
			else if (myName == "FollowPosThird_1P")
			{
				previousBitObj = GameObject.Find("FollowPosSecond_1P");
				followBit_Script = previousBitObj.GetComponent<FollowToPreviousBit>();
			}
			else if (myName == "FollowPosFourth_1P")
			{
				previousBitObj = GameObject.Find("FollowPosThird_1P");
				followBit_Script = previousBitObj.GetComponent<FollowToPreviousBit>();
			}
		}
		else if (isFollow2P)
		{
			if (myName == "FollowPosSecond_2P")
			{
				previousBitObj = GameObject.Find("FollowPosFirst_2P");
				firstPos_Script = previousBitObj.GetComponent<FollowToPlayer_SameMotion>();
			}
			else if (myName == "FollowPosThird_2P")
			{
				previousBitObj = GameObject.Find("FollowPosSecond_2P");
				followBit_Script = previousBitObj.GetComponent<FollowToPreviousBit>();
			}
			else if (myName == "FollowPosFourth_2P")
			{
				previousBitObj = GameObject.Find("FollowPosThird_2P");
				followBit_Script = previousBitObj.GetComponent<FollowToPreviousBit>();
			}
		}


		//int cnt = 0;
		array_Num = 9;
		previousBitPos = new Vector3[array_Num];

		defCheck = true;
		pos = previousBitObj.transform.position;

	}

	void Update()
	{
		childCnt = this.transform.childCount;

		//プレイヤー格納がnullなら入れる
		if (playerObj == null)
		{
			if (isFollow1P)
			{
				//プレイヤーがいたら入れる
				playerObj = Obj_Storage.Storage_Data.GetPlayer();
                pl1 = playerObj.GetComponent<Player1>();
				//isMove = true;
				//playerPos[cnt] = playerObj.transform;
				transform.position = playerObj.transform.position;
				defCheck = true;
				//pos = playerObj.transform.position;

			}
			else if (isFollow2P)
			{
				//プレイヤーがいたら入れる
				playerObj = Obj_Storage.Storage_Data.GetPlayer2();
                pl2 = playerObj.GetComponent<Player2>();
				//isMove = true;
				//playerPos[cnt] = playerObj.transform;
				transform.position = playerObj.transform.position;
				defCheck = true;
				//isPlayerLive = true;
				//pos = playerObj.transform.position;

			}
		}
		else
		{
			isPlayerLive = true;
		}

		if (isFollow1P)
		{
			if (isPlayerLive)
			{
				//if (pl1.Is_Resporn_End)
				//{
				//	if (!isResetPos)
				//	{
				//		endDDDDDDDDDDDDDDDDDDDDDDDDDDD = true;
				//		//pl1.Is_Resporn_End = false;
				//		transform.position = playerObj.transform.position;
				//		pos = playerObj.transform.position;
				//		savePos = playerObj.transform.position;
				//		for (int i = 0; i < array_Num; i++)
				//		{
				//			previousBitPos[i] = playerObj.transform.position;
				//			previousBitPos[i] = new Vector3(previousBitPos[i].x, previousBitPos[i].y, 0);
				//		}

				//		//transform.position = playerObj.transform.position;
				//		//transform.position = new Vector3(transform.position.x, transform.position.y, 0);
				//		followParent_Script.resetPosCnt++;
				//		isResetPos = true;
				//	}
				//}

				if (!followParent_Script.isResetPosEnd && !isResetPos)
				{
					endDDDDDDDDDDDDDDDDDDDDDDDDDDD = true;
					//pl1.Is_Resporn_End = false;
					transform.position = playerObj.transform.position;
					pos = playerObj.transform.position;
					savePos = playerObj.transform.position;
					for (int i = 0; i < array_Num; i++)
					{
						previousBitPos[i] = playerObj.transform.position;
						previousBitPos[i] = new Vector3(previousBitPos[i].x, previousBitPos[i].y, 0);
					}

					//transform.position = playerObj.transform.position;
					//transform.position = new Vector3(transform.position.x, transform.position.y, 0);
					followParent_Script.resetPosCnt++;
					isResetPos = true;
					isFreeze = false;
				}
				if (!pl1.Is_Resporn)
				{
					if (Input.GetButtonUp(pl1.InputManager.Manager.Button["Multiple"]) || Input.GetKeyUp(KeyCode.Y))
					{
						isFreeze = false;
						defPos = transform.position - savePos;

						for (int i = 0; i < array_Num; i++)
						{
							previousBitPos[i] += defPos;
						}
						savePos = transform.position;
						pos = previousBitObj.transform.position;
					}
					else if (Input.GetButton(pl1.InputManager.Manager.Button["Multiple"]) || Input.GetKey(KeyCode.Y))
					{
						isFreeze = true;
					}
				}
			}
		}
		else if (isFollow2P)
		{
			if (isPlayerLive)
			{
				if (!followParent_Script.isResetPosEnd && !isResetPos)
				{
					//pl2.Is_Resporn_End = false;
					transform.position = playerObj.transform.position;
					pos = playerObj.transform.position;
					savePos = playerObj.transform.position;
					for (int i = 0; i < array_Num; i++)
					{
						previousBitPos[i] = playerObj.transform.position;
						previousBitPos[i] = new Vector3(previousBitPos[i].x, previousBitPos[i].y, 0);
					}

					//transform.position = playerObj.transform.position;
					//transform.position = new Vector3(transform.position.x, transform.position.y, 0);
					followParent_Script.resetPosCnt++;
					isResetPos = true;
				}

				if (!pl2.Is_Resporn)
				{
					if (Input.GetButtonUp(pl2.InputManager.Manager.Button["Multiple"]) || Input.GetKeyUp(KeyCode.Y))
					{
						isFreeze = false;
						defPos = transform.position - savePos;

						for (int i = 0; i < array_Num; i++)
						{
							previousBitPos[i] += defPos;
						}
						savePos = transform.position;
						pos = previousBitObj.transform.position;
					}
					else if (Input.GetButton(pl2.InputManager.Manager.Button["Multiple"]) || Input.GetKey(KeyCode.Y))
					{
						isFreeze = true;
					}
				}
			}
		}

		
		//前のビットの座標と今のビットの座標が違うとき　かつ　位置配列すべてに値が入っていないとき
		if (pos != previousBitObj.transform.position && !check)
		//if ((Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical") != 0))
		{
			//位置配列にビットの位置を入れる
			//playerPos[cnt] = playerObj.transform;
			previousBitPos[cnt] = previousBitObj.transform.position;
			//if(one)
			//{
			//	transform.position=previousBitPos[0];
			//	one=false;
			//}
			cnt++;
			if (cnt > array_Num - 1)
			{
				cnt = 0;
				check = true;
			}
		}
		//配列の最後まで値が入っていたら
		//if (previousBitPos[array_Num - 1] != null)
		//{
		//	check = true;
		//}

		if (!isFreeze)
		{
			if (defCheck)
			{
				if (isFollow1P)
				{
					//前のビットの座標が動いていないとき
					if (pos == previousBitObj.transform.position)
					//if ((Input.GetAxis("Horizontal") == 0) && (Input.GetAxis("Vertical") == 0))
					{
						isMove = false;
						if ((Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical") != 0))
						{
							isMove = true;
						}
					}
					//前のビットの座標が動いていたとき
					else
					{
						isMove = true;
						//前のビットのtransform保存
						pos = previousBitObj.transform.position;
					}
				}
				else if (isFollow2P)
				{
					//前のビットの座標が動いていないとき
					if (pos == previousBitObj.transform.position)
					//if ((Input.GetAxis("Horizontal") == 0) && (Input.GetAxis("Vertical") == 0))
					{
						isMove = false;
						if ((Input.GetAxis("P2_Horizontal") != 0) || (Input.GetAxis("P2_Vertical") != 0))
						{
							isMove = true;
						}
					}
					//前のビットの座標が動いていたとき
					else
					{
						isMove = true;
						//前のビットのtransform保存
						pos = previousBitObj.transform.position;
					}

				}
			}

			//前のビットが動いていて位置配列すべてに値が入っているとき
			if (isMove && check)
			{
				//isMove = false;
				//自分の位置を配列に入っている位置に
				//transform.position = playerPos[cnt].position;
				transform.position = previousBitPos[cnt];

				//自分の位置を移動したのでその位置を今、前のビットのいる位置で更新
				//playerPos[cnt] = playerObj.transform;
				previousBitPos[cnt] = previousBitObj.transform.position;

				cnt++;
				if (cnt > array_Num - 1)
				{
					cnt = 0;
				}
                savePos = transform.position;
            }
		}
		//else if(isFreeze)
		//{
		//	freesePos = playerObj.transform.position - savePos;

		//	this.transform.position += freesePos;
		//	savePos = transform.position;

		//}

		if(followParent_Script.isResetPosEnd)
		{
			isResetPos = false;
		}
	}
}
