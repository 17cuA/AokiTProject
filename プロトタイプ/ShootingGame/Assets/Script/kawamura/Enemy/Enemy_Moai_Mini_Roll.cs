//作成者：川村良太
//ミニモアイの回転　横の移動は親がして一斉に動くのでここではしない。
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Moai_Mini_Roll : character_status
{
	GameObject parentObj;
	Enemy_Moai_Mini_Group miniMoaiGroup_Script;		//親のスクリプト

	new Renderer renderer;                      //レンダラー　3Dオブジェクトの時使う


	//public float rotaX;
	public float rotaY;			//モアイはYで横回転
	//public float rotaZ;

	//public float rotaX_Value = 0;
	public float rotaY_Value = 0;
	//public float rotaZ_Value = 0;

	Enemy_Wave_Direction ewd;

	public string myName;

	public bool isWaveEnemy = false;
	public bool isBacula;

	private void Awake()
	{
		myName = gameObject.name;
		parentObj = transform.parent.gameObject;
		miniMoaiGroup_Script = parentObj.GetComponent<Enemy_Moai_Mini_Group>();
	}

	new void Start()
	{
		//rotaX = transform.eulerAngles.x;
		rotaY = transform.eulerAngles.y;
		//rotaZ = transform.eulerAngles.z;
	}

	void Update()
	{
		transform.localRotation = Quaternion.Euler(0, rotaY, 0);
		//rotaX += rotaX_Value;
		rotaY += rotaY_Value;
		//rotaZ += rotaZ_Value;
	}
}
