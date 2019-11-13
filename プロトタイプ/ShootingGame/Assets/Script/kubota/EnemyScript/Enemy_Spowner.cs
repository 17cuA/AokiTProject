using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spowner : MonoBehaviour
{
	GameObject EnemyPrefab1;    //敵キャラのプレハブ情報を取得（リソースフォルダから直接取得）
	float ReSpown_Time;      //	復活するまでの時間をカウント
    [SerializeField]
	int ReSpown_Max;			//敵キャラのリスポーン時間
    // Start is called before the first frame update
    void Start()
    {
		Prefab_Determination();
		ReSpown_Time = 7;
		//ReSpown_Max = 7;
	}

    // Update is called once per frame
    void Update()
    {
		if (ReSpown_Time > ReSpown_Max)
		{
			Instantiate(EnemyPrefab1, gameObject.transform.position, Quaternion.identity);
			ReSpown_Time = 0;
		}
		ReSpown_Time += Time.deltaTime;
    }
	
	private void Prefab_Determination()
	{
		if(gameObject.name == "Spowner1") EnemyPrefab1 = Resources.Load("Enemy/Enemy_Test1") as GameObject;
		else EnemyPrefab1 = Resources.Load("Enemy/Enemy_Test2") as GameObject;
	}
}
