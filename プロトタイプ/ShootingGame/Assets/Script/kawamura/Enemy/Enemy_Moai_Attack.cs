using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class Enemy_Moai_Attack : MonoBehaviour
{
	public GameObject parentObj;
	public GameObject saveRingBullet;	//出したリング弾を入れる
	public GameObject saveObj;			//オブジェクト一時保存
	public GameObject miniMoaiPos;		//ミニモアイを出す位置オブジェクト
	public GameObject[] laserPos;		//レーザーを出す位置オブジェクト配列

	public string myName;
	[Header("バーストとバーストの間隔を計る")]
	public float Shot_Delay;					//バーストとバーストの間隔時間を計る
	[Header("バーストとバーストの間隔")]
	public float Shot_Delay_Max;				//１つのバーストの間隔
	public float burst_delay;					//バーストの1発1発の間隔時間を計る
	[Header("バースト内の弾の間隔")]
	public float burst_Delay_Max;				//バーストの1発1発の間隔
	[Header("バーストで撃つ数")]
	public int burst_ShotNum;					//撃つバースト数
	[Header("バーストを撃つ回数")]
	public int burst_Times;
	public int burst_Num;						//バーストを撃った回数
	public int burst_Shot_Cnt;					//何発撃ったかのカウント
	public bool isBurst = false;				//バーストを撃つかどうか
	public bool once;							//一回だけ使う処理用

	public Enemy_Moai moai_Script;						//モアイ本体のスクリプト
	public Moai_EyeLaserRotation[] eyeLaser_Script;		//目のレーザースクリプト

	public float laserTimeCnt;
	public float laserTimeMax;

	[Header("入力用　発射する弾の角度範囲設定")]
	public float bulletRota_Value;					//発射する弾の角度範囲用
	public float ringShot_DelayCnt;					//弾発射のディレイカウント
	public int ringStateNum = 1;					// 1だと拡散2だと自機狙いバースト
	[Header("入力用　弾発射の間隔(秒)")]
	public float ringShot_DelayMax;					//弾発射のディレイマックス
	[Header("入力用　弾発射の間隔(秒)")]
	public float ringShotBurstBullet_DelayMax;		//バースト内弾のディレイマックス
	public float burst_DelayCnt;
	[Header("入力用　バースト同士の間隔(秒)")]
	public float burst_DelayMax;
	public float burstBulletCnt;					//バーストの弾の打った数（未実装）
	[Header("入力用　バースト内の発射数")]
	public float burstBulletMax;

	//レーザー---------------------------------------
	[SerializeField, Tooltip("エネルギーため用のパーティクル用")] private Boss_One_A111[] supply;
	[SerializeField, Tooltip("レーザーの発射位置")] private GameObject[] laser_muzzle;

	public int Attack_Step;
	private bool Is_Attack_Now
	{
		get; set;
	}			// 現在攻撃しているか

	public bool isLaserEnd = false;
	public bool LaserEndOnce = true;

	//レーザー音追加
	[SerializeField] private AudioClip laserBegin;
	[SerializeField] private AudioClip laserContinuing;
	[SerializeField] private AudioClip laserEnd;
	private AudioSource audioSource;

	//-----------------------------------------------

	Find_Angle find_Angle_Script;		//プレイヤーのほうを向くスクリプト

	void Start()
	{
		//親とかスクリプトセット
		parentObj = transform.parent.gameObject;
		moai_Script = parentObj.GetComponent<Enemy_Moai>();
		find_Angle_Script = gameObject.GetComponent<Find_Angle>();
		//miniMoaiPos = transform.GetChild(0).gameObject;
		//-------------追加--------------
		audioSource = GetComponent<AudioSource>();
		audioSource.clip = laserEnd;
		audioSource.playOnAwake = false;
		//-------------------------------

		for (int i = 0; i < eyeLaser_Script.Length; i++)
		{
			eyeLaser_Script[i] = laserPos[i + 1].GetComponent<Moai_EyeLaserRotation>();

		}
	}


	void Update()
	{
		//無線の時は処理しない
        if (Game_Master.Management_In_Stage == Game_Master.CONFIGURATION_IN_STAGE.WIRELESS)
        {
            return;
        }
		//モアイが死んでたら音を止める
		if (moai_Script.isDead)
		{
			audioSource.Stop();
		}

		if (moai_Script.isMouthOpen && moai_Script.attackLoopCnt < 3 && !moai_Script.isDead)
		{
			switch (moai_Script.attackState)
			{
				case Enemy_Moai.AttackState.RingShot:
					if (ringStateNum == 1)
					{
						ringShot_DelayCnt += Time.deltaTime;
						if (ringShot_DelayCnt > ringShot_DelayMax)
						{
							RingShot();
						}
					}
					else if (ringStateNum == 2)
					{
						burst_DelayCnt += Time.deltaTime;
						if (burst_DelayCnt > burst_DelayMax)
						{
							ringShot_DelayCnt += Time.deltaTime;
							if (ringShot_DelayCnt > ringShotBurstBullet_DelayMax)
							{
								RingShotBurst();
								if (burstBulletCnt > burstBulletMax)
								{

								}
							}
						}
					}
					break;

				case Enemy_Moai.AttackState.MiniMoai:
					MiniMoaiCreate();
					moai_Script.miniMoaisCnt++;
					moai_Script.isMiniMoai = true;
					break;

				case Enemy_Moai.AttackState.Laser:
					Laser_Attack();
					break;
			}
		}
		void RingShot()
		{
			for (int i = 0; i < 5; i++)
			{
                //Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.)
                saveRingBullet = Obj_Storage.Storage_Data.Moai_Bullet.Active_Obj();
                saveRingBullet.transform.position = transform.position;
				saveRingBullet.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-(180f - find_Angle_Script.degree - bulletRota_Value), -(180f - find_Angle_Script.degree + bulletRota_Value)));
			}
			moai_Script.ringShotCnt++;
			ringShot_DelayCnt = 0;
		}

		void RingShotBurst()
		{
			//saveRingBullet.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-(180f - find_Angle_Script.degree - bulletRota_Value), -(180f - find_Angle_Script.degree + bulletRota_Value)));
			//burstBulletCnt++;
		}
		void MiniMoaiCreate()
		{
            GameObject save = Obj_Storage.Storage_Data.Moai_Mini_Group.Active_Obj();

            save.transform.position = miniMoaiPos.transform.position;
		}

		void Laser_Attack()
		{
			// レーザーチャージ
			if (Attack_Step == 0)
			{
				// 攻撃開始
				Is_Attack_Now = true;

				// チャージ音
				if (audioSource.clip == laserEnd)
				{
					audioSource.clip = laserBegin;
					audioSource.Stop();
					audioSource.loop = true;
					audioSource.Play();
				}

				// チャージエフェクト再生
				if (!supply[0].gameObject.activeSelf && !supply[1].gameObject.activeSelf && !supply[2].gameObject.activeSelf)
				{
					supply[0].gameObject.SetActive(true);
					supply[1].gameObject.SetActive(true);
					supply[2].gameObject.SetActive(true);

					supply[0].SetUp();
					supply[1].SetUp();
					supply[2].SetUp();

				}
				// チャージエフェクト終了
				if (supply[0].Completion_Confirmation() && supply[1].Completion_Confirmation() && supply[2].Completion_Confirmation())
				{
					supply[0].gameObject.SetActive(false);
					supply[1].gameObject.SetActive(false);
					supply[2].gameObject.SetActive(false);

					Attack_Step++;
				}
			}
			else if (Attack_Step == 1)
			{
				//--------------追加-----------------
				if (audioSource.clip == laserBegin)
				{
					audioSource.clip = laserContinuing;
					audioSource.Stop();
					audioSource.loop = true;
					audioSource.Play();
				}
				//-----------------------------------

				//isLaserEnd = false;

				if (laserTimeCnt < 8)
				{
					LaserCreate();
					EyeLaserCreate();
					if (laserTimeCnt > 4.5f)
					{
						if (LaserEndOnce)
						{
							audioSource.clip = laserEnd;
							audioSource.Stop();
							audioSource.loop = false;
							audioSource.Play();
							LaserEndOnce = false;
						}

					}
				}
				else
				{
					isLaserEnd = true;
				}

				//if (isLaserEnd)
				//{
				//	if (LaserEndOnce)
				//	{
				//		audioSource.clip = laserEnd;
				//		audioSource.Stop();
				//		audioSource.loop = false;
				//		audioSource.Play();
				//		LaserEndOnce = false;
				//	}
				//}

				laserTimeCnt += Time.deltaTime;
				if (laserTimeCnt > 10)
				{
					Attack_Step = 0;
					Is_Attack_Now = false;
					isLaserEnd = false;
					LaserEndOnce = true;
					//moai_Script.isMouthOpen = false;
					moai_Script.isLaserEmd = true;
					eyeLaser_Script[0].rotaZ = 17;
					eyeLaser_Script[1].rotaZ = 17;
					eyeLaser_Script[0].isRoll = false;
					eyeLaser_Script[1].isRoll = false;
					laserPos[1].transform.localRotation = Quaternion.Euler(0, 90, 17);
					laserPos[2].transform.localRotation = Quaternion.Euler(0, 90, 17);

					laserTimeCnt = 0;
				}
				else if (laserTimeCnt > 2)
				{
					eyeLaser_Script[0].isRoll = true;
					eyeLaser_Script[1].isRoll = true;
				}
			}
		}

		void LaserCreate()
		{
			//foreach (GameObject obj in laser_muzzle)
			//{
			//    if (obj.activeSelf)
			//    {
			//        GameObject save = Instantiate(moaiLaserObj, obj.transform.position, transform.rotation);
			//        Moai_Laser laser = save.GetComponent<Moai_Laser>();
			//        laser.Manual_Start(obj.transform);

			//        //Moai_Laser laser = Instantiate(moaiLaserObj, obj.transform.position, transform.rotation).GetComponent<Moai_Laser>();
			//        //Boss_One_Laser laser = Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eONE_BOSS_LASER, obj.transform.position, transform.right).GetComponent<Boss_One_Laser>();
			//        //laser.Manual_Start(obj.transform);
			//    }
			//}
			//for (int i = 0; i < laserPos.Length; i++)
			//{

            //口のレーザー出す
			//saveObj.transform.position = laserPos[0].transform.position;
			//saveObj.transform.parent = laserPos[0].transform;
			//saveObj.transform.localRotation = Quaternion.Euler(0, 0, 0);
			//saveObj = null;

            saveObj = Obj_Storage.Storage_Data.Moai_Mouth_Laser.Active_Obj();
            saveObj.transform.position = laserPos[0].transform.position;
            saveObj.transform.parent = laserPos[0].transform;
            saveObj.transform.localRotation = Quaternion.Euler(0, 0, 0);
            saveObj = null;

            //}
            //GameObject save = Instantiate(moaiLaserObj, transform.position, transform.rotation);
            //save.transform.rotation = Quaternion.Euler(0, 180, transform.rotation.z);
            //Moai_Laser laser = save.GetComponent<Moai_Laser>();
            //laser.Manual_Start(transform);

        }
        void EyeLaserCreate()
		{
			for (int i = 0; i < 2; i++)
			{
                saveObj = Obj_Storage.Storage_Data.Moai_Eye_Laser.Active_Obj();
				saveObj.transform.position = laserPos[i + 1].transform.position;
				saveObj.transform.parent = laserPos[i + 1].transform;
				//saveObj.transform.parent = parentObj.transform;
				saveObj.transform.localRotation = Quaternion.Euler(0, 0, 0);

				//saveObj.transform.localRotation = Quaternion.Euler(0, 90, 0);
				//saveObj.transform.localRotation = Quaternion.Euler(0, 180, 0);
				saveObj = null;
			}
		}
	}
}
