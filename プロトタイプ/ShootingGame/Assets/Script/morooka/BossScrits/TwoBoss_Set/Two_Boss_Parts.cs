//作成日2019/06/13
// 2番目のボスのパーツ管理
// 作成者:諸岡勇樹
/*
 * 2019/07/11　初期位置の確保
 * 2019/09/05　ID追加
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class Two_Boss_Parts : character_status
{
	[SerializeField,Tooltip("サポートするオブジェクト")] private GameObject[] supported_objects;
	[SerializeField, Tooltip("マルチプル")] private bool is_mul;

	private Vector3 Initial_Position { get; set; }		// 初期位置
	private MeshRenderer renderer { get; set; }		// レンダー
	private Color Damege_Color { get; set; }            // ダメージ受けた時のエフェクト

	private int ShootingTiming_Cnt{ get; set; }			// バレットを撃つタイミングカウンター
	private int Timing_Index { get; set; }					// タイミングのインデックス
	private int[] Timing_Max { get; set; }					// タイミングの最大

	private ParticleSystem smoke { get; set; }			// 煙パーティクル

	private new void Start()
	{
		Initial_Position = transform.position;
		if (is_mul)
		{
			smoke = transform.GetChild(1).GetComponent<ParticleSystem>();
			smoke.Stop();
			ShootingTiming_Cnt = 0;
			Timing_Index = 0;
			Timing_Max = new int[6] { 3, 4, 4, 9, 5, 6 };
		}
		else
		{
			base.Start();
			renderer = GetComponent<MeshRenderer>();
			Damege_Color = new Color(104.0f / 255.0f, 76.0f / 255.0f, 46.0f / 255.0f);
		}
	}
	private new void Update()
	{
		if (!is_mul)
		{
			base.Update();
			if (hp < 1)
			{
				if (supported_objects != null)
				{
					foreach (GameObject obj in supported_objects)
					{
						renderer.enabled = false;
					}
				}
				base.Died_Judgment();
				base.Died_Process();
			}
		}
	}

	/// <summary>
	/// 弾の打ち出し
	/// </summary>
	/// <param name="tra"> トランスフォーム </param>
	/// <param name="i"> タイミングの種類数 </param>
	public void Bullet_Shot(Transform tra, int i = 7)
	{
		if (i < 7)
		{
			Timing_Index = i;
		}

		Shot_Delay++;
		if (Shot_Delay >= Shot_DelayMax)
		{
			ShootingTiming_Cnt++;
			Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET, tra.position, tra.forward);
			if (ShootingTiming_Cnt >= Timing_Max[Timing_Index])
			{
				Timing_Index++;
				if (Timing_Index == Timing_Max.Length)
				{
					Timing_Index = 0;
				}
				ShootingTiming_Cnt = 0;
				Shot_Delay = 0;
			}
		}
	}
}
