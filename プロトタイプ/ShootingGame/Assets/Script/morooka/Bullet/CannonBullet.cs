//作成日2019/07/18
// 戦艦用の弾の管理
// 作成者:諸岡勇樹
/*
 * 2019/07/19 戦艦用の弾の管理
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBullet : bullet_status
{
	public GameObject Person_Who_Shot { get; set; }		// 撃った大砲の情報
	new void Start()
    {
		base.Start();
    }

    new void Update()
    {
		transform.position -= transform.right.normalized * shot_speed;
    }

	new private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject != Person_Who_Shot)
		{
			if (other.tag != "Enemy_Bullet" && other.tag != "Player_Bullet")
			{
				gameObject.SetActive(false);
			}
		}
	}
}
