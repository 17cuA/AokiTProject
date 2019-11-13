//作成者：川村良太
//4つのオプションの追従位置オブジェクトの親スクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPositions : MonoBehaviour
{
	GameObject playerObj;
	public Vector3 pos;
	public Vector3 defPos;
	public Vector3 savePos;

	Player1 pl1;
	Player2 pl2;

	public string myName;

	public int resetPosCnt;

	bool check = false;
	public bool isFreeze = false;
	public bool isMove;
	bool defCheck;
	public bool isFollow1P;
	public bool isFollow2P;
	public bool isResetPosEnd;
	private void Awake()
	{
		
	}
	void Start()
    {
		isResetPosEnd = false;
		resetPosCnt = 0;
		myName = gameObject.name;

		if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eONE_PLAYER)
		{
			if (myName == "Four_FollowPos_2P")
			{
				gameObject.SetActive(false);    //オブジェクトをオフにする
			}
		}
		if (myName == "Four_FollowPos_1P")
		{
			isFollow1P = true;
		}
		else if (myName == "Four_FollowPos_2P")
		{
			isFollow2P = true;
		}
    }

	void Update()
    {
		if(playerObj==null)
		{
			if (isFollow1P)
			{
				if (GameObject.Find("Player"))
				{
					playerObj = Obj_Storage.Storage_Data.GetPlayer();/*GameObject.Find("Player")*/;
					pl1 = playerObj.GetComponent<Player1>();
					check = true;
					defCheck = true;
					pos = playerObj.transform.position;
					savePos = playerObj.transform.position;
				}
			}
			else if (isFollow2P)
			{
				if (GameObject.Find("Player_2"))
				{
					playerObj = Obj_Storage.Storage_Data.GetPlayer2();/*GameObject.Find("Player_2")*/;
					pl2 = playerObj.GetComponent<Player2>();
					check = true;
					defCheck = true;
					pos = playerObj.transform.position;
					savePos = playerObj.transform.position;
				}
			}
		}

		if (pos == playerObj.transform.position)
		{
			isMove = false;
		}
		else
		{
			isMove = true;
			pos = playerObj.transform.position;
		}

		if (check)
		{
			if (isFollow1P)
			{
				if (Input.GetButtonUp(pl1.InputManager.Manager.Button["Multiple"]) || Input.GetKeyUp(KeyCode.Y))
				{
					isFreeze = false;
					//transform.parent = null;
				}
				else if (Input.GetButton(pl1.InputManager.Manager.Button["Multiple"]) || Input.GetKey(KeyCode.Y))
				{
					isFreeze = true;
					//transform.parent = playerObj.transform;

				}

			}
			else if (isFollow2P)
			{
				if (Input.GetButtonUp(pl2.InputManager.Manager.Button["Multiple"]) || Input.GetKeyUp(KeyCode.Y))
				{
					isFreeze = false;
					//transform.parent = null;
				}
				else if (Input.GetButton(pl2.InputManager.Manager.Button["Multiple"]) || Input.GetKey(KeyCode.Y))
				{
					isFreeze = true;
					//transform.parent = playerObj.transform;

				}

			}
			//savePos = playerObj.transform.position;
		}

		if (!isFreeze)
		{
			if (defCheck)
			{
			}

			//プレイヤーが動いていて位置配列すべてに値が入っているとき
			if (isMove)
			{
				savePos = playerObj.transform.position;
			}
		}

		else if (isFreeze && isMove)
		{
			defPos = pos - savePos;
			//defPos = playerObj.transform.position - transform.position;
			transform.position = transform.position + defPos;
			savePos = playerObj.transform.position;
		}

		if (isFollow1P)
		{
			if (pl1.Is_Resporn_End)
			{
				isResetPosEnd = false;
			}
			//pl1.Is_Resporn_End = false;
		}
		else if (isFollow2P)
		{
			if (pl2.Is_Resporn_End)
			{
				isResetPosEnd = false;
			}
			//pl2.Is_Resporn_End = false;
		}


		if (resetPosCnt == 4)
		{
			resetPosCnt = 0;
			if (isFollow1P)
			{
				pl1.Is_Resporn_End = false;
			}
			else if (isFollow2P)
			{
				pl2.Is_Resporn_End = false;				
			}
			isResetPosEnd = true;
			isFreeze = false;
		}
	}
}
