//作成者：川村良太
//エネミーのバーストショットのスクリプト
//バースト出発射する数、バーストの間隔、バースト中の弾の間隔を指定可能。単発もこれでいける
//プレイヤーに向かって打つことが多いと思うので、その時はFind_Angleスクリプトをつけたオブジェクトにこれもつける。

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class Enemy_BurstShot : MonoBehaviour
{
	private Transform Enemy_transform;  //自身のtransform
	public GameObject Bullet;  //弾のプレハブ、リソースフォルダに入っている物を名前から取得。
	public GameObject parentObj;
	ShotCheck sc;

	public string myName;
	[Header("バーストとバーストの間隔を計る")]
	public float Shot_Delay;						//バーストとバーストの間隔時間を計る
	[Header("バーストとバーストの間隔")]
	public float Shot_Delay_Max;					//１つのバーストの間隔
	public float burst_delay;						//バーストの1発1発の間隔時間を計る
	[Header("バースト内の弾の間隔")]
	public float burst_Delay_Max;					//バーストの1発1発の間隔
	[Header("バーストで撃つ数")]
	public int burst_ShotNum;						//撃つバースト数
	[Header("バーストを撃つ回数")]
	public int burst_Times;
	public int burst_Num;							//バーストを撃った回数
	public int burst_Shot_Cnt;						//何発撃ったかのカウント
	public bool isShot = false;
	public bool isBurst = false;					//バーストを撃つかどうか
	public bool once;

	private void Awake()
	{
		once = true;
		parentObj = transform.parent.gameObject;
		myName = parentObj.name;
		if(parentObj.name== "Enemy_UFO(Clone)")
		{
			myName = parentObj.name;
		}
		//else
		//{
		//	myName = "aaa";
		//}
		else if (parentObj.transform.parent)
		{
			myName = parentObj.transform.parent.gameObject.name;
		}
	}
	void Start()
	{
		Enemy_transform = transform.parent;
		if (myName == "Enemy_Bullfight")
		{
			Bullet = Resources.Load("Bullet/Beam_Bullet") as GameObject;
		}
		//else if (myName == "ClamChowderType_Enemy_Item")
		//{
		//	Bullet = Resources.Load("Bullet/Beam_Bullet") as GameObject;
		//}
		else
		{
			Bullet = Resources.Load("Bullet/Enemy_Bullet") as GameObject;
		}
		burst_delay = 0;
		Shot_Delay = 0;
		burst_Shot_Cnt = 0;
	}

	void Update()
	{
		//最初に一回だけ行うリセット処理
		if(once)
		{
			Shot_Reset();
			once = false;
		}
        //親のtransformを代入
        Enemy_transform = transform.parent.transform;

		//自分が大砲かモアイなら
		if (myName == "taiho"|| myName == "Enemy_Moai(Clone)")
		{
			if (isShot/* && transform.position.x < 15f && transform.position.x > -17.5*/)
			{
				if (isBurst)
				{
					//バーストショット関数呼び出し
					if (burst_Times > burst_Num)
					{
						BurstShot();
					}
				}

				else if (Shot_Delay > Shot_Delay_Max)
				{
					isBurst = true;
					Shot_Delay = 0;
				}
				else
				{
					Shot_Delay += Time.deltaTime;
				}
			}

		}
		else if (isShot && transform.position.z == 0 && transform.position.x < 17.5f && transform.position.x > -17.5 && transform.position.y < 5 && transform.position.y > -5)
		{
			if (isBurst)
			{
				//バーストショット関数呼び出し
				if (burst_Times > burst_Num)
				{
					BurstShot();
				}
			}

			else if (Shot_Delay > Shot_Delay_Max)
			{
				isBurst = true;
				Shot_Delay = 0;
			}
			else
			{
				Shot_Delay += Time.deltaTime;
			}
		}
	}
	void BurstShot()
	{
		//撃つ
		if (burst_delay >= burst_Delay_Max)
		{
			//闘牛はレーザー
			if (myName == "Enemy_Bullfight")
			{
				//Instantiate(Bullet, gameObject.transform.position, transform.rotation);
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_LASER, Enemy_transform.position, Enemy_transform.rotation);
			}
			//それ以外は普通の弾
			else
			{
				//弾生成
				//Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_LASER, transform.position, transform.rotation);
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET, transform.position, transform.rotation);
			}

			//発射数カウントプラス
			++burst_Shot_Cnt;
			//バースト計測リセット
			burst_delay = 0;
		}
		//バースト計測プラス
		burst_delay += Time.deltaTime;
		//バーストを指定の数撃ち切ったら
		if (burst_Shot_Cnt == burst_ShotNum)
		{
			//バーストをfalse、発射数リセット
			isBurst = false;
			burst_Shot_Cnt = 0;
			burst_Num++;
		}
	}
	void Shot_Reset()
	{
		Shot_Delay = 0;
		burst_delay = 0;
		burst_Num = 0;
		isBurst = false;
	}
}
