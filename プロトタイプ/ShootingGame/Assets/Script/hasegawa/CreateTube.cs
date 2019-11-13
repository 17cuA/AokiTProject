/*
 * 20190701 作成
 */
/* ステージのコロニーの部品を並べるためのプログラム */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTube : MonoBehaviour
{
	[SerializeField] GameObject tubePrefab;							// 生成する部品
	[SerializeField] Vector3[] startWorldPosition = new Vector3[2];	// 生成開始位置
	[SerializeField] Vector3[] createOffset = new Vector3[2];		// 生成ごとにずらす位置
	[SerializeField] Vector3[] createEularAngles = new Vector3[2];	// 生成するオイラー角
	[SerializeField] int[] createNum = new int[2];					// 一列に生成する部品の数
	[SerializeField] int tubeNum;									// 生成する塊の数(例：──── ──　←これでも2個)

	void Start()
	{
		// 塊の数を生成する
		for (int i = 0; i < tubeNum; ++i)
		{
			// 塊を組み立てる
			Vector3 createPos = startWorldPosition[i];
			for (int j = 0; j < createNum[i]; ++j)
			{
				Instantiate(tubePrefab, createPos, Quaternion.Euler(createEularAngles[i]), transform);
				createPos += createOffset[i];
			}
		}
	}
}
