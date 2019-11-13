using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class Enemy_Manta : character_status
{
	public enum Move_Type
	{
		Entry,
		Fight,
		Exit,
		None
	}
	Move_Type type;     //行動の管理用

	[SerializeField,Header("バレットを発射位置用オブジェクト")]
	private GameObject[] Shot_Mazle;        //unity側で設定
	int mazlecnt;		//マズル選択用

	float Beam_Delay;					//弾発射管理変数
	public float Beam_DelayMax;     //弾発射感覚決定用変数（unity側にて決定）


	[SerializeField, Header("ビーム攻撃発射位置")]
	private GameObject[] Beam_Mazle;		//ビーム用位置情報
	bool Is_Laser_Attack;			//レーザー攻撃が出来るかどうか

	private void OnEnable()
	{
		type = Move_Type.Entry;
	}

	private void OnDisable()
	{
		mazlecnt = 0;
	}

	new void Start()
    {
		type = Move_Type.Entry;
		mazlecnt = 0;

		base.Start();
    }

    // Update is called once per frame
    new void Update()
    {

		Fire_Bullet();

		base.Update();
    }

	/// <summary>
	/// 動きのモード変更用
	/// </summary>
	/// <param name="num">対応した値に変える</param>
	void Change_Type(int num)
	{
		type = (Move_Type)num;
	}

	void Fire_Bullet()
	{
		Shot_Delay++;

		if(Shot_Delay > Shot_DelayMax)
		{
			Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BEAM, Shot_Mazle[mazlecnt].transform.position, Vector3.left);

			mazlecnt++;
			if (mazlecnt == 2) mazlecnt = 0;

			Shot_Delay = 0;
		}

	}

	void Fire_Laser()
	{
				Shot_Delay++;

		if (Shot_Delay > Shot_DelayMax)
		{
			foreach (GameObject obj in Beam_Mazle)
			{
				if (obj.activeSelf)
				{
					//Boss_One_Laser laser = Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eONE_BOSS_LASER, obj.transform.position, transform.right).GetComponent<Boss_One_Laser>();
					//laser.Manual_Start(obj.transform,true);
				}
			}
			Shot_Delay = 0;
		}
	}

}
