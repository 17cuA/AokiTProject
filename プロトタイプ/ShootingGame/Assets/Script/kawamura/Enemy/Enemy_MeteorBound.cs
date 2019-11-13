//作成者：川村良太
//バウンドのスクリプト　バウンド時のスピードを親に渡したりする

//0.9 ~ -1 モデルのxの範囲
//1 ~ -1.2 モデルのyの範囲
//つまり
//xは1.8 ~ -2.0
//yは2.0 ~ -2.4 の範囲なのだ

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_MeteorBound : character_status
{
	GameObject parentObj;
	Enemy_MeteorBound_Move myBoundMove;   //親の移動スピード取得用
	Enemy_MeteorBound opponentMeteorBound;      //相手のバウンドスクリプト取得用

	Vector3 velocity;
	Vector3 defaultLocalPos;

	public float speedX;
	public float speedY;

	public float defPosX;
	public float defPosY;

	public float defPercentX;
	public float defPercentY;

	public string meteorname;
	public bool atarimasita_migigawa;
	public bool atarimasita = false;
	public bool baunndoooooooooooooooooo = false;
	new void Start()
	{
		parentObj = transform.parent.gameObject;
		myBoundMove = parentObj.GetComponent<Enemy_MeteorBound_Move>();
		defaultLocalPos = transform.localPosition;
		speedX = Random.Range(2.0f, 3.5f);
		HP_Setting();
		base.Start();
	}

	new void Update()
	{
		speedX = myBoundMove.speedX;
		speedY = myBoundMove.speedY;
		//velocity = gameObject.transform.rotation * new Vector3(-speedX, speedY, 0);
		//gameObject.transform.position += velocity * Time.deltaTime;
		transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);

		if (hp < 1)
		{
			Died_Process();
		}

		base.Update();
	}

	//private void OnCollisionEnter(Collision col)
	//{
	//	meteorname = col.gameObject.name;
	//	//atarimasita = true;

	//	if (col.gameObject.name == "WallTop")
	//	{
	//		baunndoooooooooooooooooo = true;
	//		myBoundMove.speedY *= -1;
	//	}
	//	else if (col.gameObject.name == "WallUnder")
	//	{
	//		baunndoooooooooooooooooo = true;
	//		myBoundMove.speedY *= -1;
	//	}

	//	else if (col.gameObject.name == "Enemy_MeteorBound_Model")
	//	{
	//		atarimasita = true;

	//		opponentMeteorBound = col.gameObject.GetComponent<Enemy_MeteorBound>();
	//		defPosX = col.transform.position.x - transform.position.x;
	//		defPosY = col.transform.position.y - transform.position.y;

	//		//敵が自分より右側
	//		if (defPosX > 0)
	//		{
	//			defPercentX = defPosX / 1.8f;
	//			if (defPercentX > 1)
	//			{
	//				defPercentX = 1;
	//			}
	//			//defPercentX *= 0.7f;
	//			defPercentX *= 1f;

	//			//if (opponentMeteorBound.speedX < 0)
	//			//{
	//			//	myBoundMove.speedY += opponentMeteorBound.speedY * defPercentY;
	//			//}
	//			//else
	//			//{
	//			//	myBoundMove.speedY -= opponentMeteorBound.speedY * defPercentY;
	//			//}
	//		}
	//		//敵が自分より左側
	//		else if (defPosX < 0)
	//		{
	//			defPercentX = defPosY / -2.0f;
	//			if (defPercentX > 1)
	//			{
	//				defPercentX = 1;
	//			}
	//			//defPercentX *= 0.7f;
	//			defPercentX *= 1f;
	//		}
	//		//x座標が一緒
	//		else
	//		{
	//			defPercentX = 0;
	//		}


	//		if (defPosY > 0)
	//		{
	//			defPercentY = defPosY / 2.0f;
	//			if (defPercentY > 1)
	//			{
	//				defPercentY = 1;
	//			}
	//			defPercentY *= 1f;
	//			//if (opponentMeteorBound.speedY < 0)
	//			//{
	//			//	myBoundMove.speedY += opponentMeteorBound.speedY * defPercentY;
	//			//}
	//			//else
	//			//{
	//			//	myBoundMove.speedY -= opponentMeteorBound.speedY * defPercentY;
	//			//}
	//		}
	//		else if (defPosY < 0)
	//		{
	//			defPercentY = defPosY / -2.4f;
	//			if (defPercentY > 1)
	//			{
	//				defPercentY = 1;
	//			}
	//			defPercentY *= 1f;

	//			//if (opponentMeteorBound.speedY < 0)
	//			//{
	//			//	myBoundMove.speedY -= opponentMeteorBound.speedY * defPercentY;
	//			//}
	//			//else
	//			//{
	//			//	myBoundMove.speedY += opponentMeteorBound.speedY * defPercentY;
	//			//}
	//		}
	//		//Y座標が一緒
	//		else
	//		{
	//			defPercentY = 0;
	//		}

	//		//当たった相手の位置が自分より上
	//		if (col.transform.position.y > transform.position.y)
	//		{
	//			if (opponentMeteorBound.speedY < 0)
	//			{
	//				//myBoundMove.speedY += opponentMeteorBound.speedY * defPercentY;
	//				myBoundMove.speedY = opponentMeteorBound.speedY * defPercentY - myBoundMove.speedY;
	//				//myBoundMove.speedY = opponentMeteorBound.speedY * defPercentY;
	//			}
	//			else
	//			{
	//				myBoundMove.speedY = myBoundMove.speedY - opponentMeteorBound.speedY * defPercentY;
	//				//myBoundMove.speedY -= opponentMeteorBound.speedY * defPercentY;
	//			}
	//		}
	//		//当たった相手の位置が自分より下
	//		else if (col.transform.position.y < transform.position.y)
	//		{
	//			if (opponentMeteorBound.speedY < 0)
	//			{
	//				myBoundMove.speedY = myBoundMove.speedY - opponentMeteorBound.speedY * defPercentY;
	//				//myBoundMove.speedY -= opponentMeteorBound.speedY * defPercentY;
	//			}
	//			else
	//			{
	//				myBoundMove.speedY = myBoundMove.speedY + opponentMeteorBound.speedY * defPercentY;
	//				//myBoundMove.speedY += opponentMeteorBound.speedY * defPercentY;
	//			}
	//		}

	//		//自分より相手が右側
	//		if (col.transform.position.x > transform.position.x)
	//		{
	//			if (opponentMeteorBound.speedX < 0)
	//			{
	//				if (myBoundMove.speedX < 0)
	//				{
	//					atarimasita_migigawa = true;
	//					myBoundMove.speedX = myBoundMove.speedX + opponentMeteorBound.speedX;
	//					//myBoundMove.speedX = opponentMeteorBound.speedX * defPercentX + myBoundMove.speedX;
	//					//myBoundMove.speedX += opponentMeteorBound.speedX - myBoundMove.speedX;
	//				}
	//				else
	//				{
	//					atarimasita_migigawa = true;
	//					myBoundMove.speedX = opponentMeteorBound.speedX * defPercentX + myBoundMove.speedX;
	//					//	myBoundMove.speedX = myBoundMove.speedX - opponentMeteorBound.speedX * defPercentX;
	//					//	//myBoundMove.speedX += opponentMeteorBound.speedX * defPercentX;
	//				}
	//			}
	//			else if (opponentMeteorBound.speedX > 0)
	//			{
	//				//if (myBoundMove.speedX > 0)
	//				//{
	//				//myBoundMove.speedX = myBoundMove.speedX - opponentMeteorBound.speedX * defPercentX;
	//				atarimasita_migigawa = true;
	//				myBoundMove.speedX = myBoundMove.speedX - (myBoundMove.speedX - myBoundMove.speedX - opponentMeteorBound.speedX * defPercentX);
	//				//myBoundMove.speedX -= myBoundMove.speedX - opponentMeteorBound.speedX;
	//				//}
	//				//	myBoundMove.speedX -= opponentMeteorBound.speedX * defPercentX;
	//				//}
	//			}
	//		}
	//		//自分より相手が左側
	//		else if (col.transform.position.x < transform.position.x)
	//		{
	//			if (opponentMeteorBound.speedX < 0)
	//			{
	//				if (myBoundMove.speedX < 0)
	//				{
	//					myBoundMove.speedX = myBoundMove.speedX - opponentMeteorBound.speedX;
	//				}
	//				else
	//				{
	//					//myBoundMove.speedX = myBoundMove.speedX - opponentMeteorBound.speedX * defPercentX;

	//				}
	//				myBoundMove.speedX = myBoundMove.speedX - opponentMeteorBound.speedX * defPercentX;
	//				//myBoundMove.speedX -= opponentMeteorBound.speedX * defPercentX;
	//			}
	//			else if (opponentMeteorBound.speedX > 0)
	//			{
	//				myBoundMove.speedX = myBoundMove.speedX + opponentMeteorBound.speedX * defPercentX;
	//			}
	//		}
	//		opponentMeteorBound = null;
	//	}

	//	//base.OnTriggerEnter(col);
	//}


	new void OnTriggerEnter(Collider col)
	{
		meteorname = col.gameObject.name;
		//atarimasita = true;

		if (col.gameObject.name == "WallTop")
		{
			baunndoooooooooooooooooo = true;
			myBoundMove.speedY *= -1;
		}
		else if (col.gameObject.name == "WallUnder")
		{
			baunndoooooooooooooooooo = true;
			myBoundMove.speedY *= -1;
		}

		else if (col.gameObject.name == "Enemy_MeteorBound_Model")
		{
			atarimasita = true;

			opponentMeteorBound = col.gameObject.GetComponent<Enemy_MeteorBound>();
			defPosX = col.transform.position.x - transform.position.x;
			defPosY = col.transform.position.y - transform.position.y;

			//敵が自分より右側
			if (defPosX > 0)
			{
				defPercentX = defPosX / 1.8f;
				if (defPercentX > 1)
				{
					defPercentX = 1;
				}
				//defPercentX *= 0.7f;
				defPercentX *= 1f;

				//if (opponentMeteorBound.speedX < 0)
				//{
				//	myBoundMove.speedY += opponentMeteorBound.speedY * defPercentY;
				//}
				//else
				//{
				//	myBoundMove.speedY -= opponentMeteorBound.speedY * defPercentY;
				//}
			}
			//敵が自分より左側
			else if (defPosX < 0)
			{
				defPercentX = defPosY / -2.0f;
				if (defPercentX > 1)
				{
					defPercentX = 1;
				}
				//defPercentX *= 0.7f;
				defPercentX *= 1f;
			}
			//x座標が一緒
			else
			{
				defPercentX = 0;
			}


			if (defPosY > 0)
			{
				defPercentY = defPosY / 2.0f;
				if (defPercentY > 1)
				{
					defPercentY = 1;
				}
				defPercentY *= 1f;
				//if (opponentMeteorBound.speedY < 0)
				//{
				//	myBoundMove.speedY += opponentMeteorBound.speedY * defPercentY;
				//}
				//else
				//{
				//	myBoundMove.speedY -= opponentMeteorBound.speedY * defPercentY;
				//}
			}
			else if (defPosY < 0)
			{
				defPercentY = defPosY / -2.4f;
				if (defPercentY > 1)
				{
					defPercentY = 1;
				}
				defPercentY *= 1f;

				//if (opponentMeteorBound.speedY < 0)
				//{
				//	myBoundMove.speedY -= opponentMeteorBound.speedY * defPercentY;
				//}
				//else
				//{
				//	myBoundMove.speedY += opponentMeteorBound.speedY * defPercentY;
				//}
			}
			//Y座標が一緒
			else
			{
				defPercentY = 0;
			}

			//当たった相手の位置が自分より上
			if (col.transform.position.y > transform.position.y)
			{
				if (opponentMeteorBound.speedY < 0)
				{
					//myBoundMove.speedY += opponentMeteorBound.speedY * defPercentY;
					myBoundMove.speedY = opponentMeteorBound.speedY * defPercentY - myBoundMove.speedY;
					//myBoundMove.speedY = opponentMeteorBound.speedY * defPercentY;
				}
				else
				{
					myBoundMove.speedY = myBoundMove.speedY - opponentMeteorBound.speedY * defPercentY;
					//myBoundMove.speedY -= opponentMeteorBound.speedY * defPercentY;
				}
			}
			//当たった相手の位置が自分より下
			else if (col.transform.position.y < transform.position.y)
			{
				if (opponentMeteorBound.speedY < 0)
				{
					myBoundMove.speedY = myBoundMove.speedY - opponentMeteorBound.speedY * defPercentY;
					//myBoundMove.speedY -= opponentMeteorBound.speedY * defPercentY;
				}
				else
				{
					myBoundMove.speedY = myBoundMove.speedY + opponentMeteorBound.speedY * defPercentY;
					//myBoundMove.speedY += opponentMeteorBound.speedY * defPercentY;
				}
			}

			//自分より相手が右側
			if (col.transform.position.x > transform.position.x)
			{
				if (opponentMeteorBound.speedX < 0)
				{
					if (myBoundMove.speedX < 0)
					{
						atarimasita_migigawa = true;
						myBoundMove.speedX = myBoundMove.speedX + opponentMeteorBound.speedX;
						//myBoundMove.speedX = opponentMeteorBound.speedX * defPercentX + myBoundMove.speedX;
						//myBoundMove.speedX += opponentMeteorBound.speedX - myBoundMove.speedX;
					}
					else
					{
						atarimasita_migigawa = true;
						myBoundMove.speedX = opponentMeteorBound.speedX * defPercentX + myBoundMove.speedX;
						//	myBoundMove.speedX = myBoundMove.speedX - opponentMeteorBound.speedX * defPercentX;
						//	//myBoundMove.speedX += opponentMeteorBound.speedX * defPercentX;
					}
				}
				else if (opponentMeteorBound.speedX > 0)
				{
					//if (myBoundMove.speedX > 0)
					//{
					//myBoundMove.speedX = myBoundMove.speedX - opponentMeteorBound.speedX * defPercentX;
					atarimasita_migigawa = true;
					myBoundMove.speedX = myBoundMove.speedX - (myBoundMove.speedX - myBoundMove.speedX - opponentMeteorBound.speedX * defPercentX);
					//myBoundMove.speedX -= myBoundMove.speedX - opponentMeteorBound.speedX;
					//}
					//	myBoundMove.speedX -= opponentMeteorBound.speedX * defPercentX;
					//}
				}
			}
			//自分より相手が左側
			else if (col.transform.position.x < transform.position.x)
			{
				if (opponentMeteorBound.speedX < 0)
				{
					if (myBoundMove.speedX < 0)
					{
						myBoundMove.speedX = myBoundMove.speedX - opponentMeteorBound.speedX;
					}
					else
					{
						//myBoundMove.speedX = myBoundMove.speedX - opponentMeteorBound.speedX * defPercentX;

					}
					myBoundMove.speedX = myBoundMove.speedX - opponentMeteorBound.speedX * defPercentX;
					//myBoundMove.speedX -= opponentMeteorBound.speedX * defPercentX;
				}
				else if (opponentMeteorBound.speedX > 0)
				{
					myBoundMove.speedX = myBoundMove.speedX + opponentMeteorBound.speedX * defPercentX;
				}
			}
			opponentMeteorBound = null;
		}

		base.OnTriggerEnter(col);
	}

}
