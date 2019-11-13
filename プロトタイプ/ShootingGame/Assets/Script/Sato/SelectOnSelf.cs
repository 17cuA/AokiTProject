// メニュー選択でのアクティブ化に必要
// 作成者:佐藤翼
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectOnSelf : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		// 自分を選択状態にする
		Selectable sel = GetComponent<Selectable>();
		sel.Select();
	}
}