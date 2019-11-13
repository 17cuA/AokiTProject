//作成日2019/08/05
// 一面のボスのレーザー
// 作成者:諸岡勇樹
/*
 * 2019/07/30　レーザーの挙動
 * 2019/09/07　フレームのレーザー状態の追加
 * 2019/10/17　レーザーの軽量化：void Update()の呼び出し数の減量
 * 2019/10/28　高田：レーザーの仕様変更
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class Boss_One_Laser : MonoBehaviour
{
	public bool IsShoot { get; set; }   //レーザー発射状態フラグ

	public GameObject laserGameobjects ;   //レーザー本体

	private void Start()
	{
		//初期化
		IsShoot = false;
	}

	void Update()
	{
		//レーザー本体をアクティブにする
		if (IsShoot && !laserGameobjects.activeSelf)
		{
			laserGameobjects.SetActive(true);
		}
	}
}
