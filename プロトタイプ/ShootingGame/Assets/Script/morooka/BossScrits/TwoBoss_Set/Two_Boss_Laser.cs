//作成日2019/09/05
// 一面のボス本番_2匹目_レーザー挙動
// 作成者:諸岡勇樹
/*
 * 2019/09/05　作成
 * 2019/10/17　レーザーの軽量化：void Update()の呼び出し数の減量
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class Two_Boss_Laser : MonoBehaviour
{
	public bool IsShoot { get; set; }   //レーザー発射状態フラグ

	public bool patternWave = true;

	public bool useTwoBossLaserWave = false;
	public GameObject twoBossLaserWaveGameobjects;
	public bool useTwoBossLaserEnclose = false;
	public GameObject twoBossLaserEncloseGameobjects;

	private void Start()
	{
		//初期化
		IsShoot = false;
		patternWave = true;
	}

	void Update()
	{
		//レーザー本体をアクティブにする
		if (IsShoot && useTwoBossLaserWave && patternWave && !twoBossLaserWaveGameobjects.activeSelf)
		{
			twoBossLaserWaveGameobjects.SetActive(true);
		}
		else if (IsShoot && useTwoBossLaserEnclose && !patternWave && !twoBossLaserEncloseGameobjects.activeSelf)
		{
			twoBossLaserEncloseGameobjects.SetActive(true);
		}
	}
}