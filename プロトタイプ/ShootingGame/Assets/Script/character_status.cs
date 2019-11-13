/*
/*
 * 2019/05/27   Rigidbodyの削除
 */
using UnityEngine;
using Power;
using UnityEngine.SceneManagement;

public class character_status : MonoBehaviour
{
	[Header("基本ステータスがはいってるものを入れる")]
	public ParameterTable Parameter;                                    //ScriptableObjectを入れる

	public float speed;                                                 // スピード
	public int hp;                                                      // 体力
	private int hp_Max;											//リスポーン時に体力を設定するよう変数
	public Vector3 direction;                                           // 向き
    public Vector4 setColor;
	public Collider capsuleCollider;									// collider
	private Rigidbody rigidbody;                                        //rigitbody
	public int Shot_DelayMax;                                           // 弾を打つ時の間隔（最大値::unity側にて設定）
	public int Shot_Delay;                                              // 弾を撃つ時の間隔
	public uint score;                                                  // 保持しているスコア
	private int shield;                                                 //シールド（主にプレイヤーのみ使うと思う）
	public bool activeShield;                                           //現在シールドが発動しているかどうかの判定用（初期値false）
	public int Remaining;                                               //残機（あらかじめ設定）
	public float v_Value;                                               //テクスチャの明るさの増える値
    public Vector4[] defaultColor;
	public int childCnt;
	public Renderer[] object_material;                                  // オブジェクトのマテリアル情報
	public bool isrend = false;
	public bool Is_Dead = false;															
	public Material[] self_material;									//初期マテリアル保存用

	[Header("ダメージ用material設定")]
	public Material white_material;										//ダメージくらったときに一瞬のホワイト

	private int framecnt;
	private bool check;

	public int Opponent;
	public string namenamename;
	public void Start()
	{
		if(Parameter != null)
		{
			hp = Parameter.Get_Life;
			speed = Parameter.Get_Speed;
			score = Parameter.Get_Score;
			shield = Parameter.Get_Shield;
			Remaining = Parameter.Get_Reaming;
		}

		//rigidbodyがアタッチされているかどうかを見てされていなかったらアタッチする（Gravityも切る）
		if (!gameObject.GetComponent<Rigidbody>())
		{
			rigidbody = gameObject.AddComponent<Rigidbody>() as Rigidbody;
			rigidbody.useGravity = false;
		}
		//CapsuleColliderがついていたら取得する
		if (gameObject.GetComponent<Collider>())
		{
			capsuleCollider = GetComponent<Collider>();
		}

		//if (tag == "Player") Remaining = 5;
		//else Remaining = 1;
		if(tag == "Enemy") white_material = Resources.Load<Material>("Material/Enemy_Damege_Effect") as Material;
		else if(tag == "Player") white_material = Resources.Load<Material>("Material/Player_Damege_Effect") as Material;
		if(gameObject.name == "Bacula") white_material = Resources.Load<Material>("Material/Bacula") as Material;
		self_material = new Material[object_material.Length];
        defaultColor = new Vector4[object_material.Length];
		for (int i = 0; i < self_material.Length; i++) self_material[i] = object_material[i].material;
        for (int i = 0; i < defaultColor.Length; i++) defaultColor[i] = object_material[i].material.color;

        HP_Setting();
		framecnt = 0;
		check = false;
	}
	public void Update()
	{
		if (check)
		{
			for (int i = 0; i < object_material.Length; i++)
			{

				if (framecnt > 1)
				{
					material_Reset();
					framecnt = 0;
					check = false;
					return;
				}
			}
			framecnt++;

		}
	}
	//初期の体力を保存
	public void HP_Setting()
	{
		hp_Max = hp;
	}
	//再利用可能にするための処理
	public void Reset_Status()
	{
		hp = hp_Max;
	}
	//ダメージを与える関数
	public void Damege_Process(int damege)
	{
		hp -= damege;
	}
	/// <summary>
	/// 死んだときに呼び出される
	/// </summary>
	public void Died_Process()
	{
		if (transform.name == "Middle_Boss" || transform.name == "Enemy_MiddleBoss_Father")
		{
			//スコア
			Game_Master.MY.Score_Addition(score, Opponent);
			SE_Manager.SE_Obj.SE_Explosion(Obj_Storage.Storage_Data.audio_se[22]);
			//爆発処理の作成
			ParticleCreation(7);
			Is_Dead = true;
			Reset_Status();
		}
        else if (transform.name == "BattleshipType_Enemy(Clone)" || transform.name == "BattleshipType_Enemy")
        {
            //スコア
            Game_Master.MY.Score_Addition(score, Opponent);
            SE_Manager.SE_Obj.SE_Explosion(Obj_Storage.Storage_Data.audio_se[19]);
			//SE_Manager.SE_Obj.Enemy_Scleem(Obj_Storage.Storage_Data.audio_se[3]);
            //爆発処理の作成
            ParticleCreation(10);
            Is_Dead = true;
            Reset_Status();
        }
        else if(transform.name == "Enemy_MeteorBound_Model(Clone)"|| transform.name ==  "Enemy_MeteorBound_Model")
        {
            //スコア
            Game_Master.MY.Score_Addition(score, Opponent);
            SE_Manager.SE_Obj.SE_Explosion(Obj_Storage.Storage_Data.audio_se[1]);
            //爆発処理の作成
            ParticleCreation(13);
            Is_Dead = true;
            Reset_Status();
		}
        else if (gameObject.tag != "Player")
		{
			//スコア
			Game_Master.MY.Score_Addition(score, Opponent);
			SE_Manager.SE_Obj.SE_Explosion_small(Obj_Storage.Storage_Data.audio_se[18]);
			//爆発処理の作成
			ParticleCreation(4);
			Is_Dead = true;
			Reset_Status();
		}
		else
		{
			//爆発処理の作成
			ParticleCreation(0);
			Is_Dead = true;
			Reset_Status();
		}

		//死んだらゲームオブジェクトを遠くに飛ばす処理
		transform.position = new Vector3(0, 800.0f, 0);
		//稼働しないようにする
		//Debug.Log(gameObject.transform.parent.name + "	Destroy");
		material_Reset();
		gameObject.SetActive(false);

	}
	//パーティクルの作成（爆発のみ）
	public GameObject ParticleCreation(int particleID)
	{
		//呼び出し元オブジェクトの座標で指定IDのパーティクルを生成
		//Instantiate(Obj_Storage.Storage_Data.particle[particleID], gameObject.transform.position, Obj_Storage.Storage_Data.particle[particleID].transform.rotation);
		GameObject effect = Obj_Storage.Storage_Data.Effects[particleID].Active_Obj();
		ParticleSystem particle = effect.GetComponent<ParticleSystem>();

		//爆発の位置をランダムに変更
		float range = 1.0f;
		Vector3 temp = new Vector3(Random.Range(-range, range), Random.Range(-range, range),0.0f);
		effect.transform.position = transform.position + temp.normalized;
		/*********************************************************/
		particle.Play();
		return effect;
	}
	//自分以外の玉と当たった時にダメージを食らう
	public void OnTriggerEnter(Collider col)
	{
		if (gameObject.tag == "Player" || tag == "Invincible")
		{
			if (col.tag == "Item")
			{
				var item = col.GetComponent<Item>();
				if (item.itemType != ItemType.Item_KillAllEnemy)
				{
					if(gameObject.name == "Player")
					{
						P1_PowerManager.Instance.Pick();
						SE_Manager.SE_Obj.SE_Item_Catch();
						col.gameObject.SetActive(false);
						Game_Master.MY.Score_Addition(1600, 1);
					}
					else
					{
						P2_PowerManager.Instance.Pick();
						SE_Manager.SE_Obj.SE_Item_Catch();
						col.gameObject.SetActive(false);
						Game_Master.MY.Score_Addition(1600, 2);

					}
				}
				else
					PowerManager.Instance.Annihilate();
			}
			if (col.tag == "Enemy_Bullet")
			{
				bullet_status BS = col.gameObject.GetComponent<bullet_status>();
				if(activeShield && shield > 1)
				{
					shield--;
				}
				else
				{
					namenamename = col.gameObject.name;
					Damege_Process((int)BS.attack_damage);
				}
			}
			if (col.tag == "Enemy")
			{
				if (activeShield && shield > 1)
				{
					shield -= 3;
				}
				else
				{
					Damege_Process(3);
				}
			}
		}
		if (tag == "Enemy")
		{
			if (col.tag == "Player_Bullet")
			{
				bullet_status BS = col.gameObject.GetComponent<bullet_status>();
				Damege_Process((int)BS.attack_damage);
				Damege_Effect();

				if(gameObject.name == "Bacula")
				{
					SE_Manager.SE_Obj.SE_Baculor(Obj_Storage.Storage_Data.audio_se[0]);
				}
				if(gameObject.name == "Shiled_father")
				{
					SE_Manager.SE_Obj.Boss_Core(Obj_Storage.Storage_Data.audio_se[7]);
				}

			}
			else if(col.gameObject.name == "Player" || col.gameObject.name == "Player_2")
			{
				if (gameObject.name != "Enemy_Moai")
				{
					Damege_Process(3);
				}
			}
		}
	}
	public void OnTriggerStay(Collider col)
	{
		if(tag == "Player")
		{
			if(col.tag == "Enemy")
			{
				Damege_Process(3);
			}
		}
	}
	//キャラクターが死んだか(残機とHP両方)どうかの判定用関数
	public bool Died_Judgment()
	{
		bool is_died = false;
		if (hp < 1 && Remaining < 1) is_died = true;
		return is_died;
	}
	//キャラクターが死んでいるかどうかの判定用関数
	public bool Dead_Check()
	{
		bool isDead = false;
		if (hp < 1 && Remaining > 0) isDead = true;
		return isDead;
	}
	//明るさを変える関数
	public void HSV_Change()
	{
		v_Value = 1.0f - transform.position.z * 0.015f;
        
		if (v_Value > 1.0f)
		{
			v_Value = 1.0f;
		}
        //setColor = new Vector4(1 * v_Value, 1 * v_Value, 1 * v_Value, 1 * v_Value);
        setColor = new Vector4(1 * v_Value, 1 * v_Value, 1 * v_Value, 1 * v_Value);

        for (int i = 0; i < object_material.Length; i++)
        {
            setColor = new Vector4(defaultColor[i].x * v_Value, defaultColor[i].y * v_Value, defaultColor[i].z * v_Value, 1);
            object_material[i].material.SetVector("_BaseColor", setColor);
        }

  //      foreach (Renderer renderer in object_material)
		//{
  //          renderer.material.SetVector("_BaseColor", setColor);
		//	//renderer.material.color = UnityEngine.Color.HSVToRGB(0, 0, v_Value);
		//}
	}
	//ダメージを食らうとダメージエフェクトが走るように
	public void Damege_Effect()
	{
		if (object_material != null)
		{
			for (int i = 0; i < object_material.Length; i++)
			{
				object_material[i].material = white_material;
				if (!check) check = true;
			}
		}
	}
	//ダメージを受けた時のエフェクトが元のエフェクトに戻すための関数
	public void material_Reset()
	{
		for (int i = 0; i < object_material.Length; i++)
		{
			object_material[i].material = self_material[i];
		}
	}
	//シールドの値を取得する
	public int Get_Shield()
	{
		return shield;
	}
	//シールドの値設定
	public void Set_Shield(int setvalue)
	{
		shield = setvalue;
	}
	//キャラクタの設定してある体力を取得するための関数
	public uint Get_Score()
	{
		return score;
	}
	public Material Get_self_material(int num)
	{
		return self_material[num];
	}

    
    //追加
    //残機増やす
    public void BossRemainingBouns(int bonusRemaining)
    {
        if(Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eONE_PLAYER)
        {
            return;
        }

        //if(transform.name == "Middle_Boss" || transform.name == "One_Boss" || transform.name == "Two_Boss" || transform.name == "Moai")
        //{
        //    var player1 = Obj_Storage.Storage_Data.GetPlayer();
        //    var player2 = Obj_Storage.Storage_Data.GetPlayer2();

        //    if(player1.activeSelf && !player2.activeSelf)
        //    {
        //        player2.SetActive(true);
        //        player2.GetComponent<Player2>().ResponPreparation(bonusRemaining);

        //    }

        //    else if(player2.activeSelf && !player1.activeSelf)
        //    {
        //        player1.SetActive(true);
        //        player1.GetComponent<Player1>().ResponPreparation(bonusRemaining);
        //    }
        //}
    }
}