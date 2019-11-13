/*
 * 直線に進んでくる敵キャラ
 * 久保田達己		
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_straight : character_status
{
	private new void Start()
	{
		base.Start();
	}
	// Update is called once per frame
	public new void Update()
    {
		if (hp < 1)
		{
			Died_Process();
		}
		Enemy_Move();
		base.Update();
	}

	private void Enemy_Move()
	{
		transform.position += new Vector3(-0.1f, 0, 0) * Time.deltaTime * speed;
	}
}
