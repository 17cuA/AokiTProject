//作成日2019/08/06
// 一面のボスのレーザーためエフェクトのパーティクルアニメーションの終わり確認
// 作成者:諸岡勇樹
/*
 * 2019/08/06　終わり確認
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_One_A111_Individual : MonoBehaviour
{
	public bool Completion { get; set; }		// 終わっているかどうか

	/// <summary>
	/// パーティクルの再生が終わった時に実行される
	/// </summary>
	private void OnParticleSystemStopped()
	{
		Completion = true;
	}

	private void OnEnable()
	{
		Completion = false;
	}
}
