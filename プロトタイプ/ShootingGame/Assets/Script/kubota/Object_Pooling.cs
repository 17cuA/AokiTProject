/*	久保田達己	
 *	オブジェクトプーリングするためのスクリプト
 *	
 *	更新履歴：
 *	2019/06/02	まず初めに敵キャラをオブジェクトプーリングする。
 *	2019/06/03	プレイヤーをオブジェクトプーリングする。
 *				作り直し。
 * 2019/06/06	完成
 * 2019/06/21	チェック用のログを出すようにした。
 */
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Object_Pooling
{
	//生成するキャラクタを入れるリスト
	List<GameObject> obj = new List<GameObject>();      //作成したオブジェクトたちをリストに保存し管理する
	GameObject z = new GameObject();        //親オブジェクトの作成
	string obj_name;                        //オブジェクトの名前を保存用変数
	private GameObject prefab;      //オブジェクトを再利用するためのプレハブを保存

	/// <summary>
	/// コンストラクタ
	/// </summary>
	/// <param name="Create_obj">生成したいオブジェクト</param>
	/// <param name="Create_num">生成したい数</param>
	/// <param name="name">そのオブジェクトの名前</param>
	public Object_Pooling(GameObject Create_obj, int Create_num, string name)
	{
		//親オブジェクトの名前を変更
		z.name = name;
		//生成したい数だけ処理を回す
		for (int i = 0; i < Create_num; i++)
		{
			//オブジェクトの作成とリストに追加するリスト
			Create_newObj(name, Create_obj, z, false);
		}
		//名前を保存
		obj_name = name;
		//プレハブを保存
		prefab = Create_obj;
	}
	//リストをすべて返す関数
	public List<GameObject> Get_Obj()
	{
		return obj;
	}
	//オブジェクトを稼働にする関数
	//稼働するオブジェクトが足りない場合はさらに作成し、稼働状態にする
	public GameObject Active_Obj()
	{
		//暇な状態のオブジェクトを検索し、発見し次第稼働状態にし、処理を終了する
		//暇な状態のオブジェクトが見つからない場合は一つ作成し、稼働状態にする
		int i = 0;
		while (i < obj.Count)
		{
			if (obj[i].activeSelf == false)
			{
				obj[i].SetActive(true);
				return obj[i];
                
			}
			i++;
		}
		return Create_newObj(obj_name, prefab, z, true);
	}

	/// <summary>
	/// オブジェクトを生成して、各種設定したのちリストに追加する処理
	/// </summary>
	/// <param name="name">オブジェクトに入れる名前</param>
	/// <param name="Create_Obj">作成するオブジェクト</param>
	/// <param name="Parent_Obj">親オブジェクトにするもの</param>
	/// <param name="Is_Active">SetActiveをtrueにするのかfalseにするのかを変更する</param>
	private GameObject Create_newObj(string name, GameObject Create_Obj, GameObject Parent_Obj, bool Is_Active)
	{
		GameObject gameObject = Object.Instantiate(Create_Obj);//オブジェクトの作成
		gameObject.name = name;         //名前の変更
		gameObject.transform.parent = Parent_Obj.transform;//生成してある親オブジェクトの子供にする
		gameObject.SetActive(Is_Active);    //活動するかどうかの変更
		obj.Add(gameObject);                                            // 生成をした値を配列に入れる
		return gameObject;
	}

	public void Set_Parent_Obj(ref GameObject childe)
	{
		childe.transform.parent = z.transform;
	}

	public GameObject Get_Parent_Obj()
	{
		return z;
	}
}