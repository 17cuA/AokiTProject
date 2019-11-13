using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class Effects_Management : MonoBehaviour
{
	//Particleprefablist
	private GameObject[] Effects = new GameObject[18];

	void Start()
	{
		Effects[0] = Resources.Load<GameObject>("Effects/Effects_000");
		Effects[1] = Resources.Load<GameObject>("Effects/Effects_001");
		Effects[2] = Resources.Load<GameObject>("Effects/Effects_002");
		Effects[3] = Resources.Load<GameObject>("Effects/Effects_003");
		Effects[4] = Resources.Load<GameObject>("Effects/Effects_004");     //none
		Effects[5] = Resources.Load<GameObject>("Effects/Effects_005");
		Effects[6] = Resources.Load<GameObject>("Effects/Effects_006");
		Effects[7] = Resources.Load<GameObject>("Effects/Effects_007");
		Effects[8] = Resources.Load<GameObject>("Effects/Effects_008");
		Effects[9] = Resources.Load<GameObject>("Effects/Effects_009");     //none
		Effects[10] = Resources.Load<GameObject>("Effects/Effects_010");
		Effects[11] = Resources.Load<GameObject>("Effects/Effects_011");
		Effects[12] = Resources.Load<GameObject>("Effects/Effects_012");
		Effects[13] = Resources.Load<GameObject>("Effects/Effects_013");
		Effects[14] = Resources.Load<GameObject>("Effects/Effects_014");        //none
		Effects[15] = Resources.Load<GameObject>("Effects/Effects_015");        //none
		Effects[16] = Resources.Load<GameObject>("Effects/Effects_016");        //none
		Effects[17] = Resources.Load<GameObject>("Effects/Effects_017");
	}

	void Update()
	{
	}

	//Particleの生成
	//1:自身のオブジェクト
	//2:エフェクトのID
	//3:自身のオブジェクトの座標
	public void ParticleCreation(GameObject gameObject, int particleID, Vector3 objectPosition)
	{
		//呼び出し元オブジェクトの座標で指定IDのパーティクルを生成
		GameObject particleGameObject = Instantiate(Effects[particleID], objectPosition, Effects[particleID].transform.rotation);
		//呼び出し元をパーティクルの親に設定
		particleGameObject.transform.parent = gameObject.transform;
	}
}