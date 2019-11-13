//作成日2019/09/05
// 2番目のボスのエフェクト管理
// タイムラインで動きを制御できるようにする
// 作成者:諸岡勇樹
/*
 * 2019/09/05　挙動設定用
 * 2019/09/05　終了時削除
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class big_core_mk3_EF : MonoBehaviour
{
	// Unity側(タイムライン)で動かす値 -----------------------------------------------------
	[SerializeField, Tooltip("ベース位置")] private Vector3 ef_base_position;
	[SerializeField, Tooltip("ベース回転")] private Vector3 ef_base_rotation;
	[SerializeField, Tooltip("武器_右位置")] private Vector3 ef_weapon_right_position;
	[SerializeField, Tooltip("武器_右回転")] private Vector3 ef_weapon_right_rotation;
	[SerializeField, Tooltip("武器_左位置")] private Vector3 ef_weapon_left_position;
	[SerializeField, Tooltip("武器_左回転")] private Vector3 ef_weapon_left_rotation;
	[SerializeField, Tooltip("マルチプル1位置")] private Vector3 ef_multipl_1_position;
	[SerializeField, Tooltip("マルチプル1回転")] private Vector3 ef_multipl_1_rotation;
	[SerializeField, Tooltip("マルチプル2位置")] private Vector3 ef_multipl_2_position;
	[SerializeField, Tooltip("マルチプル2回転")] private Vector3 ef_multipl_2_rotation;
	[SerializeField, Tooltip("マルチプル3位置")] private Vector3 ef_multipl_3_position;
	[SerializeField, Tooltip("マルチプル3回転")] private Vector3 ef_multipl_3_rotation;
	[SerializeField, Tooltip("マルチプル4位置")] private Vector3 ef_multipl_4_position;
	[SerializeField, Tooltip("マルチプル4回転")] private Vector3 ef_multipl_4_rotation;
	[SerializeField, Tooltip("マルチプル5位置")] private Vector3 ef_multipl_5_position;
	[SerializeField, Tooltip("マルチプル5回転")] private Vector3 ef_multipl_5_rotation;
	[SerializeField, Tooltip("マルチプル6位置")] private Vector3 ef_multipl_6_position;
	[SerializeField, Tooltip("マルチプル6回転")] private Vector3 ef_multipl_6_rotation;
	// Unity側(タイムライン)で動かす値 -----------------------------------------------------

	[SerializeField, Tooltip("削除判定")] private bool is_deleat;

	// 実際に動かすゲームオブジェクト ------------------------------------------------------
	public GameObject EF_Base;
	public GameObject EF_Weapon_R;
	public GameObject EF_Weapon_L;
	public GameObject EF_Multipl_1;
	public GameObject EF_Multipl_2;
	public GameObject EF_Multipl_3;
	public GameObject EF_Multipl_4;
	public GameObject EF_Multipl_5;
	public GameObject EF_Multipl_6;
	// 実際に動かすゲームオブジェクト ------------------------------------------------------

	// もとになるポジションの保存用 --------------------------------------------------------
	private Vector3 Base_IniPos;
	private Vector3 WeaponR_IniPos;
	private Vector3 WeaponL_IniPos;
	private Vector3 Multipl_IniPos_1;
	private Vector3 Multipl_IniPos_2;
	private Vector3 Multipl_IniPos_3;
	private Vector3 Multipl_IniPos_4;
	private Vector3 Multipl_IniPos_5;
	private Vector3 Multipl_IniPos_6;
	// もとになるポジションの保存用 --------------------------------------------------------

	// もとになるローテーションの保存用 ---------------------------------------------------
	private Vector3 Base_IniRo;
	private Vector3 WeaponR_IniRo;
	private Vector3 WeaponL_IniRo;
	private Vector3 Multipl_IniRo_1;
	private Vector3 Multipl_IniRo_2;
	private Vector3 Multipl_IniRo_3;
	private Vector3 Multipl_IniRo_4;
	private Vector3 Multipl_IniRo_5;
	private Vector3 Multipl_IniRo_6;
	// もとになるローテーションの保存用 ---------------------------------------------------

	void Update()
	{
		EF_Base.transform.position = Base_IniPos + ef_base_position;
		EF_Weapon_R.transform.position = WeaponR_IniPos + ef_weapon_right_position;
		EF_Weapon_L.transform.position = WeaponL_IniPos + ef_weapon_left_position;
		EF_Multipl_1.transform.position = Multipl_IniPos_1 + ef_multipl_1_position;
		EF_Multipl_2.transform.position = Multipl_IniPos_2 + ef_multipl_2_position;
		EF_Multipl_3.transform.position = Multipl_IniPos_3 + ef_multipl_3_position;
		EF_Multipl_4.transform.position = Multipl_IniPos_4 + ef_multipl_4_position;
		EF_Multipl_5.transform.position = Multipl_IniPos_5 + ef_multipl_5_position;
		EF_Multipl_6.transform.position = Multipl_IniPos_6 + ef_multipl_6_position;

		EF_Base.transform.rotation = Quaternion.Euler(Base_IniRo + ef_base_rotation);
		EF_Weapon_R.transform.rotation = Quaternion.Euler(WeaponR_IniRo + ef_weapon_right_rotation);
		EF_Weapon_L.transform.rotation = Quaternion.Euler(WeaponL_IniRo + ef_weapon_left_rotation);
		EF_Multipl_1.transform.eulerAngles = Multipl_IniRo_1 + ef_multipl_1_rotation;
		EF_Multipl_2.transform.eulerAngles = Multipl_IniRo_2 + ef_multipl_2_rotation;
		EF_Multipl_3.transform.eulerAngles = Multipl_IniRo_3 + ef_multipl_3_rotation;
		EF_Multipl_4.transform.eulerAngles = Multipl_IniRo_4 + ef_multipl_4_rotation;
		EF_Multipl_5.transform.eulerAngles = Multipl_IniRo_5 + ef_multipl_5_rotation;
		EF_Multipl_6.transform.eulerAngles = Multipl_IniRo_6 + ef_multipl_6_rotation;

		if(is_deleat)
		{
			Destroy(gameObject);
		}
	}
	/// <summary>
	///	 初期設定
	/// </summary>
	public void Set_Init()
	{
		Base_IniPos			= EF_Base.transform.position;
		WeaponR_IniPos	= EF_Weapon_R.transform.position;
		WeaponL_IniPos		= EF_Weapon_L.transform.position;
		Multipl_IniPos_1		= EF_Multipl_1.transform.position;
		Multipl_IniPos_2		= EF_Multipl_2.transform.position;
		Multipl_IniPos_3		= EF_Multipl_3.transform.position;
		Multipl_IniPos_4		= EF_Multipl_4.transform.position;
		Multipl_IniPos_5		= EF_Multipl_5.transform.position;
		Multipl_IniPos_6		= EF_Multipl_6.transform.position;

		Base_IniRo = EF_Base.transform.eulerAngles;
		WeaponR_IniRo = EF_Weapon_R.transform.eulerAngles;
		WeaponL_IniRo = EF_Weapon_L.transform.eulerAngles;
		Multipl_IniRo_1 = EF_Multipl_1.transform.eulerAngles;
		Multipl_IniRo_2 = EF_Multipl_2.transform.eulerAngles;
		Multipl_IniRo_3 = EF_Multipl_3.transform.eulerAngles;
		Multipl_IniRo_4 = EF_Multipl_4.transform.eulerAngles;
		Multipl_IniRo_5 = EF_Multipl_5.transform.eulerAngles;
		Multipl_IniRo_6 = EF_Multipl_6.transform.eulerAngles;
	}
}
