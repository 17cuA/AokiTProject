//作成日2019/07/08
// バウンドする１面のボスの弾
// 作成者:諸岡勇樹
/*
 * 2019/07/17 バウンド処理
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class One_Boss_BoundBullet : bullet_status
{
	[SerializeField, Tooltip("バウンド回数")] private int bound_count;
	[SerializeField, Tooltip("レイの長さ")] private float length_on_landing;

	private RaycastHit hit_mesh;
	private Vector3 Ray_Direction { get; set; }
	private GameObject mae { get; set; }
	private GameObject boss { get; set; }
	private Collider _Collider { get; set; }


	//----------------------追加--------------------------
	private GameObject deathEffect;
	[SerializeField] private float deathEffectLifeTime;
	private float deathEffectLifeTimer;
	private ParticleSystem[] particleSystems;

	[SerializeField] private bool canDropItem;
	private float dropItemRandomNum = 10;
	[SerializeField] private float dropItemSeed;
	//---------------------------------------------------

	private new void Start()
	{
		base.Start();
		Ray_Direction = transform.right;
		boss = Obj_Storage.Storage_Data.GetBoss(1);
		_Collider = GetComponent<Collider>();

		deathEffect = transform.GetChild(1).gameObject;
		particleSystems = GetComponentsInChildren<ParticleSystem>();
	}

	// Update is called once per frame
	private new void Update()
	{
		//----------------------------------------------------------------------
		if (transform.position.x >= 19.0f || transform.position.x <= -19.0f
			|| transform.position.y >= 10.5f || transform.position.y <= -10.5f)
		{
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

			deathEffectLifeTimer = 0;
			deathEffect.SetActive(false);
			transform.GetChild(0).gameObject.SetActive(true);
			transform.GetChild(2).gameObject.SetActive(true);
			_Collider.enabled = true;
			gameObject.SetActive(false);
			for (var i = 0; i < particleSystems.Length; ++i)
			{
				particleSystems[i].Stop();
			}
		}
		//---------------------------------------------------------------------

		Ray_Direction = transform.right;
		transform.position -= Ray_Direction.normalized * shot_speed;

		var v3 = transform.position;
		v3.z = 0.0f;
		transform.position = v3;

		Debug.DrawRay(transform.position, -Ray_Direction.normalized * length_on_landing, Color.red);
		if (Physics.Raycast(transform.position, -Ray_Direction.normalized, out hit_mesh, length_on_landing)
			&& boss.activeSelf)
		{
			// コライダーの持ち主がWAllのとき
			if (hit_mesh.collider.gameObject.tag != "Player_Bullet" && hit_mesh.collider.gameObject.tag != "Enemy_Bullet"/*&& hit_mesh.collider.gameObject != mae*/)
			{
				mae = hit_mesh.transform.gameObject;
				Ray_Direction = ReflectionCalculation(Ray_Direction, hit_mesh.normal);
				transform.right = Ray_Direction;
				_Collider.isTrigger = false;
;			}
		}

		//--------------------------------------------------------
		if (gameObject.activeSelf)
		{
			if (deathEffect.activeSelf)
			{
				for (var i = 0; i < particleSystems.Length; ++i)
				{
					if (particleSystems[i].isStopped) particleSystems[i].Play();
				}

				deathEffectLifeTimer += Time.deltaTime;

				if (transform.GetChild(0).gameObject.activeSelf)
					transform.GetChild(0).gameObject.SetActive(false);

				if (transform.GetChild(2).gameObject.activeSelf)
					transform.GetChild(2).gameObject.SetActive(false);


				if (_Collider.enabled)
					_Collider.enabled = false;
			}

			if (deathEffectLifeTimer >= deathEffectLifeTime)
			{
				deathEffectLifeTimer = 0;
				deathEffect.SetActive(false);
				transform.GetChild(0).gameObject.SetActive(true);
				transform.GetChild(2).gameObject.SetActive(true);
				_Collider.enabled = true;
				gameObject.SetActive(false);
				for (var i = 0; i < particleSystems.Length; ++i)
				{
					particleSystems[i].Stop();
				}
			}

			if (!transform.GetChild(0).gameObject.activeSelf && !transform.GetChild(1).gameObject.activeSelf && !transform.GetChild(2).gameObject.activeSelf)
			{
				deathEffectLifeTimer = 0;
				deathEffect.SetActive(false);
				transform.GetChild(0).gameObject.SetActive(true);
				transform.GetChild(2).gameObject.SetActive(true);
				_Collider.enabled = true;
				gameObject.SetActive(false);
				for (var i = 0; i < particleSystems.Length; ++i)
				{
					particleSystems[i].Stop();
				}
			}
		}
	}

	private void OnEnable()
	{
		mae = null;
		if (_Collider != null)_Collider.isTrigger = true;
	}

	/// <summary>
	/// 反射の計算
	/// </summary>
	/// <param name="progressVector_F"> 進行方向のベクトル </param>
	/// <param name="normalVector_N"> 法線ベクトル </param>
	/// <returns></returns>
	private Vector2 ReflectionCalculation(Vector3 progressVector_F, Vector3 normalVector_N)
	{
		Vector2 vecocity = Vector2.zero;

		//　公式の利用
		vecocity = progressVector_F + (2 * Vector2.Dot(-progressVector_F, normalVector_N) * normalVector_N);

		return vecocity;
	}

	//------------------------------------------
	public void RougeSeed()
	{
		dropItemSeed = 0;
		dropItemSeed = UnityEngine.Random.Range(0, 100f);
		canDropItem = dropItemSeed > dropItemRandomNum ? false : true;
		if (canDropItem)
		{
			transform.GetChild(0).gameObject.SetActive(false);
			transform.GetChild(2).gameObject.SetActive(true);
		}
		else
		{
			transform.GetChild(0).gameObject.SetActive(true);
			transform.GetChild(2).gameObject.SetActive(false);
		}
	}

	private new void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player_Bullet")
		{
			deathEffect.SetActive(true);
			if (canDropItem)
				StorageReference.Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePOWERUP_ITEM, transform.position,Vector3.left);

			col.GetComponent<bullet_status>().Player_Bullet_Des();
			col.gameObject.SetActive(false);

		}
	}

	private void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Player_Bullet")
		{
			deathEffect.SetActive(true);
			if (canDropItem)
				StorageReference.Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePOWERUP_ITEM, transform.position, Vector3.left);

			col.gameObject.GetComponent<bullet_status>().Player_Bullet_Des();
			col.gameObject.SetActive(false);
		}
	}
}