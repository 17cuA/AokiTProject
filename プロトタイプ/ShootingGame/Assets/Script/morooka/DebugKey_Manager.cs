//----------------------------------------------------------------------------------------------
// 制作者：諸岡勇樹
// 制作日：2019/11/12
//----------------------------------------------------------------------------------------------
// デバッグキーのまとめ
//----------------------------------------------------------------------------------------------
// 2019/11/12　ステージ移行のデバッグキー
//----------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugKey_Manager : MonoBehaviour
{
    void Update()
    {
		// 任意ステージに移動---------------------------------
		if(Input.GetKey(KeyCode.S))
		{
			if (Input.GetKey(KeyCode.Alpha1)) SceneMove(Scene_Manager.SCENE_NAME.eSTAGE_01);
		}
		// 任意ステージに移動---------------------------------
	}

	/// <summary>
	/// デバッグキーのシーン以降
	/// </summary>
	/// <param name="sceneName"> 移行したいシーンのイナム </param>
	void SceneMove(Scene_Manager.SCENE_NAME sceneName )
	{
		// プレイヤー人数が設定されてないとき
		if(Game_Master.Number_Of_People == Game_Master.PLAYER_NUM.eUNDECIDED)
		{
			var type = Game_Master.MY.GetType();
			var prop_Number_Of_People = type.GetProperty("Number_Of_People");
			prop_Number_Of_People.SetValue(Game_Master.MY, Game_Master.PLAYER_NUM.eONE_PLAYER);
		}
		// シーン以降
		Scene_Manager.Manager.Scene_Transition(sceneName);
	}
}
