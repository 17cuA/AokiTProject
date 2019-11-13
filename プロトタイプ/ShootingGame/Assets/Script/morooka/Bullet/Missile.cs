//作成日2019/06/19
// プレイヤーの使う、ミサイルの挙動管理
// 作成者:諸岡勇樹
/*
 * 2019/06/19 落下と地面衝突時の移動向き変更
 * 2019/06/21 上り坂に衝突時自身の破壊
 * 2019/06/24 EAGLEWINDに対応
 * 2019/09/06 爆発追加
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : bullet_status
{
	[SerializeField]
	[Header("等速直線運動のスピード")]
	private float constant_velocity_line_speed;     // 等速直線速度
	[SerializeField]
	[Header("レイの長さ")]
	private float ray_length;       // レイの長さ
	private RaycastHit hit_mesh;            // 衝突したオブジェクトのメッシュ(コライダーの一部)の情報

	[SerializeField, Tooltip("爆発")] public ParticleSystem Bomb_EF;        // 爆発エフェクト

	private int Act_Step { get; set; }                  // 行動の変更用
	private int Running_Flame { get; set; }             // 起動している間のフレーム
	public Vector3 Ray_Direction { get; set; }          // レイの向き
	public float Length_On_Landing { get; set; }		// 落下時のレイの長さ
	public float Length_On_Gliding {get; set;}			// 滑空時のレイの長さ

	private new void Start()
	{
		base.Start();
		FacingChange(new Vector3(1.0f, 0.0f, 0.0f));
		hit_mesh = new RaycastHit();
		Length_On_Landing = ray_length;
		Length_On_Gliding = ray_length * 3.0f;
		Tag_Change("Player_Bullet");
	}

	private new void Update()
	{
		base.Update();

		// 障害物に当たる前の行動
		if (Act_Step == 0)
		{
			HorizontalProjection();
		}
		// 障害物に当たった後の行動
		else if (Act_Step == 2)
		{
			// 自身の向いている方向に移動
			transform.position += transform.right.normalized * shot_speed;
			//	自身の下方向にコライダーがあるとき
			Vector3 vector = transform.position;
			vector.x += 0.5f;
		}
		// 先端に触れたメッシュ(コライダーの一部)があるとき
		if (Physics.Raycast(transform.position, Ray_Direction, out hit_mesh, Length_On_Landing))
		{
			// コライダーの持ち主がWAllのとき
			if (hit_mesh.transform.gameObject.tag == "Wall")
			{
				Moving_Facing_Confirmation(hit_mesh.normal);
				// 移動の種類を変える
				Act_Step = 2;
				Length_On_Landing = Length_On_Gliding;
			}
		}
	}

	/// <summary>
	/// 再起動時の設定関数
	/// </summary>
	/// <param name="y"> Y軸方向の設定へ渡す数値 </param>
	public void Setting_On_Reboot(int y)
	{
		Ray_Direction = new Vector3(0.0f, Y_Axis_Orientation_Preference(y), 0.0f);
		Running_Flame = 0;
		Act_Step = 0;
		Length_On_Landing = ray_length;
	}

	/// <summary>
	/// 水平投射
	/// </summary>
	private void HorizontalProjection()
	{
		Vector3 vector = new Vector3(constant_velocity_line_speed, Ray_Direction.y * (Running_Flame * shot_speed));
		transform.right = vector;
		transform.position += vector.normalized * shot_speed;
		Running_Flame++;
	}

	/// <summary>
	/// 移動向き確認
	/// </summary>
	private void Moving_Facing_Confirmation(Vector3 mesh_normal)
	{
		// 内閣の計算---------------------------------------------------------------------------------------------
		mesh_normal.y *= (Ray_Direction.y * -1.0f);
		mesh_normal.x *= Ray_Direction.y;
		transform.right = new Vector2(mesh_normal.y, mesh_normal.x);
		float inner_product = transform.right.x * Ray_Direction.x + transform.right.y * Ray_Direction.y;
		// 内閣の計算---------------------------------------------------------------------------------------------
　
		// 内角が0以下のとき
		if (inner_product < 0)
		{
			// 自信を消す
			AddExplosionProcess();
			gameObject.SetActive(false);
		}
	}

	/// <summary>
	/// Y軸方向の設定
	/// </summary>
	/// <param name="select_number"> 偶数を渡すと上方向に移動、奇数を渡すと下方向に移動 </param>
	private float Y_Axis_Orientation_Preference(int select_number)
	{
		float y_di = -1.0f;
		if (select_number % 2 == 0)
		{
			y_di = 1.0f;
		}
		return y_di;
	}

	private void OnDisable()
	{
		//呼び出し元オブジェクトの座標で指定IDのパーティクルを生成
		if (Obj_Storage.Storage_Data.Effects[16] != null)
		{
			GameObject effect = Obj_Storage.Storage_Data.Effects[16].Active_Obj();
			ParticleSystem particle = effect.GetComponent<ParticleSystem>();

			//爆発の位置をランダムに変更
			effect.transform.position = transform.position;
			/*********************************************************/
			particle.Play();
		}
	}
}
