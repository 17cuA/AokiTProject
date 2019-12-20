using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MonoBehaviourがアタッチされていないオブジェクトのモデル等の読み込み不具合時の処理をするためのやつ
/// </summary>
public class ModelErrerProcessInterface : MonoBehaviour, IModelChangeErrerHandler
{
	[SerializeField] Vector3 defaultEulerAngles = Vector3.zero;
	[SerializeField] Vector3 defaultScale = Vector3.one;
	public void ModelErrerModification()
	{
		transform.eulerAngles = defaultEulerAngles;
		transform.localScale = defaultScale;
	}
}
