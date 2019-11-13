using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title_Manager : MonoBehaviour
{
	public enum TITLE_MODE
	{
		eCAUTION,		// 注意書き
		eROGO,				// ロゴ
		eLEGEND,			// 伝説
		eTITLE,				// タイトル
		eSELECT,			// 人数の選択
	}

	[SerializeField, Tooltip("注意書き")]	private Image caution_writing;
	[SerializeField, Tooltip("ロゴ")]			private Image lorgo_writing;
	[SerializeField, Tooltip("伝説")]			private Image legend_writing;

	public TITLE_MODE Mode { get; private set; }

	void Start()
    {
		Mode = TITLE_MODE.eCAUTION;
    }
}
