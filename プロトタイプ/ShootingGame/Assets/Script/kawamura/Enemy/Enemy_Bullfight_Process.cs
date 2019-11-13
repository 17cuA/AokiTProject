using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorageReference;

public class Enemy_Bullfight_Process : character_status
{
	public enum State
	{
		WaveUp,
		WaveDown,
		WaveOnlyUp,
		WaveOnlyDown,
		Straight,
	}
	public State eState;

	GameObject childObj;		//子供入れる
	GameObject item;            //アイテム入れる
	GameObject parentObj;		//親
	GameObject parent_ParentObj;		//親の親入れる（群れの時のため）
	//GameObject blurObj;

	new Renderer renderer;			//レンダラー
	//HSVColorController hsvCon;	//シェーダー用
	//Color hsvColor;
	//BlurController blurCon;
	EnemyGroupManage groupManage;           //群れの時の親スクリプト
	Enemy_Bullfight_Move ebm;
	//public ParticleSystem sonicBoom;			//ジェット噴射の衝撃波のようなパーティクル

	int childNum;
	float rotaY;					//Y角度


	public float sin;
	//float posX;
	//float posY;
	//float posZ;
	//float defPosX;
	//float val_Value;					//テクスチャの明るさの増える値
	//float sigma_Value;					//ブラーのぼやけ具合の値（0でぼやけなし）
	//public float h_Value;
	//public float s_Value;

	//public float v_Value;

	public bool once = true;			//updateで一回だけ呼び出す処理用
	public bool haveItem = false;

	//---------------------------------------------------------

	private void Awake()
	{

		if (gameObject.GetComponent<DropItem>())
		{
			DropItem dItem = gameObject.GetComponent<DropItem>();
			haveItem = true;
		}
		childNum = transform.childCount;
	}
	private void OnEnable()
	{
		if (transform.parent.parent)
		{
			parent_ParentObj = transform.parent.parent.gameObject;
			groupManage = parent_ParentObj.GetComponent<EnemyGroupManage>();
			groupManage.childNum++;
		}
        else
        {
            parent_ParentObj = GameObject.Find("TemporaryParent");
            transform.parent = parent_ParentObj.transform;
        }
	}
	new void Start()
	{
		item = Resources.Load("Item/Item_Test") as GameObject;

		childObj = transform.GetChild(0).gameObject;            //モデルオブジェクトの取得（3Dモデルを子供にしているので）
		//childCnt = transform.childCount;
		renderer = childObj.GetComponent<Renderer>();
		parentObj = transform.parent.gameObject;
		ebm = parentObj.GetComponent<Enemy_Bullfight_Move>();

		//if (transform.parent.parent)
		//{
		//	parent_ParentObj = transform.parent.parent.gameObject;
		//	groupManage = parent_ParentObj.GetComponent<EnemyGroupManage>();
		//}
  //      else
  //      {
  //          parent_ParentObj = GameObject.Find("TemporaryParent");
  //          transform.parent = parent_ParentObj.transform;
  //      }

		HP_Setting();
		base.Start();
	}

	new void Update()
	{
		if(once)
		{
			//状態によって値を変える
			switch(eState)
			{
				case State.WaveUp:
					HSV_Change();
					break;

				case State.WaveDown:
					HSV_Change();
					break;

				case State.WaveOnlyUp:
					HSV_Change();
					break;

				case State.WaveOnlyDown:
					HSV_Change();
					break;

				case State.Straight:
					HSV_Change();
					break;
			}
			once = false;
		}


        HSV_Change();

		if (hp < 1)
		{
			if (haveItem)
			{
				//Instantiate(item, this.transform.position, transform.rotation);
				Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePOWERUP_ITEM, this.transform.position, Quaternion.identity);
			}
			if (parent_ParentObj)
			{
                if(parent_ParentObj.name!= "TemporaryParent")
                {
				    //群を管理している親の残っている敵カウントマイナス
				    groupManage.remainingEnemiesCnt--;
				    //倒された敵のカウントプラス
				    groupManage.defeatedEnemyCnt++;
				    //群に残っている敵がいなくなったとき
				    if (groupManage.remainingEnemiesCnt == 0)
				    {
					    //倒されずに画面外に出た敵がいなかったとき(すべての敵が倒されたとき)
					    if (groupManage.notDefeatedEnemyCnt == 0 && groupManage.isItemDrop)
					    {
							//アイテム生成
							//Instantiate(item, this.transform.position, transform.rotation);
							Object_Instantiation.Object_Reboot(Game_Master.OBJECT_NAME.ePOWERUP_ITEM, this.transform.position, transform.rotation);
						}
						//一体でも倒されていなかったら
						else
					    {
						    //なにもしない
					    }
				    }
                }
            }

			Enemy_Reset();
			//Reset_Status();
			Died_Process();
		}
	}
	//-------------ここから関数------------------


	void Enemy_Reset()
	{
		//ebm.speedZ = 0;
		once = true;
		//ebm.isSlerp = false;
		//ebm.isWave = false;
	}

	private void OnTriggerExit(Collider col)
	{
		if (col.gameObject.name == "WallLeft")
		{
			groupManage.notDefeatedEnemyCnt++;
			groupManage.remainingEnemiesCnt -= 1;
			gameObject.SetActive(false);
		}
	}
}
