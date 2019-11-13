using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

	Vector3 MOVEX = new Vector3(0.175f, 0, 0); // x軸方向に１マス移動するときの距離
	Vector3 MOVEY = new Vector3(0, 0.175f, 0); // y軸方向に１マス移動するときの距離

	public float step = 2f;     // 移動速度
	Vector3 target;      // 入力受付時、移動後の位置を算出して保存 
	Vector3 prevPos;     // 何らかの理由で移動できなかった場合、元の位置に戻すため移動前の位置を保存

	// Use this for initialization
	void Start()
	{
		target = transform.position;
	}

	// Update is called once per frame
	void Update()
	{

		// ① 移動中かどうかの判定。移動中でなければ入力を受付
		if (transform.position == target)
		{
			SetTargetPosition();
		}
		Move();
	}

	// ② 入力に応じて移動後の位置を算出
	void SetTargetPosition()
	{

		prevPos = target;

		//if (Input.GetKey(KeyCode.RightArrow))
		//{
		//	target = transform.position + MOVEX;
		//	return;
		//}
		//if (Input.GetKey(KeyCode.LeftArrow))
		//{
		//	target = transform.position - MOVEX;
		//	return;
		//}
		if (Input.GetKey(KeyCode.UpArrow))
		{
			target = transform.position + MOVEY;
			return;
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			target = transform.position - MOVEY;
			return;
		}
	}

	// ③ 目的地へ移動する
	void Move()
	{
		transform.position = Vector3.MoveTowards(transform.position, target, step * Time.deltaTime);
	}
}
