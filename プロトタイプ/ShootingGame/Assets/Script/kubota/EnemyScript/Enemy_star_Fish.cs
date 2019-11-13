using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class Enemy_star_Fish : character_status
{
    GameObject item;
	public Vector3 playerPos;
	public Vector3 firstPos;
	public int num = 0;
	private Vector3 vector;		//単位ベクトルを入れる
    public bool haveItem = false;
    // Start is called before the first frame update
    new void Start()
	{
        if (gameObject.GetComponent<DropItem>())
        {
            DropItem dItem = gameObject.GetComponent<DropItem>();
            haveItem = true;
        }
        item = Resources.Load("Item/Item_Test") as GameObject;
        base.Start();
	}
	// Update is called once per frame
	new void Update()
	{
		if (vector == new Vector3(0, 0, 0)) vector = calcPos();		//単位ベクトルの取得 
		transform.position -= vector * speed;
		if(hp < 1)
		{
            if (haveItem)
            {
                Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePOWERUP_ITEM, this.transform.position, Quaternion.identity);
            }
            base.Died_Process();
		}
		base.Update();
	}

	private void OnTriggerExit(Collider col)
	{
		if (col.gameObject.name == "WallLeft" || col.gameObject.name == "WallTop" || col.gameObject.name == "WallUnder" || col.gameObject.name == "WallRight")
		{
			gameObject.SetActive(false);
		}
	}
	//単位ベクトル計算用
	Vector3 calcPos()
	{
		Vector3 pos = playerPos - firstPos;
		//pos.z = 0;
		return pos.normalized;
	}

	public void Attack_Target_Decision(int number)
	{
		num = number;
	}

}
