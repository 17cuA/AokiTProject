//作成日2019/08/18
// 戦艦用の爆発エフェクトの管理
// 作成者:諸岡勇樹
/*
 * 2019/07/19 タイムラインで動きの制御ができるように
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BattleshipType_EF : MonoBehaviour
{
	public Vector3 pulus_Pos;				// 初期位置に加算する数値
	public Vector3 plus_rota;				// 初期回転地に加算する数値
	public bool delete_flag;					// デリートしてもよいフラグ
	private Vector3 Initial_Position;		// 初期位置
	private Vector3 Initial_Rota;			// 初期回転値
	private PlayableDirector PD;			// プレイアブルディレクター

	private void Start()
	{
		PD = GetComponent<PlayableDirector>();
		PD.playOnAwake = true;
		Initial_Position = transform.position;
		Initial_Rota = transform.eulerAngles;
	}

	void Update()
    {
		// タイムラインの変動値加算
		transform.position = Initial_Position + pulus_Pos;
		transform.rotation = Quaternion.Euler(Initial_Rota + plus_rota);

		if(delete_flag)
		{
			Destroy(gameObject);
		}
    }
}
