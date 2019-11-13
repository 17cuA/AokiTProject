using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
	Item_Normal,
	Item_KillAllEnemy,
}
/// <summary>
/// アイテムクラス
/// 必ずこのスクリプトをアイテムオブジェクトにアタッチする
/// </summary>
public class Item : MonoBehaviour
{
	[Header("パワーアップタイプ")]
	public ItemType itemType;

	[SerializeField, Header("移動速度")]
	private float move_speed;

	private void Update()
	{
		Vector3 pos = transform.position;
		pos.x -= move_speed;
		transform.position = pos;
	}
}

