//作成者：川村良太
//ビットン（下のビットン）の斜め撃ちでの角度を変えるスクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObliquePosUnder : MonoBehaviour
{
	public float rotaZ;				//角度の値
	float rotaZ_ChangeValue;	//角度の変化値
	float rotaZ_Mini;			//開いている角度の最小
	float rotaZ_Max;			//開いている角度の最大

	private Quaternion Direction;   //オブジェクトの向きを変更する時に使う  

    void Start()
    {
        rotaZ = -15.0f;
		rotaZ_ChangeValue = 10.0f;
		rotaZ_Mini = -10.0f;			
		rotaZ_Max = -25.0f;
    }

    void Update()
    {
		Direction = transform.rotation;

		//ビットンの角度を小さくさせる
		if (Input.GetKeyDown(KeyCode.J))
		{
			//角度を変化値分小さく
			rotaZ += rotaZ_ChangeValue;
			//角度が最小値より小さくなったら
			if(rotaZ > rotaZ_Mini)
			{
				//最小値に戻す
				rotaZ = rotaZ_Mini;
			}
		}
		//ビットンの角度を大きくする
		else if(Input.GetKeyDown(KeyCode.K))
		{
			//角度を変化値分大きく
			rotaZ -= rotaZ_ChangeValue;
			//角度が最大値より大きくなったら
			if(rotaZ < rotaZ_Max)
			{
				//最大値に戻す
				rotaZ = rotaZ_Max;
			}

		}
		//角度を代入
        transform.rotation=Quaternion.Euler(transform.rotation.x,transform.rotation.y,rotaZ);
		Direction = transform.rotation;

		if (Input.GetKeyDown(KeyCode.V) || Input.GetButtonDown("Fire1"))
		{
			//方向転換させる関数の呼び出し
			Change_In_Direction();
			
		}

    }

	//ビットンの方向転換
	private void Change_In_Direction()
	{
		//方向に−１をかけて反転した物を入れる
		Direction *= new Quaternion(0, -1, -1, 0);
		transform.rotation = Direction;
	}


}
