/*
 * 久保田達己
 *  プレイヤーのマズルファイアを管理するスクリプト
 */
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mazle_Flash_off : MonoBehaviour
{
	ParticleSystem particle;    //particleの情報を取得
	int frame;                //生成されてからの時間をカウント
	public int frame_Max;	//パーティクルがを止めるための設定用
	private void Start()
	{
		//パーティクルの情報取得
		particle = GetComponent<ParticleSystem>();
		//変数の初期化
		frame = 0;
	}
	void Update()
	{
		frame++;
		//既定のフレーム数まで増えたら、
		if (frame > frame_Max)
		{
			frame = 0;
			particle.Stop();
		}
	}
}
