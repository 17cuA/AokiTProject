//作成日2019/07/08
// 戦艦型エネミーの大砲(パーツ)の挙動
// 作成者:諸岡勇樹
/*
 * 2019/07/09　必要情報の格納
 * 2019/07/10　HPが0以下のときの挙動
 * 2019/07/31　弾を撃つ
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class BattleshipType_Battery : character_status
{
	private Vector3 Initial_Position { get; set; }		// 自身の初期位置
	private GameObject pure { get; set; }               // プレハブ
	public ParticleSystem particle;

	private new void Start()
	{
		hp = 5;
		score = 100;
		pure = Resources.Load("Bullet/GameObject") as GameObject;

		gameObject.tag = transform.parent.tag;
		Initial_Position = transform.localPosition;
		base.Start();
	}

	private new void Update()
	{
		// HPが0以下のとき
		if (hp <= 0)
		{
			Died_Process();
		}

		// 原因不明の位置連れ防止用
		if (transform.localPosition != Initial_Position) transform.localPosition = Initial_Position;

		base.Update();
	}

	/// <summary>
	/// 再起動時の処理
	/// </summary>
	public void ReBoot()
	{
		hp = 5;
		transform.localPosition = Initial_Position;
	}

	/// <summary>
	/// 攻撃させる
	/// </summary>
	/// <returns> 撃ったバレット </returns>
	public GameObject Attack_Instruction_Receiving()
	{

		GameObject obj = Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eBATTLESHIP_ENEMY_PREFAB, transform.position, transform.right);
		CannonBullet ns = obj.GetComponent<CannonBullet>();
		ns.Person_Who_Shot = gameObject;
		return obj;
	}

	/// <summary>
	/// 初期のローカルポジションの位置を取得
	/// </summary>
	/// <returns></returns>
	public Vector3 Getinitishal_pos()
	{
		return Initial_Position;
	}
}
