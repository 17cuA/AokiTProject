//作成者：川村良太
//モアイの口を開け閉めするスクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoaiAnimation : MonoBehaviour
{
	public float speedY;
	Vector3 velocity;
	Vector3 defaultPos;
	Vector3 openPos;

	public Vector3 startMarker;
	public Vector3 endMarker;
	float startTime;
	float present_Location;
	public float testSpeed = 1.0f;

	private float distance_two;

	public Enemy_Moai moai_Script;
    Enemy_Moai_Attack moaiAttack_Script;


	public float moveSpeed;
	public Animation anim;
	public bool isOpen = true;
	public bool isClose = false;

	private void Awake()
	{
		defaultPos = transform.localPosition;
        //moaiAttack_Script = transform.parent.gameObject.GetComponent<Enemy_Moai_Attack>();

		startMarker = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
		startMarker = new Vector3(transform.localPosition.x, transform.localPosition.y - 0.0388f, transform.localPosition.z);

	}
	void Start()
	{
		moai_Script = transform.parent.gameObject.GetComponent<Enemy_Moai>();
		distance_two = Vector3.Distance(startMarker, endMarker);

		anim = this.gameObject.GetComponent<Animation>();
		isOpen = false;
	}

	void Update()
	{
        if (Game_Master.Management_In_Stage == Game_Master.CONFIGURATION_IN_STAGE.WIRELESS)
        {
            return;
        }

        if (moai_Script.attackState == Enemy_Moai.AttackState.MiniMoai)
		{
			speedY = 2.0f;
		}
		else
		{
			speedY = 1.5f;
		}

		if (isOpen && !moai_Script.isExit)
		{
			velocity = gameObject.transform.rotation * new Vector3(0, speedY, 0);
			gameObject.transform.position += velocity * Time.deltaTime;
			if (transform.localPosition.y < defaultPos.y - 0.0388f)
			{
				transform.localPosition = new Vector3(defaultPos.x, defaultPos.y - 0.0388f, defaultPos.z);
				isOpen = false;
				//isClose = true;
				moai_Script.isMouthOpen = true;
			}
		}
		else if(isClose)
		{
			velocity = gameObject.transform.rotation * new Vector3(0, -speedY, 0);
			gameObject.transform.position += velocity * Time.deltaTime;
			if (transform.localPosition.y > defaultPos.y)
			{
				transform.localPosition = defaultPos;
				isClose = false;
				isOpen = true;

				if (moai_Script.attackLoopCnt == 3)
				{
					isOpen = false;
					moai_Script.isExit = true;
					moai_Script.saveHP = moai_Script.hp;
				}
			}
		}
		//present_Location = (Time.time * testSpeed) / distance_two;
		//transform.position = Vector3.Lerp(startMarker, endMarker, present_Location);
		////startTime += moveSpeed;

		if (Input.GetMouseButtonDown(0))
		{
			//isOpen
			//bo = true;
			//anim.Play();
		}
	}
}
