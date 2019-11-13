//作成日2019/06/13
// エネミーのバレットの管理
// 作成者:諸岡勇樹
/*
 * 2019/06/06	バレットの動きの制御
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet : bullet_status
{
	private new void Start()
	{
		base.Start();
		Tag_Change("Enemy_Bullet");
	}
	private new void Update()
    {
		base.Update();
		Moving_To_Facing();
    }
}
