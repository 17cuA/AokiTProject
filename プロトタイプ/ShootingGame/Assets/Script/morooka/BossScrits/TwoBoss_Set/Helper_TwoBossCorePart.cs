//作成日2019/08/13
// 2番目のボスのコアの色変更
// 作成者:諸岡勇樹
/*
 * 2019/08/13　2番目のボスのコアの色変更
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper_TwoBossCorePart : MonoBehaviour
{
	[SerializeField] private GameObject partInstance;					// コアのオブジェクト情報
	[SerializeField] private Texture2D red_baseColorTexture;		// 赤のテクスチャー
	[SerializeField] private Texture2D red_emissiveTexture;			// 赤のエミッシブ
	[SerializeField] private ParticleSystem particleSystem;			// 色変換時のパーティクル
	[SerializeField] private int transitionLimitHp = 100;               // 色を変えるHPの量

	private Two_Boss_Parts partScript;										// ボスのパーツのスクリプト
	private new Renderer renderer;											// レンダー
	private bool isChange = false;												// 変更するか
	public bool hasChanged = false;											// 変更が終わっているか

	private void Awake()
	{
		partScript = GetComponent<Two_Boss_Parts>();
	}

	private void Start()
	{
		renderer = partInstance.GetComponent<Renderer>();
	}

	private void Update()
	{
		// HPが一定以下のとき
		if (partScript.hp <= transitionLimitHp)
		{
			// コアを色変え許可
			isChange = true;
		}

		// 変更不許可時、変更終了時
		if (!isChange || hasChanged) return;

		// パーティクルの情報があれば
		// パーティクルの使用
		if (particleSystem != null)
		{
			particleSystem.transform.position = transform.position;
			particleSystem?.Stop();
			particleSystem?.Play();
		}

		// 以下色の変更
		partScript.self_material[0].SetTexture("_BaseColorMap", red_baseColorTexture);
		partScript.self_material[0].SetTexture("_EmissiveColorMap", red_emissiveTexture);
		hasChanged = true;
	}
}
