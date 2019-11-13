/*
 * 20190913 作成
 * author hasegawa yuuta
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 動的にごみを削除していくごみ袋
/// </summary>
public class GarbageBag : MonoBehaviour
{
	/// <summary>
	/// ごみ袋
	/// </summary>
	[SerializeField, Header("ごみ袋")] List<GameObject> garbageList = new List<GameObject>();
	/// <summary>
	/// 背景のTube用ごみ袋
	/// </summary>
	[SerializeField, Header("背景のTube用ごみ袋")] List<GameObject> garbageTubeList = new List<GameObject>();
	List<GameObject> usingGarbageTubeList = new List<GameObject>();	// 細かく分断したチューブを入れる

	bool isDestroyGarbage = false;		// タイミング調整用
	bool isDestroyTube = false;			// チューブが用済みになって消すかどうか
	SetTimeTrigger setTimeTrigger;		// トリガー
	int tubeDestroyFrame = 0;			// チューブを消すまでのカウント
	const int kTubeDestroyDeley = 500;	// チューブを消すときのディレイ
	public static GarbageBag instance;

	void Start()
	{
		// トリガの取得
		setTimeTrigger = FindObjectOfType<SetTimeTrigger>();
		// チューブを細かく分断していく
		foreach(GameObject ob in garbageTubeList)
		{
			foreach(Transform t in ob.transform)
			{
				usingGarbageTubeList.Add(t.gameObject);
			}
			usingGarbageTubeList.Add(ob);
		}
		garbageTubeList.Clear();
	}

	void FixedUpdate()
	{
		DestroyTubeGarbage();
		DestroyGarbageObject();
	}

	/// <summary>
	/// ごみの収集
	/// </summary>
	/// <param name="garbage">ごみ</param>
	public void AddGarbage(GameObject garbage)
	{
		garbageList.Add(garbage);
	}

	/// <summary>
	/// 燃えるごみを燃やす
	/// </summary>
	void DestroyGarbageObject()
	{
		// ごみが無ければ処理しない
		if (garbageList.Count <= 0) { return; }
		// トリガーの反転
		isDestroyGarbage = !isDestroyGarbage;
		// 削除しないフレームであれば削除しない
		if (!isDestroyGarbage) { return; }
		// 0の要素にごみがしっかり入っていれば削除する
		if (garbageList[0])
		{
			Destroy(garbageList[0]);
		}
		// nullを入れてリストから外す
		garbageList[0] = null;
		garbageList.RemoveAt(0);
	}
	/// <summary>
	/// 燃えないごみを燃やす
	/// </summary>
	void DestroyTubeGarbage()
	{
		// ごみが無ければ処理しない
		if (usingGarbageTubeList.Count <= 0) { return; }
		// 一度もトリガーがオンになっていなかったら処理しない
		if (setTimeTrigger?.Trigger == false && !isDestroyTube) { return; }
		isDestroyTube = true;
		// ディレイ
		if (++tubeDestroyFrame < kTubeDestroyDeley) { return; }
		// 0の要素にごみがしっかり入っていれば削除する
		if (usingGarbageTubeList[0])
		{
			Destroy(usingGarbageTubeList[0]);
		}
		// nullを入れてリストから削除する
		usingGarbageTubeList[0] = null;
		usingGarbageTubeList.RemoveAt(0);
	}
}
