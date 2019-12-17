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


	void Start()
    {
		if(SceneManager.GetActiveScene().name == "Stage_01" )
		{
			CreateMap();			//マップの作成（各オブジェクトの移動）
		}
	}
	void CreateMap()
	{
		GameObject Player_obj = Obj_Storage.Storage_Data.Player.Active_Obj();
		Player_obj.transform.position = new Vector3(-2, 0, 0);
	}
}
