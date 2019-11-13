//作成日2019/04/22
//弾の移動管理
//作成者:佐藤翼
/*
 * 2019/06/06	バレットの挙動をオブジェクトプーリングの形式に変更しました
 * 2019/06/13	継承用クラスに変更
 * 2019/06/26	レンダーの初期化法の変更
 */
using System;
using System.Collections;
using UnityEngine;

public class bullet_status : MonoBehaviour
{
	public float shot_speed;//弾の速度
	public float attack_damage;//ダメージの変数
	public Vector3 Travelling_Direction;    //自分の向き
	[SerializeField]
	private Renderer Bullet_Renderer = null; // 判定したいオブジェクトのrendererへの参照
    public Player1 P1 { get; private set; }		// プレイヤー1の情報
    public Player2 P2 { get; private set; }		// プレイヤー2の情報
    public Bit_Shot bShot { get; set; }			// ビットンの情報
	public int Player_Number { get; set; }		// どのプレイヤーの弾か

	// どのオブジェクトの弾かの識別用
	public enum Type
	{
		None,					// なし
		Player1,					// プレイヤー1
		Player2,					// プレイヤー2
		Player1_Option,		// プレイヤー1ビットン
		Player2_Option,		// プレイヤー2ビットン
		Enemy,					// エネミー
	}

	public Type Bullet_Type;        //各キャラクターの弾かどうかを判定する変数

	private void Awake()
	{
		// ポーズ用のコンポーネントアタッチ
		gameObject.AddComponent<PauseComponent>();
	}
	protected void Start()
	{
		if(Bullet_Renderer == null) Bullet_Renderer = GetComponent<Renderer>();
		Travelling_Direction = transform.right;

		switch (Bullet_Type)
		{
			case Type.Player1:
				P1 = Obj_Storage.Storage_Data.GetPlayer().GetComponent<Player1>();
				Player_Number = 1;
				break;
			case Type.Player2:
				P2 = Obj_Storage.Storage_Data.GetPlayer2().GetComponent<Player2>();
				Player_Number = 2;
				break;
			case Type.Player1_Option:
                bShot = Obj_Storage.Storage_Data.GetOption().GetComponent<Bit_Shot>();
                Player_Number = 1;
                break;
			case Type.Player2_Option:
                bShot = Obj_Storage.Storage_Data.GetOption().GetComponent<Bit_Shot>();
                Player_Number = 2;
                break;
			case Type.Enemy:
				break;
			case Type.None:
				break;
			default:
				break;
		}
	}

	protected void Update()
	{
		// 画面外に出たとき
		if (transform.position.x >= 18.5f || transform.position.x <= -18.5f
			|| transform.position.y >= 6f || transform.position.y <= -6f)
		{
			// プレイヤー、ビットンの弾のとき
			// 各カウンターの減少
			if (gameObject.tag == "Player_Bullet")
			{
				switch (Bullet_Type)
				{
					case Type.Player1:
						if (P1.Bullet_cnt > 0) P1.Bullet_cnt--;
						break;
					case Type.Player2:
						if (P2.Bullet_cnt > 0) P2.Bullet_cnt--;
						break;
					case Type.Player1_Option:
                        if (bShot.Bullet_cnt > 0) bShot.Bullet_cnt--;
                        break;
                    case Type.Player2_Option:
                        if (bShot.Bullet_cnt > 0) bShot.Bullet_cnt--;
                        break;
                    case Type.Enemy:
						break;
					case Type.None:
						break;
					default:
						break;
				}
			}

			// 非アクティブ化
			gameObject.SetActive(false);
		}
	}

	protected void OnTriggerEnter(Collider col)
	{
        //それぞれのキャラクタの弾が敵とプレイヤーにあたっても消えないようにするための処理
        if (gameObject.tag == "Enemy_Bullet" && (col.gameObject.name == "Enemy_Meteor_One" || col.gameObject.name == "Enemy_Meteor_Two" || col.gameObject.name == "Enemy_Meteor_Three" || col.gameObject.name == "Enemy_Meteor_four" || col.gameObject.name == "Enemy_Meteor_Five"))
        {
            gameObject.SetActive(false);
        }
		// 自身がエネミーの弾で、プレイヤーに衝突したとき
        else if ((gameObject.tag == "Enemy_Bullet" && col.gameObject.tag == "Player"))
        {
            gameObject.SetActive(false);
            GameObject effect = Obj_Storage.Storage_Data.Effects[11].Active_Obj();
            ParticleSystem particle = effect.GetComponent<ParticleSystem>();
            effect.transform.position = gameObject.transform.position;
            particle.Play();
        }
		// 自身がプレイヤーの弾で、ヴァキュラに当たったとき
		else if(gameObject.tag == "Player_Bullet " && col.name == "Bacula")
		{
			character_status obj = col.GetComponent<character_status>();
			if (obj != null)
			{
				obj.Opponent = Player_Number;
			}
			GameObject effect = Obj_Storage.Storage_Data.Effects[11].Active_Obj();
			ParticleSystem particle = effect.GetComponent<ParticleSystem>();
			effect.transform.position = gameObject.transform.position;
			particle.Play();
			gameObject.SetActive(false);
			Player_Bullet_Des();
		}
		// 自身がプレイヤーの弾で、エネミー
		else if (gameObject.tag == "Player_Bullet" && col.gameObject.tag == "Enemy")
        {
            character_status obj = col.GetComponent<character_status>();
            if (obj != null)
            {
                obj.Opponent = Player_Number;
            }
            GameObject effect = Obj_Storage.Storage_Data.Effects[11].Active_Obj();
            ParticleSystem particle = effect.GetComponent<ParticleSystem>();
            effect.transform.position = gameObject.transform.position;
            particle.Play();
			Player_Bullet_Des();
			gameObject.SetActive(false);
        }
        else if (col.gameObject.tag == "Boss_Gard")
        {
            GameObject effect = Obj_Storage.Storage_Data.Effects[11].Active_Obj();
            ParticleSystem particle = effect.GetComponent<ParticleSystem>();
            effect.transform.position = gameObject.transform.position;
            particle.Play();
            gameObject.SetActive(false);
        }
	}

	/// <summary>
	/// 爆発エフェクト生成
	/// </summary>
	protected void AddExplosionProcess()
	{
		ParticleManagement particleManagementCS;
		particleManagementCS = GameObject.Find("ParticleManager").GetComponent<ParticleManagement>();
	}

	/// <summary>
	/// 向きの変更
	/// </summary>
	/// <param name="_Dir"> 向きたい向き </param>
	protected void Moving_Facing_Preference(Vector3 _Dir)
	{
		transform.right = _Dir;
		Travelling_Direction = _Dir;
	}

	/// <summary>
	/// 移動したい向きのみ変更
	/// </summary>
	/// <param name="_Dir"> 移動方向 </param>
	protected void FacingChange(Vector3 _Dir)
	{
		Travelling_Direction = _Dir;
	}

	/// <summary>
	/// 向いている方向に移動
	/// </summary>
	protected void Moving_To_Facing()
	{
		Vector3 temp_Pos = transform.right.normalized * shot_speed;
		transform.position += temp_Pos;
	}

	/// <summary>
	/// 進行方向に移動
	/// </summary>
	public void Moving_To_Travelling_Direction()
	{
		Vector3 tempPos = Travelling_Direction.normalized * shot_speed;
		transform.position += tempPos;
	}

	/// <summary>
	/// タグの変更
	/// </summary>
	/// <param name="tag_name"> 変更したいタグ名 </param>
	protected void Tag_Change(string tag_name)
	{
		gameObject.tag = tag_name;
	}

	public void Player_Bullet_Des()
	{
		switch (Bullet_Type)
		{
			case Type.None:
				break;
			case Type.Player1:
				P1.Bullet_cnt = 0;
				P1.shoot_number = 0;
				break;
			case Type.Player2:
				P2.Bullet_cnt = 0;
				P2.shoot_number = 0;
				break;
			case Type.Player1_Option:
				bShot.Bullet_cnt = 0;
				bShot.shotNum = 0;
				break;
			case Type.Player2_Option:
				bShot.Bullet_cnt = 0;
				bShot.shotNum = 0;

				break;
			case Type.Enemy:
				break;
			default:
				break;
		}
	}
}
