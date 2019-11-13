//作成者：川村良太
//単体でアイテムを落とす敵用のスクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class DropItem : MonoBehaviour
{
	public GameObject item;
	Vector3 itemPos;
    public bool isQuitting = false;
	public bool isDrop = false;

    void Start()
    {
        //isQuitting = true;
	}


    void Update()
    {
		//アイテムの生成位置更新
		//itemPos = transform.position;
    }
}
