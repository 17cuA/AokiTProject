//作成者：川村良太
//リング状の敵バレットスクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class Enemy_RingBullet : bullet_status
{
	GameObject[] childEffectObj;
    float rotaZ;
    Quaternion deadAttackRotation;
    new void Start()
    {
		base.Start();
		Tag_Change("Enemy_Bullet");

	}

	// Update is called once per frame
	new void Update()
    {
		base.Update();
		Moving_To_Facing();

	}
	private new void OnTriggerEnter(Collider col)
	{
        if (col.tag == "Player_Bullet")
		{
            gameObject.SetActive(false);
            col.GetComponent<bullet_status>().Player_Bullet_Des();
            DeadAttack();
            col.gameObject.SetActive(false);
        }
        else if(col.gameObject.name == "face" || col.gameObject.name == "mouth")
        {
            gameObject.SetActive(false);
        }
        base.OnTriggerEnter(col);
	}
    void DeadAttack()
    {
        for (int i = 0; i < 6; i++)
        {
            deadAttackRotation = Quaternion.Euler(0, 0, rotaZ);
            Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.eENEMY_BULLET, transform.position, deadAttackRotation);
            rotaZ += 60;
        }
        rotaZ = 0;
    }
}
