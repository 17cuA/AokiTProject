//作成者：川村良太
//ビートルの挙動

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Beetle : character_status
{
	public enum State
	{
		Front,		//左向き
		Behind,		//右向き
	}

	public State eState;

    GameObject saveObj;
    GameObject childObj;
	GameObject muzzleObj;			//発射位置用
	Vector3 velocity;		
	Vector3 defaultPos;				//初期位置セーブ

	//--------------------------------------------------------------
	//主に上に上がる挙動（登場挙動）の時に使う
	//[Header("入力用　Xスピード")]
	public float speedX;
    public float default_SpeedX;
	[Header("入力用　Xスピード")]
	public float defaultSpeedX_Value;
	[Header("入力用　Yスピード")]
	public float speedY;
	public float defaultSpeedY_Value;
	[Header("入力用　Y移動速度を減速し始める大きさ")]
	public float decelerationY_Start;        //回転の減速開始をする角度
	public float speedZ;
	[Header("入力用　Zスピード")]
	public float speedZ_Value;      //Zスピードの値
	[Header("入力用　Yの移動する距離")]
	public float moveY_Max;			//Yの最大移動値
    public float default_MoveY_Max;
	public float savePosY;          //前のY座標を入れる（移動量を求めるため）
    //--------------------------------------------------------------

    //--------------------------------------------------------------
    //登場後に使う
    float moveX_DelayCnt;
    [Header("入力用　登場後動き出すまでの時間フレーム")]
    public float moveX_DelayMax;
    public float shot_DelayCnt;
    [Header("入力用　攻撃間隔の時間フレーム")]
    public float shot_DelayMax;
    public float shotRotaZ;
    //--------------------------------------------------------------

    public bool once;				//一回だけ行う処理
    public bool isUP;				//上に上がるとき
    public bool isMove;             //登場後の動き始め

	new void Start()
    {
		//初期スピードを保存
        default_SpeedX = speedX;
		defaultSpeedY_Value = speedY;
		//子供にしている弾発射位置取得
		muzzleObj = transform.GetChild(1).gameObject;
		//初期位置保存
		defaultPos = transform.localPosition;

		isUP = true;
        once = true;
        isMove = false;
		base.Start();
    }

    new void Update()
    {
		//一回だけ行う処理
		if (once)
		{
			//位置を初期位置に戻す（再起動時用）
			transform.localPosition = defaultPos;
			switch (eState)
			{
				//自分の向きでスピードの向きを変える
				case State.Front:
					//transform.rotation = Quaternion.Euler(0, -90, 90);
					if (defaultSpeedX_Value > 0)
					{
						defaultSpeedX_Value *= -1;
					}
					break;

				case State.Behind:
					transform.rotation = Quaternion.Euler(0, 180, 0);
					if (defaultSpeedX_Value < 0)
					{
						defaultSpeedX_Value *= -1;
					}

					break;
			}
            once = false;
		}

		//上に上がる状態なら
		if (isUP)
		{
			//Y座標保存
			savePosY = transform.position.y;
			//移動
			velocity = gameObject.transform.rotation * new Vector3(speedX, speedY, -speedZ);
			gameObject.transform.position += velocity * Time.deltaTime;

			//目的の位置までの距離を減らしていく
			moveY_Max -= transform.position.y - savePosY;

			//目的の位置までの距離がYスピードを減らし始める位置より小さくなったら
			if (moveY_Max < decelerationY_Start)
			{
				//Yスピードを今いる位置によって徐々に減らしていく
				speedY = defaultSpeedY_Value * moveY_Max / decelerationY_Start;
			}
			//Yスピードが3より小さくなったら　
			else if (moveY_Max < 3)
			{
				//Zスピードを入れる
				speedZ = speedZ_Value;
				//Xスピードを入れる（画面奥から手前に来ると奥行きの変化によって奥にいた時と手前に来た時の見た目的な位置が変わってしまうためそれを相殺するスピード）
				speedX = defaultSpeedX_Value;
			}
			//手前に来終わったら
			if (transform.position.z < 0)
			{
				//スピードをなくす
				speedZ = 0;
				speedX = 0;
				//Zを正確な0の位置に
				transform.position = new Vector3(transform.position.x, transform.position.y, 0);
				//上に上がる状態を切る
                isUP = false;
			}
		}
		//上に上がった後の処理
		else
		{
			//移動
			velocity = gameObject.transform.rotation * new Vector3(speedX, 0, 0);
			gameObject.transform.position += velocity * Time.deltaTime;

			//上に上がってきた後は少し停止させる
			//動き始める状態なら
            if (isMove)
            {
				//弾発射のカウント加算
                shot_DelayCnt++;
				//カウントが弾の間隔時間を超えたら撃つ
                if (shot_DelayCnt > shot_DelayMax)
                {
					//カウントリセット
                    shot_DelayCnt = 0;
					//弾の角度
                    shotRotaZ = 30f;
					//for文の回数角度に差をつけて撃つ
                    for (int i = 0; i < 3; i++)
                    {
						//弾をアクティブにして取得
                        saveObj = Obj_Storage.Storage_Data.SmallBeam_Bullet_E.Active_Obj();
						//位置を発射位置に
						saveObj.transform.position = muzzleObj.transform.position;
						//決めた角度に変える
						saveObj.transform.rotation = Quaternion.Euler(0, 0, shotRotaZ);
						//角度を変える
						shotRotaZ -= 30f;
                    }
                }
            }
			//止まっている最大時間をカウントが越えたら
            else if (moveX_DelayCnt > moveX_DelayMax)
            {
				//動ける状態にする
                isMove= true;
                switch(eState)
                {
					//向きでスピードを決める
                    case State.Front:
                        speedX = -1.5f;
                        break;

                    case State.Behind:
                        speedX = 1.5f;
                        break;

                }
            }
			//停止状態のときにカウントを加算
            else
            {
                moveX_DelayCnt++;
            }

        }
		//奥行きの位置によってテクスチャを暗くする処理
		HSV_Change();

		//HPが0になったら
        if (hp < 1)
        {
			//死ぬ処理
			ResetEnemy();
            Died_Process();
        }
		//画面外に行ったら
        if (transform.localPosition.x < -35)
        {
			//消す
			//Destroy(this.gameObject);
			ResetEnemy();
			gameObject.SetActive(false);
        }
		base.Update();
	}

	//リセット（再起動用）
    void ResetEnemy()
    {
        speedX = default_SpeedX;
        speedY = defaultSpeedY_Value;
        moveY_Max = default_MoveY_Max;
        shot_DelayCnt = 0;
    }
}
