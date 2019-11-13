using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Collider_Manager : MonoBehaviour
{
	//コライダーの任意方向への変化のステータス
	[System.Serializable]
	public struct CoordinateChangeStatus
	{
		public AnimationCurve colliderSize;     //任意方向へサイズ変化のカーブ
		public bool coliderCenterFixed;         //コライダーの中心の固定の可否
		public float coordinateFixed;               //固定する場合のコライダーの中心の座標
		public float maxColliderSize;               //コライダーの最大サイズ
	}

	public float playTime;              //全体の再生時間
	public float elapsedTime;       //経過時間

	[SerializeField]
	public CoordinateChangeStatus xDirectionCoordinateChangeStatus; //X方向
	[SerializeField]
	public CoordinateChangeStatus yDirectionCoordinateChangeStatus; //Y方向
	[SerializeField]
	public CoordinateChangeStatus zDirectionCoordinateChangeStatus; //Z方向

	private BoxCollider myCollider;     //コライダーコンポーネント

	void Start()
	{
		elapsedTime = 0.0f;
		myCollider = GetComponent<BoxCollider>();

		xDirectionCoordinateChangeStatus.maxColliderSize = 0.0f;
		yDirectionCoordinateChangeStatus.maxColliderSize = 0.0f;
		zDirectionCoordinateChangeStatus.maxColliderSize = 0.0f;
	}

	void Update()
	{
		//経過時間の加算
		elapsedTime += Time.deltaTime;

		//長さ変更処理
		//経過時間に対応したグラフの数値を取得
		Vector3 myColliderSize;
		myColliderSize.x = (float)xDirectionCoordinateChangeStatus.colliderSize.Evaluate(elapsedTime);
		myColliderSize.y = (float)yDirectionCoordinateChangeStatus.colliderSize.Evaluate(elapsedTime);
		myColliderSize.z = (float)zDirectionCoordinateChangeStatus.colliderSize.Evaluate(elapsedTime);
		myCollider.size = myColliderSize;

		//最大値の保存
		if (xDirectionCoordinateChangeStatus.maxColliderSize < (float)xDirectionCoordinateChangeStatus.colliderSize.Evaluate(elapsedTime))
			xDirectionCoordinateChangeStatus.maxColliderSize = (float)xDirectionCoordinateChangeStatus.colliderSize.Evaluate(elapsedTime);
		if (yDirectionCoordinateChangeStatus.maxColliderSize < (float)yDirectionCoordinateChangeStatus.colliderSize.Evaluate(elapsedTime))
			yDirectionCoordinateChangeStatus.maxColliderSize = (float)yDirectionCoordinateChangeStatus.colliderSize.Evaluate(elapsedTime) ;
		if (zDirectionCoordinateChangeStatus.maxColliderSize < (float)zDirectionCoordinateChangeStatus.colliderSize.Evaluate(elapsedTime))
			zDirectionCoordinateChangeStatus.maxColliderSize = (float)zDirectionCoordinateChangeStatus.colliderSize.Evaluate(elapsedTime);

		//中心点変更処理
		Vector3 myColliderCenter = myColliderSize/2.0f;

		//中心点固定
		if (xDirectionCoordinateChangeStatus.coliderCenterFixed) myColliderCenter.x = xDirectionCoordinateChangeStatus.coordinateFixed;
		if (yDirectionCoordinateChangeStatus.coliderCenterFixed) myColliderCenter.y = yDirectionCoordinateChangeStatus.coordinateFixed;
		if (zDirectionCoordinateChangeStatus.coliderCenterFixed) myColliderCenter.z = zDirectionCoordinateChangeStatus.coordinateFixed;

		//再生前半
		if (playTime / 2.0f >= elapsedTime)
		{
			myCollider.center = myColliderCenter;
		}
		//後半
		else if (playTime > elapsedTime)
		{
			if (!xDirectionCoordinateChangeStatus.coliderCenterFixed)
				myColliderCenter.x = xDirectionCoordinateChangeStatus.maxColliderSize- myColliderCenter.x;
			if (!yDirectionCoordinateChangeStatus.coliderCenterFixed)
				myColliderCenter.y = yDirectionCoordinateChangeStatus.maxColliderSize - myColliderCenter.y;
			if (!zDirectionCoordinateChangeStatus.coliderCenterFixed)
				myColliderCenter.z = zDirectionCoordinateChangeStatus.maxColliderSize - myColliderCenter.z;

			myCollider.center = myColliderCenter ;
		}
		//再生時間時間経過後
		else if (elapsedTime >= playTime)
		{
			//変数リセット
			elapsedTime = 0.0f;
			xDirectionCoordinateChangeStatus.maxColliderSize = 0.0f;
			yDirectionCoordinateChangeStatus.maxColliderSize = 0.0f;
			zDirectionCoordinateChangeStatus.maxColliderSize = 0.0f;
			//非表示にして待機
			gameObject.SetActive(false);
		}
	}
}
