using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider_Manager : MonoBehaviour
{
	public float playTime;              //全体の再生時間

	public float elapsedTime;           //経過時間
	public AnimationCurve XColliderSize; //Xサイズのカーブ
	public AnimationCurve YColliderSize; //Yサイズのカーブ
	public AnimationCurve ZColliderSize; //Zサイズのカーブ
	private Vector3 maxColliderSize ;     //サイズの最大値

	private BoxCollider myCollider;     //コライダーコンポーネント

	void Start()
	{
		elapsedTime = 0.0f;
		myCollider = GetComponent<BoxCollider>();
		maxColliderSize = new Vector3(0.0f, 0.0f, 0.0f);

	}

	// Update is called once per frame
	void Update()
	{
		//経過時間の加算
		elapsedTime += Time.deltaTime;
		//経過時間に対応したグラフの数値を取得
		Vector3 temp;
		temp.x = (float)XColliderSize.Evaluate(elapsedTime);
		temp.y = (float)YColliderSize.Evaluate(elapsedTime);
		temp.z = (float)ZColliderSize.Evaluate(elapsedTime);
		myCollider.size = temp;

		//調整
		if (1.0 >= temp.x) temp.x = 0.0f;
		if (1.0 >= temp.y) temp.y = 0.0f;
		if (1.0 >= temp.z) temp.z = 0.0f;
		//最大値の保存
		if (maxColliderSize.x < temp.x) maxColliderSize.x = temp.x;
		if (maxColliderSize.y < temp.y) maxColliderSize.y = temp.y;
		if (maxColliderSize.z < temp.z) maxColliderSize.z = temp.z;

		//前半
		if (playTime / 2.0f >= elapsedTime)
		{
			myCollider.center = temp / 2.0f;
		}

		//後半
		else
		{
			myCollider.center = maxColliderSize - temp / 2.0f;
		}
	}

	public void RePlay()
	{
		elapsedTime = 0.0f;
		maxColliderSize = new Vector3(0.0f, 0.0f, 0.0f);
	}
}
