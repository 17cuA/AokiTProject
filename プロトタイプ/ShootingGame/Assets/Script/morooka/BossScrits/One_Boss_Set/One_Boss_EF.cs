//作成日2019/09/05
// 1番目のボスのエフェクト管理
// 作成者:諸岡勇樹
/*
 * 2019/09/05　終了時削除
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class One_Boss_EF : MonoBehaviour
{
	public bool Is_Animation_End;
	public Renderer renderer;

	private void Update()
	{
		if(!renderer.isVisible)
		{
			Destroy(gameObject);
		}
	}
}
