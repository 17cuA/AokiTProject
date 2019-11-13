using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTest : MonoBehaviour
{
	Vector3 MOVEX = new Vector3(0.166f, 0, 0); // x軸方向に１マス移動するときの距離
	Vector3 MOVEY = new Vector3(0, 0.166f, 0); // y軸方向に１マス移動するときの距離
	public float x;            //x軸の入力
	public float y;
	public float speed;
	public float step = 2f;     // 移動速度
	public Vector3 target;      // 入力受付時、移動後の位置を算出して保存 
	Vector3 prevPos;     // 何らかの理由で移動できなかった場合、元の位置に戻すため移動前の位置を保存
	Vector3 vector3;

	public bool isInpt = false;

	// Use this for initialization
	void Start()
	{
		target = transform.position;
	}

	// Update is called once per frame
	void Update()
	{
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject save = Instantiate(Resources.Load("Bullet/Player_Bullet_1P")as GameObject, transform.position, transform.rotation);
        }
		// ① 移動中かどうかの判定。移動中でなければ入力を受付
		if (transform.position == target)
		{
			//MoveX();
			SetTargetPosition();
		}
		Move();
	}

	// ② 入力に応じて移動後の位置を算出
	void SetTargetPosition()
	{
		x = Input.GetAxis("Horizontal");            //x軸の入力
		y = Input.GetAxis("Vertical");              //y軸の入力

		prevPos = target;


		//右上
		if (x > 0 && y > 0)
		{
			target = transform.position + MOVEX + MOVEY;

		}
		//右下
		else if (x > 0 && y < 0)
		{
			target = transform.position + MOVEX - MOVEY;

		}
		//左下
		else if (x < 0 && y < 0)
		{
			target = transform.position - MOVEX - MOVEY;

		}
		//左上
		else if (x < 0 && y > 0)
		{
			target = transform.position - MOVEX + MOVEY;

		}
		//上
		else if (y > 0)
		{
			target = transform.position + MOVEY;

		}
		//右
		else if (x > 0)
		{
			target = transform.position + MOVEX;

		}
		//下
		else if (y < 0)
		{
			target = transform.position - MOVEY;

		}
		//左
		else if (x < 0)
		{
			target = transform.position - MOVEX;

		}

	}

	// WalkParam  0;下移動　1;右移動　2:左移動　3:上移動
	//void SetAnimationParam(int param)
	//{
	//	animator.SetInteger("WalkParam", param);
	//}

	// ③ 目的地へ移動する
	void Move()
	{
		transform.position = Vector3.MoveTowards(transform.position, target, step * Time.deltaTime);
	}
	void MoveX()
	{
		//プレイヤーの移動に上下左右制限を設ける
		if (transform.position.y >= 4.5f && y > 0) y = 0;
		if (transform.position.y <= -4.5f && y < 0) y = 0;
		if (transform.position.x >= 17.0f && x > 0) x = 0;
		if (transform.position.x <= -17.0f && x < 0) x = 0;

		vector3 = new Vector3(x, y, 0);     //移動のベクトルをvector3に入れる

		//位置情報の更新
		transform.position = transform.position + vector3 * Time.deltaTime * speed;
	}
}
