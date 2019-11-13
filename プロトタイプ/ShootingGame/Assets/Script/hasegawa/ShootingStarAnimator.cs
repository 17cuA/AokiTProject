/*
 * 20190729 作成
 * author hasegawa yuuta
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStarAnimator : MonoBehaviour
{
	[SerializeField] List<ShootingStarBehaviour> animationObjectList = new List<ShootingStarBehaviour>();
	GameObject starParentObject = null;

	void Start()
	{
		// シーンに存在する流れ星を取得していく
		starParentObject = new GameObject("StarObjects");
		Object[] array = FindObjectsOfType<ShootingStarBehaviour>();
		foreach(Object a in array)
		{
			if (animationObjectList.Contains(a as ShootingStarBehaviour)) { continue; }
			animationObjectList.Add(a as ShootingStarBehaviour);
			(a as ShootingStarBehaviour).transform.parent = starParentObject.transform;
			(a as ShootingStarBehaviour).SettingState();
		}
	}
	/// <summary>
	/// 設定されている流れ星の中からランダムな流れ星を流す
	/// </summary>
	public void AnimationRandomShootingStar()
	{
		if (animationObjectList.Count <= 0) { return; }
		int randomNum = Random.Range(0, animationObjectList.Count);
		int i;
		// すでにアニメーションをしていたらほかの流れ星にする
		for (i = 0; i < animationObjectList.Count; ++i)
		{
			if (!animationObjectList[randomNum].IsAnimation) { break; }
			++randomNum;
			if (randomNum >= animationObjectList.Count) { randomNum = 0; }
		}
		// 全ての流れ星が流れていたら流さない
		if (i >= animationObjectList.Count) { return; }
		animationObjectList[randomNum].Activate();
	}
}
