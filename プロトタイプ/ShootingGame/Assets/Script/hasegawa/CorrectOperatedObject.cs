/*
 * 20190710 作成
 * author : yuuta hesegawa
 */
/* 移動操作をしているオブジェクトのY軸の動きを他のオブジェクトに反映させる */
/* 疑似的な無限の移動を実装 */
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ユーザーが操作しているオブジェクトの移動を他のオブジェクトに反映させる
/// </summary>
public class CorrectOperatedObject<T> where T : MonoBehaviour
{
	[SerializeField] float stageHeight = 17f;						// ステージの高さ(どの高さで画面端のワープを行うか、管理範囲)
	[SerializeField] float heightMargin = 1f;						// 上下移動の際のゆとり
	List<GameObject> reflectedObjectList = new List<GameObject>();	// ユーザーの操作を反映させるオブジェクトのリスト
	List<GameObject> operatedObjectList = new List<GameObject>();	// ユーザーに操作されているオブジェクトのリスト
	float previousOperatedObjectPosY = 0f;							// 操作されているオブジェクトを位置補正する前のY位置
	float correctSpeed = 5f;										// 補間する速さ

	/// <summary>
	/// <para>コンストラクタ</para>
	/// Obj_StorageのStartが処理されているのを確認してから生成すること
	/// </summary>
	/// <param name="_reflectedObjectList">転移を反映させたいオブジェクトのリスト</param>
	/// <param name="_operatedObjectList">移動の補正をしたいオブジェクトのリスト</param>
	/// <param name="_stageHeight">ステージの高さ</param>
	/// <param name="_heightMargin">上下のゆとり</param>
	public CorrectOperatedObject(List<T> _reflectedObjectList, List<GameObject> _operatedObjectList = null, float _stageHeight = 17f, float _heightMargin = 1f)
	{
		stageHeight = _stageHeight;
		heightMargin = _heightMargin;
		for (int i = 0; i < _reflectedObjectList.Count; ++i)
		{
			reflectedObjectList.Add(_reflectedObjectList[i].gameObject);
		}
		if (_operatedObjectList == null) { operatedObjectList = Obj_Storage.Storage_Data.Player.Get_Obj(); return; }
		for (int i = 0; i < _operatedObjectList.Count; ++i)
		{
			operatedObjectList.Add(_operatedObjectList[i].gameObject);
		}
	}
	/// <summary>
	/// <para>コンストラクタ</para>
	/// Obj_StorageのStartが処理されているのを確認してから生成すること
	/// </summary>
	/// <param name="_reflectedObjectList">転移を反映させたいオブジェクトの配列</param>
	/// <param name="_operatedObjectList">移動の補正をしたいオブジェクトの配列</param>
	/// <param name="_stageHeight">ステージの高さ</param>
	/// <param name="_heightMargin">上下のゆとり</param>
	public CorrectOperatedObject(T[] _reflectedObjectList, GameObject[] _operatedObjectList = null, float _stageHeight = 17f, float _heightMargin = 1f)
	{
		stageHeight = _stageHeight;
		heightMargin = _heightMargin;
		for (int i = 0; i < _reflectedObjectList.Length; ++i)
		{
			reflectedObjectList.Add(_reflectedObjectList[i].gameObject);
		}
		if (_operatedObjectList == null) { operatedObjectList = Obj_Storage.Storage_Data.Player.Get_Obj(); return; }
		for (int i = 0; i < _operatedObjectList.Length; ++i)
		{
			operatedObjectList.Add(_operatedObjectList[i].gameObject);
		}
	}
	/// <summary>
	/// <para>コンストラクタ</para>
	/// Obj_StorageのStartが処理されているのを確認してから生成すること
	/// </summary>
	/// <param name="_reflectedObjectList">転移を反映させたいオブジェクトのリスト</param>
	/// <param name="_operatedObject">移動の補正をしたいオブジェクト</param>
	/// <param name="_stageHeight">ステージの高さ</param>
	/// <param name="_heightMargin">上下のゆとり</param>
	public CorrectOperatedObject(List<T> _reflectedObjectList, GameObject _operatedObject = null, float _stageHeight = 17f, float _heightMargin = 1f)
	{
		stageHeight = _stageHeight;
		heightMargin = _heightMargin;
		for (int i = 0; i < _reflectedObjectList.Count; ++i)
		{
			reflectedObjectList.Add(_reflectedObjectList[i].gameObject);
		}
		if (_operatedObject == null) { operatedObjectList = Obj_Storage.Storage_Data.Player.Get_Obj(); return; }
		operatedObjectList.Add(_operatedObject);
	}
	/// <summary>
	/// <para>コンストラクタ</para>
	/// Obj_StorageのStartが処理されているのを確認してから生成すること
	/// </summary>
	/// <param name="_reflectedObjectList">転移を反映させたいオブジェクトの配列</param>
	/// <param name="_operatedObject">移動の補正をしたいオブジェクト</param>
	/// <param name="_stageHeight">ステージの高さ</param>
	/// <param name="_heightMargin">上下のゆとり</param>
	public CorrectOperatedObject(T[] _reflectedObjectList, GameObject _operatedObject = null, float _stageHeight = 17f, float _heightMargin = 1f)
	{
		stageHeight = _stageHeight;
		heightMargin = _heightMargin;
		for (int i = 0; i < _reflectedObjectList.Length; ++i)
		{
			reflectedObjectList.Add(_reflectedObjectList[i].gameObject);
		}
		if (_operatedObject == null) { operatedObjectList = Obj_Storage.Storage_Data.Player.Get_Obj(); return; }
		operatedObjectList.Add(_operatedObject);
	}
	/// <summary>
	/// 呼ばれている間、operatedObjectListに登録されているオブジェクトを真ん中に持ってくる
	/// </summary>
	public void CorrectObject()
	{
		if (operatedObjectList?.Count <= 0 && operatedObjectList?.Count >= 2) { return; }		// オブジェクトが0個、または2個以上なら処理しない
		if (Mathf.Abs(operatedObjectList[0].transform.position.y) < heightMargin) { return; }	// ゆとり範囲を超えていなければ処理しない
		// 使用する変数の定義
		GameObject operatedObject = operatedObjectList[0];
		float correctSign = Mathf.Sign(operatedObject.transform.position.y);
		// 補間する
		previousOperatedObjectPosY = operatedObject.transform.position.y;
		operatedObject.transform.position = new Vector3(
			operatedObject.transform.position.x,
			Mathf.Lerp(operatedObject.transform.position.y, heightMargin * correctSign, correctSpeed * Time.deltaTime),
			operatedObject.transform.position.z);
		float distance = previousOperatedObjectPosY - operatedObject.transform.position.y;
		// 登録されているオブジェクトをオフセットする
		for (int i = 0; i < reflectedObjectList.Count; ++i)
		{
			// もし管理範囲外に行くなら、もう片方に転移する
			if (Mathf.Abs(reflectedObjectList[i].transform.position.y - distance) > stageHeight)
			{
				float transferSign = -Mathf.Sign(reflectedObjectList[i].transform.position.y);
				reflectedObjectList[i].transform.position += new Vector3(0f, stageHeight * 2f * transferSign);
			}
			reflectedObjectList[i].transform.position += new Vector3(0f, -distance, 0f);
		}
	}
	/// <summary>
	/// リストに登録されていないオブジェクトを代入する
	/// </summary>
	/// <param name="_reflectedObjectList">オブジェクトのリスト</param>
	public void AddReflectedObject(List<T> _reflectedObjectList)
	{
		// 登録されていないオブジェクトのみを代入する
		for (int i = 0; i < _reflectedObjectList.Count; ++i)
		{
			if (reflectedObjectList.Contains(_reflectedObjectList[i].gameObject)) { continue; }
			reflectedObjectList.Add(_reflectedObjectList[i].gameObject);
		}
	}
	/// <summary>
	/// リストに登録されていないオブジェクトを登録する
	/// </summary>
	/// <param name="_reflectedObjectList">オブジェクトの配列</param>
	public void AddReflectedObject(T[] _reflectedObjectList)
	{
		// 登録されていないオブジェクトのみを代入する
		for (int i = 0; i < _reflectedObjectList.Length; ++i)
		{
			if (reflectedObjectList.Contains(_reflectedObjectList[i].gameObject)) { continue; }
			reflectedObjectList.Add(_reflectedObjectList[i].gameObject);
		}
	}
	/// <summary>
	/// リストから解放する
	/// </summary>
	/// <param name="_reflectedObject">解放するオブジェクト</param>
	public void RemoveReflectedObject(T _reflectedObject)
	{
		reflectedObjectList.Remove(_reflectedObject.gameObject);
	}
}
/// <summary>
/// ユーザーが操作しているオブジェクトの移動を他のオブジェクトに反映させる
/// </summary>
public class CorrectOperatedObject
{
	[SerializeField] float stageHeight = 17f;						// ステージの高さ(どの高さで画面端のワープを行うか、管理範囲)
	[SerializeField] float heightMargin = 1f;						// 上下移動の際のゆとり
	List<GameObject> reflectedObjectList = new List<GameObject>();	// ユーザーの操作を反映させるオブジェクトのリスト
	List<GameObject> operatedObjectList = new List<GameObject>();	// ユーザーに操作されているオブジェクトのリスト
	float previousOperatedObjectPosY = 0f;							// 操作されているオブジェクトを位置補正する前のY位置
	float correctSpeed = 2f;										// 補間する速さ

	/// <summary>
	/// <para>コンストラクタ</para>
	/// Obj_StorageのStartが処理されているのを確認してから生成すること
	/// </summary>
	/// <param name="_reflectedObjectList">転移を反映させたいオブジェクトのリスト</param>
	/// <param name="_operatedObjectList">移動の補正をしたいオブジェクトのリスト</param>
	/// <param name="_stageHeight">ステージの高さ</param>
	/// <param name="_heightMargin">上下のゆとり</param>
	public CorrectOperatedObject(List<GameObject> _reflectedObjectList, List<GameObject> _operatedObjectList = null, float _stageHeight = 17f, float _heightMargin = 1f)
	{
		stageHeight = _stageHeight;
		heightMargin = _heightMargin;
		reflectedObjectList = _reflectedObjectList;
		//operatedObjectList = Obj_Storage.Storage_Data.Player.Get_Obj();
		if (_operatedObjectList == null) { operatedObjectList = Obj_Storage.Storage_Data.Player.Get_Obj(); return; }
		operatedObjectList = _operatedObjectList;
	}
	/// <summary>
	/// <para>コンストラクタ</para>
	/// Obj_StorageのStartが処理されているのを確認してから生成すること
	/// </summary>
	/// <param name="_reflectedObjectList">転移を反映させたいオブジェクトのリスト</param>
	/// <param name="_operatedObjectList">移動の補正をしたいオブジェクトの配列</param>
	/// <param name="_stageHeight">ステージの高さ</param>
	/// <param name="_heightMargin">上下のゆとり</param>
	public CorrectOperatedObject(GameObject[] _reflectedObjectList, GameObject[] _operatedObjectList = null, float _stageHeight = 17f, float _heightMargin = 1f)
	{
		stageHeight = _stageHeight;
		heightMargin = _heightMargin;
		reflectedObjectList.AddRange(_reflectedObjectList);
		//operatedObjectList.AddRange(_operatedObjectList);
		if (_operatedObjectList == null) { operatedObjectList = Obj_Storage.Storage_Data.Player.Get_Obj(); return; }
		operatedObjectList.AddRange(_operatedObjectList);
	}
	/// <summary>
	/// <para>コンストラクタ</para>
	/// Obj_StorageのStartが処理されているのを確認してから生成すること
	/// </summary>
	/// <param name="_reflectedObjectList">転移を反映させたいオブジェクトのリスト</param>
	/// <param name="_operatedObject">移動の補正をしたいオブジェクト</param>
	/// <param name="_stageHeight">ステージの高さ</param>
	/// <param name="_heightMargin">上下のゆとり</param>
	public CorrectOperatedObject(List<GameObject> _reflectedObjectList, GameObject _operatedObject = null, float _stageHeight = 17f, float _heightMargin = 1f)
	{
		stageHeight = _stageHeight;
		heightMargin = _heightMargin;
		for (int i = 0; i < _reflectedObjectList.Count; ++i)
		{
			reflectedObjectList.Add(_reflectedObjectList[i].gameObject);
		}
		if (_operatedObject == null) { operatedObjectList = Obj_Storage.Storage_Data.Player.Get_Obj(); return; }
		operatedObjectList.Add(_operatedObject);
	}
	/// <summary>
	/// <para>コンストラクタ</para>
	/// Obj_StorageのStartが処理されているのを確認してから生成すること
	/// </summary>
	/// <param name="_reflectedObjectList">転移を反映させたいオブジェクトの配列</param>
	/// <param name="_operatedObject">移動の補正をしたいオブジェクト</param>
	/// <param name="_stageHeight">ステージの高さ</param>
	/// <param name="_heightMargin">上下のゆとり</param>
	public CorrectOperatedObject(GameObject[] _reflectedObjectList, GameObject _operatedObject = null, float _stageHeight = 17f, float _heightMargin = 1f)
	{
		stageHeight = _stageHeight;
		heightMargin = _heightMargin;
		for (int i = 0; i < _reflectedObjectList.Length; ++i)
		{
			reflectedObjectList.Add(_reflectedObjectList[i].gameObject);
		}
		if (_operatedObject == null) { operatedObjectList = Obj_Storage.Storage_Data.Player.Get_Obj(); return; }
		operatedObjectList.Add(_operatedObject);
	}
	/// <summary>
	/// 呼ばれている間、operatedObjectListに登録されているオブジェクトを真ん中に持ってくる
	/// </summary>
	public void CorrectObject()
	{
		if (operatedObjectList?.Count <= 0 && operatedObjectList?.Count >= 2) { return; }		// オブジェクトが0個、または2個以上なら処理しない
		if (Mathf.Abs(operatedObjectList[0].transform.position.y) < heightMargin) { return; }	// ゆとり範囲を超えていなければ処理しない
		// 使用する変数の定義
		GameObject operatedObject = operatedObjectList[0];
		float correctSign = Mathf.Sign(operatedObject.transform.position.y);
		// 補間する
		previousOperatedObjectPosY = operatedObject.transform.position.y;
		operatedObject.transform.position = new Vector3(
			operatedObject.transform.position.x,
			Mathf.Lerp(operatedObject.transform.position.y, heightMargin * correctSign, correctSpeed * Time.deltaTime),
			operatedObject.transform.position.z);
		float distance = previousOperatedObjectPosY - operatedObject.transform.position.y;
		// 登録されているオブジェクトをオフセットする
		for (int i = 0; i < reflectedObjectList.Count; ++i)
		{
			// もし管理範囲外に行くなら、もう片方に転移する
			if (Mathf.Abs(reflectedObjectList[i].transform.position.y - distance) > stageHeight)
			{
				float transferSign = -Mathf.Sign(reflectedObjectList[i].transform.position.y);
				reflectedObjectList[i].transform.position += new Vector3(0f, stageHeight * 2f * transferSign);
			}
			reflectedObjectList[i].transform.position += new Vector3(0f, -distance, 0f);
		}
	}
	/// <summary>
	/// リストに登録されていないオブジェクトを登録する
	/// </summary>
	/// <param name="_reflectedObjectList">オブジェクトのリスト</param>
	public void AddReflectedObject(List<GameObject> _reflectedObjectList)
	{
		// 登録されていないオブジェクトのみ代入する
		for (int i = 0; i < _reflectedObjectList.Count; ++i)
		{
			if (reflectedObjectList.Contains(_reflectedObjectList[i])) { continue; }
			reflectedObjectList.Add(_reflectedObjectList[i]);
		}
	}
	/// <summary>
	/// リストに登録されていないオブジェクトを登録する
	/// </summary>
	/// <param name="_reflectedObjectList">オブジェクトの配列</param>
	public void AddReflectedObject(GameObject[] _reflectedObjectList)
	{
		// 登録されていないオブジェクトのみ代入する
		for (int i = 0; i < _reflectedObjectList.Length; ++i)
		{
			if (reflectedObjectList.Contains(_reflectedObjectList[i])) { continue; }
			reflectedObjectList.Add(_reflectedObjectList[i]);
		}
	}
	/// <summary>
	/// リストから解放する
	/// </summary>
	/// <param name="_reflectedObject">解放するオブジェクト</param>
	public void RemoveReflectedObject(GameObject _reflectedObject)
	{
		reflectedObjectList.Remove(_reflectedObject);
	}
}
