//作成者：川村良太
//画面奥から来たり上下移動をしながら来る敵（闘牛型）のスクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class Enemy_Wave : character_status
{
	public enum State
	{
		WaveUp,
		WaveDown,
		WaveOnlyUp,
		WaveOnlyDown,
		BackWaveUp,
		BackWaveDown,
		BackWaveOnlyUp,
		BackWaveOnlyDown,
		Rush,
		BackRush,
		Straight,
		BackStraight,
	}
	public State eState;

	GameObject childObj;        //子供入れる
	public GameObject childObj_Shot;
	public GameObject childObj_Angle;
	GameObject item;			//アイテム入れる
	GameObject parentObj;		//親入れる（群れの時のため）
	//GameObject blurObj;

	Renderer renderer;			//レンダラー
	//HSVColorController hsvCon;	//シェーダー用
	//Color hsvColor;
	//BlurController blurCon;
	EnemyGroupManage groupManage;			//群れの時の親スクリプト
	Find_Angle fd;
	public Find_Angle fd_Rush;
	//public ParticleSystem sonicBoom;			//ジェット噴射の衝撃波のようなパーティクル

	Vector3 velocity;
	Vector3 defaultPos;
	public Quaternion diedAttackRota;

	//----------
	public Vector3 startMarker;
	public Vector3 endMarker;
	float startTime;
	public float slaep_IncValue;
	float present_Location;
	public float testSpeed = 1.0f;

	private float distance_two;
	//----------

	public float speedX;			//Xスピード
	public float speedY;			//Yスピード
	public float speedZ;			//Zスピード（移動時）
	public float speedZ_Value;		//Zスピードの値だけ
	float startPosY;                //最初のY座標値
	float rotaY;					//Y角度
	public float amplitude;         //画面奥から出てこない時の上下の振れ幅
	public float rushStayCnt;
	[Header("突進角度が変わり始めるまでの秒")]
	public float rushStayCntMax;
	[Header("角度が変え終わって突進するまでの秒")]
	public float rushStartTime;
	public float saverushRotaZ;
	public float rushRotaZ;
	public float rushRotaZ_Value;
	public float defaultSpeedY;         //Yスピードの初期値（最大値でもある）を入れておく
	public float addAndSubValue;        //Yスピードを増減させる値

	public float sin;
	[Header("死亡時の弾発射の角度範囲()")]
	public float diedAttack_RotaValue;

	//float posX;
	//float posY;
	//float posZ;
	//float defPosX;
	//float val_Value;					//テクスチャの明るさの増える値
	//float sigma_Value;					//ブラーのぼやけ具合の値（0でぼやけなし）
	//public float h_Value;
	//public float s_Value;

	//public float v_Value;

	public bool isAddSpeedY = false;	//Yスピードを増加させるかどうか
	public bool isSubSpeedY = false;	//Yスピードを減少させるかどうか

	public bool once = true;			//updateで一回だけ呼び出す処理用
	public bool isWave = false;			//奥からくる敵を上下移動に変える用
	public bool isStraight = false;		//直進かどうか
	public bool isOnlyWave;             //上下移動のみか（左へ進みながら）
	public bool haveItem = false;

	public bool isSlerp = false;
	//public bool susumimasu=true;
	public bool isNoSlerp=false;
	bool isSonicPlay = false;
    public bool utsuttemasuyo=false;
    bool isWaveStart = false;
	bool isRushStart = false;
	bool isRush = false;
	public bool Died_Attack = false;
	public bool isFromBack = false;				//奥からくるやつ用
	public bool isBehind = false;
	//float present_Location = 0;
	//---------------------------------------------------------

	private void Awake()
	{
		//sonicBoom.Stop();
		isSonicPlay = false;
		defaultPos = transform.localPosition;

		if (gameObject.GetComponent<DropItem>())
		{
			DropItem dItem = gameObject.GetComponent<DropItem>();
			haveItem = true;
		}
		switch (eState)
		{
			case State.WaveUp:
				isFromBack = true;
				isBehind = false;
				endMarker = new Vector3(12.0f, transform.position.y, 0);
				distance_two = Vector3.Distance(startMarker, endMarker);
				break;

			case State.WaveDown:
				isFromBack = true;
				isBehind = false;
				endMarker = new Vector3(12.0f, transform.position.y, 0);
				distance_two = Vector3.Distance(startMarker, endMarker);
				break;

			case State.BackWaveUp:
				isFromBack = true;
				isBehind = true;
				endMarker = new Vector3(-14.0f, transform.position.y, 0);
				distance_two = Vector3.Distance(startMarker, endMarker);
				break;

			case State.BackWaveDown:
				isFromBack = true;
				isBehind = true;
				endMarker = new Vector3(-14.0f, transform.position.y, 0);
				distance_two = Vector3.Distance(startMarker, endMarker);
				break;

			case State.Rush:
				isFromBack = true;
				isBehind = false;
				endMarker = new Vector3(12.0f, transform.position.y, 0);
				distance_two = Vector3.Distance(startMarker, endMarker);
				break;

			case State.BackRush:
				isFromBack = true;
				isBehind = true;
				endMarker = new Vector3(-14.0f, transform.position.y, 0);
				distance_two = Vector3.Distance(startMarker, endMarker);
				break;
		}
	}
	private void OnEnable()
	{
		//transform.localPosition = defaultPos;
		//startMarker = new Vector3(12.0f, transform.position.y, 40.0f);
		//endMarker = new Vector3(12.0f, transform.position.y, 0);

	}

	new void Start()
	{
		//startMarker = new Vector3(12.0f, transform.position.y, 40.0f);
		item = Resources.Load("Item/Item_Test") as GameObject;

		childObj = transform.GetChild(0).gameObject;            //モデルオブジェクトの取得（3Dモデルを子供にしているので）
		//childObj_Shot = transform.GetChild(1).gameObject;
		childObj_Angle = transform.GetChild(1).gameObject;
		//childCnt = transform.childCount;
		renderer = childObj.GetComponent<Renderer>();
		fd = childObj_Angle.GetComponent<Find_Angle>();
		fd_Rush = childObj_Angle.GetComponent<Find_Angle>();

		if (transform.parent)
		{
			parentObj = transform.parent.gameObject;
			groupManage = parentObj.GetComponent<EnemyGroupManage>();
		}
        else
        {
            parentObj = GameObject.Find("TemporaryParent");
            transform.parent = parentObj.transform;
        }

        speedZ = 0;
		//posX = transform.position.x;
		startPosY = transform.position.y;
		//posZ = -5.0f;
		//defPosX = (13.0f - transform.position.x) / 120.0f;         //13.0fはとりあえず敵が右へ向かう限界の座標
		startTime = 0.0f;
		HP_Setting();
		base.Start();
	}

	new void Update()
	{
		//if (transform.childCount == 0)
		//{
		//	Destroy(this.gameObject);
		//}
		if(once)
		{
            transform.localPosition = defaultPos;
			//状態によって値を変える
			switch(eState)
			{

				//画面左から右へ、後ろからきて上下移動は上からし始める
				case State.WaveUp:
					isStraight = false;
					isOnlyWave = false;
					isBehind = false;
					if (defaultSpeedY < 0)
					{
						defaultSpeedY *= -1;
					}
					isSubSpeedY = true;
					isAddSpeedY = false;
					speedX = 18;
					speedZ_Value = 40;
					//transform.position = new Vector3(transform.position.x, transform.position.y, 40.0f);
					isWave = false;
					//hsvCon.val = 0.4f;
					//v_Value = 0.4f;
					//hsvColor = UnityEngine.Color.HSVToRGB(24.0f, 100.0f, 40.0f);
					//hsvColor = UnityEngine.Color.HSVToRGB(0, 0, v_Value);
					//renderer.material.color = UnityEngine.Color.HSVToRGB(0, 0, v_Value);
					HSV_Change();
					break;
				
				//画面左から右へ、後ろからきて上下移動は下からし始める
				case State.WaveDown:
					isStraight = false;
					isOnlyWave = false;
					isBehind = false;
					if (defaultSpeedY > 0)
					{
						defaultSpeedY *= -1;
					}
					isAddSpeedY = true;
					isSubSpeedY = false;
					speedX = 18;
					speedZ_Value = 40;
					//transform.position = new Vector3(transform.position.x, transform.position.y, 40.0f);
					isWave = false;
					//hsvCon.val = 0.4f;
					//v_Value = 0.4f;
					//hsvColor = UnityEngine.Color.HSVToRGB(1, 1, 0.4f);
					//hsvColor = UnityEngine.Color.HSVToRGB(0, 0, v_Value);
					//renderer.material.color = UnityEngine.Color.HSVToRGB(0, 0, v_Value);
					HSV_Change();
					break;

				//画面右からきて上下移動は上からし始める
				case State.WaveOnlyUp:
					//transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
					isWaveStart = true;
					isBehind = false;
					if (defaultSpeedY < 0)
					{
						defaultSpeedY *= -1;
					}
                    //speedY = defaultSpeedY;
                    speedY = 0;
					amplitude = 0.1f;
					speedX = 7.5f;
					speedZ_Value = 0;
					isStraight = false;
					isOnlyWave = true;
					//isWave = true;
					isAddSpeedY = true;
					//hsvCon.val = 1.0f;
					//hsvColor = UnityEngine.Color.HSVToRGB(0, 0, 1);
					//renderer.material.color = UnityEngine.Color.HSVToRGB(0, 0, 1);
					HSV_Change();
					break;

				//画面右からきて上下移動は下からし始める
				case State.WaveOnlyDown:
					//transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
					isWaveStart = true;
					isBehind = false;
					if (defaultSpeedY > 0)
					{
						defaultSpeedY *= -1;
					}
                    //speedY = defaultSpeedY;
                    speedY = 0;
					amplitude = -0.1f;
					speedX = 7.5f;
					speedZ_Value = 0;
					isOnlyWave = true;
					//isWave = true;
					isStraight = false;
					isSubSpeedY = true;
					//hsvCon.val = 1.0f;
					//hsvColor = UnityEngine.Color.HSVToRGB(0, 0, 1);
					//renderer.material.color = UnityEngine.Color.HSVToRGB(0, 0, 1);
					HSV_Change();
					break;

				//画面右から左へ、後ろからきて上下移動は上からし始める
				case State.BackWaveUp:
					isStraight = false;
					isOnlyWave = false;
					isBehind = true;
					if (defaultSpeedY < 0)
					{
						defaultSpeedY *= -1;
					}
					isSubSpeedY = true;
					isAddSpeedY = false;
					speedX = -18;
					speedZ_Value = 40;
					transform.position = new Vector3(transform.position.x, transform.position.y, 40.0f);
					isWave = false;
					//hsvCon.val = 0.4f;
					//v_Value = 0.4f;
					//hsvColor = UnityEngine.Color.HSVToRGB(24.0f, 100.0f, 40.0f);
					//hsvColor = UnityEngine.Color.HSVToRGB(0, 0, v_Value);
					//renderer.material.color = UnityEngine.Color.HSVToRGB(0, 0, v_Value);
					HSV_Change();
					break;

				//画面右から左へ、後ろからきて上下移動は下からし始める
				case State.BackWaveDown:
					isStraight = false;
					isOnlyWave = false;
					isBehind = true;
					if (defaultSpeedY > 0)
					{
						defaultSpeedY *= -1;
					}
					isAddSpeedY = true;
					isSubSpeedY = false;
					speedX = -18;
					speedZ_Value = 40;
					transform.position = new Vector3(transform.position.x, transform.position.y, 40.0f);
					isWave = false;
					//hsvCon.val = 0.4f;
					//v_Value = 0.4f;
					//hsvColor = UnityEngine.Color.HSVToRGB(1, 1, 0.4f);
					//hsvColor = UnityEngine.Color.HSVToRGB(0, 0, v_Value);
					//renderer.material.color = UnityEngine.Color.HSVToRGB(0, 0, v_Value);
					HSV_Change();
					break;

				//画面左からきて上下移動は下からし始める
				case State.BackWaveOnlyUp:
					transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
					isWaveStart = true;
					isBehind = true;
					if (defaultSpeedY < 0)
					{
						defaultSpeedY *= -1;
					}
					//speedY = defaultSpeedY;
					speedY = 0;
					amplitude = 0.1f;
					speedX = -7.5f;
					speedZ_Value = 0;
					isStraight = false;
					isOnlyWave = true;
					//isWave = true;
					isAddSpeedY = true;
					//hsvCon.val = 1.0f;
					//hsvColor = UnityEngine.Color.HSVToRGB(0, 0, 1);
					//renderer.material.color = UnityEngine.Color.HSVToRGB(0, 0, 1);
					HSV_Change();
					break;
				
				//画面左からきて上下移動は下からし始める
				case State.BackWaveOnlyDown:
					transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
					isWaveStart = true;
					isBehind = true;
					if (defaultSpeedY > 0)
					{
						defaultSpeedY *= -1;
					}
					//speedY = defaultSpeedY;
					speedY = 0;
					amplitude = -0.1f;
					speedX = -7.5f;
					speedZ_Value = 0;
					isOnlyWave = true;
					//isWave = true;
					isStraight = false;
					isSubSpeedY = true;
					//hsvCon.val = 1.0f;
					//hsvColor = UnityEngine.Color.HSVToRGB(0, 0, 1);
					//renderer.material.color = UnityEngine.Color.HSVToRGB(0, 0, 1);
					HSV_Change();
					break;
				
				//突進
				case State.Rush:
					isStraight = false;
					isOnlyWave = false;
					isBehind = false;
					speedX = 18;
					speedZ_Value = 40;
					transform.position = new Vector3(transform.position.x, transform.position.y, 40.0f);
					isWave = false;
					//hsvCon.val = 0.4f;
					//v_Value = 0.4f;
					//hsvColor = UnityEngine.Color.HSVToRGB(24.0f, 100.0f, 40.0f);
					//hsvColor = UnityEngine.Color.HSVToRGB(0, 0, v_Value);
					//renderer.material.color = UnityEngine.Color.HSVToRGB(0, 0, v_Value);
					HSV_Change();

					break;

				//突進
				case State.BackRush:
					isStraight = false;
					isOnlyWave = false;
					isBehind = true;
					speedX = -18;
					speedZ_Value = 40;
					transform.position = new Vector3(transform.position.x, transform.position.y, 40.0f);
					isWave = false;
					//hsvCon.val = 0.4f;
					//v_Value = 0.4f;
					//hsvColor = UnityEngine.Color.HSVToRGB(24.0f, 100.0f, 40.0f);
					//hsvColor = UnityEngine.Color.HSVToRGB(0, 0, v_Value);
					//renderer.material.color = UnityEngine.Color.HSVToRGB(0, 0, v_Value);
					HSV_Change();

					break;
				
				//直進
				case State.Straight:
					transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
					isStraight = true;
					isBehind = false;
					speedX = 7.5f;
					amplitude = 0;
					//hsvCon.val = 1.0f;
					//hsvColor = UnityEngine.Color.HSVToRGB(0, 0, 1);
					//renderer.material.color = UnityEngine.Color.HSVToRGB(0, 0, 1);
					HSV_Change();
					break;

				//後ろから直進
				case State.BackStraight:
					transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
					isStraight = true;
					isBehind = true;
					speedX = -7.5f;
					amplitude = 0;
					//hsvCon.val = 1.0f;
					//hsvColor = UnityEngine.Color.HSVToRGB(0, 0, 1);
					//renderer.material.color = UnityEngine.Color.HSVToRGB(0, 0, 1);
					HSV_Change();
					break;

			}
			once = false;
		}


		if (eState == State.Straight || eState == State.BackStraight)
		{
			velocity = gameObject.transform.rotation * new Vector3(-speedX, 0, 0);
			gameObject.transform.position += velocity * Time.deltaTime;
		}
		//else if (eState == State.Rush)
		//{

		//}
		else if (eState == State.WaveOnlyUp || eState == State.WaveOnlyDown || eState == State.BackWaveOnlyUp || eState == State.BackWaveOnlyDown)
        {
			if(!isBehind)
			{
				if (transform.position.x < 20 && transform.position.z == 0)
				{
					if (!isWaveStart)
					{
						speedY = defaultSpeedY;
						isWaveStart = true;
					}
				}

				if (isWaveStart)
				{
					SpeedY_Check();
					SpeedY_Calculation();

				}
				else
				{
					speedY = 0;
				}
				//transform.position = new Vector3(transform.position.x, startPosY + Mathf.Sin(Time.frameCount * amplitude), transform.position.z);
				velocity = gameObject.transform.rotation * new Vector3(-speedX, speedY, 0);

				//velocity = gameObject.transform.rotation * new Vector3(-speedX, 0, 0);
				gameObject.transform.position += velocity * Time.deltaTime;
			}
			else if(isBehind)
			{
				if (transform.position.x > -18 && transform.position.z == 0)
				{
					if (!isWaveStart)
					{
						speedY = defaultSpeedY;
						isWaveStart = true;
					}
				}

				if (isWaveStart)
				{
					SpeedY_Check();
					SpeedY_Calculation();

				}
				else
				{
					speedY = 0;
				}
				//transform.position = new Vector3(transform.position.x, startPosY + Mathf.Sin(Time.frameCount * amplitude), transform.position.z);
				velocity = gameObject.transform.rotation * new Vector3(-speedX, speedY, 0);

				//velocity = gameObject.transform.rotation * new Vector3(-speedX, 0, 0);
				gameObject.transform.position += velocity * Time.deltaTime;

			}

		}
        else if (!isWave)
        {
            if (isSlerp)
            {
                //if (transform.position.x < 12)
                //{
                //	velocity = gameObject.transform.rotation * new Vector3(speedX, 0, -speedZ);
                //	gameObject.transform.position += velocity * Time.deltaTime;
                //	if (transform.position.x >= 12)
                //	{
                //		transform.position = new Vector3(12.0f, transform.position.y, 40.0f);
                //	}
                //}
                //else if(transform.position.x>=12.0f)
                //{
                //if (isSonicPlay)
                //{
                //    //sonicBoom.Stop();
                //    isSonicPlay = false;
                //}
                present_Location = (Time.time * testSpeed) / distance_two;
                transform.position = Vector3.Slerp(startMarker, endMarker, startTime);
                startTime += slaep_IncValue;
                if (startTime > 1)
                {
                    startTime = 1;
                }
                //startTime++;
                HSV_Change();

                if (transform.position == endMarker)
                {
                    isWave = true;
					transform.position = new Vector3(transform.position.x, transform.position.y, 0);
					
					//前からなら
					if(!isBehind)
					{
						speedX = 7.5f;
					}
					//後ろからなら
					else if(isBehind)
					{
						speedX = -7.5f;
					}
					speedY = defaultSpeedY;

                }
                //}
            }
            else if (isNoSlerp)
            {
                velocity = gameObject.transform.rotation * new Vector3(speedX, 0, -speedZ);
                gameObject.transform.position += velocity * Time.deltaTime;

				//前からなら
				if(!isBehind)
				{
					if (transform.position.z < 0)
					{
						transform.position = new Vector3(transform.position.x, transform.position.y, 0);
						speedX = 7.5f;
						speedY = defaultSpeedY;
						isWave = true;
					}

					if (transform.position.x >= 12)
					{
						isSlerp = true;
						startMarker = transform.position;
						//sonicBoom.Play();
						isSonicPlay = true;
					}

					if (transform.position.x > 7)
					{
						//明るさを変える関数
						HSV_Change();

					}
				}
				else if(isBehind)
				{
					if (transform.position.z < 0)
					{
						transform.position = new Vector3(transform.position.x, transform.position.y, 0);
						speedX = 7.5f;
						speedY = defaultSpeedY;
						isWave = true;
					}

					if (transform.position.x <= -14.0f)
					{
						isSlerp = true;
						startMarker = transform.position;
						//sonicBoom.Play();
						isSonicPlay = true;
					}

					if (transform.position.x < 9)
					{
						//明るさを変える関数
						HSV_Change();

					}
				}
			}
        }
        else if (isWave)
		{
			if (eState == State.Rush || eState == State.BackRush)
			{
				//前からなら
				if(!isBehind)
				{
					//突進
					if (isRush)
					{
						rushStayCnt += Time.deltaTime;
						//画面手前に来てからの時間。向きを変えて突進するまでの時間
						if (rushStayCnt > rushStartTime)
						{
							speedX = 16;
							velocity = gameObject.transform.rotation * new Vector3(-speedX, 0, 0);
							gameObject.transform.position += velocity * Time.deltaTime;

						}
					}
					//ここは向きを変える処理
					else if (isRushStart)
					{
						//向きを変える　変え終わったら突進へ
						if (rushRotaZ_Value > 0)
						{
							rushRotaZ += 0.5f;
							if (rushRotaZ > rushRotaZ_Value)
							{
								rushRotaZ = rushRotaZ_Value;
								isRush = true;
							}
							transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rushRotaZ);
						}
						else if (rushRotaZ_Value < 0)
						{
							rushRotaZ -= 0.5f;
							if (rushRotaZ < rushRotaZ_Value)
							{
								rushRotaZ = rushRotaZ_Value;
								isRush = true;
							}
							transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rushRotaZ);
						}
					}
					else if (!isRush && !isRushStart)
					{
						rushStayCnt += Time.deltaTime;
						//向きを変え始めるまでの時間がCntMax
						if (rushStayCnt > rushStayCntMax)
						{
							isRushStart = true;
							rushStayCnt = 0;
							//向く角度を決める
							saverushRotaZ = fd_Rush.degree;

							if (saverushRotaZ > 0)
							{
								rushRotaZ_Value = saverushRotaZ - 180;
							}
							else if (saverushRotaZ < 0)
							{
								rushRotaZ_Value = saverushRotaZ + 180;
							}
						}
					}
				}
				//後ろからなら
				else if(isBehind)
				{
					//突進
					if (isRush)
					{
						rushStayCnt += Time.deltaTime;
						//画面手前に来てからの時間。向きを変えて突進するまでの時間
						if (rushStayCnt > rushStartTime)
						{
							speedX = -16;
							velocity = gameObject.transform.rotation * new Vector3(-speedX, 0, 0);
							gameObject.transform.position += velocity * Time.deltaTime;

						}
					}
					//ここは向きを変える処理
					else if (isRushStart)
					{
						//向きを変える　変え終わったら突進へ
						if (rushRotaZ_Value > 0)
						{
							rushRotaZ += 0.5f;
							if (rushRotaZ > rushRotaZ_Value)
							{
								rushRotaZ = rushRotaZ_Value;
								isRush = true;
							}
							transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rushRotaZ);
						}
						else if (rushRotaZ_Value < 0)
						{
							rushRotaZ -= 0.5f;
							if (rushRotaZ < rushRotaZ_Value)
							{
								rushRotaZ = rushRotaZ_Value;
								isRush = true;
							}
							transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rushRotaZ);
						}
					}
					else if (!isRush && !isRushStart)
					{
						rushStayCnt += Time.deltaTime;
						//向きを変え始めるまでの時間がCntMax
						if (rushStayCnt > rushStayCntMax)
						{
							isRushStart = true;
							rushStayCnt = 0;
							//向く角度を決める
							saverushRotaZ = fd_Rush.degree;

							if (saverushRotaZ > 0)
							{
								rushRotaZ_Value = saverushRotaZ;
							}
							else if (saverushRotaZ < 0)
							{
								rushRotaZ_Value = saverushRotaZ ;
							}
						}
					}
				}
			}
			else
			{
				speedX = 7.5f;
				//sin =posY + Mathf.Sin(Time.time*5);

				SpeedY_Check();
				SpeedY_Calculation();

				//this.transform.position = new Vector3(transform.position.x, sin, 0);
				//transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.frameCount * 0.1f), transform.position.z);
				velocity = gameObject.transform.rotation * new Vector3(-speedX, speedY, 0);
				gameObject.transform.position += velocity * Time.deltaTime;
			}
		}

		if (hp < 1)
		{
			if (haveItem)
			{
				//Instantiate(item, this.transform.position, transform.rotation);
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePOWERUP_ITEM, this.transform.position, Quaternion.identity);

			}
			if(Died_Attack)
			{
				diedAttackRota = Quaternion.Euler(0, 0, Random.Range(fd.degree - diedAttack_RotaValue, fd.degree + diedAttack_RotaValue));
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET, transform.position, diedAttackRota);

			}

			if (parentObj)
			{
                if(parentObj.name!= "TemporaryParent")
                {
				    //群を管理している親の残っている敵カウントマイナス
				    groupManage.remainingEnemiesCnt--;
				    //倒された敵のカウントプラス
				    groupManage.defeatedEnemyCnt++;
				    //群に残っている敵がいなくなったとき
				    if (groupManage.remainingEnemiesCnt == 0)
				    {
					    //倒されずに画面外に出た敵がいなかったとき(すべての敵が倒されたとき)
					    if (groupManage.notDefeatedEnemyCnt == 0 && groupManage.isItemDrop)
					    {
							//アイテム生成
							//Instantiate(item, this.transform.position, transform.rotation);
							Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePOWERUP_ITEM, this.transform.position, transform.rotation);
						}
						//一体でも倒されていないのがいたら
						else
					    {
						    //なにもしない
					    }
				    }
                }
            }
			Enemy_Reset();
			//Reset_Status();
			Died_Process();
		}
		base.Update();
	}
	//-------------ここから関数------------------

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

	public void SetState(State s)
	{
		eState = s;
		//switch(s)
		//{
		//	case State.WaveUp:
		//		eState = State.WaveUp;
		//		break;

		//	case State.WaveDown:
		//		eState = State.WaveDown;
		//		break;

		//	case State.WaveOnlyUp:
		//		eState = State.WaveOnlyUp;
		//		break;

		//	case State.WaveOnlyDown:
		//		eState = State.WaveOnlyDown;
		//		break;

		//	case State.Straight:
		//		eState = State.Straight;
		//		break;
		//}
	}
	void Enemy_Reset()
	{
        startTime = 0;
		speedZ = 0;
		speedY = 0;
		once = true;
		isSlerp = false;
		isWave = false;
	}

	private void OnTriggerExit(Collider col)
	{
        if (eState == State.Rush || eState == State.BackRush)
        {
            if (col.gameObject.name == "BattleshipType_Enemy(Clone)")
            {
                hp = 0;
            }
        }

		if(!isBehind)
		{
            if (eState == State.Rush)
            {
                if (col.gameObject.name == "WallLeft" || col.gameObject.name == "WallTop" || col.gameObject.name == "WallUnder")
                {
                    groupManage.notDefeatedEnemyCnt++;
                    groupManage.remainingEnemiesCnt -= 1;
                    gameObject.SetActive(false);

                }
            }
            else if (col.gameObject.name == "WallLeft")
			{
				groupManage.notDefeatedEnemyCnt++;
				groupManage.remainingEnemiesCnt -= 1;
				gameObject.SetActive(false);
			}
		}
		else if(isBehind)
		{
            if(eState==State.BackRush)
            {
                if(col.gameObject.name=="WallRight"|| col.gameObject.name == "WallTop"|| col.gameObject.name == "WallUnder")
                {
                    groupManage.notDefeatedEnemyCnt++;
                    groupManage.remainingEnemiesCnt -= 1;
                    gameObject.SetActive(false);

                }
            }
			else if (col.gameObject.name == "WallRight")
			{
				groupManage.notDefeatedEnemyCnt++;
				groupManage.remainingEnemiesCnt -= 1;
				gameObject.SetActive(false);
			}
		}
	}
}