//作成者：川村良太
//隕石の移動挙動

//0.9 ~ -1 モデルのyの範囲
//1 ~ -1.2 モデルのyの範囲

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_MeteorBound_Move : MonoBehaviour
{
	Vector3 velocity;

	public float speedX;
	public float speedY;

	void Start()
	{
        // 隕石バフ効果中 1.25倍
		speedX = Random.Range(-3.5f * 1.25f, -8.0f * 1.25f);
		speedY = Random.Range(-0.3f, 0.3f);
	}

	void Update()
	{
		velocity = gameObject.transform.rotation * new Vector3(speedX, speedY, 0);
		gameObject.transform.position += velocity * Time.deltaTime;

        if (transform.position.x < -20)
        {
            gameObject.SetActive(false);
        }

	}

}