//作成日2019/07/08
// 戦艦型エネミーの挙動
// 作成者:諸岡勇樹
/*
 * 2019/07/08　挟み込み移動と弾を撃つ行動
 * 2019/07/08　まっすぐ移動
 * 2019/07/09　バレットの打ち出しタイミングの変更(ベリーイージー → ノーマル)
 * 2019/07/15　奥行対応
 * 2019/07/25　加減速適応
 * 2019/07/30　弾の打ち方変更
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class BattleshipType_Enemy : character_status
{
	[SerializeField, Tooltip("初期位置")] private Vector3 initial_position;
	[SerializeField, Tooltip("移動変更ポイント")] private Vector3[] moving_change_point;
	[SerializeField, Tooltip("はさみ込むタイプはTrue")] public bool is_sandwich;
	[SerializeField, Tooltip("マズルパーツスクリプト")] private BattleshipType_Battery[] muzzle_parts_scriptes;
	[SerializeField, Tooltip("マズルパーツオブジェクト")] private GameObject[] muzzle_parts;
	[SerializeField, Tooltip("本体パーツ")] private BattleshipType_Battery body_scriptes;
	[SerializeField, Tooltip("本体")] private GameObject body;
	[SerializeField, Tooltip("パーツのスコア")] private uint parts_score;
	public GameObject DestructiveEffect;                    // 破壊時エフェクトの保存


	public Vector3 defautpos;													// デフォルトの位置

	public Vector3 Original_Position { get; set; }						// 元の位置
	public int Now_Target { get; set; }										// 現在の移動目標番号
	public Vector3 Move_Facing { get; set; }								// Y軸の移動向き
	public Vector3 Moving_Facing { get; set; }							// 移動向き
	public int Muzzle_Select { get; set; }									// マズル指定
	public List<GameObject> Bullet_Object { get; set; }				// 自身が発射した弾の情報の保存
	public List<MeshRenderer> Parts_Renderer { get; set; }		// パーツたちのレンダー
	public float velocity;															// X軸の移動量保存
	public List<bool> Is_Muzzle_Active { get; set; }					// 大砲が破壊されているかどうか
	public int Attack_Type_Num { get; set; }								// 攻撃種類指定
	public float Initial_Speed { get; set; }									// 初速(最低速度)
	public float Max_Speed { get; set; }									// 最大速度
	public float Deceleration_Distance { get; set; }						// 加減速開始移動量
    public bool Is_up{ get; set; }												// 上側下側判定
    public bool Is_InitialSetting { get; set; }                             // 初期設定したかどうか

	private new void Start()
	{
        Is_InitialSetting = false;
		base.Start();
		 
		Now_Target = 0;
		// 初期位置と目標位置のX軸の中間
		Moving_Facing = new Vector3(-1.0f, 0.0f,0.0f);
		Muzzle_Select = 0;
		Shot_Delay = Shot_DelayMax / 3;
		Bullet_Object = new List<GameObject>();
		Parts_Renderer = new List<MeshRenderer>();

		for (int i = 0; i < muzzle_parts_scriptes.Length; i++)
		{
			Parts_Renderer.Add(muzzle_parts_scriptes[i].GetComponent<MeshRenderer>());
		}

		if (is_sandwich)
		{
			if (!Is_up)
			{
				initial_position.y *= -1.0f;
				for (int i = 0; i < moving_change_point.Length; i++)
				{
					moving_change_point[i].y *= -1.0f;
				}
			}
			Original_Position = transform.position = initial_position;
		}

        defautpos = initial_position;
        // 加減速用初期化群
        speed = 0.05f;
        Deceleration_Distance = 0;
        Max_Speed = speed;
		speed = Initial_Speed = speed / 60.0f;
		for (int i = 0; i < 60; i++)
		{
			Deceleration_Distance += speed;
			speed += Initial_Speed;
		}

		Is_Muzzle_Active = new List<bool>();
		for (int i = 0; i < muzzle_parts_scriptes.Length; i++)
		{
			Is_Muzzle_Active.Add(muzzle_parts_scriptes[i].gameObject.activeSelf);
		}
	}

	private new void Update()
	{
		// ポーズでないとき
		if (!PauseManager.IsPause)
		{
			// 初期設定が終わっていないとき
            if(Is_InitialSetting)
            {
				// 挟み込むタイプのとき
                if(is_sandwich)
                {
					// 下側のとき
                    if (!Is_up)
                    {
                        initial_position.y *= -1.0f;
                        for (int i = 0; i < moving_change_point.Length; i++)
                        {
                            moving_change_point[i].y *= -1.0f;
                        }
                    }
                    Original_Position = transform.position = initial_position;

					// 上側のとき
                    if (Is_up)
                    {
                        defautpos = new Vector3(25, 5, 0);
                        transform.position = defautpos;
                    }
                    else
                    {
                        defautpos = new Vector3(25, -5, 0);
                        transform.position = defautpos;
                    }
                }
                Is_InitialSetting = false;
            }
			// 大砲パーツ位置ずれ修正
            for (int i = 0; i < muzzle_parts_scriptes.Length; i++)
			{
				if (muzzle_parts_scriptes[i].transform.localPosition != muzzle_parts_scriptes[i].Getinitishal_pos())
					muzzle_parts_scriptes[i].transform.localPosition = muzzle_parts_scriptes[i].Getinitishal_pos();
			}
			// 本体の位置ずれ修正
			if (body_scriptes.transform.localPosition != body_scriptes.Getinitishal_pos())
				body_scriptes.transform.localPosition = body_scriptes.Getinitishal_pos();

			base.Update();

			HSV_Change();


			// 最終ポイントについたとき
			if (transform.position == moving_change_point[moving_change_point.Length - 1])
			{
				gameObject.SetActive(false);
			}

			Vector3 temp = Vector3.zero;
			//// 移動したい向きに移動
			//Vector3 velocity = Moving_Facing.normalized * speed;
			//transform.position = transform.position + velocity;
			// 挟み込み型の挙動
			if (is_sandwich)
			{
				// 移動先に近づいたとき
				// ターゲット番号が要素数を超えていないとき
				if (Vector_Size(transform.position, moving_change_point[Now_Target]) <= speed && Now_Target < moving_change_point.Length - 1)
				{
					// 位置を保存
					Original_Position = transform.position = moving_change_point[Now_Target];
					Now_Target++;
					speed = Initial_Speed;
				}
				else
				{
					//　加減速
					if (Vector_Size(transform.position, Original_Position) < Deceleration_Distance)
					{
						if (speed < Max_Speed) speed += Initial_Speed;
					}
					else if (Vector_Size(transform.position, moving_change_point[Now_Target]) < Deceleration_Distance)
					{
						if (speed > Initial_Speed) speed -= Initial_Speed;
					}
					temp = Moving_To_Target(transform.position, moving_change_point[Now_Target], speed);
					velocity = transform.position.x - temp.x;
					transform.position = temp;
				}
			}
			// 直進タイプのとき
			else if (!is_sandwich)
			{
				temp = transform.position + transform.forward * speed;
				velocity = transform.position.x - temp.x;
				transform.position = temp;
			}


			// 自身のZ軸が0のとき攻撃する
			if (transform.position.z == 0.0f)
			{
				Shot_Delay++;
				if (Shot_Delay > Shot_DelayMax)
				{
					if (Attack_Type_Num == 0)
					{
						Wave_Attack();
					}
					else if (Attack_Type_Num == 1)
					{
						Zigzag_Attack();
					}
				}
			}

			// 保管したバレットの確認
			for (int i = 0; i < Bullet_Object.Count; i++)
			{
				// バレットが起動しているとき
				if (Bullet_Object[i].activeSelf)
				{

					// 機体のX軸移動距離と同じ距離をバレットにも移動させる
					Vector3 pos = Bullet_Object[i].transform.position;
					pos.x -= velocity;
					Bullet_Object[i].transform.position = pos;
				}
				// 起動していないとき
				else
				{
					// バレット情報のリリース
					Bullet_Object.RemoveAt(i);
				}
			}

			// 本体がHP０以下のとき
			if (hp <= 0)
			{
				// 大砲の破壊
				for (int i = 0; i < muzzle_parts_scriptes.Length; i++)
				{
					if (muzzle_parts_scriptes[i].gameObject.activeSelf)
					{
						muzzle_parts_scriptes[i].Died_Process();
					}
				}
				// 本体の破壊
				if (body_scriptes.gameObject.activeSelf)
				{
					body_scriptes.Died_Process();
				}
				// バレット情報のリリース
				Bullet_Object.Reverse();

				// エフェクトの生成
				Instantiate(DestructiveEffect, transform.position, Quaternion.identity);

                Is_InitialSetting = true;
				Died_Process();
			}

			CannonDestructionConfirmation();
		}
	}
	void OnEnable()
	{
        if (Is_up)
        {
            defautpos = new Vector3(25, 5, 0);
        }
        else
        {
            defautpos = new Vector3(25, -5, 0);

        }
        speed = 0.05f;
        Deceleration_Distance = 0;
        Max_Speed = speed;
        speed = Initial_Speed = speed / 60.0f;
        for (int i = 0; i < 60; i++)
        {
            Deceleration_Distance += speed;
            speed += Initial_Speed;
        }

		Now_Target = 0;
		Shot_Delay = 0;

		if (muzzle_parts_scriptes != null)
		{
			// 子供が不能のとき、再起動
			for (int i = 0; i < muzzle_parts_scriptes.Length; i++)
			{
				if (!muzzle_parts_scriptes[i].gameObject.activeSelf)
				{
					muzzle_parts[i].SetActive(true);
					muzzle_parts_scriptes[i].ReBoot();
					Is_Muzzle_Active[i] = muzzle_parts_scriptes[i].gameObject.activeSelf;
				}
			}
		}

		if (!body_scriptes.gameObject.activeSelf)
		{
			body.SetActive(true);
			body_scriptes.ReBoot();
		}
	}

	/// <summary>
	/// ベクトルの長さを出す
	/// </summary>
	/// <param name="a"> 開始座標 </param>
	/// <param name="b"> 目標座標 </param>
	/// <returns></returns>
	private float Vector_Size( Vector3 a, Vector3 b )
	{
		float xx = a.x - b.x;
		float yy = a.y - b.y;
		float zz = a.z - b.z;

		return Mathf.Sqrt(xx * xx + yy * yy + zz * zz);
	}

	/// <summary>
	/// 移動モードの設定(0：上側の移動、1：中心直進、2：下側の移動)
	/// </summary>
	/// <param name="num"></param>
	public void Moving_Mode_Preference(int num)
	{
		switch(num)
		{
			case 0:
				is_sandwich = false;
				break;
			case 1:
				is_sandwich = true;
				break;
			case 2:
				is_sandwich = false;
				for(int i =0;i< moving_change_point.Length;i++)
				{
					moving_change_point[i].y *= -1.0f;
				}
				break;
			default:
				break;
		}
	}

	/// <summary>
	/// ターゲットに移動
	/// </summary>
	/// <param name="origin"> 元の位置 </param>
	/// <param name="target"> ターゲットの位置 </param>
	/// <param name="speed"> 1フレームごとの移動速度 </param>
	/// <returns> 移動後のポジション </returns>
	private Vector3 Moving_To_Target(Vector3 origin,Vector3 target, float speed)
	{
		Vector3 direction = Vector3.zero;		// 移動する前のターゲットとの向き
		Vector3 return_pos	 = Vector3.zero;				// 返すポジション
		
		direction = target - origin;
		return_pos = origin + (direction.normalized * speed);

		if (Vector_Size(return_pos, target) <= speed)
		{
			return_pos = target;
		}

		return return_pos;
	}

	/// <summary>
	/// 大砲の破壊確認
	/// </summary>
	private void CannonDestructionConfirmation()
	{
		for (int i = 0; i < Is_Muzzle_Active.Count; i++)
		{
			// 破壊の前フレームで破壊されておらず、現フレームで破壊されたとき
			if (Is_Muzzle_Active[i] && !muzzle_parts_scriptes[i].gameObject.activeSelf)
			{
				// 部位破壊スコアの加算
				Game_Master.MY.Score_Addition(parts_score, muzzle_parts_scriptes[i].Opponent);
				// 破壊情報の保管
				Is_Muzzle_Active[i] = muzzle_parts_scriptes[i].gameObject.activeSelf;
			}
		}
	}


	/// <summary>
	/// ジグザグに攻撃
	/// </summary>
	private void Zigzag_Attack()
	{
		if (muzzle_parts_scriptes[Muzzle_Select + 0].gameObject.activeSelf) Bullet_Object.Add(muzzle_parts_scriptes[Muzzle_Select + 0].Attack_Instruction_Receiving());
		if (muzzle_parts_scriptes[Muzzle_Select + 2].gameObject.activeSelf) Bullet_Object.Add(muzzle_parts_scriptes[Muzzle_Select + 2].Attack_Instruction_Receiving());
		if (muzzle_parts_scriptes[Muzzle_Select + 4].gameObject.activeSelf) Bullet_Object.Add(muzzle_parts_scriptes[Muzzle_Select + 4].Attack_Instruction_Receiving());
		if (muzzle_parts_scriptes[Muzzle_Select + 6].gameObject.activeSelf) Bullet_Object.Add(muzzle_parts_scriptes[Muzzle_Select + 6].Attack_Instruction_Receiving());

		Muzzle_Select++;
		if (Muzzle_Select == 2)
		{
			Muzzle_Select = 0;
		}

		Shot_Delay = 0;
	}

	/// <summary>
	/// ウェーブで攻撃
	/// </summary>
	private void Wave_Attack()
	{
		if (muzzle_parts_scriptes[Muzzle_Select + 0].gameObject.activeSelf) Bullet_Object.Add(muzzle_parts_scriptes[Muzzle_Select + 0].Attack_Instruction_Receiving());
		if (muzzle_parts_scriptes[Muzzle_Select + 4].gameObject.activeSelf) Bullet_Object.Add(muzzle_parts_scriptes[Muzzle_Select + 4].Attack_Instruction_Receiving());
		Muzzle_Select++;
		if(Muzzle_Select == 4)
		{
			Muzzle_Select = 0;
			Shot_Delay -= Shot_DelayMax * 2;
		}
		else
		{
			Shot_Delay = 0;
		}
	}
}

