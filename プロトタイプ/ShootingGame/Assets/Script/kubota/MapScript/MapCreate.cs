using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapCreate : MonoBehaviour
{
	//ワールド座標でのマップの左下の部分
	//ここから敵の配置などをしていく
	private const int up_left_pos_y = 5;
	private const int up_left_pos_x = -8;

	//シーンを切り替えるときにプレイヤーの死亡情報などを取得するための変数
	private SceneChanger SC;

	void Start()
    {
		if(SceneManager.GetActiveScene().name == "Stage_01" 
			|| SceneManager.GetActiveScene().name == "Stage_02" 
			|| SceneManager.GetActiveScene().name == "Stage_03" 
			|| SceneManager.GetActiveScene().name == "Stage_04"
			 || SceneManager.GetActiveScene().name == "Stage_05"
			  || SceneManager.GetActiveScene().name == "Stage_06"
			   || SceneManager.GetActiveScene().name == "Stage_07")
		{
			SC = GetComponent<SceneChanger>();
			//OC = GetComponent<Object_Creation>();
			//csvフォルダからマップ情報を取得
			//１列ごとに取得
			CreateMap();			//マップの作成（各オブジェクトの移動）
		}
	}
	void CreateMap()
	{
		for (int y = 0; y < Obj_Storage.Storage_Data.CsvData.Count; y++)
		{
			for (int x = 0; x < Obj_Storage.Storage_Data.CsvData[y].Length; x++)
			{
				Obj_Storage.Storage_Data.pos = new Vector3(up_left_pos_x + x * 2, up_left_pos_y - y, 0);
				switch(Obj_Storage.Storage_Data.CsvData[y][x])
				{
					case "0":
						break;
					case "1":
						GameObject Player_obj = Obj_Storage.Storage_Data.Player.Active_Obj();
						if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eONE_PLAYER) Player_obj.transform.position = new Vector3(-2, 0, 0);
						else if (Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eTWO_PLAYER) Player_obj.transform.position = new Vector3(-2, 2, 0);
						//Player_obj.transform.position = Obj_Storage.Storage_Data.pos;
						break;
					case "2":
						//GameObject Enemy_obj = Obj_Storage.Storage_Data.Enemy1.Active_Obj();
						//Enemy_obj.transform.position = Obj_Storage.Storage_Data.pos;
						break;
					case "3":
						GameObject Player_obj2 = Obj_Storage.Storage_Data.Player_2.Active_Obj();
						Player_obj2.transform.position = new Vector3(-2,-2,0);
						break;
					default:
						break;
				}
			}
		}
	}
}
