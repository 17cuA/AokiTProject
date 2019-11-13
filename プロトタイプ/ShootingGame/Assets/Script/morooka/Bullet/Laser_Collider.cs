//作成日2019/10/17
// 一面のボスのレーザーのコライダー判定の処理
// 作成者:諸岡勇樹
/*
 * 2019/10/17：　一面のボスのレーザーのコライダー判定の処理
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Collider : MonoBehaviour
{
	/// <summary>
	///  ボスの種類
	/// </summary>
	public enum BOSS_TYPE
	{
		eBOSS_ONE,			// 一ボス
		eBOSS_TWO,			// 二ボス
	}

	public BOSS_TYPE bType;		// ボスの種類

	protected void OnTriggerEnter(Collider col)
	{
		switch (bType)
		{
			// 1ボスのとき
			case BOSS_TYPE.eBOSS_ONE:
				// プレイヤーに衝突したとき
				if (col.gameObject.tag == "Player")
				{
					GameObject effect = Obj_Storage.Storage_Data.Effects[11].Active_Obj();
					ParticleSystem particle = effect.GetComponent<ParticleSystem>();
					effect.transform.position = gameObject.transform.position;
					particle.Play();
				}
				break;
			// 2ボスのとき
			case BOSS_TYPE.eBOSS_TWO:
				// プレイヤーに衝突したとき
				if (col.gameObject.tag == "Player")
				{
					GameObject effect = Obj_Storage.Storage_Data.Effects[11].Active_Obj();
					ParticleSystem particle = effect.GetComponent<ParticleSystem>();
					effect.transform.position = gameObject.transform.position;
					particle.Play();
				}
				// 発射元以外のオプションに当たったとき
				else if (col.transform != transform.parent && col.gameObject.layer != 14 && col.gameObject.tag != "Option")
				{
					//var temp = transform.parent;
					//temp.GetComponent<Two_Boss_Laser>().Scraps.Add(gameObject);
				}
				break;
			default:
				break;
		}
	}
}
