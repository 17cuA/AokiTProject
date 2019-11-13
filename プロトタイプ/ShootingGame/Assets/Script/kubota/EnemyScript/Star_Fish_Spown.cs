using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star_Fish_Spown : MonoBehaviour
{
	int frame;      //生成タイミングを計る用
	public int frameMax;    //生成のタイミングを設定する用
	private int attack_type;    //攻撃対象をプレイヤーの数に応じて変更するため

	Vector3[] pos = new Vector3[12];    //出現位置を入れておくもの
	private int Create_Pos_cnt;             //生成位置を決めるための

	public int Active_Frame;	//活動しているフレーム数
	[Header("動き続ける時間を設定")]
	public int Active_Frame_Max;            //活動しているフレーム最大数

	private GameObject P1_obj;
	private GameObject P2_obj;
	private Player1 P1;			//1Pの情報
	private Player2 P2;			  //２Ｐの情報


	private void Start()
	{
		//生成位置を決められたポジションに配置
		pos[0] = new Vector3(7.0f + 9.0f, 3.15f, 0.0f);
		pos[1] = new Vector3(7.0f + 9.0f, 0.996f, 0.0f);
		pos[2] = new Vector3(6.0f + 9.0f, 2.158f, 0.0f);
		pos[3] = new Vector3(3.154f + 9.0f, 4.15f, 0.0f);
		pos[4] = new Vector3(2.158f + 9.0f, -3.154f, 0.0f);
		pos[5] = new Vector3(0.996f + 9.0f, 4.15f, 0.0f);
		pos[6] = new Vector3(7.0f + 9.0f, -3.154f, 0.0f);
		pos[7] = new Vector3(7.0f + 9.0f, -0.996f, 0.0f);
		pos[8]= new Vector3(6.0f + 9.0f, -2.158f, 0.0f);
		pos[9] = new Vector3(3.154f + 9.0f, -4.15f, 0.0f);
		pos[10] = new Vector3(2.158f + 9.0f, 3.154f, 0.0f);
		pos[11] = new Vector3(0.996f + 9.0f, -4.15f, 0.0f);

		//各値の初期化
		frame = 0;
		attack_type = 0;
		Create_Pos_cnt = 0;
		Active_Frame = 0;
		P1_obj = Obj_Storage.Storage_Data.GetPlayer();
		P2_obj = Obj_Storage.Storage_Data.GetPlayer2();
		P1 = P1_obj.GetComponent<Player1>();
		P2 = P2_obj.GetComponent<Player2>();
	}
    void Update()
    {
		
		if(Active_Frame > Active_Frame_Max)
		{
			gameObject.SetActive(false);
		}
		Active_Frame++;
		//------------------------------------------------
		//生成処理
		Respown_Enemy();
    }
	void Respown_Enemy()
	{
		frame++;
		if (Create_Pos_cnt >= pos.Length) Create_Pos_cnt = 0;
		if (frame > frameMax)
		{
			//一人プレイか二人プレイかによって生成を変える
			if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eTWO_PLAYER)
			{
				//稼働にし、規定の位置に移動させる
				GameObject effect = Obj_Storage.Storage_Data.Effects[14].Active_Obj();
				effect.transform.position = pos[Create_Pos_cnt];
				Enemy_star_Fish ESF = Obj_Storage.Storage_Data.StarFish_Enemy.Active_Obj().GetComponent<Enemy_star_Fish>();
				ESF.transform.position = pos[Create_Pos_cnt];
				//-------------------------------------------
				//生成したポジションを保存
				//ここでやらなければ、生成位置がずれてしまうため。
				ESF.firstPos = ESF.transform.position;
				//攻撃のターゲットを設定
				if (attack_type % 2 == 0)
				{
					if (!P1_obj.activeSelf) ESF.playerPos = P2.transform.position;
					else if (P1.Is_Resporn) ESF.playerPos = new Vector3(ESF.firstPos.x - 1, ESF.firstPos.y, ESF.firstPos.z);
					else ESF.playerPos = P1.transform.position;
				}
				else
				{
					if (!P2_obj.activeSelf) ESF.playerPos = P1.transform.position;
					else if (P2.Is_Resporn) ESF.playerPos = new Vector3(ESF.firstPos.x - 1, ESF.firstPos.y, ESF.firstPos.z);
					else ESF.playerPos = P2.transform.position;

				}
				ESF.Attack_Target_Decision(attack_type % 2);
				attack_type++;
				Create_Pos_cnt++;
				frame = 0;
			}
			else
			{
				//稼働にし、規定の位置に移動させる-------------------------------------
				GameObject effect = Obj_Storage.Storage_Data.Effects[14].Active_Obj();
				effect.transform.position = pos[Create_Pos_cnt];
				//--------------------------------------------------------------
				Enemy_star_Fish ESF = Obj_Storage.Storage_Data.StarFish_Enemy.Active_Obj().GetComponent<Enemy_star_Fish>();
				ESF.transform.position = pos[Create_Pos_cnt];
				ESF.firstPos = ESF.transform.position;
				if (P1.Is_Resporn) ESF.playerPos = new Vector3(ESF.firstPos.x - 1, ESF.firstPos.y, ESF.firstPos.z);
				else ESF.playerPos = P1.transform.position; Create_Pos_cnt++;
				frame = 0;
			}
		}
	}

	void Player_Pos_Get()
	{
		
	}
}
