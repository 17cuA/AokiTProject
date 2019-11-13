/*
 * 久保田達己
 * 敵キャラの挙動、途中でバックを行う挙動
 * 途中で、使用により没となった
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Entrance_And_Exit : MonoBehaviour
{
	public enum Move_Type
	{
		Front,
		Back,
		None,
	}
	public Move_Type type;
	public float X_speed;       //Ｘ軸の行動の時のスピード
	public float Z_speed;
	private Vector3 pos;                //複雑な動きをするときに計算結果をxyzごとに入れまとめて動かす
	private Vector3 start_pos;
	float first_Time;
	float pre_Time;
	float now_Time;
	public int count;
	private float movetime;
	public float _return;
	public float rotation_speed;
	private int rotation_cnt;
	private bool Is_return;
	private PlayableDirector anim;
	void Start()
	{
		start_pos = transform.position;
		first_Time = 0;
		now_Time = 0;
		pre_Time = 0;
		type = Move_Type.Front;
		count = 0;
		movetime = 0;
		rotation_cnt = 0;
		Is_return = false;
		anim = GetComponent<PlayableDirector>();
	}

	void Update()
    {
		movetime += Time.deltaTime;
		pos = new Vector3(0,transform.position.y,0); //中心を決めます。今回は(0,0,0)
		Calc_ExitPosition();
		Move();
		transform.position = pos;
		//if(rotation_cnt < 2)
		if (transform.position.z < -0.5)
		{
			self_rotation();
		}
		if(transform.position.z == 0)
		{
			Restore_Rotation();
		}
	}
	private void Move()
	{
		switch (type)
		{
			case Move_Type.Front:
				X_Move();
				//if(transform.localRotation != 0)
				//{
				//	transform.localRotation = Quaternion.Euler(rotation_speed, 0, 0);
				//}
				break;
			case Move_Type.Back:
				pos.x += transform.position.x + Mathf.Sin(movetime * X_speed) * _return;
				if (transform.position.z > -0.5)
				{
					type = Move_Type.Front;
					transform.position = new Vector3(transform.position.x,0,0);
				}
				break;
			case Move_Type.None:
				break;
			default:
				break;
		}

		if (transform.position.x > -32.0f && count < 2 && type != Move_Type.Back)
		{
			type = Move_Type.Back;
			start_pos = transform.position;
			movetime = 0f;
		}

		if (transform.position.z < 0 )
		{
			Z_Move();
		}
	}
	private void X_Move()
	{
		pos.x += transform.position.x + X_speed * Time.deltaTime ;
	}

	private void Z_Move()
	{
		pos.z += transform.position.z + Z_speed * Time.deltaTime;
		//float currentVelocity = 0, smoothTime = 0.1f;
		//pos.z = Mathf.SmoothDamp(transform.position.z, 0, ref currentVelocity, smoothTime, Z_speed, Time.deltaTime);
	}

	private void Calc_ExitPosition()
	{
		now_Time = Mathf.Sin(movetime * X_speed) * _return;
		if(first_Time == 0)
		{
			first_Time = now_Time;
		}
		if (pre_Time < first_Time && first_Time < now_Time /*&& type == Move_Type.Back*/)
		{
			//count += 1;
		}
		if (pre_Time > now_Time)
		{
			count++;
		}
		if(count > 10)
		{
			Is_return = true;
		}

		pre_Time = now_Time;
	}
	private void self_rotation()
	{
		transform.localRotation = Quaternion.Euler(rotation_speed, 0, 0);
		if (Is_return)
		{
			rotation_speed -= 10;
		}
		else
		{
			rotation_speed += 10;
		}
		if (rotation_speed > 360)
		{
			rotation_speed = 0;
			rotation_cnt++;
		}
	}
	private void Restore_Rotation()
	{
		if(transform.localRotation.x != 0)
		{
			if (transform.localRotation.x > 0)
			{
				transform.localRotation = Quaternion.Euler(rotation_speed, 0, 0);
				rotation_speed -= 10;
			}
			else if(transform.localRotation.x < 0)
			{
				transform.localRotation = Quaternion.Euler(rotation_speed, 0, 0);
				rotation_speed += 10;
			}
		}
	}
}
