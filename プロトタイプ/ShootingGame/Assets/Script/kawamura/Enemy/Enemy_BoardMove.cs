using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BoardMove : MonoBehaviour
{
	public GameObject parentObj;
	public Enemy_Board_Parent ebp;
	Vector3 velocity;
	public float speedX;            //Xスピード
	public float speedY;            //Yスピード
	//public float speedZ;            //Zスピード（移動時）
	//public float speedZ_Value;      //Zスピードの値だけ
	float startPosY;                //最初のY座標値
	public float defaultSpeedY;         //Yスピードの初期値（最大値でもある）を入れておく
	public float addAndSubValue;        //Yスピードを増減させる値
	public float sin;

	public bool isAddSpeedY = false;    //Yスピードを増加させるかどうか
	public bool isSubSpeedY = false;    //Yスピードを減少させるかどうか

	private void Awake()
	{
		parentObj = transform.parent.gameObject;
		ebp = parentObj.GetComponent<Enemy_Board_Parent>();
	}
	void Start()
    {
        
    }

    void Update()
    {

		if (ebp.speedX == 0)
		{
            //speedX = 1;
            //sin =posY + Mathf.Sin(Time.time*5);

            SpeedY_Check();
            SpeedY_Calculation();

            //this.transform.position = new Vector3(transform.position.x, sin, 0);
            //transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.frameCount * 0.05f), transform.position.z);
            velocity = gameObject.transform.rotation * new Vector3(-speedX, speedY, 0);
            gameObject.transform.position += velocity * Time.deltaTime;

        }

	}
	//Yスピードを見てYスピードを増加させるか減少させるかを決める
	void SpeedY_Check()
	{
		if (defaultSpeedY >= 0)
		{
			//スピードが初期値以上になった時
			if (speedY > defaultSpeedY)
			{
				//増加をfalse 減少をtrue
				isAddSpeedY = false;
				isSubSpeedY = true;
			}
			//スピードが0以下になったとき
			else if (speedY < -defaultSpeedY)
			{
				//減少をfalse 増加をtrue
				isSubSpeedY = false;
				isAddSpeedY = true;
			}
		}
		else if (defaultSpeedY < 0)
		{
			//スピードが初期値以上になった時
			if (speedY > -defaultSpeedY)
			{
				//増加をfalse 減少をtrue
				isAddSpeedY = false;
				isSubSpeedY = true;
			}
			//スピードが0以下になったとき
			else if (speedY < defaultSpeedY)
			{
				//減少をfalse 増加をtrue
				isSubSpeedY = false;
				isAddSpeedY = true;
			}
		}
	}

	//スピードを増減させる
	void SpeedY_Calculation()
	{
		//増加がtrueなら
		if (isAddSpeedY)
		{
			//Yスピードを増加
			speedY += addAndSubValue;
		}
		//減少がtrueなら
		else if (isSubSpeedY)
		{
			//Yスピードを減少
			speedY -= addAndSubValue;
		}
	}

}
