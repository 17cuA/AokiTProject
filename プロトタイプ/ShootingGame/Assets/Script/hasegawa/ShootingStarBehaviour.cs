/*
 * 20190729 作成
 * author hasegawa yuuta
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStarBehaviour : MonoBehaviour
{
	// 変数宣言---------------------------------------------------------
	[SerializeField] bool isAnimation = false;
	public bool IsAnimation { get { return isAnimation; } set { isAnimation = value; } }
	const int animationFrameMax = 75;
	int animationFrame = 0;
	const float delayWeight = 0.3f;
	float moveValue = 40f;
	float defaultScale = 0f;
	Vector3 initialPosition = Vector3.zero;
	TrailRenderer trailRenderer;
	List<Transform> childrenObjectList = new List<Transform>();
	List<Vector3> childrenObjectInitialPositionList = new List<Vector3>();
	//-----------------------------------------------------------------
	/// <summary>
	/// アニメーションを開始する
	/// </summary>
	public void Activate()
	{
		isAnimation = true;
		animationFrame = 0;
		transform.localScale = Vector3.one;
		trailRenderer.enabled = true;
	}

	void Update()
	{
		// アニメーションをしていなければ処理しない
		if (!isAnimation) { return; }
		++animationFrame;
		// 軌跡が消えたら戻る
		if (animationFrame > animationFrameMax + 60 * trailRenderer.time)
		{
			Init();
			return;
		}
		// アニメーションを終えたら移動させない
		if (animationFrame > animationFrameMax)　{　return;　}
		// 自身を移動させる
		Vector3 direction = GetDirection();
		transform.localPosition += direction * moveValue * Time.deltaTime;
		// 自身の移動量に応じて子供を遅らせる
		for (int i = 0; i < childrenObjectList.Count; ++i)
		{
			childrenObjectList[i].transform.localPosition += direction * moveValue * ((i + 1) / (float)childrenObjectList.Count) * Time.deltaTime;
		}
		// フレーム数に応じて大きさを変える
		transform.localScale = Vector3.one * defaultScale * ((animationFrameMax - animationFrame) / (float)animationFrameMax);
		// 子供の大きさを変える
		for (int i = 0; i < childrenObjectList.Count; ++i)
		{
			childrenObjectList[i].transform.localScale = Vector3.one * defaultScale * ((i + 1) / (float)childrenObjectList.Count) * ((animationFrameMax - animationFrame) / (float)animationFrameMax);
		}
	}

	/// <summary>
	/// 状態の初期化
	/// </summary>
	void Init()
	{
		trailRenderer.enabled = false;
		transform.position = initialPosition;
		isAnimation = false;
		transform.localScale = Vector3.zero;
		animationFrame = 0;
		for (int i = 0; i < childrenObjectList.Count; ++i)
		{
			childrenObjectList[i].position = childrenObjectInitialPositionList[i];
			childrenObjectList[i].localScale = Vector3.zero;
		}
	}
	/// <summary>
	/// 進む方向を取得する
	/// </summary>
	/// <returns>進む方向</returns>
	Vector3 GetDirection()
	{
		Vector3 eulerAngle = transform.eulerAngles;
		Vector3 direction = Vector3.zero;
		direction.y = Mathf.Cos((eulerAngle.z + 90f) * Mathf.Deg2Rad);
		float planeMoveValue = Mathf.Sin((eulerAngle.z + 90f) * Mathf.Deg2Rad);
		direction.z = Mathf.Cos((eulerAngle.y - 90f) * Mathf.Deg2Rad) * planeMoveValue;
		direction.x = Mathf.Sin((eulerAngle.y - 90f) * Mathf.Deg2Rad) * planeMoveValue;
		return direction;
	}
	/// <summary>
	/// 状態の設定
	/// </summary>
	public void SettingState()
	{
		// 情報の取得
		trailRenderer = GetComponent<TrailRenderer>();
		trailRenderer.enabled = false;
		// 初期位置を保存
		initialPosition = transform.position;
		// 自身の子供を取得、格納、設定
		foreach (Transform child in transform)
		{
			childrenObjectList.Add(child);
			childrenObjectInitialPositionList.Add(child.position);
			child.localScale = Vector3.zero;
		}
		// 自身の子供を自身の親にする
		for (int i = 0; i < childrenObjectList.Count; ++i)
		{
			childrenObjectList[i].parent = transform.parent;
		}
		// 規定スケール値を保存
		defaultScale = transform.localScale.magnitude;
		// 状態の設定
		transform.localScale = Vector3.zero;
	}
}
