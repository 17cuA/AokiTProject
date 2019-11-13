using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Power;

[RequireComponent(typeof(PauseComponent))]
[RequireComponent(typeof(CapsuleCollider))]
public class LaserLine : Player_Bullet
{
	private TrailRenderer trailRenderer;
	public TrailRenderer TrailRenderer
	{
		get
		{
			return trailRenderer;
		}
	}

	private new Rigidbody rigidbody;

	public Material redMaterial;
	public Material blueMaterial;
	private void Awake()
    {
		this.trailRenderer = GetComponent<TrailRenderer>();
		this.rigidbody = GetComponent<Rigidbody>();
	}

	private new void Start()
	{
		base.Start();
		this.Travelling_Direction = Vector3.right;
	}

	private new void Update()
    {
		if (gameObject.name == "Option_Player1_Laser")
		{
			if (!transform.GetChild(0).gameObject.activeSelf)
				transform.GetChild(0).gameObject.SetActive(true);
			if (transform.GetChild(1).gameObject.activeSelf)
				transform.GetChild(1).gameObject.SetActive(false);
			if (trailRenderer.material != blueMaterial)
				trailRenderer.material = blueMaterial;

			if (base.Player_Number != 1)
				base.Player_Number = 1;
		}

		else if(gameObject.name == "Option_Player2_Laser")
		{
			if (transform.GetChild(0).gameObject.activeSelf)
				transform.GetChild(0).gameObject.SetActive(false);
			if (!transform.GetChild(1).gameObject.activeSelf)
				transform.GetChild(1).gameObject.SetActive(true);
			if (trailRenderer.material != redMaterial)
				trailRenderer.material = redMaterial;

			if (base.Player_Number != 2)
				base.Player_Number = 2;
		}

		else if (gameObject.name == "Player_Laser")
		{

			if (!transform.GetChild(0).gameObject.activeSelf)
				transform.GetChild(0).gameObject.SetActive(true);
			if (transform.GetChild(1).gameObject.activeSelf)
				transform.GetChild(1).gameObject.SetActive(false);
			if(trailRenderer.material != blueMaterial)
				trailRenderer.material = blueMaterial;

			if(base.Player_Number != 1)
				base.Player_Number = 1;
		}

		else if (gameObject.name == "Player2_Laser")
		{

			if (transform.GetChild(0).gameObject.activeSelf)
				transform.GetChild(0).gameObject.SetActive(false);
			if (!transform.GetChild(1).gameObject.activeSelf)
				transform.GetChild(1).gameObject.SetActive(true);
			if(trailRenderer.material != redMaterial)
				trailRenderer.material = redMaterial;

			if (base.Player_Number != 2)
				base.Player_Number = 2;
		}
		


		if (transform.position.x >= 19f || transform.position.x <= -19f
			|| transform.position.y >= 6f || transform.position.y <= -6f)
		{
			trailRenderer.Clear();

			gameObject.SetActive(false);
		}

		//rigidbody.velocity = new Vector3(shot_speed * 40, 0, 0);
		base.Moving_To_Travelling_Direction();
	}

    public new void OnTriggerEnter(Collider col)
    {      
		if(gameObject.tag == "Player_Bullet " && col.name == "Bacula")
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

            trailRenderer.Clear();
			gameObject.SetActive(false);
			Player_Bullet_Des();

		}
        else if (gameObject.tag == "Player_Bullet" && col.gameObject.tag == "Enemy" && col.gameObject.name != "One_Boss_Laser" && col.gameObject.name != "Two_Boss_Laser" && col.gameObject.name != "Moai_Eye_Laser"&& col.gameObject.name != "Moai_Mouth_Laser")
        {
            //add:0513_takada 爆発エフェクトのテスト
            //AddExplosionProcess();
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
			//if (P1 != null) P1.Bullet_cnt--;
			//if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eTWO_PLAYER)
			//{
			//    if (P2 != null) P2.Bullet_cnt--;
			//}

			trailRenderer.Clear();
			gameObject.SetActive(false);
        }
        else if (col.gameObject.tag == "Boss_Gard")
        {
            GameObject effect = Obj_Storage.Storage_Data.Effects[11].Active_Obj();
            ParticleSystem particle = effect.GetComponent<ParticleSystem>();
            effect.transform.position = gameObject.transform.position;
            particle.Play();

			trailRenderer.Clear();
			gameObject.SetActive(false);
        }
    }
}
