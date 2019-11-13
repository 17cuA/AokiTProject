//作成者：川村良太
//モアイ

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Moai : character_status
{
	//モアイの攻撃状態
	public enum AttackState
	{
		RingShot,	//リング弾攻撃
		MiniMoai,	//ミニモアイ
		Laser,		//レーザー
		Stay,		//停止
	}

	public AttackState attackState;		//状態変数

	GameObject faceObj;					//顔のオブジェクト
	GameObject mouthObj;				//口のオブジェクト

	public Rigidbody moai_rigidbody;	//rigidbody 死亡時の落下に使う

	MoaiAnimation moaiAnime_Script;		//口の開け閉めスクリプト

	Vector3 velocity;
	public Vector4 moaiColor;			//モアイの色

	public Renderer[] moai_material;	// オブジェクトのマテリアル情報

	public int MoaiHpMax;				//最大HP
	public int saveHP;					//退場時HPを減らなくさせるため

	public float speedY;				//Yスピード
	[Header("入力用 Y速度値")]
	public float speedY_Value;
	public float rotaY;					//Y角度
	[Header("入力用 回転変化値")]
	public float rotaY_Value;
	public float defaultRotaY_Value;	//初期の回転変化値
	[Header("入力用 回転最大値")]		
	public float rotationYMax;			//(残りどれだけ回転するか)

	public float rotaX;					//X角度
	public float rotaX_Value;			//X回転変化値
	public float rotaZ;					//Z角度
	public float rotaZ_Value;			//Z回転変化値

	public int attackLoopCnt;			//攻撃のループカウント（3種類の攻撃が一周したら+1）
	public float aliveCnt;

	public float color_Value;		//残りHPで変わる色の値
	public float bulletRota_Value;	//発射する弾の角度範囲用

	//攻撃関係の管理で使うよ！--------------------------------------------
	public int ringShotCnt;			//リング攻撃した数
	[Header("入力用 リング攻撃回数")]
	public int ringShotMax;			//リング攻撃回数
	public float ringShotDelay;

	public int miniMoaisCnt;
	[Header("入力用 ミニモアイを出す回数")]
	public int miniMoaisMax;
	public float attackDelay;
	//攻撃関係の管理で使うやつの終わりだよ！-----------------------------

	public ParticleSystem explosionEffect;

	public bool isAppearance = true;		//最初の登場用
	public bool isExit = false;				//退場用
	public bool isMouthOpen = false;		//口が開いているかどうか
	public bool isMiniMoai = false;
	public bool isLaserEmd = false;
	public bool isDead = false;             //Is_Deadはcharacter_statusのやつ
	new void Start()
	{
		//オブジェクトとスクリプトセット
		faceObj = transform.GetChild(0).gameObject;
		mouthObj = transform.GetChild(1).gameObject;
		moaiAnime_Script = mouthObj.GetComponent<MoaiAnimation>();

		//Yスピードセット
		speedY = speedY_Value;
		//回転初期値保存
		defaultRotaY_Value = rotaY_Value;
		//Y角度代入
		rotaY = 90;
		//最大HP保存
		MoaiHpMax = hp;
		isDead = false;

		HP_Setting();
		base.Start();
	}


	new void Update()
	{
		//重力設定（死ぬとオンになって落ちていく）
		Physics.gravity = new Vector3(0, -0.32f, 0);

		//
		if (!isAppearance && !isExit && Game_Master.Management_In_Stage == Game_Master.CONFIGURATION_IN_STAGE.WIRELESS)
		{
			hp = MoaiHpMax;
			for (int i = 0; i < object_material.Length; i++)
			{
				object_material[i].material = self_material[i];
			}

			return;
		}

		//登場
		if (isAppearance)
		{
			//登場中ダメージを受けないようにする
			hp = MoaiHpMax;
			//移動
			velocity = gameObject.transform.rotation * new Vector3(0, speedY, 0);
			gameObject.transform.position += velocity * Time.deltaTime;

			//Y座標が0より大きくなったら0に直す
			if (transform.position.y > 0)
			{
				transform.position = new Vector3(transform.position.x, 0, transform.position.z);
			}


			if (transform.position.y > -6.5f)
			{
				//モアイの向きを回転
				rotaY += rotaY_Value;
				//残りの回転値を回転した分減らす
				rotationYMax -= rotaY_Value;
				//Y角度を270超えないように
				if (rotaY > 270f)
				{
					rotaY = 270f;
				}


				if (rotationYMax < 180f)
				{
					//回転させる値を減らしていって止まるのを滑らかに
					rotaY_Value = defaultRotaY_Value * (rotationYMax / 180f) + 0.35f;

				}
				if (transform.position.y > -6.5f)
				{
					//Yスピードを減らしていって止まるのを滑らかに
					speedY = speedY_Value * (transform.position.y / -6.5f) + 0.5f;
				}
			}

			//rotation代入
			transform.rotation = Quaternion.Euler(0, rotaY, 0);


			if (transform.position.y >= 0 && rotaY >= 270)
			{
				//登場判定オフ，口開ける判定オン
				isAppearance = false;
				moaiAnime_Script.isOpen = true;
			}

			//ダメージ入った時のマテリアルを戻す
			for (int i = 0; i < object_material.Length; i++)
			{
				object_material[i].material = self_material[i];
			}
			//以降の処理を飛ばす
			return;
		}
		//死亡
		else if (isDead)
		{
			//死亡時に傾けるための値を入れる
			rotaX_Value = 0.1f;
			rotaX -= rotaX_Value;
			rotaY_Value = 0.1f;
			rotaY -= 0.1f;
			rotaZ_Value = 0.5f;
			rotaZ -= rotaZ_Value;

			//角度変更
			transform.rotation = Quaternion.Euler(rotaX, rotaY, 0);

			if (transform.position.y < -9.5f)
			{
				Is_Dead = true;
			}
			//下まで行ったら消す
			if (transform.position.y < -10f)
			{
				gameObject.SetActive(false);
			}
		}
		//退場
		else if (isExit)
		{
			//上に退場していくときのスピードと回転値を入れる
			speedY = 3;
			rotaY_Value = -1f;
			rotaY += rotaY_Value;
			//移動
			velocity = gameObject.transform.rotation * new Vector3(0, speedY, 0);
			gameObject.transform.position += velocity * Time.deltaTime;
			//角度変える
			transform.rotation = Quaternion.Euler(0, rotaY, 0);

			//上まで行ったら消す
			if (transform.position.y > 12)
			{
				Is_Dead = true;
				gameObject.SetActive(false);
			}
			//退場中ダメージ受けないようにする
			hp = saveHP;
			material_Reset();
			HpColorChange();
			return;
		}
		//攻撃とかをしているとき
		else
		{
			//生きている時間をカウント
			aliveCnt += Time.deltaTime;
		}

		//口が開いているとき
		if (isMouthOpen)
		{
			//攻撃の状態を見る
			switch (attackState)
			{
				//リング弾攻撃
				case AttackState.RingShot:
					//指定の回数攻撃したら次の攻撃へ移る
					if (ringShotCnt > ringShotMax)
					{
						//口が開いているかどうかをオフ
						isMouthOpen = false;
						//口スクリプトの口を閉じる判定オン
						moaiAnime_Script.isClose = true;
						//攻撃状態変更
						attackState = AttackState.MiniMoai;
						//リング弾の攻撃回数リセット
						ringShotCnt = 0;
					}
					break;

				//ミニモアイ
				case AttackState.MiniMoai:
					//ミニモアイを出したら
					if (isMiniMoai)
					{
						//一度口を閉じる
						isMouthOpen = false;
						moaiAnime_Script.isClose = true;
					}
					//指定の回数ミニモアイをだしたら
					if (miniMoaisCnt > miniMoaisMax)
					{
						//口閉じる
						isMouthOpen = false;
						moaiAnime_Script.isClose = true;
						//攻撃状態をレーザーに
						attackState = AttackState.Laser;
						//ミニモアイ出した回数リセット
						miniMoaisCnt = 0;
					}
					break;

				//レーザー
				case AttackState.Laser:
					//レーザーが終わったら
					if (isLaserEmd)
					{
						//口閉じる
						isMouthOpen = false;
						moaiAnime_Script.isClose = true;
						//攻撃をリング弾に
						attackState = AttackState.RingShot;
						isLaserEmd = false;
						//攻撃のループカウントプラス
						attackLoopCnt++;
					}
					break;
			}
		}

		//死ぬデバッグキー　OとH同時押し
		if (Input.GetKey(KeyCode.O) && Input.GetKey(KeyCode.H))
		{
			hp = 0;
		}

		//if (attackLoopCnt == 1)
		//{
		//	isExit = true;
		//	saveHP = hp;
		//}
		//if (aliveCnt > 116)
		//{
		//	isExit = true;
		//	saveHP = hp;
		//}

		//HPがなくなって死亡判定がないとき
		if (hp < 1 && !isDead)
		{
			//死亡判定入れる
			isDead = true;
			//重力をオンに
			moai_rigidbody.useGravity = true;
			//口関係の処理を止める
			moaiAnime_Script.isOpen = false;
			moaiAnime_Script.isClose = false;

			//レイヤー変更
			faceObj.layer = LayerMask.NameToLayer("Explosion");
			mouthObj.layer = LayerMask.NameToLayer("Explosion");
			//死亡関数
			MoaiDead();
		}

		//死んでいなければ
		if (!isDead)
		{
			base.Update();
		}
		//色変化関数
		HpColorChange();
	}

	//-------------ここから関数-------------

	//のこりHPの割合で色を変える関数
	void HpColorChange()
	{
		//色の値計算
		color_Value = (float)hp / MoaiHpMax;
		//色変更
		for (int i = 0; i < moai_material.Length; i++)
		{
			moaiColor = new Vector4(1, color_Value, color_Value, 1);
			moai_material[i].material.SetVector("_BaseColor", moaiColor);
		}
	}
	void MoaiDead()
	{
		//スコア加算、SE、爆発エフェクト
		Game_Master.MY.Score_Addition(score, Opponent);
		SE_Manager.SE_Obj.SE_Explosion(Obj_Storage.Storage_Data.audio_se[11]);

		explosionEffect.gameObject.SetActive(true);

	}
}

