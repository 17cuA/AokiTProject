//作成者：川村良太
//ビットの円運動をするスクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bit_Move : MonoBehaviour
{
	//GameObject playerObject;                //プレイヤーのオブジェクトを入れる
	public GameObject parent;                       //ビット達を管理している親を入れる
													//public GameObject previousBit;		//一つ前のビットを入れる

	//Bit_Move bm;                                //一つ前のビットの動きのスクリプトを取得する用（一つ前のビットの回転位置を知るため）
	public Vector3 defaultPos;

	public float timeCnt = 0;                   //回転の度合い（0～59）で周期

	public float speed = 10.0f;             //移動速度
	public float radius = 1.0f;             //回転する円の大きさ
	float _y;
	float _z;

	//bool isStart = true;                            //生成直後の処理用

	void Start()
	{
		parent = transform.parent.gameObject;
		transform.parent = parent.transform;

	}

	void Update()
	{
		//if (!isStart)
		//{
		_y = radius * Mathf.Cos(timeCnt * speed);
		_z = radius * Mathf.Sin(timeCnt * speed);

		//}
		//else
		//{
		//	_y = radius * Mathf.Cos(timeCnt * speed);
		//	_z = radius * Mathf.Sin(timeCnt * speed);
		//	isStart = false;
		//}
		//_y = radius * Mathf.Cos(timeCnt * speed) + transform.position.y;
		//_z = radius * Mathf.Sin(timeCnt * speed) + transform.position.z;

		transform.position = new Vector3(parent.transform.position.x + _z, parent.transform.position.y + _y, parent.transform.position.z );


		timeCnt += 0.01f;
		if (timeCnt > 0.59f)
		{
			timeCnt = 0;
		}
	}
}
