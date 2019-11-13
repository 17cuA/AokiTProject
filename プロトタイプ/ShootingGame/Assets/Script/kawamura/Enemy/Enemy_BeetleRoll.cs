//作成者：川村良太
//ビートル敵の回転スクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BeetleRoll : MonoBehaviour
{
	GameObject parentObj;
	Enemy_Beetle beetle_Script;

	public int rollCnt;							//回転した回数を数える
	public float rotaZ;                         //代入する角度の値
	[Header("入力用　回転する最大値")]
	public float rotaZ_Max;                 //回転する最大値（この値を減らしていって、どれくらい回転したかを見る。この値が残りの回転角度）
	[Header("入力用　回転速度を減速し始める大きさ")]
	public float deceleration_Start;		//回転の減速開始をする角度
	[Header("入力用　角度を変える数値の大きさ設定")]
	public float RotaZ_ChangeValue;     //角度を変える値
	public float defaultRotaZ_ChangeValue;	//回転の変化値

	public bool isRoll;
    void Start()
    {
		parentObj = transform.parent.gameObject;
		beetle_Script = parentObj.GetComponent<Enemy_Beetle>();
		if (beetle_Script.eState == Enemy_Beetle.State.Behind)
		{
			RotaZ_ChangeValue += -1;
		}
		//モデル自体の向きの関係で初期値が0じゃないので、初期のRotation.Zを入れる
		rotaZ = transform.eulerAngles.z;
		//回転変化値を保存します
		defaultRotaZ_ChangeValue = RotaZ_ChangeValue;
		//rotaZ_Max = 720;
		isRoll = true;
    }

    void Update()
    {
		//回転trueのとき回転関数呼び出し
		if (isRoll)
		{
			BeetleRoll();
		}
	}

	void BeetleRoll()
	{
		//残りの回転値が減速開始値より小さくなったら
		if (rotaZ_Max < deceleration_Start)
		{
			//回転値を小さく
			RotaZ_ChangeValue = defaultRotaZ_ChangeValue * (rotaZ_Max / deceleration_Start);
		}

		rotaZ += RotaZ_ChangeValue;
		if (RotaZ_ChangeValue < 0)
		{
			rotaZ_Max += RotaZ_ChangeValue;
		}
		else
		{
			rotaZ_Max -= RotaZ_ChangeValue;
		}

		transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, rotaZ);

		if (rotaZ_Max < 0)
		{
			rotaZ_Max = 0;
			isRoll = false;
		}
	}
}
