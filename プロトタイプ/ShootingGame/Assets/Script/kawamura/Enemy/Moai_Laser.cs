using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moai_Laser : MonoBehaviour
{
    public float shot_speed;//弾の速度
    public float attack_damage;//ダメージの変数
    public bool State_Laser;
    public GameObject Laser_Appearance;     // レーザー時の見た目
    public GameObject Frame_Appearance;     //　フレーム時の見た目
    public BoxCollider _collider;

    public string myName;

    private void Start()
    {
        myName = gameObject.name;
    }

    void Update()
    {
        if (transform.position.x >= 18.0f || transform.position.x <= -18.0f
            || transform.position.y >= 6.0f || transform.position.y <= -6.0f)
        {

            if (myName == "Moai_Mouth_Laser")
            {
                GameObject obj = gameObject;

                Obj_Storage.Storage_Data.Moai_Mouth_Laser.Set_Parent_Obj(ref obj);

            }
            else if(myName == "Moai_Eye_Laser")
            {
                GameObject obj = gameObject;

                Obj_Storage.Storage_Data.Moai_Eye_Laser.Set_Parent_Obj(ref obj);

            }
			gameObject.SetActive(false);
			//Destroy(gameObject);
		}
    }

    private void LateUpdate()
    {
        Vector3 temp = transform.localPosition;
        temp.x += shot_speed;
        transform.localPosition = temp;
    }

    public void Manual_Start(Transform parent)
    {
        transform.parent = parent;
        transform.localScale = new Vector3(12.0f, 12.0f, 12.0f);

        // レーザーモードのとき
        //if(laser_mode)
        //{
        //	State_Laser = laser_mode;
        //	Laser_Appearance.SetActive(true);
        //	Frame_Appearance.SetActive(false);
        //	_collider.size = new Vector3(0.2f, 0.1f, 5.0f);
        //}
        //else if(!laser_mode)
        //{
        //	State_Laser = laser_mode;
        //	State_Laser = laser_mode;
        //	Laser_Appearance.SetActive(false);
        //	Frame_Appearance.SetActive(true);
        //	_collider.size = new Vector3(0.2f, 0.1f, 0.1f);
        //}
    }

    protected void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameObject effect = Obj_Storage.Storage_Data.Effects[11].Active_Obj();
            ParticleSystem particle = effect.GetComponent<ParticleSystem>();
            effect.transform.position = gameObject.transform.position;
            particle.Play();
        }
        if (State_Laser)
        {
            if (col.GetComponent<One_Boss_BoundBullet>() != null)
            {
                col.gameObject.SetActive(false);
            }
        }
    }
}
