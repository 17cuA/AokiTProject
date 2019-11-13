//作成日2019/06/21
// プレイヤーの使う、ミサイル(2_WAY)の挙動管理
// 作成者:諸岡勇樹
/*
 * 2019/06/19 落下と上昇
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile_Tow_Way : bullet_status
{
	[SerializeField]
	[Header("等速直線運動のスピード")]
	private float constant_velocity_line_speed;     // 等速直線速度
	[SerializeField]
	[Header("レイの長さ")]
	private float ray_length;                        // レイの長さ

	private int Running_Flame { get; set; }				// 起動している間のフレーム
	public float Y_Axis_Facing { get; private set; }		// Y軸の方向

	private new void Start()
	{
		base.Start();
		FacingChange(new Vector3(1.0f, 0.0f, 0.0f));
		Tag_Change("Player_Bullet");
	}

	private new void Update()
	{
		base.Update();
		HorizontalProjection();
	}

	void OnEnable()
	{
		Running_Flame = 0;
	}

	/// <summary>
	/// 水平投射
	/// </summary>
	public void HorizontalProjection()
	{
		Vector3 vector = new Vector3(constant_velocity_line_speed, Y_Axis_Facing * (Running_Flame * shot_speed));
		transform.right = vector;
		transform.position += vector.normalized * shot_speed;
		Running_Flame++;
	}

	public void che()
	{
		constant_velocity_line_speed *= -1.0f;
	}

	/// <summary>
	/// Y軸方向の設定
	/// </summary>
	/// <param name="select_number"> 偶数を渡すと上方向に移動、奇数を渡すと下方向に移動 </param>
	public void Y_Axis_Orientation_Preference(int select_number)
	{
		if(select_number % 2 == 0)
		{
			Y_Axis_Facing = 1.0f;
		}
		else if(select_number % 2 == 1)
		{
			Y_Axis_Facing = -1.0f;
		}
	}
}
