//作成日2019/06/13
// 一面のボスのパーツ管理
// 作成者:諸岡勇樹
/*
 * 2019/07/11　初期位置の確保
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class One_Boss_Parts : character_status
{
	[SerializeField,Tooltip("サポートするオブジェクト")] private GameObject[] supported_objects;

	private new void Start()
    {
		base.Start();
    }
    private new void Update()
    {
		base.Update();

		// hp が0以下のとき
        if(hp < 1)
		{
			//　サポートするオブジェクトがあるとき
			if (supported_objects != null)
			{
				// サポートしているオブジェクトの見た目を消す
				foreach (GameObject obj in supported_objects)
				{
					MeshRenderer ms = obj.GetComponent<MeshRenderer>();
					ms.enabled = false;
				}
			}
			base.Died_Judgment();
			base.Died_Process();
		}
	}
}
